/*==============================================================================
Copyright (c) 2013-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Qualcomm Connected Experiences, Inc.
==============================================================================*/

using UnityEngine;

namespace Vuforia
{
    /// <summary>
    /// This script renders the mesh from the MeshFilter as wireframe.
    /// This is mainly supposed to be used for visualization/debugging purpoes. It uses GL.LINES to draw the wireframe,
    /// which is not very fast for larger meshes.
    /// In order to draw large wireframe meshes in an app, it is recommended to use 3rd party libraries such as Vectrosity.
    /// </summary>
    public class WireframeBehaviour : MonoBehaviour
    {
        #region PRIVATE_MEMBERS

        private Material mLineMaterial;

        #endregion // PRIVATE_MEMBERS


        #region PUBLIC_MEMBERS

        public bool ShowLines = true;
        public Color LineColor = Color.green;
        
        #endregion // PUBLIC_MEMBERS


        #region PRIVATE_METHODS

        private void CreateLineMaterial() 
        {
            mLineMaterial = new Material("Shader \"Lines/Colored Blended\" {" +
                "SubShader {" +
                    "Pass { Color (" + LineColor.r + "," + LineColor.g + "," + LineColor.b + "," + LineColor.a + ") }" + 
                "} }" );
            mLineMaterial.hideFlags = HideFlags.HideAndDontSave;
            mLineMaterial.shader.hideFlags = HideFlags.HideAndDontSave; 
        }

        #endregion // PRIVATE_METHODS


        #region UNITY_MONOBEHAVIOUR_METHODS

        void OnRenderObject ()
        {
            // avoid lines being rendered in Background-camera
            GameObject go = QCARManager.Instance.ARCameraTransform.gameObject;
            Camera[] cameras = go.GetComponentsInChildren<Camera>();
            bool valid = false;
            foreach (Camera cam in cameras)
            {
                if(Camera.current == cam)
                    valid = true;
            }
            if(!valid)
                return;
        
            if (!ShowLines) return;

            var mf = GetComponent<MeshFilter>();
            if (!mf) return;

            if (mLineMaterial == null)
                CreateLineMaterial();

            var mesh = mf.sharedMesh;
            var vertices = mesh.vertices;
            var triangles = mesh.triangles;
    
            GL.PushMatrix();

            GL.MultMatrix(transform.localToWorldMatrix);

            mLineMaterial.SetPass(0);
            GL.Begin(GL.LINES); 
            for (int i=0; i<triangles.Length; i+=3) {

                var P0 = (vertices[triangles[i+0]]);
                var P1 = (vertices[triangles[i+1]]);
                var P2 = (vertices[triangles[i+2]]);
            
                GL.Vertex(P0);
                GL.Vertex(P1);
                GL.Vertex(P1);
                GL.Vertex(P2);
                GL.Vertex(P2);
                GL.Vertex(P0);
            }
    
            GL.End();
            GL.PopMatrix();
        }

        void OnDrawGizmos()
        {
            if (ShowLines && enabled)
            {

                var mf = GetComponent<MeshFilter>();
                if (!mf) return;

                Gizmos.matrix = Matrix4x4.TRS(gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.lossyScale);
                Gizmos.color = LineColor;

                var mesh = mf.sharedMesh;
                var vertices = mesh.vertices;
                var triangles = mesh.triangles;
                for (int i = 0; i < triangles.Length; i += 3)
                {

                    var P0 = (vertices[triangles[i + 0]]);
                    var P1 = (vertices[triangles[i + 1]]);
                    var P2 = (vertices[triangles[i + 2]]);

                    Gizmos.DrawLine(P0, P1);
                    Gizmos.DrawLine(P1, P2);
                    Gizmos.DrawLine(P2, P0);
                }
            }
        }

        #endregion // UNITY_MONOBEHAVIOUR_METHODS
    }
}
