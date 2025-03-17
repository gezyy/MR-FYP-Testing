Shader "Custom/UI/NoiseShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _NoiseIntensity ("Noise Intensity", Range(0, 1)) = 0.1
        _NoiseScale ("Noise Scale", Range(0.01, 1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
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
                float4 vertex : POSITION;
            };

            sampler2D _MainTex;
            float _NoiseIntensity;
            float _NoiseScale;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float rand(float2 co)
            {
                return frac(sin(dot(co ,float2(12.9898,78.233))) * 43758.5453);
            }

            half4 frag(v2f i) : SV_Target
            {
                float2 noiseCoord = i.uv * _NoiseScale;
                float noise = rand(noiseCoord);
                half4 col = tex2D(_MainTex, i.uv);
                col.rgb += (noise - 0.5) * _NoiseIntensity;
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
