using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

using static  UIExperimentVR.LibraryForVRTextbook;

namespace UIExperimentVR
{
    public class LineRemove : ActionToControl
    {
        [SerializeField] private GameObject lineCase;
        private bool isStartPt = true;
        private bool isPressed = false;
        private bool isWaitingToShot;
        void Start()
        {
            if (!isReady) { return; }
        }
        
        protected override void OnActionPerformed(InputAction.CallbackContext ctx) => UpdateValue(ctx);

        protected override void OnActionCanceled(InputAction.CallbackContext ctx) => UpdateValue(ctx);

        private void UpdateValue(InputAction.CallbackContext ctx)
        {
            if(isWaitingToShot)return;
            
            if (isStartPt == true)
            {
                foreach (Transform child in lineCase.transform)
                {
                    GameObject.Destroy(child.gameObject);
                    errorMessage = "erased the line";
                    displayMessage.text = errorMessage;
                    isWaitingToShot = true;
                    StartCoroutine(RemoveLine());
                    isStartPt = false;
                }
            }
            else if(!isStartPt == true)
            {
                errorMessage = "";
                displayMessage.text = errorMessage;
                isStartPt = true;
            }
            else if(!isPressed)
            {
                isStartPt = true;
            }

        }
        
        IEnumerator RemoveLine()
        {
            yield return new WaitForSeconds(1.5f);
            errorMessage = "";
            displayMessage.text = errorMessage;
            isWaitingToShot = false;
        }
        
        
    }
}