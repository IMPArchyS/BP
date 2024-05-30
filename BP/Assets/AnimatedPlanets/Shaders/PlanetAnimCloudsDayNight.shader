Shader "Space/PlanetAnimCloudsDayNight" 
{
    Properties 
    {
		_MainTex ("Main Texture", 2D) = "white" {}
		_NightTex ("Night Lights Texture", 2D) = "black" {}
        _DispTex ("Mask Texture", 2D) = "gray" {}
        _RampTex ("Colors (RGB)", 2D) = "white" {}
		_RimColor ("Rim Color", Color) = (0.5,0.5,0.5,0)
		_RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
		_MinAngle ("Min Night Lights Angle", Range(0,180)) = 75
		_MaxAngle ("Max Night Lights Angle", Range(0,180)) = 90
        _ChannelFactor ("Mask ChannelFactor (r,g,b)", Vector) = (1,0,0)
        _SunDir ("Sun Direction", Vector) = (1,0,0)
    }
    SubShader 
    {
		Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert nolightmap
        #pragma target 3.0
		
        sampler2D _MainTex;
        sampler2D _NightTex;
        sampler2D _DispTex;
        float3 _ChannelFactor;
        float2 _Range;
        float _ClipRange;
        sampler2D _RampTex;
		float4 _RimColor;
		float _RimPower;
		float3 _SunDir;
		float _MinAngle;
		float _MaxAngle;

        struct Input 
        {
            float2 uv_MainTex;
			float2 uv_NightTex;
            float2 uv_DispTex;
			float3 viewDir;
        };

        void surf (Input IN, inout SurfaceOutput o) 
		{
			half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
			float4 dcolor = tex2D (_DispTex, IN.uv_DispTex);
			float d = (dcolor.r*_ChannelFactor.r + dcolor.g*_ChannelFactor.g + dcolor.b*_ChannelFactor.b);
			half4 clouds = tex2D (_RampTex, float2(d,0.5));
			o.Albedo = lerp( tex2D (_MainTex, IN.uv_MainTex), clouds.rgb, clouds.a) + _RimColor.rgb * pow (rim, _RimPower);
			half night = 0.5 * (dot (normalize(-_SunDir), o.Normal) + 1);
			night = saturate((night*180 - _MinAngle) / (_MaxAngle-_MinAngle));
			o.Emission = tex2D (_NightTex, IN.uv_NightTex).rgb * night * (1-clouds.a);
		}
		ENDCG
	}
    Fallback "Diffuse"
}
