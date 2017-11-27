Shader "BanMing/DiffuseVertex" {
	Properties{

	}
	SubShader{
		Pass{
		CGPROGRAM

#pragma vertex vert

	struct a2v {
		float4 vertex:POSITION;//告诉unity把模型空间下的顶点坐标填充给vertex
	};

	float4 vert(a2v v) :SV_POSITION{
		return mul(UNITY_MATRIX_MVP,v.vertex000);
	}



		ENDCG
	}
	}
		FallBack "Diffuse"
}
