/*============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
============================================================================*/


package com.qualcomm.QCARUnityPlayer;

import android.app.Activity;
import android.content.Context;
import android.content.pm.ActivityInfo;
import android.content.res.Configuration;
import android.os.Build;
import android.util.DisplayMetrics;
import android.view.Display;
import android.view.Surface;
import android.view.WindowManager;

import com.qualcomm.QCAR.QCAR;

/* This class is responsible for initializing and deinitializting QCAR from Java.
*  The initQCAR and deinitQCAR methods are invoked from C# 
**/
public class QCARInitializer
{
    // Name of the native dynamic libraries to load:
    private static final String NATIVE_LIB_UNITYPLAYER = "QCARUnityPlayer";
    private static final String NATIVE_LIB_QCARWRAPPER = "QCARWrapper";
    private static final String NATIVE_LIB_QCAR = "Vuforia";
	
    /** Native platform initialization prior to QCAR initialization */
    private static native void initPlatformNative();
	
    /** Load native libraries stored in "libs/armeabi*" */
	public static void loadNativeLibraries()
	{
        loadLibrary(NATIVE_LIB_QCAR);
        loadLibrary(NATIVE_LIB_QCARWRAPPER);
        loadLibrary(NATIVE_LIB_UNITYPLAYER);
	}

	// initializes platform specific aspects of QCAR.
    public static void initPlatform()
    {				
        // Carry out native platform initialization:
        initPlatformNative();
    }
	
	
	// initializes QCAR. This is a blocking call that is invoked from c# - Unity will prevent an ANR.
    public static int initQCAR(Activity activity, String licenseKey)
    {
		DebugLog.LOGD("Initializing Vuforia...");
		
		// Always set GLES 2.0 - GLES 1.x is no longer supported.
		QCAR.setInitParameters(activity, QCAR.GL_20, licenseKey);
		
        int progressValue = -1;
		
		do
		{
			// QCAR.init() blocks until an initialization step is complete,
			// then it proceeds to the next step and reports progress in
			// percents (0 ... 100%)
			// If QCAR.init() returns -1, it indicates an error.
			// Initialization is done when progress has reached 100%.
			progressValue = QCAR.init();
			
			// We check whether the task has been canceled in the meantime
			// (by calling AsyncTask.cancel(true))
			// and bail out if it has, thus stopping this thread.
			// This is necessary as the AsyncTask will run to completion
			// regardless of the status of the component that started is.
		} while (progressValue >= 0 && progressValue < 100);
		
		if (progressValue < 0)
		{
			DebugLog.LOGE("QCAR initialization failed");
			
			return progressValue;
		}
		
		return 0;
    }

	
    /** A helper for loading native libraries stored in "libs/armeabi*". */
    private static boolean loadLibrary(String nLibName)
    {
        try
        {
            System.loadLibrary(nLibName);
            //DebugLog.LOGI("Native library lib" + nLibName + ".so loaded");
            return true;
        }
        catch (UnsatisfiedLinkError ulee)
        {
            DebugLog.LOGE("The library lib" + nLibName +
                            ".so could not be loaded: " + ulee.toString());
        }
        catch (SecurityException se)
        {
            DebugLog.LOGE("The library lib" + nLibName +
                            ".so was not allowed to be loaded");
        }
        
        return false;
    }
}