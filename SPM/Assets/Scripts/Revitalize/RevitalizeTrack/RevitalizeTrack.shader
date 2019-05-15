Shader "Custom/RevitalizeTrack"
{
    Properties
    {
        _RevitalizeColor ("RevColor", Color) = (1,1,1,1)
        _RevitalizeTexture ("RevAlbedo (RGB)", 2D) = "white" {}
		_ScorchedColor ("ScorchedColor", Color) = (1,1,1,1)
        _ScorchedTexture ("ScorchedAlbedo (RGB)", 2D) = "white" {}
		_Splat ("SplatMap", 2D) = "black" {}
      
		// teselation
		_Tess ("Tessellation", Range(1,32)) = 4
		_Displacement ("Displacement", Range(0, 1.0)) = 0.3
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:disp tessellate:tessDistance
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 4.6

		#include "Tessellation.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            float _Tess;

            float4 tessDistance (appdata v0, appdata v1, appdata v2) {
                float minDist = 10.0;
                float maxDist = 25.0;
                return UnityDistanceBasedTess(v0.vertex, v1.vertex, v2.vertex, minDist, maxDist, _Tess);
            }

            sampler2D _Splat;
            float _Displacement;

            void disp (inout appdata v)
            {
                float d = tex2Dlod(_Splat, float4(v.texcoord.xy,0,0)).r * _Displacement;
                v.vertex.xyz -= v.normal * d;
				v.vertex.xyz += v.normal * _Displacement;
            }

      


        struct Input
        {
            float2 uv_ScorchedTexture;
            float2 uv_RevitalizeTexture;
			float2 uv_Splat;

        };
		sampler2D _ScorchedTexture;
		fixed4 _ScorchedColor;
		sampler2D _RevitalizeTexture;
		fixed4 _RevitalizeColor;
        half _Glossiness;
        half _Metallic;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
			half amount = tex2Dlod(_Splat, float4(IN.uv_Splat.xy,0,0)).r;
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
