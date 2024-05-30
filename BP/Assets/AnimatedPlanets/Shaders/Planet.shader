Shader "Space/Planet"
{
    Properties 
	{
		_MainTex ("Texture", 2D) = "white" {}
		_RimColor ("Rim Color", Color) = (0.5,0.5,0.5,0)
		_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
    }
    SubShader 
	{
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert
      struct Input 
	  {
          float2 uv_MainTex;
          float3 viewDir;
      };
      sampler2D _MainTex;
      float4 _RimColor;
      float _RimPower;
      void surf (Input IN, inout SurfaceOutput o) 
	  {
          half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb + _RimColor.rgb * pow (rim, _RimPower);
          o.Emission = 0.2 * _RimColor.rgb * pow (rim, _RimPower);
      }
      ENDCG
    } 
    Fallback "Diffuse"
}
