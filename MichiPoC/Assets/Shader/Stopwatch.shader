Shader "Unlit/Stopwatch"
{
	Properties
	{
		_TimeRatio ("Time Ratio since start", Range (0, 1)) = 0
		_Color ("Color", Vector) = (0, 0, 0, 0)
	}
	SubShader
	{
		Tags 
		{ 
			"RenderType" = "Opaque"
			"Queue" = "Transparent+1"
		}
		LOD 100

		Pass
		{

			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};


			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			float _TimeRatio;
			float4 _Color;

			fixed4 frag(v2f i) : SV_Target
			{
				float2 center = float2(.5, .5);

				float isInStopwatch = step(distance(center, i.uv), .5);

				float2 up = float2(0, 1);
				float2 myPos = i.uv - center;

				float angle = acos(dot(up, normalize(myPos)));

				float xIsNegative = step(myPos.x, 0);
				angle = (1 - xIsNegative) * angle + xIsNegative * (2 * 3.1415 - angle);

				float angleRatio = angle / (2 * 3.1415);

				float isColored = step(angleRatio, _TimeRatio);

				float4 color = _Color * isColored + float4(0, 0, 0, 0) * (1 - isColored);

				return color * isInStopwatch;
			}
			ENDCG
		}
	}
}
