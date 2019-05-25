Shader "Unlit/DrawScorched"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Coordinates("Coordinates", Vector) = (0,0,0,0)
		_Color ("DrawColor", Color) = (0,0,0,1) 
		_BrushSize ("BrushSize", Range(1,1000)) = 250
		_Strength ("Strength", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			half _BrushSize;
			half _Strength;
			fixed4 _Coordinates, _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 textureCoord = tex2D(_MainTex, i.uv);
				float brush = pow(saturate(1 - distance(i.uv, _Coordinates.xy)), _BrushSize);
				fixed4 drawColor = _Color * (brush * _Strength);
                return saturate(textureCoord - drawColor);
            }

            ENDCG
        }
    }
}
