Shader "Space/PlanetDayNight"
{
    Properties 
	{
		_MainTex ("Main Texture", 2D) = "white" {}
		_NightTex ("Night Lights Texture", 2D) = "black" {}
		_RimColor ("Rim Color", Color) = (0.5,0.5,0.5,0)
		_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
        _SunDir ("Sun Direction", Vector) = (1,0,0)
		_MinAngle ("Min Angle", Range(0,180)) = 80
		_MaxAngle ("Max Angle", Range(0,180)) = 90
    }
    SubShader 
	{
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert

      struct Input 
	  {
          float2 uv_MainTex;
          float2 uv_NightTex;
          float3 viewDir;
      };

      sampler2D _MainTex;
      sampler2D _NightTex;
      float4 _RimColor;
      float _RimPower;
	  float3 _SunDir;
	  float _MinAngle;
	  float _MaxAngle;

      void surf (Input IN, inout SurfaceOutput o) 
	  {
          half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb + _RimColor.rgb * pow (rim, _RimPower);
		  half night = 0.5 * (dot (normalize(-_SunDir), o.Normal) + 1);
		  night = saturate((night*180 - _MinAngle) / (_MaxAngle-_MinAngle));
          o.Emission = 0.2 * _RimColor.rgb * pow (rim, _RimPower) + tex2D (_NightTex, IN.uv_NightTex).rgb * night;
      }

      ENDCG
    } 
    Fallback "Diffuse"
}
