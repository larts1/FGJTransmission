Shader "AlpacaSound/RetroPixelUnlimited" 
{
	Properties
	{
	 	_MainTex ("", 2D) = "white" {}
	}
	 
	SubShader
	{
		Lighting Off
		ZTest Always
		Cull Off
		ZWrite Off
		Fog { Mode Off }
	 
	 	Pass
	 	{
	  		CGPROGRAM
	  		#pragma exclude_renderers flash
	  		#pragma vertex vert_img
	  		#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
	  		#include "UnityCG.cginc"
	    
			uniform fixed4 _Colors[256];
			uniform int _ColorCount;

	  		uniform sampler2D _MainTex;
	    
	  		fixed4 frag (v2f_img i) : COLOR
	  		{
	   			fixed3 original = tex2D (_MainTex, i.uv).rgb;

				fixed dist = 0.0f;
				fixed minDist = 10.0f;
				fixed4 color = fixed4(0, 0, 0, 0);

				for (int i = 0; i < _ColorCount; i++)
				{
					dist = distance(original, _Colors[i].rgb);
					if (dist < minDist) 
					{
						minDist = dist;
						color = _Colors[i];
					}
				}

				return color;
	  		}
	  		
	  		ENDCG
	 	}
	}
	
	FallBack "Diffuse"
}
