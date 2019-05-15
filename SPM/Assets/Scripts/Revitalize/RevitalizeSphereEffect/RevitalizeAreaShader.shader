Shader "Revitalize/RevitalizeShader"
{
    Properties
    {
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

		_RevitalizeColor ("RevColor", Color) = (1,1,1,1)
        _RevitalizeTexture ("RevAlbedo (RGB)", 2D) = "white" {}
		_ScorchedColor ("ScorchedColor", Color) = (1,1,1,1)
        _ScorchedTexture ("ScorchedAlbedo (RGB)", 2D) = "white" {}
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
			float3 worldPos;
        };
		sampler2D _ScorchedTexture;
		fixed4 _ScorchedColor;
		sampler2D _RevitalizeTexture;
		fixed4 _RevitalizeColor;
        half _Glossiness;
        half _Metallic;
		
		//custom
		uniform float4 _Position;
		uniform half _Radius;
		uniform half _Falloff;


        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 revColor = tex2D (_RevitalizeTexture, IN.uv_RevitalizeTexture) * _RevitalizeColor;
            fixed4 scorchedColor = tex2D (_ScorchedTexture, IN.uv_ScorchedTexture) * _ScorchedColor;

			half d = distance(_Position, IN.worldPos);
			half sum = saturate((d - _Radius) / - _Falloff);
			fixed4 lerpColor = lerp(fixed4(scorchedColor),revColor,sum);

            o.Albedo = lerpColor.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            //o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
