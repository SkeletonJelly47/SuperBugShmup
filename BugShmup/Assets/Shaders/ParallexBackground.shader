Shader "Custom/Background Parallex" {

	Properties {
		[NoScaleOffset] _TopTex ("Top Texture", 2D) = "white" {}
		_TopTexSpeed("Top Texture Speed", Range(0, 10)) = 1
		[NoScaleOffset] _MiddleTex ("Middel Texture", 2D) = "gray" {}
		_MiddleTexSpeed("Middle Texture Speed", Range(0, 10)) = 1
		[NoScaleOffset] _BottomTex ("Bottom Texture", 2D) = "white" {}
		_BottomTexSpeed("Bottom Texture Speed", Range(0, 10)) = 1
	}

	SubShader{

			Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
			LOD 100

			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM

			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram

			#include "UnityCG.cginc"

			sampler2D _TopTex, _MiddleTex, _BottomTex;
			float4 _TopTex_ST, _MiddleTex_ST, _BottomTex_ST;
			float _TopTexSpeed, _MiddleTexSpeed, _BottomTexSpeed;

			struct VertexData 
			{
				// vertex positions
				float4 position : POSITION;
				// Mesh UV cordinate
				float2 uv : TEXCOORD0;
			};

			struct Interpolators 
			{
				float4 position : SV_POSITION;
				// Texture UV cordinate
				float2 uv0 : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
				float2 uv2 : TEXCOORD2;
			};

			Interpolators MyVertexProgram (VertexData v) 
			{
				Interpolators i;
				// Calculate position
				i.position = UnityObjectToClipPos(v.position);
				
				// Top texture UV offset
				_TopTex_ST.w += _Time * -_TopTexSpeed;
				i.uv0 = TRANSFORM_TEX(v.uv, _TopTex);
				
				// Middle texture UV offset
				_MiddleTex_ST.w += _Time * -_MiddleTexSpeed;
				i.uv1 = TRANSFORM_TEX(v.uv, _MiddleTex);

				// Bottom texture UV offset
				_BottomTex_ST.w += _Time * -_BottomTexSpeed;
				i.uv2 = TRANSFORM_TEX(v.uv, _BottomTex);
				return i;
			}

			float4 MyFragmentProgram (Interpolators i) : SV_TARGET 
			{	
				// Draw top texture
				float4 top = tex2D(_TopTex, i.uv0);
				float4 middle = tex2D(_MiddleTex, i.uv1);
				float4 bottom = tex2D(_BottomTex, i.uv2);

				float4 color = top;

				if (top.a == 1 && middle.a <= 1 && bottom.a <= 1)
				{
					color.rgba = top.rgba;
				}
				else if (top.a <= 1 && middle.a == 1 && bottom.a <= 1)
				{
					color.rgba = middle.rgba;
				}
				else if (top.a <= 1 && middle.a <= 1 && bottom.a == 1)
				{
					color.rgba = bottom.rgba;
				}

				return color;
			}

			ENDCG
		}
	}
}