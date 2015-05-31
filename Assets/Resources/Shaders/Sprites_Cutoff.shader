Shader "Sprites/Cutoff" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Main Texture", 2D) = "white" {}
		_AlphaTex ("Alpha Texture", 2D) = "white" {}
		_Cutoff ("Cutoff", Range(-0, 1.001)) = 0.2
	}
	SubShader {
		Pass{
			Tags {"Queue" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			uniform float4 _Color;
			uniform float _Cutoff;
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			uniform float4 _MainTex_ST;
			uniform float4 _AlphaTex_ST;

			struct vertexInput {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};

			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;
			}; 

			vertexOutput vert(vertexInput v) {
				vertexOutput o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.tex = v.texcoord;
				return o;
			}

			float4 frag(vertexOutput i) : COLOR {

				float4 tex = tex2D(_MainTex, i.tex.xy * _MainTex_ST.xy + _MainTex_ST.zw); 
				float alphaTex = tex2D(_AlphaTex, i.tex.xy * _AlphaTex_ST.xy + _AlphaTex_ST.zw).a;

				float cutOff;
				if (alphaTex < _Cutoff)
				   cutOff = 0;
				else
				   cutOff = 1;

				return float4(float4(0.4, 0.4, 0.4, 0.2) + tex * _Color * cutOff);
			}

			ENDCG
		}
	} 
	FallBack "Diffuse"
}
