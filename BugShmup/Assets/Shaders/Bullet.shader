// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Bullet" 
{
	Properties
	{
		_Color("Main Color", Color) = (1, 1, 1, 1)
		_OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
		_Threshold ("Threshold", Range(0.0, 1.0)) = 0.5
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 posWorld : TEXCOORD0;
				float3 normalDir : TEXCOORD1;
			};

			float3 _Color;
			float3 _OutlineColor;
			float _Threshold;

			vertexOutput vert(vertexInput i)
			{
				vertexOutput o;

				float4x4 modelMatrix = unity_ObjectToWorld;
				float4x4 modelMatrixInverse = unity_WorldToObject;

				o.posWorld = mul(modelMatrix, i.vertex);
				o.normalDir = normalize(mul(float4(i.normal, 0.0), modelMatrixInverse).rgb);

				o.pos = UnityObjectToClipPos(i.vertex);
				return o;
			}

			fixed3 frag (vertexOutput i) : COLOR
			{
				float3 normalDirection = normalize(i.normalDir);

				float3 viewDirection = normalize(_WorldSpaceCameraPos - i.posWorld.xyz);

				float3 fragmentColor = _Color;

				if (dot(normalDirection, viewDirection) > _Threshold)
				{
					fragmentColor = _OutlineColor;
				}

				return fragmentColor;
			}

			ENDCG
		}
	}
}
