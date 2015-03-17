/*============================================================================
Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Qualcomm Confidential and Proprietary
============================================================================*/


#ifdef __cplusplus
extern "C"
{
#endif
   
   int getRotationFlag(int screenOrientation);
   void setPlatFormNative();
   int initQCARiOS(int ScreenOrientation, const char* licenseKey);
   void setSurfaceOrientationiOS(int orientation);
    
#ifdef __cplusplus
}
#endif