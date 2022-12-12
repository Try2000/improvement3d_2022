Shader "Custom/Unlit/SmartGrid" {
    Properties {
        _BlankColor ("Brank Color", Color) = (0.4,0.4,0.4,0.3)
        _MainTex ("Texture", 2D) = "white" {}
        [IntRange] _SplitCountX("Split Count", Range(1, 30)) = 10
        [IntRange] _SplitCountY("Split Count", Range(1, 30)) = 2
        _SphereThreshold("SphereRadius" , Range(0,1)) = 1
        _SphereColor("Sphere Color" ,Color) = (1,1,1,1)
        _LineSize("Line Size", Range(0.01, 1)) = 0.1
    }
    SubShader {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha 
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
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
                float4 vertex : SV_POSITION;
                UNITY_FOG_COORDS(3)
            };
            
            sampler2D _MainTex;
            fixed4 _BlankColor;
            float _SplitCountX;
            float _SplitCountY;
            float _LineSize;
            float _SphereThreshold;
            fixed4 _SphereColor;
            float4 _MainTex_ST;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _MainTex_ST.xy;
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }
    
            fixed4 frag (v2f i) : SV_Target
            {
                fixed2 splituv = fixed2(fmod(i.uv.x * _SplitCountX,1),fmod(i.uv.y * _SplitCountY,1));
                fixed isSphere = step(_SphereThreshold,  length(splituv)) * step(_SphereThreshold,  length(splituv - fixed2(1 ,0))) * step(_SphereThreshold,  length(splituv - fixed2(0,1  ))) * step(_SphereThreshold,  length(splituv - fixed2(1 ,1 )));
                fixed4 col = _BlankColor * isSphere + _SphereColor * (1-isSphere);
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;           
            }
            ENDCG
        }
    }
}