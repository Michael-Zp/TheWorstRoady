Shader "Unlit/BloodDrop"
{
	Properties
	{
		_RandomId ("Custom 1", Vector) = (0, 0, 0, 0)
	}
	SubShader
	{
		Tags { 
		"RenderType"="Opaque"
		"Queue" = "Transparent"
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

			float4 _RandomId;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			float random(float seed)
			{
				return frac(sin(seed) * 1231534.9);
			}

			float random2D(float2 coord)
			{
				return random(dot(coord, float2(21.97898, 7809.33123)));
			}

			//random vector with length 1
			float2 rand2(float2 seed)
			{
				const float pi = 3.1415926535897932384626433832795;
				const float twopi = 2 * pi;
				float r = random2D(seed) * twopi;
				return float2(cos(r), sin(r));
			}

			float gnoise(float2 coord)
			{
				float2 i = floor(coord); // integer position

									   //random gradient at nearest integer positions
				float2 g00 = rand2(i);
				float2 g10 = rand2(i + float2(1, 0));
				float2 g01 = rand2(i + float2(0, 1));
				float2 g11 = rand2(i + float2(1, 1));

				float2 f = frac(coord);
				float v00 = dot(g00, f);
				float v10 = dot(g10, f - float2(1, 0));
				float v01 = dot(g01, f - float2(0, 1));
				float v11 = dot(g11, f - float2(1, 1));

				float2 weight = f; // linear interpolation
				weight = smoothstep(0, 1, f); // cubic interpolation

				float x1 = lerp(v00, v10, weight.x);
				float x2 = lerp(v01, v11, weight.x);
				return lerp(x1, x2, weight.y) + 0.5;
			}

			float noise(float value)
			{
				float xPos = floor(value);

				float currentVal = random(xPos);
				float lastVal = random(xPos - 1);

				float weight = frac(value);
				weight = smoothstep(0, 1, weight);

				return lerp(lastVal, currentVal, weight);
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 vecFromMiddle = i.uv - float2(0.5, 0.5);
				if (length(vecFromMiddle) > gnoise(normalize(vecFromMiddle) * 4 + _RandomId.xy) / 4.0 + 0.25)
				{
					return float4(0, 0, 0, 0);
				}

				return float4(.7, .1, .1, 1) - float4(.5, .5, .5, 0) * pow(clamp(gnoise(i.uv * 8), 0, 1), 4);
			}
			ENDCG
		}
	}
}
