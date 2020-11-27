Shader "Refsa/ColorPicker/ColorPickerShader"
{
    Properties
    {
        _MainTex ("MainTex", 2D) = "white" {}
        _Hue ("Hue", Range(0.0, 1.0)) = 0.0
        _Saturation ("Saturation", Range(0.0, 1.0)) = 0.0
        _Value ("Value", Range(0.0, 1.0)) = 0.0
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
            // make fog work
            #pragma multi_compile_fog

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float _Hue;
            float _Saturation;
            float _Value;

            float3 hsv2rgb(float3 c) {
              c = float3(c.x, clamp(c.yz, 0.0, 1.0));
              float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
              float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
              return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = float4(hsv2rgb(float3(_Hue, 1, 1)), 1.0);

                col.rgb = lerp(1, col.rgb, i.uv.x);
                col.rgb = lerp(0, col.rgb, i.uv.y);

                return col;
            }
            ENDCG
        }
    }
}
