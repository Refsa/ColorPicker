Shader "Refsa/ColorPicker/CircleHuePickerShader"
{
    Properties
    {
        _CenterRadius ("Center Radius", Range(0.0, 0.49)) = 0.25
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float _CenterRadius;

            float3 hsv2rgb(float3 c) {
              c = float3(c.x, clamp(c.yz, 0.0, 1.0));
              float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
              float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
              return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = 1;

                float centerDist = distance(i.uv, float2(0.5, 0.5));

                float2 dir = normalize(i.uv - float2(0.5, 0.5));
                float angle = atan2(dir.y, dir.x) / UNITY_PI * 0.5;

                col.rgb = hsv2rgb(float3(angle, 1, 1));

                col.a = (centerDist > _CenterRadius) && (centerDist < 0.5);

                return col;
            }
            ENDCG
        }
    }
}
