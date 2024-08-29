Shader "BlendedDecal"
{
    Properties
    {
        _Color ("Tint", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Lighting Off
        ZTest LEqual
        ZWrite Off
        Tags { "Queue"="Transparent" }
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Offset -1,-1
            SetTexture [_MainText]
            {
                ConstantColor[_Color]
                Combine texture * constant
            }
        }
    }
}