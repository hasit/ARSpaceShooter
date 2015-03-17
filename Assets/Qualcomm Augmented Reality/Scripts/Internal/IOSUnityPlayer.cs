/*==============================================================================
Copyright (c) 2013-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// This class encapsulates functionality to detect various surface events
    /// (size, orientation changed) and delegate this to native.
    /// These are used by Unity Extension code and should usually not be called by app code.
    /// </summary>
    class IOSUnityPlayer : IUnityPlayer
    {
        private ScreenOrientation mScreenOrientation = ScreenOrientation.Unknown;
        private int mScreenWidth = 0;
        private int mScreenHeight = 0;

        /// <summary>
        /// Loads native plugin libraries on platforms where this is explicitly required.
        /// </summary>
        public void LoadNativeLibraries()
        {
        }

        /// <summary>
        /// Initialized platform specific settings
        /// </summary>
        public void InitializePlatform()
        {
            setPlatFormNative();
        }

        /// <summary>
        /// Initializes QCAR; called from Start
        /// </summary>
        public QCARUnity.InitError Start(string licenseKey)
        {
            int errorCode = initQCARiOS((int)Screen.orientation, licenseKey);
            if (errorCode >= 0)
                InitializeSurface();
            return (QCARUnity.InitError)errorCode;
        }

        /// <summary>
        /// Called from Update, checks for various life cycle events that need to be forwarded
        /// to QCAR, e.g. orientation changes
        /// </summary>
        public void Update()
        {
            if (SurfaceUtilities.HasSurfaceBeenRecreated())
            {
                InitializeSurface();
            }
            else
            {
                // if Unity reports that the orientation has changed, set it correctly in native
                if (Screen.orientation != mScreenOrientation)
                    SetUnityScreenOrientation();

                if (mScreenWidth != Screen.width || mScreenHeight != Screen.height)
                {
                    mScreenWidth = Screen.width;
                    mScreenHeight = Screen.height;
                    SurfaceUtilities.OnSurfaceChanged(mScreenWidth, mScreenHeight);
                }
            }

        }

        public void Dispose()
        {
        }

        /// <summary>
        /// Pauses QCAR
        /// </summary>
        public void OnPause()
        {
            QCARUnity.OnPause();
        }

        /// <summary>
        /// Resumes QCAR
        /// </summary>
        public void OnResume()
        {
            QCARUnity.OnResume();
        }

        /// <summary>
        /// Deinitializes QCAR
        /// </summary>
        public void OnDestroy()
        {
            QCARUnity.Deinit();
        }


        private void InitializeSurface()
        {
            SurfaceUtilities.OnSurfaceCreated();

            SetUnityScreenOrientation();

            mScreenWidth = Screen.width;
            mScreenHeight = Screen.height;
            SurfaceUtilities.OnSurfaceChanged(mScreenWidth, mScreenHeight);
        }

        private void SetUnityScreenOrientation()
        {
            mScreenOrientation = Screen.orientation;
            SurfaceUtilities.SetSurfaceOrientation(mScreenOrientation);
            // set the native orientation (only required on iOS)
            setSurfaceOrientationiOS((int) mScreenOrientation);
        }

        [DllImport("__Internal")]
        private static extern void setPlatFormNative();

        [DllImport("__Internal")]
        private static extern int initQCARiOS(int screenOrientation, string licenseKey);

        [DllImport("__Internal")]
        private static extern void setSurfaceOrientationiOS(int screenOrientation);
    }
}
