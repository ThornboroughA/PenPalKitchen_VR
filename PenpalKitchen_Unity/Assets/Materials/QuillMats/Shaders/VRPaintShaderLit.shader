Shader "VertCol/VertexColorLit"
{
    Properties
    {
        _AmbientColor ("AmbientColor", Color) = (0.02, 0.05, 0.1)
		_Opacity ("Opacity", Range(0.0, 1.0)) = 1.0
        _LightThreshold ("LightThreshold", Range(0.0, 1.0)) = 0.0
        _LightSmoothness ("LightSmoothness", Range(0.0003, 1.0)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry"}
        LOD 100
        Blend Off
        Cull Off
        ZWrite On
        ZTest On

        Pass
        {
            AlphaToMask On
            Tags {
                "LightMode" = "ForwardBase"
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            #pragma shader_feature _ENABLETRANSPARENCY_ON
            #pragma target 3.0

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"
            #include "common/DitherAlpha.cginc"

            fixed4 _AmbientColor;
			float _Opacity;

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 world_pos : TEXCOORD0;
                float4 screen_pos : TEXCOORD1;
                float4 color : COLOR;
                UNITY_FOG_COORDS(2)
                LIGHTING_COORDS(3,4)
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                o.world_pos = mul(unity_ObjectToWorld, v.vertex);
                o.screen_pos = ComputeScreenPos(o.pos);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                dither_mask(i.screen_pos, _Opacity * i.color.a);
                //fixed4 col = i.color;
                //fixed3 col = dot(i.normal, normalize(_WorldSpaceLightPos0.xyz));
                //fixed light = dot(normalize(i.world_pos), _WorldSpaceLightPos0.xyz);
                fixed shadows = LIGHT_ATTENUATION(i);
                fixed light = clamp(dot(fixed3(0, 1, 0), _WorldSpaceLightPos0.xyz), 0, 1) * shadows;
                fixed3 col = _AmbientColor.rgb + (light * i.color * _LightColor0);
                UNITY_APPLY_FOG(i.fogCoord, col);
                
                //return fixed4(col, 1.0);
                //return _WorldSpaceLightPos0;
                return fixed4(col, 1);
            }
            ENDCG
        }
        Pass
        {
            AlphaToMask On
            Tags {
                "LightMode" = "ForwardAdd"
            }
            Blend One One
            ZWrite Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #pragma shader_feature _ENABLETRANSPARENCY_ON
            #pragma target 3.0

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "common/DitherAlpha.cginc"

            #define POINT /* this pass is for point lights */
            #include "AutoLight.cginc"

            fixed _LightSmoothness;
            fixed _LightThreshold;
            float _Opacity;

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 world_pos : TEXCOORD0;
                float4 screen_pos : TEXCOORD1;
                float4 color : COLOR;
                UNITY_FOG_COORDS(2)
                LIGHTING_COORDS(3,4)
            };
            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.world_pos = mul(unity_ObjectToWorld, v.vertex);
                o.screen_pos = ComputeScreenPos(o.pos);
                o.color = v.color;
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                dither_mask(i.screen_pos, _Opacity * i.color.a);
                //fixed4 col = i.color;
                //fixed light = clamp(1 - length(i.world_pos - _WorldSpaceLightPos0) / 16, 0, 1);
                //float3 light_vec = _WorldSpaceLightPos0.xyz - i.world_pos;
                //fixed attenuation = 1 / (1 + dot(light_vec, light_vec));
                
                //UNITY_LIGHT_ATTENUATION(attenuation, 0, i.world_pos);
                fixed shadows = LIGHT_ATTENUATION(i);
                fixed light = smoothstep(_LightThreshold, _LightThreshold + _LightSmoothness, shadows);

                fixed3 col = i.color * light * _LightColor0;
                //fixed3 col = _LightColor0;

                UNITY_APPLY_FOG(i.fogCoord, col);

                return fixed4(col, 1.0);
                //return fixed4(i.world_pos, 1.0);
                //return _WorldSpaceLightPos0;
            }
            ENDCG
        }
    }
    Fallback "VertexLit"
}
