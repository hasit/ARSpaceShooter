
/*==============================================================================
Copyright (c) 2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;
using UnityEditor;

namespace Vuforia.EditorClasses
{
    public class ExtensionImport : AssetPostprocessor
    {
        // This method is called by Unity whenever assets are updated (deleted,
        // moved or added)
        public static void OnPostprocessAllAssets(string[] importedAssets,
                                                  string[] deletedAssets,
                                                  string[] movedAssets,
                                                  string[] movedFromAssetPaths)
        {
            foreach (var importedAsset in importedAssets)
            {
                // if this script is imported, force the Android build settings to ARMv7 only, 
                // as Vuforia does not support native Android x86 and need to run in emulation mode on those devices.
                if (importedAsset.Equals("Assets/Editor/QCAR/Scripts/ExtensionImport.cs"))
                {
                    if (PlayerSettings.Android.targetDevice != AndroidTargetDevice.ARMv7)
                    {
                        Debug.Log("Setting Android target device to ARMv7");
                        PlayerSettings.Android.targetDevice = AndroidTargetDevice.ARMv7;
                    }
// TODO: replace this with something like #if INCLUDE_METAL once Unity adds this
// not sure if Unity 4.7+ will ever exist, but better safe than sorry
#if UNITY_5_0 || UNITY_4_9 || UNITY_4_8 || UNITY_4_7 || (UNITY_4_6 && !UNITY_4_6_1 && !UNITY_4_6_2)
                    // check if Graphics API for iOS is set to Metal or Universal
                    if ((PlayerSettings.targetIOSGraphics == TargetIOSGraphics.Automatic) ||
                        (PlayerSettings.targetIOSGraphics == TargetIOSGraphics.Metal))
                    {
                        Debug.Log("Setting iOS Graphics API to OpenGL ES 2.0, Vuforia does not support Metal yet.");
                        PlayerSettings.targetIOSGraphics = TargetIOSGraphics.OpenGLES_2_0;
                    
                        if (PlayerSettings.targetIOSGraphics != TargetIOSGraphics.OpenGLES_2_0)
                        {
                            Debug.LogWarning("Failed to set iOS Graphics API to OpenGL ES 2.0. Please make sure to set this manually in the "+
                                             "player settings, Vuforia does not support the Metal graphics API yet.");
                        }
                    }
#endif

#if INCLUDE_IL2CPP
#if UNITY_5_0
                    BuildTargetGroup buildTarget = BuildTargetGroup.iOS;
#else
                    BuildTargetGroup buildTarget = BuildTargetGroup.iPhone;
#endif
                    int scriptingBackend = PlayerSettings.GetPropertyInt("ScriptingBackend", buildTarget);
                    if (scriptingBackend != (int) ScriptingImplementation.IL2CPP)
                    {
                        Debug.Log("Setting iOS scripting backend to IL2CPP to enable 64bit support.");
                        PlayerSettings.SetPropertyInt("ScriptingBackend", (int)ScriptingImplementation.IL2CPP, buildTarget);
                    }
#endif
                }
            }
        }
    }

}
