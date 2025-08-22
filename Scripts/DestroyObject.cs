using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static  UIExperimentVR.LibraryForVRTextbook;
using TMPro;
using Object = UnityEngine.Object;

namespace UIExperimentVR
{
    public class DestroyObject : ActionToControl
    {
        //private bool isStartPt = true;

        [SerializeField] private XRRayInteractor _rayInteractor;
        [SerializeField] private GameObject shotFx;

        //private Vector3 _startPt;
        //private Vector3 _endPt;
        private GameObject obj;
        private Transform spo;

        private bool isWaitingToshot;
        //private bool isPressed = false;
        //private string preHitObjName = "";
        //private List<Vector3> _positions = new List<Vector3>();
           
        RaycastHit hit;
        //Color c1 = new Color(255, 165, 0, 1);
        //private Color c2 = new Color(255, 165, 0, 1);
        void Start()
        {
             //----------------------------------------------------------------------
            if (_rayInteractor is null)
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

        //protected override void OnActionCanceled(InputAction.CallbackContext ctx) => UpdateValue(ctx);
        

        private void UpdateValue(InputAction.CallbackContext ctx)
        {
            if(isWaitingToshot)return;
            
            if (_rayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                //var spo = startCubePref.transform.position;
                spo = hit.transform;
                obj= hit.transform.gameObject;
                Instantiate(shotFx, spo);
                errorMessage = "Destroy!";
                displayMessage.text = errorMessage;
                isWaitingToshot = true;
                StartCoroutine(RemoveObject());
                //isStartPt = false;
            }
            // else if(!isStartPt == true)
            // {
            //     errorMessage = "";
            //     displayMessage.text = errorMessage;
            //     isStartPt = true;
            // }
            // else if(!isPressed)
            // {
            //     isStartPt = true;
            // }
        }

        IEnumerator RemoveObject()
        {
            yield return new WaitForSeconds(0.8f);
            Destroy(obj);
            errorMessage = "";
            displayMessage.text = errorMessage;
            isWaitingToshot = false;
        }
       


                   
                   

               //errorMessage += obj.name;
                //displayMessage.text = errorMessage;
          
            // else if(!isStartPt == true && _rayInteractor.TryGetCurrent3DRaycastHit(out hit) && hit.transform.name == preHitObjName)
            // {
            //     var spo = hit.transform.position;
            //     _positions.Add(spo);
            //     GameObject go = Instantiate(linePrefab);
            //     var lineRenderer = go.AddComponent<LineRenderer>();
            //     lineRenderer.positionCount = _positions.Count;
            //     lineRenderer.SetPositions(_positions.ToArray());
            //     lineRenderer.startWidth = 0.02f;
            //     lineRenderer.endWidth = 0.02f;
            //     lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            //     lineRenderer.SetColors(c1, c2);
            //     _positions.Clear();
            //     isStartPt = true;
            //     errorMessage += $"Hit end";
            //     displayMessage.text = errorMessage;
            // }
            // else if(!isPressed)
            // {
            //     _positions.Clear();
            //     isStartPt = true;
            //     preHitObjName = "";
            //     errorMessage += $"Hit reset";
            //     displayMessage.text = errorMessage;
            //
            // }
    }
}