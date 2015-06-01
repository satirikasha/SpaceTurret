Shader "Space/Planet" {
    Properties {
      _MainTex ("Texture", 2D) = "white" {}
      _BumpMap ("Bumpmap", 2D) = "bump" {}
	  [KeywordEnum(OFF, ON)] _UseLightMap ("Use LightMap", float) = 0
	  _LightMap ("LightMap", 2D) = "black" {}
	  [Space]
      _RimColor ("Rim Color", Color) = (0.26,0.19,0.16,0.0)
      _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
    }
    SubShader {
      Tags { "RenderType" = "Opaque" }
      CGPROGRAM
      #pragma surface surf Lambert
	  #pragma multi_compile _USELIGHTMAP_ON _USELIGHTMAP_OFF
      
	  sampler2D _MainTex;
      sampler2D _BumpMap;
	  #ifdef _USELIGHTMAP_ON
	  sampler2D _LightMap;
	  #endif
      float4 _RimColor;
      float _RimPower;
	  
	  struct Input {
          half2 uv_MainTex;
          half2 uv_BumpMap;
		  #ifdef _USELIGHTMAP_ON
		  half2 uv_LightMap;
		  #endif
          float3 viewDir;
      };

      void surf (Input IN, inout SurfaceOutput o) {
          o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
          o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
          half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));
          o.Emission = _RimColor.rgb * pow (rim, _RimPower);
		  #ifdef _USELIGHTMAP_ON
		  o.Emission += tex2D(_LightMap, IN.uv_LightMap);
		  #endif
      }
      ENDCG
    } 
    Fallback "Diffuse"
  }