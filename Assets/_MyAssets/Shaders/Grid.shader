Shader "Custom/Unlit/Grid" {
    Properties {
        _LineColor ("Line Color", Color) = (1,1,1,1)
        _BlankColor ("Brank Color", Color) = (0.4,0.4,0.4,0.3)
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}
        [IntRange] _SplitCountX("Split Count", Range(1, 30)) = 10
        [IntRange] _SplitCountY("Split Count", Range(1, 30)) = 2
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
    
            fixed4 _LineColor;
            fixed4 _BlankColor;
            float _SplitCountX;
            float _SplitCountY;
            float _LineSize;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }
    
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = lerp(_BlankColor,_LineColor, saturate((frac(i.uv.x * (_SplitCountX + _LineSize)) < _LineSize) + (frac(i.uv.y * (_SplitCountY + _LineSize)) < _LineSize)));
                 UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}