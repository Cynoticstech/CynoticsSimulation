Shader"Custom/BoxBlur"
{
    Properties 
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
 
    SubShader {
        Tags { "RenderType"="Opaque" }
LOD100
 
        Pass {
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
 
v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    return o;
}
 
fixed4 frag(v2f i) : SV_Target
{
    fixed4 col = fixed4(0, 0, 0, 0);
    float blurSize = 0.01; // Adjust the blur size here
 
    for (float x = -blurSize; x <= blurSize; x += blurSize / 4.0)
    {
        for (float y = -blurSize; y <= blurSize; y += blurSize / 4.0)
        {
            col += tex2D(_MainTex, i.uv + float2(x, y));
        }
    }
 
    col /= pow((2.0 * blurSize + 1.0), 2.0);
    return col;
}
 
            ENDCG
        }
    }
}
