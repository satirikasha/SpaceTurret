  Shader "Effects/Rim" {
	Properties {
	_Color ("Rim Color", Color) = (0.5,0.5,0.5,0.5)
	_FPOW("FPOW Fresnel", Float) = 5.0
	_R0("R0 Fresnel", Float) = 0.05
	_Cutoff("Cutoff", Range(0, 1)) = 0.25
	_MixingCoeff("MixingCoeff", Range(0.001, 1)) = 0.2
	_Transparency ("Transparency", Range(0, 1)) = 1
	}
    SubShader {
      Tags { "RenderType" = "Transparent" "IgnoreProjector"="True" "Queue" = "Transparent"}
	  Cull Off
      CGPROGRAM
      #pragma surface surf Lambert alpha vertex:vert

	  uniform fixed4 _Color;
	  uniform float _FPOW;
	  uniform float _R0;
	  uniform float _Transparency;
	  uniform float _MixingCoeff;
	  uniform float _Cutoff;

      struct Input {
	    float3 viewDir;
		float3 worldPos;
		float3 objectPos;
	  };

	  void vert (inout appdata_full v, out Input o) {
        UNITY_INITIALIZE_OUTPUT(Input,o);
        o.objectPos = v.vertex;
      }

      void surf (Input IN, inout SurfaceOutput o) {
        half fresnel = saturate(1.0 - dot(o.Normal, IN.viewDir));
		fresnel = pow(fresnel, _FPOW);
		fresnel = _R0 + (1.0 - _R0) * fresnel;
		float4 cutAway = IN.objectPos.y + IN.objectPos.z * 0.25;
		clip (-cutAway + (0.52 - _Cutoff * 0.52 * 2));
        o.Albedo = _MixingCoeff * _Color.rgb * _Color.a * _Transparency;
        o.Emission = ((1 - _MixingCoeff) / _MixingCoeff) * _Color.rgb * _Color.a * fresnel * o.Albedo * _Transparency;
      }
      ENDCG
    }
    Fallback "Diffuse"
  }