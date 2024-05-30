// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Space/PlanetUnlit" 
{
    Properties 
    {
		_MainTex("Texture (RGB)", 2D) = "white" {}
		_RimFactor("Rim Amount", Float) = 0.5
		_RimColor ("Rim Color", Color) = (0.5,0.5,0.5,0)
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
	
	        uniform sampler2D _MainTex;
	        uniform float4 _MainTex_ST;
	        float _RimFactor;
            uniform float4 _RimColor;
	
			struct v2f
	        {
	            float4 pos : SV_POSITION;
	            float2 uv : TEXCOORD0;
				float rim : TEXCOORD1;
	        };
	
	        v2f vert(appdata_base v)
	        {
	            v2f o;
	                   
	            o.pos = UnityObjectToClipPos (v.vertex);
	            o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
				o.rim = saturate(_RimFactor-dot(viewDir,v.normal));
	                   
	            return o;
	        }
	              
	        float4 frag(v2f i) : COLOR
	        {
                float4 color = tex2D(_MainTex, i.uv);
				return color + i.rim*_RimColor;
	        }
			ENDCG
		}
	}
	FallBack OFF
}
