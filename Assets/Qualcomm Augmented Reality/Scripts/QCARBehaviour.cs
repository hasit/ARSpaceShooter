/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// The QCARBehaviour class handles tracking and triggers native video
    /// background rendering. The class updates all Trackables in the scene.
    /// </summary>
    public class QCARBehaviour : QCARAbstractBehaviour
    {
        protected void Awake()
        {
            IUnityPlayer unityPlayer = new NullUnityPlayer();

            // instantiate the correct UnityPlayer for the current platform
            if (Application.platform == RuntimePlatform.Android)
                unityPlayer = new AndroidUnityPlayer();
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                unityPlayer = new IOSUnityPlayer();
            else if (QCARRuntimeUtilities.IsPlayMode())
                unityPlayer = new PlayModeUnityPlayer();

            SetUnityPlayerImplementation(unityPlayer);

            gameObject.AddComponent<ComponentFactoryStarterBehaviour>();
        }
    }
}
