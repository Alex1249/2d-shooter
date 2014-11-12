Shader "Custom/coloredObject" {
	Properties {
		_Color ("Color (RGB)", Color) = (1, 1, 1, 1)
	}
	SubShader {
		Tags { 
				"Queue"="Transparent" 
				"IgnoreProjector"="True"
				"RenderType"="Transparent"
			}
			Cull Off
			Lighting Off
			ZWrite Off
        	Fog { Mode Off }
			Blend SrcAlpha OneMinusSrcAlpha
		
		Pass{

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"

			float4 _Color;
			
			struct a_base {
				float4 vertex : SV_POSITION;
			};
			

			float4 vert (float4 v : SV_POSITION) : SV_POSITION
			{
				return mul(UNITY_MATRIX_MVP, v);
			}
			

			fixed4 frag(float4 i : SV_POSITION) : COLOR
			{
				return _Color;
			}
			ENDCG
		} 
	} 
	FallBack Off
}