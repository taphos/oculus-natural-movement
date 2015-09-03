Shader "OVRNaturalMovement/Line" { 
	SubShader { 
		Pass { 
			Blend SrcAlpha OneMinusSrcAlpha 
			BindChannels { Bind "Color", color } 
			ZWrite Off 
			Cull Front 
			Fog { Mode Off } 
		} 
	} 
}