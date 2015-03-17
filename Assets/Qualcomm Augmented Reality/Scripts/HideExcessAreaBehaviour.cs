/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;
using System.Collections;

namespace Vuforia
{
    /// <summary>
    /// This Behaviour creates four planes (mattes) at the near clipping plane of camera frustum 
    /// to hide the augmentation going off the limit of video background due to scaling
    /// </summary>
    public class HideExcessAreaBehaviour : HideExcessAreaAbstractBehaviour
    {
    }
}
