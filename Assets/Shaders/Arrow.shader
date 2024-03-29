Shader "ArcheryClash/Arrow" {
	Properties {
		[NoScaleOffset] _MainTex("Main Texture", 2D) = "black" {}
		[NoScaleOffset] _HighTex("Highlighted Texture", 2D) = "black" {}
		_Threshold("Effect threshold", Range(0,1)) = 0.0
		_Cutoff("Shadow alpha cutoff", Range(0,1)) = 0.1
	}

		SubShader {
			Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" }

			Fog { Mode Off }
			Cull Off
			ZWrite Off
			Blend One OneMinusSrcAlpha
			Lighting Off

			Pass {
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				sampler2D _MainTex;
				sampler2D _HighTex;
				float _Threshold;

				struct VertexInput {
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct VertexOutput {
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				VertexOutput vert(VertexInput v) {
					VertexOutput o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				float4 frag(VertexOutput i) : COLOR {
					if (i.uv.x < (1-_Threshold))
						return tex2D(_MainTex, i.uv);
					else
						return tex2D(_HighTex, i.uv);
				}
				ENDCG
			}

			Pass {
				Name "Caster"
				Tags { "LightMode" = "ShadowCaster" }
				Offset 1, 1
				ZWrite On
				ZTest LEqual

				Fog { Mode Off }
				Cull Off
				Lighting Off

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_shadowcaster
				#pragma fragmentoption ARB_precision_hint_fastest
				#include "UnityCG.cginc"
				sampler2D _MainTex;
				fixed _Cutoff;

				struct VertexOutput {
					V2F_SHADOW_CASTER;
					float2 uv : TEXCOORD1;
				};

				VertexOutput vert(appdata_base v) {
					VertexOutput o;
					o.uv = v.texcoord;
					TRANSFER_SHADOW_CASTER(o)
					return o;
				}

				float4 frag(VertexOutput i) : COLOR {
					fixed4 texcol = tex2D(_MainTex, i.uv);
					clip(texcol.a - _Cutoff);
					SHADOW_CASTER_FRAGMENT(i)
				}
				ENDCG
			}
		}
}
