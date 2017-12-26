// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

  Shader "Custom/Shader"

	{

		SubShader

		{

			Pass

		{

			ZTest Always Cull Off ZWrite Off

			Fog{ Mode off }



			CGPROGRAM

#include "UnityCG.cginc"

#pragma target 5.0

#pragma vertex vert

#pragma fragment frag

 


			struct Vert
		{
			  float3 pos;
		};





		uniform StructuredBuffer<Vert> buffer;

	 
		struct v2f

		{

			float4  pos : SV_POSITION;

		 

		};
		float3  h=0;


		v2f vert(uint id : SV_VertexID)

		{

			float3 pos = buffer[id].pos;
			h = pos;
			v2f OUT;

			OUT.pos = UnityObjectToClipPos(float4(buffer[id].pos, 1));
			
			h = OUT.pos;
			return OUT;

		}



		float4 frag(v2f IN) : COLOR

		{

			return float4(1,0,0,1);

		}



			ENDCG

		}

		}

	}