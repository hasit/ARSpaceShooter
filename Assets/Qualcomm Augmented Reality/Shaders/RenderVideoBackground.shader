//Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc.
//All Rights Reserved.
//Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
Shader "Custom/RenderVideoBackground" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
        Tags {"Queue"="overlay+1" "RenderType"="overlay" }
        Pass {
            // Render the teapot
            SetTexture [_MainTex] {
                combine texture 
            }
        }
    } 
    FallBack "Diffuse"
}
