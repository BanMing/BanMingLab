
Shader "UI/BanMing/BinderAplha"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1, 1, 1, 1)
        [MaterialToggle] PixelSnap ("Pixel snap", float) = 0       

        /* alphaDown */
        _R("alphaDownStartRange",Range(0.1,1)) = 0.9
        _L("alphaDownEndRange",Range(0,0.1)) = 0.05
        /* -- */
    }

    SubShader
    {
        Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha
        
        /* UI */
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp] 
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }
		ColorMask[_ColorMask]
        /* -- */

        Pass
        {
        CGPROGRAM        
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR0;
                half2 texcoord : TEXCOORD0;
                fixed aplhaColor: COLOR1;
            };
            
            fixed4 _Color;

            /* alphaDown */
            float _R;
            float _L;
            /* ----------*/

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;

                OUT.color = IN.color * _Color;
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif

                OUT.x = OUT.vertex.x/OUT.vertex.w;
                // if (OUT.x<-_R)
                //     OUT.aplhaColor=saturate(_L-abs(OUT.x-_R))/_L;
                // if(OUT.x>_R)
                //     OUT.aplhaColor=saturate(_L-abs(OUT.x+_R))/_L;

                return OUT;
            }

            sampler2D _MainTex;

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 c = tex2D(_MainTex, IN.texcoord);

                c.rgb *= c.a;
                c *=IN.aplhaColor;
                if(IN.x<-_R)
                	c *= saturate(_L-abs(IN.x+_R))/_L;
                if(IN.x>_R)
                	c *= saturate(_L-abs(IN.x-_R))/_L;
                /* --------- */

                return c;
            }
        ENDCG
        }
    }
}