// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/OUtLine_normal" {
	Properties{

	 _Range("Rim Range",Range(0,1))=0.1
	 _DiffuseColor("Diffuse Color",Color)=(1,1,1,1)
	}
	subShader{
	   pass{
	       cull front
	       CGPROGRAM
	       #pragma vertex vert
	       #pragma fragment frag
	       #include "unitycg.cginc"


	       float _Range;
	       struct a2v{
	         float4 vertex:POSITION;
	         float4 normal:NORMAL;
	       };

	       struct v2f{
	          float4 Pos:SV_POSITION;
	          float3 normal:TEXCOORD0;
	       };

	       v2f vert(a2v v){
	          v2f o;

	          float4 V_POS=mul(UNITY_MATRIX_MV,v.vertex);
	          o.normal=mul(UNITY_MATRIX_IT_MV,v.normal).xyz;

	          V_POS.xyz+=normalize(o.normal)*_Range;

	          o.Pos=mul(UNITY_MATRIX_P,V_POS);
	          return o;

	       }

	       fixed4 frag(v2f i):SV_Target{
	          return fixed4(1,1,1,1);
	       }
	       ENDCG

	        
	   }

	    pass{


	       cull Back
	       CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members worldViewLDir)
#pragma exclude_renderers d3d11
	       #pragma vertex vert
	       #pragma fragment frag
	       #include "unitycg.cginc"


	       float _Range;
	       fixed4 _DiffuseColor;
	       struct a2v{
	         float4 vertex:POSITION;
	         float4 normal:NORMAL;
	       };

	       struct v2f{
	          float4 Pos:SV_POSITION;
	          float3 normal:TEXCOORD0;
	          float3 worldLightDir:TEXCOORD1;
	          float3 worldViewLDir:TEXCOORD2;
	       };

	       v2f vert(a2v v){
	          v2f o;
	          o.worldViewDir=(_WorldSpaceCameraPos-o.vertex).xyz;
	          o.worldLightDir=(_WorldSpaceLightPos0.xyz);
	          float4 V_POS=mul(UNITY_MATRIX_MV,v.vertex);
	          o.Pos=mul(UNITY_MATRIX_P,V_POS);
	          o.normal=mul(v.normal,unity_WorldToObject).xyz;
	         
	          return o;

	       }

	       fixed4 frag(v2f i):SV_Target{

	          float3 viewDir=normalize(i.worldViewDir);
	          float3 lightDir=normalize(i.worldLightDir);
	          fixed3 col=_DiffuseColor.rgb*(dot(i.worldLightDir,i.normal)*0.5+0.5)
	          return fixed4(1,1,1,1);
	       }
	       ENDCG

	        
	   }
	}
}
