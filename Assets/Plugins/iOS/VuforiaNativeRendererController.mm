/*============================================================================
Copyright (c) 2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
============================================================================*/
 
#import "UnityAppController.h"
#import "VuforiaRenderDelegate.h"


// Unity native rendering callback plugin mechanism is only supported 
// from version 4.5 onwards
#if UNITY_VERSION>434

// Exported methods for native rendering callback
extern "C" void UnitySetGraphicsDevice(void* device, int deviceType, int eventType);
extern "C" void UnityRenderEvent(int marker);

#endif

// Controller to support native rendering callback
@interface VuforiaNativeRendererController : UnityAppController
{
}
- (void)shouldAttachRenderDelegate;
@end

@implementation VuforiaNativeRendererController

- (void)shouldAttachRenderDelegate
{
	self.renderDelegate = [[VuforiaRenderDelegate alloc] init];
	
// Unity native rendering callback plugin mechanism is only supported 
// from version 4.5 onwards
#if UNITY_VERSION>434
	UnityRegisterRenderingPlugin(&UnitySetGraphicsDevice, &UnityRenderEvent);
#endif
}
@end


IMPL_APP_CONTROLLER_SUBCLASS(VuforiaNativeRendererController)