Shader "Custom/Unlit/Grid_Pilotis" {
    Properties {
        _LineColor ("Line Color", Color) = (1,1,1,1)
        _BlankColor ("Brank Color", Color) = (0.4,0.4,0.4,0.3)
        _WallColor ("Wall Color", Color) = (0,0,0,1)
        _LineThreshold("Line Threshold" ,Range(0,1)) = 0.5
        _LineMult("LineMult",Float) = 4
        _MainTex ("Texture", 2D) = "white" {}
        _UpDirection("UpDirection" , Vector) = (0,1,0)
        _UpThreshold("UpThreshold",Range(0,1)) = 0.5
        _SpecColor("SpecularColor" , Color) = (1,1,1,1)
        _Ambient("Ambient" , Color) = (1,1,1,1)
        _LightColor("LightColor" , Color) = (1,1,1,1)
        _Ka("ka", Float)  = 1
        _Kd("kd", Float)  = 1
        _Ks("ks", Float)  = 1
        _Diffuse("Diffuse", Float)  = 1
        _Shininess("Shiness", Float)  = 1
    }
    SubShader {

        Blend SrcAlpha OneMinusSrcAlpha 
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                
            };
    
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                 float3 normal : TEXCOORD1;
                 float3 view : TEXCOORD2;
                 UNITY_FOG_COORDS(3)
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;

            fixed4 _LineColor;
            fixed4 _BlankColor;
            fixed4 _WallColor;
            float _LineThreshold;
            float _LineSize;
            float _LineMult;
            fixed3 _UpDirection;
            fixed _UpThreshold;
            fixed _Ka;
            fixed _Kd;
            fixed _Ks;
            fixed _Diffuse;
            fixed _Shininess;
            fixed3 _Ambient;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _MainTex_ST;
                o.normal = mul (UNITY_MATRIX_MV, float4(v.normal,0));
                o.view = WorldSpaceViewDir(v.vertex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }
    
            fixed4 frag (v2f i) : SV_Target
            {
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 NdotL = dot(i.normal,lightDir);
                fixed isUp = step(0,dot(i.normal,_UpDirection));
                fixed t = saturate(step(_LineThreshold, frac((i.uv.x + i.uv.y)*_LineMult)) + step(_LineThreshold, frac((i.uv.x - i.uv.y)*_LineMult)));
                fixed4 col = isUp * lerp(  _BlankColor,_LineColor,  t ) + (1-isUp) * _WallColor;
                col.xyz = col.xyz * max(0.0, NdotL) * _LightColor0.rgb;

                float3 reflect = normalize(- lightDir + 2.0 * i.normal * NdotL); // ”½ŽËƒxƒNƒgƒ‹
                float3 diffuse = _Kd * col.xyz * NdotL * _Diffuse; // ŠgŽU”½ŽË
                float3 spec = _Ks * pow(max(0, dot(reflect, i.view)), _Shininess) * _SpecColor; // ‹¾–Ê”½ŽË
                float ambient = col.xyz * _Ka * _SpecColor; // ŠÂ‹«Œõ
                col.rgb = diffuse + spec + ambient;
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}