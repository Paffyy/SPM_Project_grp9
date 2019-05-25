Shader "Revitalize/RevitalizeTerrain2"
{
    Properties
    {
		_Color ("Color", Color) = (1,1,1,1)

		_Control("ControlMap", 2D) = "red"{}
		_Splat0("SplatMap (R)", 2D) = "white" {}
		_Splat1("SplatMap (G)", 2D) = "white" {}
		_Splat2("SplatMap (B)", 2D) = "white" {}

		_Normal("Normal", 2D) = "black" {}

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows 
        #pragma target 4.0

        struct Input
        {
			float2 uv_Control : TEXCOORD0;
			float2 uv_Normal : TEXCOORD1;
			float2 uv_Splat0 : TEXCOORD2;
			float2 uv_Splat1 : TEXCOORD3;
			float2 uv_Splat2 : TEXCOORD4;
        };

		fixed4 _Color;

		sampler2D _Control;
        sampler2D _Splat0;
        sampler2D _Splat1;
        sampler2D _Splat2;
        sampler2D _Normal;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
			half red = tex2D(_Control, float4(IN.uv_Control.xy,0,0)).r;
			half green = tex2D(_Control, float4(IN.uv_Control.xy,0,0)).g;
			fixed3 col;
			col  = red * tex2D (_Splat0, IN.uv_Splat0);
			col += green * tex2D (_Splat1, IN.uv_Splat1);
		    fixed3 blueCol = tex2D (_Splat2, IN.uv_Splat2);
			o.Albedo = lerp(blueCol,col, green) + lerp(blueCol,col,red);
			o.Normal = UnpackNormal(tex2D(_Normal,IN.uv_Normal));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
