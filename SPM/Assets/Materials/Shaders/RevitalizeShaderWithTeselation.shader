﻿Shader "Revitalize/RevitalizeShaderTess"
{
    Properties
    {
        _RevitalizeColor ("RevColor", Color) = (1,1,1,1)
        _RevitalizeTexture ("RevAlbedo (RGB)", 2D) = "white" {}
	    //RevitalizeNormal("RevNormal", 2D) = "bump" {}
		//_RevitalizeOcclusion ("RevOcclusion", 2D) = "white" {}

		_ScorchedColor ("ScorchedColor", Color) = (1,1,1,1)
        _ScorchedTexture ("ScorchedAlbedo (RGB)", 2D) = "white" {}
		//_ScorchedNormal("ScorchedNormal", 2D) = "bump" {}
		//_ScorchedOcclusion ("ScorchedOcclusion", 2D) = "white" {}

		_RevitalizeFactor("RevitalizeFactor", Range(0,1)) = 0

		_Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
		//_OcclusionScale("OcclusionFactor", Range(0,1)) = 1
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5

		_Tess ("Tessellation", Range(1,32)) = 4
		_DispTex ("Disp Texture", 2D) = "gray" {}
		_Displacement ("Displacement", Range(0, 1.0)) = 0.3
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="TransparentCutout" }
        LOD 200
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:disp tessellate:tessDistance nolightmap
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

            sampler2D _DispTex;
            float _Displacement;

            void disp (inout appdata v)
            {
                float d = tex2Dlod(_DispTex, float4(v.texcoord.xy,0,0)).r * _Displacement;
                v.vertex.xyz += v.normal * d;
            }

        struct Input
        {
            float2 uv_ScorchedTexture : TEXCOORD1;
			//float2 uv_ScorchedNormal : TEXCOORD2;
			//float2 uv_ScorchedOcclusion : TEXCOORD3;

            float2 uv_RevitalizeTexture : TEXCOORD2;
			//float2 uv_RevitalizeNormal : TEXCOORD5;
			//float2 uv_RevitalizeOcclusion : TEXCOORD6;
        };

		sampler2D _ScorchedTexture;
		//sampler2D _ScorchedNormal;
		// sampler2D _ScorchedOcclusion;
		fixed4 _ScorchedColor;

		sampler2D _RevitalizeTexture;
		//sampler2D _RevitalizeNormal;
		//sampler2D _RevitalizeOcclusion;
		fixed4 _RevitalizeColor;

		half _RevitalizeFactor;
		//half _OcclusionScale;
        half _Glossiness;
        half _Metallic;
		fixed _Cutoff;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
			fixed4 c = lerp(tex2D (_ScorchedTexture, IN.uv_ScorchedTexture) * _ScorchedColor, tex2D (_RevitalizeTexture, IN.uv_RevitalizeTexture) * _RevitalizeColor, _RevitalizeFactor);
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//fixed4 occ = lerp(tex2D(_ScorchedOcclusion, IN.uv_ScorchedOcclusion), tex2D(_RevitalizeOcclusion, IN.uv_RevitalizeOcclusion), _RevitalizeFactor);
			//o.Normal = lerp(UnpackNormal(tex2D(_ScorchedNormal,IN.uv_ScorchedNormal)), UnpackNormal(tex2D(_RevitalizeNormal,IN.uv_RevitalizeNormal)), _RevitalizeFactor);
            o.Albedo = c.rgb; /* * occ.rgb; */
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
			o.Alpha = c.a;
			clip(c.a - _Cutoff);
		
        }
        ENDCG
    }
    FallBack "Diffuse"
}
