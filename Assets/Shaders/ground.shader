Shader "Custom/ground" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
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

			sampler2D _MainTex;
			
			struct a_base {
				float4 vertex : SV_POSITION;
				float2 texcoord: TEXCOORD0;
			};
			

			a_base vert (float4 v : SV_POSITION)
			{
				a_base o;
				o.vertex = mul(UNITY_MATRIX_MVP, v);
				o.texcoord = v * 0.25f;
				return o;
			}
			

			fixed4 frag(a_base i) : COLOR
			{
				return tex2D(_MainTex, i.texcoord);
			}
			ENDCG
		} 
	} 
	FallBack Off
}