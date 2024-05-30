// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Space/Sun" 
{
    Properties 
    {
		_DispTex ("Mask Texture", 2D) = "gray" {}
		_RampTex ("Colors (RGB)", 2D) = "white" {}
        _ChannelFactor ("ChannelFactor (r,g,b)", Vector) = (1,0,0)
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
	
	        uniform sampler2D _DispTex;
	        uniform float4 _DispTex_ST;
	        sampler2D _RampTex;
	        float3 _ChannelFactor;
	
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
	            o.uv = TRANSFORM_TEX(v.texcoord, _DispTex);
				float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
				o.rim = saturate(0.5-dot(viewDir,v.normal));
	                   
	            return o;
	        }
	              
	        float4 frag(v2f i) : COLOR
	        {
				float3 dcolor = tex2D (_DispTex, i.uv);
				float d = dcolor.r*_ChannelFactor.r + dcolor.g*_ChannelFactor.g + dcolor.b*_ChannelFactor.b;
				float4 c = tex2D (_RampTex, float2(d,0.5));
				return c + i.rim;
	        }
			ENDCG
		}
	}
	FallBack OFF
}
