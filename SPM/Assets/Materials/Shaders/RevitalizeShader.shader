Shader "Revitalize/RevitalizeShader"
{
    Properties
    {
        _RevitalizeColor ("RevColor", Color) = (1,1,1,1)
        _RevitalizeTexture ("RevAlbedo (RGB)", 2D) = "white" {}
		_ScorchedColor ("ScorchedColor", Color) = (1,1,1,1)
        _ScorchedTexture ("ScorchedAlbedo (RGB)", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_Occlusion ("Occlusion", 2D) = "white" {}
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
            float2 uv_ScorchedTexture : TEXCOORD0;
            float2 uv_RevitalizeTexture : TEXCOORD1;
			float2 uv_Normal : TEXCOORD2;
			float2 uv_Occlusion : TEXCOORD3;
        };

		sampler2D _ScorchedTexture;
		fixed4 _ScorchedColor;
		sampler2D _RevitalizeTexture;
		fixed4 _RevitalizeColor;

		sampler2D _Normal;
		sampler2D _Occlusion;

		half _RevitalizeFactor;
		half _OcclusionScale;
        half _Glossiness;
        half _Metallic;
		fixed _Cutoff;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			fixed4 pixelColor = lerp(tex2D (_ScorchedTexture, IN.uv_ScorchedTexture) * _ScorchedColor,
				tex2D (_RevitalizeTexture, IN.uv_RevitalizeTexture) * _RevitalizeColor, _RevitalizeFactor); 
			fixed4 ambientOcclusion = tex2D(_Occlusion, IN.uv_Occlusion); 
			o.Normal = UnpackNormal(tex2D(_Normal,IN.uv_Normal));
            o.Albedo = pixelColor.rgb;
            o.Metallic = _Metallic;
			o.Occlusion = ambientOcclusion.rbg;
            o.Smoothness = _Glossiness;
			o.Alpha = pixelColor.a;
			clip(pixelColor.a - _Cutoff);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
