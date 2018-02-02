Shader "BanMing/BorderAplha"
{
	Properties
	{
		[PerRendererData]_MainTex ("Texture", 2D) = "white" {}
		_R("AlphaDownStartRange",Range(0.1,1))=0.9
		_L("AlphaDownEndRange",Range(0,0.1))=0.05
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

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			float _R;
			float _L;

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				fixed4 alphaColor : Color;
				float x : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.x=o.vertex.x/o.vertex.w;

				// if(o.x<-_R)
				// 	o.alphaColor= saturate(_L-abs(o.x+_R))/_L;
				// if(o.x>_R)
				// 	o.alphaColor= saturate(_L-abs(o.x-_R))/_L;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				if(i.x<-_R)
                	col *= saturate(_L-abs(i.x+_R))/_L;
                if(i.x>_R)
                	col *= saturate(_L-abs(i.x-_R))/_L;

				return col;
			}
			ENDCG
		}
	}
}
