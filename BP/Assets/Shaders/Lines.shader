Shader "Unlit/Lines"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
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
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                // Calculate distance from camera
                float distance = length(_WorldSpaceCameraPos - i.worldPos);
                float blurAmount = clamp(distance / 250.0, 0.0, 1.0); // Adjust the divisor for your scene scale

                // Sample the texture and apply transparency based on distance
                half4 color = tex2D(_MainTex, i.uv) * _Color;
                color.a = lerp(_Color.a, 0.0, blurAmount); // Adjust alpha to become more transparent with distance
                return color;
            }
            ENDCG
        }
    }
}
