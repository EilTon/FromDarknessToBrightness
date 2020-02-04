// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Cartoon"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_Color("Color", Color) = (0,0,0,0)
		_Normal("Normal", 2D) = "bump" {}
		_ShadowColor("ShadowColor", Color) = (0,0,0,0)
		_LitShaprness("LitShaprness", Float) = 1.75
		_ShadowOffset("ShadowOffset", Range( -1 , 1)) = 1
		_LightColorIntensity("LightColorIntensity", Range( 0 , 1)) = 0
		_Specular("Specular", 2D) = "white" {}
		_SpecularColor("SpecularColor", Color) = (1,1,1,1)
		_SpecSize("SpecSize", Range( 0 , 1)) = 0.1
		_SpecSmoothness("SpecSmoothness", Range( 0 , 1)) = 0.07058827
		_SpecIntensity("SpecIntensity", Range( 0 , 1)) = 0
		_FresnelColor("FresnelColor", Color) = (0,0,0,0)
		_FresnelBias("FresnelBias", Float) = 0
		_FresnelScale("FresnelScale", Float) = 0
		_FresnelPower("FresnelPower", Float) = 0
		_FresnelColorOverride("FresnelColorOverride", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "UnityCG.cginc"
		#include "UnityShaderVariables.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
			float2 uv_texcoord;
			float3 worldRefl;
		};

		struct SurfaceOutputCustomLightingCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			Input SurfInput;
			UnityGIInput GIData;
		};

		uniform float4 _ShadowColor;
		uniform float4 _Color;
		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform float _LitShaprness;
		uniform float _ShadowOffset;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float _LightColorIntensity;
		uniform float _SpecSize;
		uniform float _SpecSmoothness;
		uniform sampler2D _Specular;
		uniform float4 _Specular_ST;
		uniform float _SpecIntensity;
		uniform float4 _SpecularColor;
		uniform float _FresnelBias;
		uniform float _FresnelScale;
		uniform float _FresnelPower;
		uniform float4 _FresnelColor;
		uniform float _FresnelColorOverride;

		inline half4 LightingStandardCustomLighting( inout SurfaceOutputCustomLightingCustom s, half3 viewDir, UnityGI gi )
		{
			UnityGIInput data = s.GIData;
			Input i = s.SurfInput;
			half4 c = 0;
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_vertexNormal = mul( unity_WorldToObject, float4( ase_worldNormal, 0 ) );
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float3 tex2DNode96 = UnpackNormal( tex2D( _Normal, uv_Normal ) );
			float4 transform7 = mul(unity_ObjectToWorld,float4( ( ase_vertexNormal + (WorldNormalVector( i , tex2DNode96 )) ) , 0.0 ));
			float4 normalizeResult63 = normalize( transform7 );
			float dotResult8 = dot( float4( ase_worldlightDir , 0.0 ) , normalizeResult63 );
			float clampResult61 = clamp( ( ( dotResult8 * _LitShaprness ) + ( _LitShaprness * _ShadowOffset ) ) , -1.0 , 1.0 );
			float temp_output_46_0 = (0.0 + (clampResult61 - -1.0) * (1.0 - 0.0) / (1.0 - -1.0));
			float4 lerpResult76 = lerp( _ShadowColor , _Color , temp_output_46_0);
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
			float4 ase_lightColor = 0;
			#else //aselc
			float4 ase_lightColor = _LightColor0;
			#endif //aselc
			float4 lerpResult72 = lerp( float4( 1,1,1,1 ) , ase_lightColor , _LightColorIntensity);
			float clampResult84 = clamp( ( _SpecSize - _SpecSmoothness ) , 0.0 , 1.0 );
			float dotResult102 = dot( WorldReflectionVector( i , tex2DNode96 ) , ase_worldlightDir );
			float smoothstepResult81 = smoothstep( ( 1.0 - _SpecSize ) , ( 1.0 - clampResult84 ) , dotResult102);
			float2 uv_Specular = i.uv_texcoord * _Specular_ST.xy + _Specular_ST.zw;
			float4 temp_output_103_0 = ( tex2D( _Specular, uv_Specular ) * _SpecIntensity * _SpecularColor );
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float fresnelNdotV87 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode87 = ( _FresnelBias + _FresnelScale * pow( 1.0 - fresnelNdotV87, _FresnelPower ) );
			float clampResult110 = clamp( fresnelNode87 , 0.0 , 1.0 );
			float4 lerpResult95 = lerp( ase_lightColor , _FresnelColor , _FresnelColorOverride);
			float4 clampResult52 = clamp( ( ( ( lerpResult76 * tex2D( _MainTex, uv_MainTex ) ) * lerpResult72 ) + ( smoothstepResult81 * temp_output_103_0 ) + ( ( 1.0 - temp_output_46_0 ) * clampResult110 * lerpResult95 ) ) , float4( 0,0,0,0 ) , float4( 1,1,1,1 ) );
			c.rgb = clampResult52.rgb;
			c.a = 1;
			return c;
		}

		inline void LightingStandardCustomLighting_GI( inout SurfaceOutputCustomLightingCustom s, UnityGIInput data, inout UnityGI gi )
		{
			s.GIData = data;
		}

		void surf( Input i , inout SurfaceOutputCustomLightingCustom o )
		{
			o.SurfInput = i;
			o.Normal = float3(0,0,1);
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardCustomLighting keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.worldRefl = -worldViewDir;
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputCustomLightingCustom o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputCustomLightingCustom, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16900
451;104;1830;843;2437.795;-683.7734;1;True;False
Node;AmplifyShaderEditor.SamplerNode;96;-4938.079,259.455;Float;True;Property;_Normal;Normal;2;0;Create;True;0;0;False;0;None;223a9e132133eb84fa330f2525f65c5b;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldNormalVector;100;-4585.063,264.9537;Float;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.NormalVertexDataNode;5;-4653.237,71.42762;Float;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;97;-4392.608,167.0773;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;7;-4216.565,120.3294;Float;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NormalizeNode;63;-3950.44,120.3562;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;6;-3720.84,-168.9833;Float;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;54;-3276.165,673.9868;Float;False;Property;_ShadowOffset;ShadowOffset;5;0;Create;True;0;0;False;0;1;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-3330.362,519.2252;Float;False;Property;_LitShaprness;LitShaprness;4;0;Create;True;0;0;False;0;1.75;8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;8;-3140.649,139.649;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;-2963.597,580.4351;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;-2855.029,221.8772;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;67;-2773.554,865.2134;Float;False;Property;_SpecSize;SpecSize;9;0;Create;True;0;0;False;0;0.1;0.355;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;85;-2941.741,1050.278;Float;False;Property;_SpecSmoothness;SpecSmoothness;10;0;Create;True;0;0;False;0;0.07058827;0.266;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;68;-2525.611,248.1132;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;83;-2615.18,1038.178;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;61;-2215.84,222.0362;Float;True;3;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldReflectionVector;101;-3739.42,-371.8939;Float;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;75;-1768.056,-402.5634;Float;False;Property;_ShadowColor;ShadowColor;3;0;Create;True;0;0;False;0;0,0,0,0;0.2389482,0.03644535,0.3679245,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;46;-1867.093,188.8436;Float;True;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;84;-2421.741,930.2784;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;74;-1914.37,-52.8595;Float;False;Property;_Color;Color;1;0;Create;True;0;0;False;0;0,0,0,0;1,0.995283,0.995283,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;92;-1986.8,1570.452;Float;False;Property;_FresnelPower;FresnelPower;15;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;90;-1999.959,1348.558;Float;False;Property;_FresnelBias;FresnelBias;13;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;91;-1998.116,1438.052;Float;False;Property;_FresnelScale;FresnelScale;14;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;80;-1911.821,1055.73;Float;False;Property;_SpecIntensity;SpecIntensity;11;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;86;-2340.741,845.2784;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;77;-1562.96,25.98237;Float;True;Property;_MainTex;MainTex;0;0;Create;True;0;0;False;0;None;5942237c38e03e9459be2d0df577efd8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;71;-1724.27,559.3058;Float;False;Property;_LightColorIntensity;LightColorIntensity;6;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;102;-3399.99,-223.9905;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;66;-2404.3,718.0014;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;87;-1673.888,1335.71;Float;False;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;94;-1617.285,1553.946;Float;False;Property;_FresnelColor;FresnelColor;12;0;Create;True;0;0;False;0;0,0,0,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;93;-1666.69,1787.83;Float;False;Property;_FresnelColorOverride;FresnelColorOverride;16;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;104;-1932.49,770.7324;Float;True;Property;_Specular;Specular;7;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;111;-2083.048,938.9745;Float;False;Property;_SpecularColor;SpecularColor;8;0;Create;True;0;0;False;0;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LightColorNode;1;-1611.825,444.3103;Float;False;0;3;COLOR;0;FLOAT3;1;FLOAT;2
Node;AmplifyShaderEditor.LerpOp;76;-1391.523,-198.9568;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;72;-1398.495,414.6523;Float;False;3;0;COLOR;1,1,1,1;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;95;-1279.746,1347.565;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;89;-1098.272,452.4344;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;103;-1615.3,842.2155;Float;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;-1056.198,-84.73538;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;110;-1244.329,939.3634;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;81;-2133.68,660.9782;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;79;-1280.834,631.4279;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;88;-888.4341,539.207;Float;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-1022.23,165.2766;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;53;-625.5364,337.1159;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SwizzleNode;108;-1467.731,834.4913;Float;False;FLOAT3;0;0;0;3;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;52;-329.8816,249.5054;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,1;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;0;CustomLighting;Cartoon;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;100;0;96;0
WireConnection;97;0;5;0
WireConnection;97;1;100;0
WireConnection;7;0;97;0
WireConnection;63;0;7;0
WireConnection;8;0;6;0
WireConnection;8;1;63;0
WireConnection;69;0;48;0
WireConnection;69;1;54;0
WireConnection;64;0;8;0
WireConnection;64;1;48;0
WireConnection;68;0;64;0
WireConnection;68;1;69;0
WireConnection;83;0;67;0
WireConnection;83;1;85;0
WireConnection;61;0;68;0
WireConnection;101;0;96;0
WireConnection;46;0;61;0
WireConnection;84;0;83;0
WireConnection;86;0;84;0
WireConnection;102;0;101;0
WireConnection;102;1;6;0
WireConnection;66;0;67;0
WireConnection;87;1;90;0
WireConnection;87;2;91;0
WireConnection;87;3;92;0
WireConnection;76;0;75;0
WireConnection;76;1;74;0
WireConnection;76;2;46;0
WireConnection;72;1;1;0
WireConnection;72;2;71;0
WireConnection;95;0;1;0
WireConnection;95;1;94;0
WireConnection;95;2;93;0
WireConnection;89;0;46;0
WireConnection;103;0;104;0
WireConnection;103;1;80;0
WireConnection;103;2;111;0
WireConnection;78;0;76;0
WireConnection;78;1;77;0
WireConnection;110;0;87;0
WireConnection;81;0;102;0
WireConnection;81;1;66;0
WireConnection;81;2;86;0
WireConnection;79;0;81;0
WireConnection;79;1;103;0
WireConnection;88;0;89;0
WireConnection;88;1;110;0
WireConnection;88;2;95;0
WireConnection;3;0;78;0
WireConnection;3;1;72;0
WireConnection;53;0;3;0
WireConnection;53;1;79;0
WireConnection;53;2;88;0
WireConnection;108;0;103;0
WireConnection;52;0;53;0
WireConnection;0;13;52;0
ASEEND*/
//CHKSM=D768EC5B6D9E4D76CA947851642D03B6211A327F