Shader "Unlit/PaulsInverseShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_OverlayTex("Texture", 2D) = "white" {}
		_TargetEye("Target eye (-1 - left, 1 - right, 0 - both)", Range(-1.0,1.0)) = -1.0
		_Contrast("Contrast", Range(0.0,1.5)) = 1.0
	}
		SubShader
		{
			Tags { "RenderType" = "Opaque" }
			LOD 100

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				// make fog work
				#pragma multi_compile_fog

				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				sampler2D _OverlayTex;
				fixed _EyeFloatFlag;
				fixed _TargetEye;
				fixed _Contrast;

				float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
				o.vertex = lerp(UnityObjectToClipPos(v.vertex), float4(0, 0, 0, 0), saturate(abs(_TargetEye - _EyeFloatFlag) - 1));


                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 base = tex2D(_MainTex, i.uv);
			// Contrast Adjustment
				base.rgb = (base.rgb - 0.5f) * (_Contrast)+0.5f;
				fixed4 overlay = tex2D(_OverlayTex, i.uv);
				fixed4 col = base;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                //return col * (1 - overlay.a);
				return lerp(col, overlay, overlay.a);
            }
            ENDCG
        }
    }
}
