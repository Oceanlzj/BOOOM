Shader "Custom/TVSnowShader"
 {
    Properties{
     _Color("Main Color", Color) = (1.000000,1.000000,1.000000,1.000000)
     _NoiseStrength("Noise Strength", Float) = 1.0
     
    }
        SubShader{
         LOD 100
         Tags { "RenderType" = "Opaque" }

         Pass {
          Tags { "RenderType" = "Opaque" }

          CGPROGRAM
          #pragma vertex vert
          #pragma fragment frag

          #include "UnityCG.cginc"

          struct appdata {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
          };

          struct v2f {
            float4 vertex : SV_POSITION;
            float2 uv : TEXCOORD0;
          };

          fixed4 _Color;
          float _NoiseStrength;

          v2f vert(appdata v) {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = v.uv;
            return o;
          }

          float PerlinNoise(float2 uv) {
            return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
          }

          fixed4 frag(v2f i) : SV_Target {
            float2 uv = i.uv;
            float noise = PerlinNoise(uv * _NoiseStrength);
            fixed4 color = _Color;
            color.rgb += noise;
            return color;
          }
          ENDCG

        }
    }
}
//Shader "Custom/TVSnowShader"
//{
//    Properties
//    {
//        _MainTex("Texture", 2D) = "white" {}
//        _NoiseStrength("Noise Strength", Float) = 1.0
//        _Offset("Offset", Float) = 0.0
//    }
//
//        SubShader
//        {
//            Tags { "RenderType" = "Opaque" }
//
//            CGPROGRAM
//            #pragma surface surf Lambert
//
//            sampler2D _MainTex;
//            float _NoiseStrength;
//            float _Offset;
//
//            struct Input
//            {
//                float2 uv_MainTex;
//            };
//
//            // Perlin noise function
//            float PerlinNoise(float2 uv)
//            {
//                float2 p = floor(uv);
//                float2 f = frac(uv);
//
//                f = f * f * (3.0 - 2.0 * f);
//
//                float n = p.x + p.y * 57.0;
//                return lerp(lerp(f.x, f.y, n), lerp(f.y, f.x, n + 1.0), n);
//            }
//
//            void surf(Input IN, inout SurfaceOutput o)
//            {
//                float noise = PerlinNoise(IN.uv_MainTex * 10.0); // 使用Perlin噪声函数生成噪波效果
//                float offset = _Offset + noise * _NoiseStrength;
//                o.Albedo = offset;
//            }
//            ENDCG
//        }
//
//            FallBack "Diffuse"
//}
