Shader "Custom/Bullet" 
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 normal : TEXCOORD0;
			};

			float3 _Color;

			v2f vert(appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.normal = normalize(v.normal);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				
				fixed4 color;
				color = i.pos;


				return color;
			}

			ENDCG
		}
	}
}
