﻿
Shader "Common/Transparent Cutout 2Side"
{
	Properties 
	{
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	}
	SubShader 
	{
		Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
		Cull Off
		Blend Off
		Fog { Mode Off }
		Alphatest Greater [_Cutoff]
		LOD 100
	
		Pass 
		{
			Lighting Off
			SetTexture [_MainTex] { combine texture } 
		}
	}
}