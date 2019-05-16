Shader "Custom/RevitalizeColorTrack"
{
    Properties
    {
        _RevitalizeColor ("RevColor", Color) = (1,1,1,1)
        _RevitalizeTexture ("RevAlbedo (RGB)", 2D) = "white" {}
		_ScorchedColor ("ScorchedColor", Color) = (1,1,1,1)
        _ScorchedTexture ("ScorchedAlbedo (RGB)", 2D) = "white" {}
		_Splat ("SplatMap", 2D) = "black" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows 
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        struct Input
        {
            float2 uv_ScorchedTexture;
            float2 uv_RevitalizeTexture;
			float2 uv_Splat;

        };
            sampler2D _Splat;
		sampler2D _ScorchedTexture;
		fixed4 _ScorchedColor;
		sampler2D _RevitalizeTexture;
		fixed4 _RevitalizeColor;
        half _Glossiness;
        half _Metallic;


        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
			half amount = saturate(tex2D(_Splat, float4(IN.uv_Splat.xy,0,0)).r);
			fixed4 c = lerp(tex2D (_ScorchedTexture, IN.uv_ScorchedTexture) * _ScorchedColor, tex2D (_RevitalizeTexture, IN.uv_RevitalizeTexture) * _RevitalizeColor, amount);
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
