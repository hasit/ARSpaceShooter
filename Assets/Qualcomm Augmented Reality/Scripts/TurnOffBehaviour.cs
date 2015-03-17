/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// A utility behaviour to disable rendering of a game object at run time.
    /// </summary>
    public class TurnOffBehaviour : TurnOffAbstractBehaviour
    {

        #region UNITY_MONOBEHAVIOUR_METHODS

        void Awake()
        {
            if (QCARRuntimeUtilities.IsQCAREnabled())
            {
                // We remove the mesh components at run-time only, but keep them for
                // visualization when running in the editor:
                MeshRenderer targetMeshRenderer = this.GetComponent<MeshRenderer>();
                Destroy(targetMeshRenderer);
                MeshFilter targetMesh = this.GetComponent<MeshFilter>();
                Destroy(targetMesh);
            }
        }

        #endregion // UNITY_MONOBEHAVIOUR_METHODS

    }
}
