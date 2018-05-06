Shader "Unlit/FillBar"
{
	Properties
	{
		_OffColor("Off Color", Vector) = (0, 0, 0, 1)
		_TransitionColorPositive("Positive transition color", Vector) = (0, 1, 0, 1)
		_TransitionColorNegative("Negative transition color", Vector) = (1, 0, 0, 1)
		_TransitPositive("Transit color shuld be positive color", Float) = 0
		_OnColor("On Color", Vector) = (1, 0, 0, 1)
		_TransitionState("TransitionState", Range(0, 1)) = 0
		_FillState("FillState", Range(0, 1)) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
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

			float4 _OffColor;
			float4 _OnColor;
			float4 _TransitionColorPositive;
			float4 _TransitionColorNegative;
			float _TransitPositive;
			float _TransitionState;
			float _FillState;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float isOn = step(i.uv.x, _FillState);
				float4 color = isOn * _OnColor + (1 - isOn) * _OffColor;


				float isTransit = step(_FillState, i.uv.x) * step(i.uv.x, _TransitionState);
				float4 transitColor = lerp(_TransitionColorNegative, _TransitionColorPositive, _TransitPositive);

				color = isTransit * transitColor + (1 - isTransit) * color;

				return color;
			}
			ENDCG
		}
	}
}
