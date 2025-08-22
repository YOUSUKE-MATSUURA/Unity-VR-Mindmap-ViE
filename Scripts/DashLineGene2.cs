using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static  UIExperimentVR.LibraryForVRTextbook;
using TMPro;

namespace UIExperimentVR
{
    public class DashLineGene2 : ActionToControl
    {
        private bool isStartPt = true;

        [SerializeField] private XRRayInteractor _rayInteractor;
        [SerializeField] private GameObject linePrefab;
        [SerializeField] private GameObject dashLineCase;
        
        private bool isPressed = false;
        private string preHitObjName = "";
        private string cubeObjectName = "Teleportation Anchor(Clone)";
        private string sphereObjectName = "Teleportation Anchor(Sphere)(Clone)";
        private List<Vector3> _positions = new List<Vector3>();
        RaycastHit hit;
        //Color c1 = Color.gray;
        //private Color c2 = Color.gray;
        void Start()
        {
             //----------------------------------------------------------------------
            if (_rayInteractor is null || linePrefab is null)
            {
                 isReady = false; errorMessage += "NoCube";
             } 
             if (!isReady) 
             {
                 displayMessage.text = $"{GetSourceFileName()}\r\nError: {errorMessage}";
                 return; 
             }
            //----------------------------------------------------------------------
        }
        
        protected override void OnActionPerformed(InputAction.CallbackContext ctx) => UpdateValue(ctx);

        protected override void OnActionCanceled(InputAction.CallbackContext ctx) => UpdateValue(ctx);

        private void UpdateValue(InputAction.CallbackContext ctx)
        {
            if(isStartPt == true && _rayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                _positions.Add(hit.transform.position);
                isStartPt = false;
                preHitObjName = hit.transform.name;
                errorMessage = "dashed line(red)";
                displayMessage.text = errorMessage;
            }
            else if(!isStartPt == true && _rayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                var spo = hit.transform.position;
                _positions.Add(spo);
                GameObject go = Instantiate(linePrefab);
                go.transform.parent = dashLineCase.transform;
                var lineRenderer = go.GetComponent<LineRenderer>();
                lineRenderer.enabled = true;
                lineRenderer.positionCount = _positions.Count;
                lineRenderer.SetPositions(_positions.ToArray());
                //lineRenderer.startWidth = 0.04f;
                //lineRenderer.endWidth = 0.04f;
                //lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
                //lineRenderer.SetColors(c1, c2);
                _positions.Clear();
                isStartPt = true;
                errorMessage = "";
                displayMessage.text = errorMessage;
                errorMessage = "";
                displayMessage.text = errorMessage;
            }
            else if(!isPressed)
            {
                _positions.Clear();
                isStartPt = true;
                preHitObjName = "";
                errorMessage = "";
                displayMessage.text = errorMessage;

            }
        }
    }
}