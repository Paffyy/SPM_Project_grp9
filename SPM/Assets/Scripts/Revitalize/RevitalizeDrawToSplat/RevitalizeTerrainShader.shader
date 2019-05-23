Shader "Revitalize/RevitalizeTerrain"
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

		_Splat ("SplatMap", 2D) = "black" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows 
        #pragma target 3.0

        struct Input
        {
            float2 uv_ScorchedTexture;
            float2 uv_RevitalizeTexture;
			float2 uv_Splat;
        };

		sampler2D _ScorchedTexture;
		sampler2D _ScorchedNormal;
		sampler2D _ScorchedOcclusion;
		fixed4 _ScorchedColor;

		sampler2D _RevitalizeTexture;
		sampler2D _RevitalizeNormal;
		sampler2D _RevitalizeOcclusion;
		fixed4 _RevitalizeColor;

        sampler2D _Splat;
        half _Glossiness;
        half _Metallic;


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
			half revitalizeAmount = saturate(tex2D(_Splat, float4(IN.uv_Splat.xy,0,0)).r);
			fixed4 pixelColor = lerp(tex2D (_ScorchedTexture, IN.uv_ScorchedTexture) * _ScorchedColor, tex2D (_RevitalizeTexture, IN.uv_RevitalizeTexture) * _RevitalizeColor, revitalizeAmount);
          	fixed4 ambientOcclusion = lerp(tex2D(_ScorchedOcclusion, IN.uv_ScorchedTexture), tex2D(_RevitalizeOcclusion, IN.uv_ScorchedTexture), revitalizeAmount);
			o.Normal = lerp(UnpackNormal(tex2D(_ScorchedNormal,IN.uv_ScorchedTexture)), UnpackNormal(tex2D(_RevitalizeNormal,IN.uv_ScorchedTexture)), revitalizeAmount);
            o.Albedo = pixelColor.rgb * ambientOcclusion.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
			o.Alpha = pixelColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
