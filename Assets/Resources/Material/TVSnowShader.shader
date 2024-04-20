Shader "Custom/TVSnowShader"
{
    Properties
    {
        _Color("Main Color", Color) = (1.000000, 1.000000, 1.000000, 1.000000)
        _NoiseStrength("Noise Strength", Float) = 1.0
        _Offset("Offset", Float) = 0.0
    }

        SubShader
    {
        LOD 100
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            Tags { "RenderType" = "Opaque" }

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
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            fixed4 _Color;
            float _NoiseStrength;
            float _Offset;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float PerlinNoise(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float noise = PerlinNoise(uv + _Offset) * _NoiseStrength;
                fixed4 color = _Color;
                color.rgb += noise;
                return color;
            }
            ENDCG
        }
    }
}
