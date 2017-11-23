Shader "Custom/Bullet" 
{
	Properties
	{
		_Color("Main Color", Color) = (1, 1, 1, 1)
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
				float3 normal : TEXCOORD0;
			};

			float3 _Color;

			vertexOutput vert(vertexInput i)
			{
				vertexOutput o;
				o.pos = UnityObjectToClipPos(i.vertex);
				o.normal = normalize(i.normal);
				return o;
			}

			fixed4 frag (vertexOutput i) : SV_Target
			{
				fixed4 color;
				color.rgb = _Color;
				return color;
			}

			ENDCG
		}
	}
}
