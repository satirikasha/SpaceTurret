Shader "Effects/Transparent" {
	Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB) Gloss (A)", 2D) = "black" {}		
}

SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }	
		ZWrite On
		Cull Off

CGPROGRAM

#pragma surface surf Lambert alpha

sampler2D _MainTex;
float4 _Color;

struct Input {
		float2 uv_MainTex;
};
 
void surf (Input IN, inout SurfaceOutput o) {
		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex) * _Color;
		o.Emission = _Color.xyz * tex * _Color.a;
}
ENDCG
}

FallBack "Diffuse"
}