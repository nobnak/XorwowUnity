// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Second" {
	Properties {
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200 ZTest Always ZWrite Off Cull Off Fog { Mode Off }
		
		Pass {
			CGPROGRAM
			#pragma target 5.0
			#pragma vertex vert
			#pragma fragment frag
			#include "Packages/jp.nobnak.random-xorwow/ShaderLibrary/Xorwow.cginc"
			
			struct vsin {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			struct vs2ps {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			int _Width, _Height;
			
			vs2ps vert(vsin IN) {
				vs2ps OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv * int2(_Width, _Height);
				return OUT;
			}
			float4 frag(vs2ps IN) : COLOR {
				uint i = dot(uint2(IN.uv), int2(1, _Width));
				float r = Xorwow_NextFloat(i);
				return float4(r, r, r, 1);
			}
			ENDCG
		}
	} 
	FallBack Off
}
