Shader "Space/Atmosphere" 
{
	Properties 
	{
		_Color ("Color", Color) = (0,0.5,1,0)
		_RimPower ("Rim Smoothness", Range(0.1,2)) = 1
		_Width ("Width", Float) = 0.1
		_Intensity ("Intensity", Float) = 1
	}
	SubShader 
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		Cull Front
		Blend One One

		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		struct Input 
		{
			float3 viewDir;
		};
		
		float4 _Color;
		float _RimPower;
		float _Width;
		float _Intensity;

		void vert (inout appdata_full v) 
		{
			v.vertex.xyz += v.normal * _Width;
		}		

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half rim = saturate(dot (normalize(IN.viewDir), -o.Normal)-0.3)/0.7;
			//half rim = dot (normalize(IN.viewDir), -o.Normal);
			//rim = saturate((rim*90 - 50) / 40);
			float3 color = _Color.rgb * saturate(_Intensity * pow (rim, _RimPower));
			o.Albedo = color;
			o.Emission = 0.0001*color; //otherwise it doesn't compile for some reason
		}
		ENDCG
	} 
	//FallBack "Transparent/Diffuse"
}
