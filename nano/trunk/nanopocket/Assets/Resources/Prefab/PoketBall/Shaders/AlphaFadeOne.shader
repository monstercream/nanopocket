Shader "AFootball/AlphaFadeOne"
{
	Properties
	{
		_Color ("Color", Color) = (0.5,0.5,0.5,1)
		_MainTex ("Texture (RGB) Trans (A)", 2D) = "gray" {}
		_SliceGuide0 ("Slice Guide0 (RGB)", 2D) = "white" {}
		_SliceGuide1 ("Slice Guide1 (RGB)", 2D) = "white" {}
		_SliceAmount ("Slice Amount", Range(-1.0, 1.0)) = -1.0
		_SliceType ("Slice Type", Float) = 0.0
	}

	SubShader
	{
		Tags
		{
		 "Queue"="Transparent+1"
		 "Render Type" = "Transparent"
		}

		LOD 200
		Blend SrcAlpha One
		Lighting Off
		ZWrite Off
		Fog { Mode Off }

		CGPROGRAM

		#pragma surface surf NoLighting
		#include "UnityCG.cginc"

		fixed4 LightingNoLighting(SurfaceOutput s, half3 lightDir, half atten)
		{
			fixed4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			return c;
		}

		struct Input 
		{
			float2 uv_MainTex;
		};

		sampler2D _MainTex;
		sampler2D _SliceGuide0;
		sampler2D _SliceGuide1;
		float4 _Color;
		float _SliceAmount;
		float _SliceType;

		void surf(Input IN, inout SurfaceOutput OUT) 
		{
			half4 BaseColor = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			OUT.Albedo = BaseColor.rgb;
			float alpha = 0.0;
			if (_SliceType==1.0)
			{
				alpha = tex2D(_SliceGuide1, IN.uv_MainTex).r + _SliceAmount;
			}
			else
			{
				alpha = tex2D(_SliceGuide0, IN.uv_MainTex).r + _SliceAmount;
			}
			if (alpha<0.0)
				alpha=0.0;
			OUT.Alpha = BaseColor.a - alpha;
		}

		ENDCG
	}
	Fallback "Diffuse"
}