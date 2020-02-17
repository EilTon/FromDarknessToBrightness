// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Ray"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		_FallofPower("FallofPower", Float) = 2.21
		_WidthPanSpeed("WidthPanSpeed", Float) = 0.1
		_HeightPanSpeed("HeightPanSpeed", Float) = 1
		_GodRayIntensity("GodRayIntensity", Float) = 1
		_GodRayWidth("GodRayWidth", Float) = 10
		_GodRayLenght1("GodRayLenght", Float) = 10
		_GodRayFallofPower("GodRayFallofPower", Float) = 2.21
		_SpecFallofPower("SpecFallofPower", Float) = 1.82
		_SpecSize("SpecSize", Float) = 0.001
		_SpecNoiseScale("SpecNoiseScale", Float) = 5.42
		_SpecSpeed("SpecSpeed", Float) = 10

	}

	SubShader
	{
		LOD 0

		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		
		
		Pass
		{
		CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord1 : TEXCOORD1;
			};
			
			uniform fixed4 _Color;
			uniform float _EnableExternalAlpha;
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			uniform float _FallofPower;
			uniform float _GodRayFallofPower;
			uniform float _HeightPanSpeed;
			uniform float _GodRayLenght1;
			uniform float _WidthPanSpeed;
			uniform float _GodRayWidth;
			uniform float _GodRayIntensity;
			uniform float _SpecFallofPower;
			uniform float _SpecNoiseScale;
			uniform float _SpecSpeed;
			uniform float _SpecSize;
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			
					float2 voronoihash45( float2 p )
					{
						
						p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
						return frac( sin( p ) *43758.5453);
					}
			
					float voronoi45( float2 v, float time, inout float2 id, float smoothness )
					{
						float2 n = floor( v );
						float2 f = frac( v );
						float F1 = 8.0;
						float F2 = 8.0; float2 mr = 0; float2 mg = 0;
						for ( int j = -1; j <= 1; j++ )
						{
							for ( int i = -1; i <= 1; i++ )
						 	{
						 		float2 g = float2( i, j );
						 		float2 o = voronoihash45( n + g );
								o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = g - f + o;
								float d = 0.5 * dot( r, r );
						 		if( d<F1 ) {
						 			F2 = F1;
						 			F1 = d; mg = g; mr = r; id = o;
						 		} else if( d<F2 ) {
						 			F2 = d;
						 		}
						 	}
						}
						return F1;
					}
			

			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
				OUT.ase_texcoord1 = IN.vertex;
				
				IN.vertex.xyz +=  float3(0,0,0) ; 
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				fixed4 alpha = tex2D (_AlphaTex, uv);
				color.a = lerp (color.a, alpha.r, _EnableExternalAlpha);
#endif //ETC1_EXTERNAL_ALPHA

				return color;
			}
			
			fixed4 frag(v2f IN  ) : SV_Target
			{
				float2 uv02 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 break5 = (float2( -1,-1 ) + (uv02 - float2( 0,0 )) * (float2( 1,1 ) - float2( -1,-1 )) / (float2( 1,1 ) - float2( 0,0 )));
				float temp_output_6_0 = ( 1.0 - abs( break5.y ) );
				float mulTime32 = _Time.y * _HeightPanSpeed;
				float mulTime29 = _Time.y * _WidthPanSpeed;
				float2 appendResult27 = (float2(( ( break5.x + mulTime32 ) * _GodRayLenght1 ) , ( ( break5.y + mulTime29 ) * _GodRayWidth )));
				float simplePerlin2D30 = snoise( appendResult27*0.29 );
				simplePerlin2D30 = simplePerlin2D30*0.5 + 0.5;
				float mulTime47 = _Time.y * _SpecSpeed;
				float time45 = mulTime47;
				float3 break43 = mul( float4( IN.ase_texcoord1.xyz , 0.0 ), unity_ObjectToWorld ).xyz;
				float2 appendResult44 = (float2(break43.x , break43.y));
				float2 coords45 = appendResult44 * _SpecNoiseScale;
				float2 id45 = 0;
				float voroi45 = voronoi45( coords45, time45,id45, 0 );
				float clampResult24 = clamp( ( pow( temp_output_6_0 , _FallofPower ) + ( pow( temp_output_6_0 , _GodRayFallofPower ) * simplePerlin2D30 * _GodRayIntensity ) + ( pow( temp_output_6_0 , _SpecFallofPower ) * step( voroi45 , _SpecSize ) ) ) , 0.0 , 1.0 );
				float4 appendResult7 = (float4(_Color.rgb , clampResult24));
				
				fixed4 c = ( IN.color * appendResult7 );
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17600
206;97;1906;972;7036.941;1432.523;4.29397;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-4119.517,-323.7632;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ObjectToWorldMatrixNode;41;-3690.516,990.0852;Inherit;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.PosVertexDataNode;40;-3664.516,832.0852;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;35;-3289.602,-159.4959;Inherit;False;Property;_HeightPanSpeed;HeightPanSpeed;2;0;Create;True;0;0;False;0;1;-5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-3505.983,149.5223;Inherit;False;Property;_WidthPanSpeed;WidthPanSpeed;1;0;Create;True;0;0;False;0;0.1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;3;-3815.513,-323.7632;Inherit;True;5;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT2;1,1;False;3;FLOAT2;-1,-1;False;4;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.BreakToComponentsNode;5;-3533.513,-329.7632;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleTimeNode;29;-3265.984,116.4328;Inherit;False;1;0;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-3401.516,873.0852;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleTimeNode;32;-3061.01,-172.6008;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-2809.01,-204.6008;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;28;-3013.984,84.43279;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;59;-2873.645,-72.4795;Inherit;False;Property;_GodRayLenght1;GodRayLenght;5;0;Create;True;0;0;False;0;10;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-3163.39,263.5019;Inherit;False;Property;_GodRayWidth;GodRayWidth;4;0;Create;True;0;0;False;0;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;43;-3173.661,868.5399;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;52;-3039.414,697.192;Inherit;False;Property;_SpecSpeed;SpecSpeed;10;0;Create;True;0;0;False;0;10;6.26;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;4;-2238.778,-406.2881;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;44;-2830.478,897.1856;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;-2561.367,-140.9885;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-2803.329,1086.894;Inherit;False;Property;_SpecNoiseScale;SpecNoiseScale;9;0;Create;True;0;0;False;0;5.42;5.42;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-2829.39,223.502;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;47;-2780.406,741.6888;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;27;-2389.091,122.7406;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VoronoiNode;45;-2568.74,850.0181;Inherit;True;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;6.21;False;3;FLOAT;0;False;2;FLOAT;0;FLOAT;1
Node;AmplifyShaderEditor.OneMinusNode;6;-1438.097,-581.7219;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;50;-1534.749,803.6907;Inherit;False;Property;_SpecFallofPower;SpecFallofPower;7;0;Create;True;0;0;False;0;1.82;1.82;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-1598.805,33.58564;Inherit;False;Property;_GodRayFallofPower;GodRayFallofPower;6;0;Create;True;0;0;False;0;2.21;1.78;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-2383.435,1128.081;Inherit;False;Property;_SpecSize;SpecSize;8;0;Create;True;0;0;False;0;0.001;0.001;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;46;-2232.406,895.6888;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.001;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-1268.919,377.0852;Inherit;False;Property;_GodRayIntensity;GodRayIntensity;3;0;Create;True;0;0;False;0;1;0.56;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1459.931,-290.2826;Inherit;False;Property;_FallofPower;FallofPower;0;0;Create;True;0;0;False;0;2.21;8.76;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;57;-1295.887,30.49601;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;49;-1295.839,644.9619;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;30;-1928.473,201.2628;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;0.29;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;-974.2429,186.4102;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;-1008.569,1016.748;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;12;-1222.021,-449.0114;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-139.4356,173.7869;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;24;-23.73548,64.58704;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;36;-100.6806,-556.9997;Inherit;False;0;0;_Color;Shader;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;7;79.83785,-294.8835;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.VertexColorNode;37;141.8446,-583.1299;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;346.0884,-357.8112;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;661.2399,-283.7001;Float;False;True;-1;2;ASEMaterialInspector;0;6;Ray;0f8ba0101102bb14ebf021ddadce9b49;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;3;1;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;False;False;True;2;False;-1;False;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;3;0;2;0
WireConnection;5;0;3;0
WireConnection;29;0;34;0
WireConnection;42;0;40;0
WireConnection;42;1;41;0
WireConnection;32;0;35;0
WireConnection;31;0;5;0
WireConnection;31;1;32;0
WireConnection;28;0;5;1
WireConnection;28;1;29;0
WireConnection;43;0;42;0
WireConnection;4;0;5;1
WireConnection;44;0;43;0
WireConnection;44;1;43;1
WireConnection;60;0;31;0
WireConnection;60;1;59;0
WireConnection;25;0;28;0
WireConnection;25;1;26;0
WireConnection;47;0;52;0
WireConnection;27;0;60;0
WireConnection;27;1;25;0
WireConnection;45;0;44;0
WireConnection;45;1;47;0
WireConnection;45;2;53;0
WireConnection;6;0;4;0
WireConnection;46;0;45;0
WireConnection;46;1;54;0
WireConnection;57;0;6;0
WireConnection;57;1;55;0
WireConnection;49;0;6;0
WireConnection;49;1;50;0
WireConnection;30;0;27;0
WireConnection;58;0;57;0
WireConnection;58;1;30;0
WireConnection;58;2;33;0
WireConnection;51;0;49;0
WireConnection;51;1;46;0
WireConnection;12;0;6;0
WireConnection;12;1;13;0
WireConnection;23;0;12;0
WireConnection;23;1;58;0
WireConnection;23;2;51;0
WireConnection;24;0;23;0
WireConnection;7;0;36;0
WireConnection;7;3;24;0
WireConnection;38;0;37;0
WireConnection;38;1;7;0
WireConnection;0;0;38;0
ASEEND*/
//CHKSM=A4E674FDA1D7F0D9F99B4BB6A20D5973F9A7A40B