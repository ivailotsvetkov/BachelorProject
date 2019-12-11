// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "GalaxyFrisbee/UIPointerShader" 
{

Properties 
{
    _TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	_Emissive ("Emissive", Float) = 1.0
}

Category 
{
    //Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
    //Blend SrcAlpha OneMinusSrcAlpha
    //ColorMask RGB
    Cull Off Lighting Off ZWrite On //ZTest Always

    SubShader {
        Pass {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_particles
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            fixed4 _TintColor;
			float _Emissive;

            struct appdata_t {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color * _TintColor;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = i.color * _Emissive;
                return col;
            }
            ENDCG
        }
    }
}
}
