Shader "ProjectP/Avatar"
{
    Properties
    {
        [Enum(Off,2,On,0)] _DoubleSided("Double Sided", int) = 2
    	
    	[Toggle(NOKEWO)] _EnableTextureTransparent("Enable Transparent", Float) = 0
    	
    	[Toggle(NOKEWO)] _FlipNormal("Flip Normal", Float) = 0
        
        _MainTex ("Texture", 2D) = "white" {}
        [HDR] _MainColor ("Main Color", Color) = (0.6886792,0.6886792,0.6886792,1)
        
        [HDR] _HighlightColor ("Highlight Color", Color) = (1,1,1,1)
        _HighlightColorPower ("Highlight Color Power", Float ) = 1
        
        [Toggle(NOKEWO)] _SelfShadowAtViewDirection ("Self Shadow At View Direction", Float) = 1
        
        _SelfShadowThreshold ("Self Shadow Threshold", Range(0, 1)) = 0.85
        _SelfShadowHardness ("Self Shadow Hardness", Range(0, 1)) = 1
    	
        [Toggle(NOKEWO)] _VertexColorGreenControlSelfShadowThreshold ("Vertex Color Green Control Self Shadow Threshold", Float ) = 0
        [HDR] _SelfShadowColor ("Self Shadow Color", Color) = (1,1,1,1)
        _SelfShadowColorPower ("Self Shadow Color Power", Float ) = 1
    	
    	_DirectionalLightIntensity ("Directional Light Intensity", Float ) = 0
    	
    	[Toggle(NOKEWO)] _UseSpecular ("Use Specular", Float) = 0
    	_SpecularPow ("Specular Pow", Float) = 80
    	[HDR] _SpecularColor ("Specular Color", Color) = (1,1,1,1)
    	
    	[Toggle(NOKEWO)] _UseReflect ("Use Reflect", Float) = 0
    	_ReflectIntensity ("Reflect Intensity", Float) = 0.5
        
        _RefVal ("ID", int ) = 0
    	[Enum(UnityEngine.Rendering.CompareFunction)] _Compa("Comp", int) = 4
        [Enum(UnityEngine.Rendering.StencilOp)] _PassOper("Pass Oper", int) = 0
    	[Enum(UnityEngine.Rendering.StencilOp)] _FailOper("Fail Oper", int) = 0
    	
    	[Toggle(USE_HAIR_GLOSS_ON)] _USE_HAIR_GLOSS_ON ("Use Hair Gloss?", Float ) = 0
	    _GlossIntensity ("Hair Gloss Intensity", Range(0, 1)) = 0.5
        [HDR] _GlossColor ("Hair Gloss Color", Color) = (1,1,1,1)
        _GlossColorPower ("Hair Gloss Color Power", Float ) = 0.5
        _GlossTexture ("Hair Gloss Texture", 2D) = "black" {}
        _GlossTextureSoftness ("Hair Gloss Softness", Float ) = 5
        _GlossTextureRotate ("Hair Gloss Rotate", Float ) = 0.07
        [Toggle(NOKEWO)] _GlossTextureFollowObjectRotation ("Hair Gloss Follow Object Rotation", Float ) = 0
        _GlossTextureFollowLight ("Hair Gloss Follow Light", Range(0, 1)) = 1
    }
    
    SubShader
    {
    	Pass 
    	{
		    Name "FORWARD"
		    Tags 
		    {
		        "LightMode"="ForwardBase"
		    }
		    
		    Cull [_DoubleSided]
		    
		    Stencil 
		    {
		        Ref[_RefVal]
		        Comp [_Compa]
		        Pass [_PassOper]
		        Fail [_FailOper]
		    }
		    
		    CGPROGRAM
		    #pragma vertex vert
		    #pragma fragment frag
		    #include "UnityCG.cginc"
		    #include "AutoLight.cginc"
            #include "Lighting.cginc"
		    #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog

			#pragma multi_compile_instancing

            #pragma only_renderers d3d9 d3d11 vulkan glcore gles3 gles metal xboxone ps4 wiiu switch
            #pragma target 3.0

		    #pragma shader_feature USE_HAIR_GLOSS_ON
		    
		    uniform sampler2D _MainTex;
		    uniform float4 _MainTex_ST;
			uniform half4 _MainColor;
		    
		    uniform half4 _HighlightColor;
		    uniform half _HighlightColorPower;

		    uniform half4 _OverallShadowColor;
		    uniform half _OverallShadowColorPower;

		    uniform fixed _UseSpecular;
		    uniform half _SpecularPow;
		    uniform half4 _SpecularColor;

		    uniform fixed _UseReflect;
		    uniform half _ReflectIntensity;

			uniform fixed _SelfShadowAtViewDirection;

		    uniform half _SelfShadowThreshold;
			uniform half _SelfShadowHardness;
			uniform fixed _VertexColorGreenControlSelfShadowThreshold;

		    uniform half4 _SelfShadowColor;
		    uniform half _SelfShadowColorPower;

		    uniform half _DirectionalLightIntensity;

		    uniform fixed _EnableTextureTransparent;

		    uniform fixed _FlipNormal;
		    
		    #if USE_HAIR_GLOSS_ON
		    	uniform half _GlossIntensity;
				uniform half4 _GlossColor;
				uniform half _GlossColorPower;
				uniform sampler2D _GlossTexture;
		        uniform float4 _GlossTexture_ST;
				uniform half _GlossTextureSoftness;
				uniform half _GlossTextureRotate;
				uniform fixed _GlossTextureFollowObjectRotation;
				uniform half _GlossTextureFollowLight;
			#endif

		    struct VertexInput
			{
		        float4 vertex : POSITION;
		        float3 normal : NORMAL;
		        float4 tangent : TANGENT;
		        float2 texcoord0 : TEXCOORD0;
		        float4 vertexColor : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
		    };

		    struct VertexOutput
			{
		        float4 pos : SV_POSITION;
		        float2 uv0 : TEXCOORD0;
		        float4 posWorld : TEXCOORD1;
		        float3 normalDir : TEXCOORD2;
		        float3 tangentDir : TEXCOORD3;
		        float3 bitangentDir : TEXCOORD4;
		        float4 vertexColor : COLOR;
		        UNITY_FOG_COORDS(5)
				UNITY_VERTEX_OUTPUT_STEREO
		    };
		    
		    VertexOutput vert (VertexInput v) 
			{
		        VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_OUTPUT(VertexOutput,o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

		        o.uv0 = v.texcoord0;
		        o.vertexColor = v.vertexColor;

		        o.normalDir = UnityObjectToWorldNormal(v.normal);
		        o.tangentDir = normalize(mul(unity_ObjectToWorld, float4(v.tangent.xyz, 0.0)).xyz);
		        o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
		    	
		        o.posWorld = mul(unity_ObjectToWorld, v.vertex);

		        o.pos = UnityObjectToClipPos(v.vertex);

		        UNITY_TRANSFER_FOG(o,o.pos);

		        return o;
		    }
		    
		    float4 frag(VertexOutput i) : COLOR 
			{
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

				float3 normalLocal = lerp(half3(0,0,1), -half3(0,0,1), _FlipNormal);
				
				float3 nor = normalize(i.normalDir);
				i.normalDir = lerp(nor, -nor, _FlipNormal);
		        float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
		        float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
		        float3 normalDirection = normalize(mul(normalLocal, tangentTransform));
				
				half4 mainTex = tex2D(_MainTex, TRANSFORM_TEX(i.uv0, _MainTex));
				clip(lerp( 1.0, mainTex.a, _EnableTextureTransparent) - 0.5);

				#ifndef UNITY_COLORSPACE_GAMMA
				_MainColor = float4(GammaToLinearSpace(_MainColor.rgb), _MainColor.a);
				#endif
				
				half3 resultMainTexColor = mainTex.rgb * _MainColor.rgb;
				
				float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				
				#ifndef UNITY_COLORSPACE_GAMMA
				_OverallShadowColor = float4(GammaToLinearSpace(_OverallShadowColor.rgb), _OverallShadowColor.a);
				#endif

				#ifndef UNITY_COLORSPACE_GAMMA
				_HighlightColor = float4(GammaToLinearSpace(_HighlightColor.rgb), _HighlightColor.a);
				#endif
				
				half3 resultHighlightColor = _HighlightColor.rgb * _HighlightColorPower + _DirectionalLightIntensity;
				
				half resultGrayscale = saturate(dot(_LightColor0.rgb, float3(0.3, 0.59, 0.11)));

				//월드 라이트의 방향을 쓸지 뷰 방향을 쓸지
				half3 selfShadowDirection = lerp(lightDirection, viewDirection, _SelfShadowAtViewDirection);
				
				half resultNdotL = 0.5 * dot(selfShadowDirection, normalDirection) + 0.5;
				
				//정점의 G채널로 그림자 컨트롤
				half resultSelfShadowVertexG = lerp(_SelfShadowThreshold, _SelfShadowThreshold * (1.0 - i.vertexColor.g),
												_VertexColorGreenControlSelfShadowThreshold);
				
				half resultSelfShadow =  smoothstep(_SelfShadowHardness, 1,
					resultNdotL * lerp(2, resultSelfShadowVertexG, _SelfShadowThreshold));
				
				#ifndef UNITY_COLORSPACE_GAMMA
				_SelfShadowColor = float4(GammaToLinearSpace(_SelfShadowColor.rgb),
														_SelfShadowColor.a);
				#endif
				
				#ifndef UNITY_COLORSPACE_GAMMA
				_SpecularColor = float4(GammaToLinearSpace(_SpecularColor.rgb), _SpecularColor.a);
				#endif
				
				half specularIntensity = pow(resultNdotL, _SpecularPow);
				specularIntensity = lerp(0, specularIntensity, _UseSpecular);
				half3 specular = specularIntensity * _SpecularColor.rgb * _LightColor0.rgb * mainTex.a;
				
				half3 worldRefl = reflect(-viewDirection, normalDirection);
				half4 skyData  = UNITY_SAMPLE_TEXCUBE(unity_SpecCube0, worldRefl);
				half3 reflected = DecodeHDR(skyData, unity_SpecCube0_HDR);
				reflected = lerp(0, reflected * _LightColor0.rgb * mainTex.a * _ReflectIntensity, _UseReflect);

				#if USE_HAIR_GLOSS_ON
				#ifndef UNITY_COLORSPACE_GAMMA
				_GlossColor = float4(GammaToLinearSpace(_GlossColor.rgb), _GlossColor.a);
				#endif
				float3 halfDirection = normalize(viewDirection + lightDirection);
				float glossRotX = cos(1.0 * _GlossTextureRotate);
				float glossRotY = sin(1.0 * _GlossTextureRotate);
				
				float2 glossPivot = float2(0.5, 0.5);
				
				float3 resultGlossDirection = lerp(viewDirection, halfDirection, _GlossTextureFollowLight);
				half3 resultReflect = reflect(resultGlossDirection, normalDirection);

				half3 followReflect = lerp(resultReflect, mul(unity_WorldToObject, float4(resultReflect, 0)).xyz,
											_GlossTextureFollowObjectRotation);

				half2 resultGlossPosition = (mul(float2((-1 * followReflect.r),followReflect.g) -
					glossPivot, float2x2(glossRotX, -glossRotY, glossRotY, glossRotX)) + glossPivot);
				resultGlossPosition = (resultGlossPosition * 0.5 + 0.5);
				
				half4 glossTex =
					tex2Dlod(_GlossTexture, float4(TRANSFORM_TEX(resultGlossPosition, _GlossTexture),
						0.0, _GlossTextureSoftness));

				half3 zeroVec = half3(0.0, 0.0, 0.0);
				half3 glossPower = lerp(zeroVec, glossTex.r, _GlossIntensity);
				half3 resultGloss = lerp(zeroVec, lerp(zeroVec, (_GlossColor.rgb * _GlossColorPower), glossPower), mainTex.a);

				#endif

				half3 resultShadow = _SelfShadowColor.rgb * _SelfShadowColorPower
										* resultGrayscale;
				#if USE_HAIR_GLOSS_ON
				float3 finalColor = resultMainTexColor *
						lerp(resultShadow,
							lerp(mainTex.rgb + (mainTex.rgb * resultHighlightColor * _LightColor0.rgb * 0.3),
								resultHighlightColor * _LightColor0.rgb, mainTex.a),
						resultSelfShadow);
				
				finalColor = finalColor + resultGloss;
				#else
				float3 finalColor = resultMainTexColor *
					lerp(resultShadow, resultHighlightColor * _LightColor0.rgb, resultSelfShadow);
				finalColor = finalColor + specular + reflected;
				#endif
				
                fixed4 finalRGBA = fixed4(finalColor, 1);
				
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
			}
		    
			ENDCG
		}

		Pass 
		{
            Name "ShadowCaster"
            Tags 
            {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull [_DoubleSided]

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog

			#pragma multi_compile_instancing

            #pragma only_renderers d3d9 d3d11 vulkan glcore gles3 gles metal xboxone ps4 wiiu switch
            #pragma target 3.0

            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform fixed _EnableTextureTransparent;

            struct VertexInput 
			{
                float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct VertexOutput 
			{
                V2F_SHADOW_CASTER;
				UNITY_VERTEX_OUTPUT_STEREO
            };

            VertexOutput vert (VertexInput v) 
			{
                VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_OUTPUT(VertexOutput,o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
            	
                o.pos = UnityObjectToClipPos(v.vertex);
            	
				TRANSFER_SHADOW_CASTER(o)
                return o;
            }

            float4 frag(VertexOutput i) : COLOR 
			{
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				
                SHADOW_CASTER_FRAGMENT(i)
            }

            ENDCG
        }
    }
}