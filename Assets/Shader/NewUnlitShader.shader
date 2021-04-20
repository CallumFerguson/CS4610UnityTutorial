Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _FogColor("Fog Color", Color) = (0.5, 0.5, 0.5, 1)
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
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXTCOORD0;
            };

            float4 _Color, _FogColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float3 worldPosition = (mul(unity_ObjectToWorld, v.vertex)).xyz;
                o.worldPos = worldPosition;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float height = i.worldPos.y;
                return float4(height, height, height, 1);
            }
            ENDCG
        }
    }
}