Shader "Space/Sun" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
	  _Emission ("Emission", 2D) = "black" {}
	  _EmissionColor ("Emission Color", Color) = (0.26,0.19,0.16,0.0)
      _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
      _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert
      
	  sampler2D _MainTex;
	  sampler2D _Emission;
	  float4 _EmissionColor;
      float4 _RimColor;
      float _RimPower;
	  
	  struct Input {
          float2 uv_MainTex;
		  float2 uv_Emission;
          float3 viewDir;
      };

      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
          half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          o.Emission = _EmissionColor.rgb * tex2D (_Emission, IN.uv_Emission) + _RimColor.rgb * pow (rim, _RimPower);
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }