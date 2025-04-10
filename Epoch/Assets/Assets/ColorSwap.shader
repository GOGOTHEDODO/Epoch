Shader "Custom/ExactColorSwap"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}

        _Color("Tint Color", Color) = (1,1,1,1) // NEW

        _OriginalColor1("Original Color 1", Color) = (1,1,1,1)
        _TargetColor1("Target Color 1", Color) = (1,1,1,1)
        _Tolerance1("Tolerance 1", Range(0, 2)) = 2

        _OriginalColor2("Original Color 2", Color) = (1,1,1,1)
        _TargetColor2("Target Color 2", Color) = (1,1,1,1)
        _Tolerance2("Tolerance 2", Range(0, 2)) = 2

        _OriginalColor3("Original Color 3", Color) = (1,1,1,1)
        _TargetColor3("Target Color 3", Color) = (1,1,1,1)
        _Tolerance3("Tolerance 3", Range(0, 2)) = 2

        _OriginalColor4("Original Color 4", Color) = (1,1,1,1)
        _TargetColor4("Target Color 4", Color) = (1,1,1,1)
        _Tolerance4("Tolerance 4", Range(0, 2)) = 2

        _OriginalColor5("Original Color 5", Color) = (1,1,1,1)
        _TargetColor5("Target Color 5", Color) = (1,1,1,1)
        _Tolerance5("Tolerance 5", Range(0, 2)) = 2

        _OriginalColor6("Original Color 6", Color) = (1,1,1,1)
        _TargetColor6("Target Color 6", Color) = (1,1,1,1)
        _Tolerance6("Tolerance 6", Range(0, 2)) = 2

        _OriginalColor7("Original Color 7", Color) = (1,1,1,1)
        _TargetColor7("Target Color 7", Color) = (1,1,1,1)
        _Tolerance7("Tolerance 7", Range(0, 2)) = 2

        _OriginalColor8("Original Color 8", Color) = (1,1,1,1)
        _TargetColor8("Target Color 8", Color) = (1,1,1,1)
        _Tolerance8("Tolerance 8", Range(0, 2)) = 2

        _OriginalColor9("Original Color 9", Color) = (1,1,1,1)
        _TargetColor9("Target Color 9", Color) = (1,1,1,1)
        _Tolerance9("Tolerance 9", Range(0, 2)) = 2
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Cull Off
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color; // Tint color

            float4 _OriginalColor1, _TargetColor1; float _Tolerance1;
            float4 _OriginalColor2, _TargetColor2; float _Tolerance2;
            float4 _OriginalColor3, _TargetColor3; float _Tolerance3;
            float4 _OriginalColor4, _TargetColor4; float _Tolerance4;
            float4 _OriginalColor5, _TargetColor5; float _Tolerance5;
            float4 _OriginalColor6, _TargetColor6; float _Tolerance6;
            float4 _OriginalColor7, _TargetColor7; float _Tolerance7;
            float4 _OriginalColor8, _TargetColor8; float _Tolerance8;
            float4 _OriginalColor9, _TargetColor9; float _Tolerance9;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);

                if (col.a == 0) return half4(0, 0, 0, 0);

                if (length(col - _OriginalColor1) < _Tolerance1) col.rgb = _TargetColor1.rgb;
                else if (length(col - _OriginalColor2) < _Tolerance2) col.rgb = _TargetColor2.rgb;
                else if (length(col - _OriginalColor3) < _Tolerance3) col.rgb = _TargetColor3.rgb;
                else if (length(col - _OriginalColor4) < _Tolerance4) col.rgb = _TargetColor4.rgb;
                else if (length(col - _OriginalColor5) < _Tolerance5) col.rgb = _TargetColor5.rgb;
                else if (length(col - _OriginalColor6) < _Tolerance6) col.rgb = _TargetColor6.rgb;
                else if (length(col - _OriginalColor7) < _Tolerance7) col.rgb = _TargetColor7.rgb;
                else if (length(col - _OriginalColor8) < _Tolerance8) col.rgb = _TargetColor8.rgb;
                else if (length(col - _OriginalColor9) < _Tolerance9) col.rgb = _TargetColor9.rgb;

                return col * _Color; // Apply tint
            }

            ENDCG
        }
    }
}
