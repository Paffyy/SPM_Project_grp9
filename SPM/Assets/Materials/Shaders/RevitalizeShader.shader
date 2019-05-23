Shader "Revitalize/RevitalizeShader"
{
    Properties
    {
        _RevitalizeColor ("RevColor", Color) = (1,1,1,1)
        _RevitalizeTexture ("RevAlbedo (RGB)", 2D) = "white" {}
		_RevitalizeNormal("RevNormal", 2D) = "bump" {}
		_RevitalizeOcclusion ("RevOcclusion", 2D) = "white" {}

		_ScorchedColor ("ScorchedColor", Color) = (1,1,1,1)
        _ScorchedTexture ("ScorchedAlbedo (RGB)", 2D) = "white" {}
		_ScorchedNormal("ScorchedNormal", 2D) = "bump" {}
		_ScorchedOcclusion ("ScorchedOcclusion", 2D) = "white" {}

		_RevitalizeFactor("RevitalizeFactor", Range(0,1)) = 0

		_Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		_OcclusionScale("OcclusionFactor", Range(0,1)) = 1
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="TransparentCutout" }
        LOD 200
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        struct Input
        {
            float2 uv_ScorchedTexture;
			float2 uv_ScorchedNormal;
			float2 uv_ScorchedOcclusion;

            float2 uv_RevitalizeTexture;
			float2 uv_RevitalizeNormal;
			float2 uv_RevitalizeOcclusion;
        };

		sampler2D _ScorchedTexture;
		sampler2D _ScorchedNormal;
		sampler2D _ScorchedOcclusion;
		fixed4 _ScorchedColor;

		sampler2D _RevitalizeTexture;
		sampler2D _RevitalizeNormal;
		sampler2D _RevitalizeOcclusion;
		fixed4 _RevitalizeColor;

		half _RevitalizeFactor;
		half _OcclusionScale;
        half _Glossiness;
        half _Metallic;
		fixed _Cutoff;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed4 pixelColor = lerp(tex2D (_ScorchedTexture, IN.uv_ScorchedTexture) * _ScorchedColor, tex2D (_RevitalizeTexture, IN.uv_RevitalizeTexture) * _RevitalizeColor, _RevitalizeFactor);
			fixed4 ambientOcclusion = lerp(tex2D(_ScorchedOcclusion, IN.uv_ScorchedOcclusion), tex2D(_RevitalizeOcclusion, IN.uv_RevitalizeOcclusion), _RevitalizeFactor);
			o.Normal = lerp(UnpackNormal(tex2D(_ScorchedNormal,IN.uv_ScorchedNormal)), UnpackNormal(tex2D(_RevitalizeNormal,IN.uv_RevitalizeNormal)), _RevitalizeFactor);
            o.Albedo = pixelColor.rgb * ambientOcclusion.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
			o.Alpha = pixelColor.a;
			clip(pixelColor.a - _Cutoff);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
