Shader "Space/PlanetSpecular"
{
    Properties 
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
		_Shininess ("Shininess", Range (0.01, 1)) = 0.078125
		_RimColor ("Rim Color", Color) = (0.5,0.5,0.5,0)
		_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
    }
    SubShader 
	{
		Tags { "RenderType" = "Opaque" }

		CGPROGRAM
		#pragma surface surf BlinnPhong

		struct Input 
		{
			float2 uv_MainTex;
			float3 viewDir;
		};

		sampler2D _MainTex;
		float4 _RimColor;
		float _RimPower;
		half _Shininess;

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
			fixed4 tex = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = tex.rgb + _RimColor.rgb * pow (rim, _RimPower);
			o.Emission = 0.2 * _RimColor.rgb * pow (rim, _RimPower);
			o.Gloss = tex.a;
			o.Specular = _Shininess;
		}

      ENDCG
    } 
    Fallback "Diffuse"
}
