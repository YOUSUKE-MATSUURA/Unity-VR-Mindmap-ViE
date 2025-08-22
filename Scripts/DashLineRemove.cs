using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

using static  UIExperimentVR.LibraryForVRTextbook;

namespace UIExperimentVR
{
    public class DashLineRemove : ActionToControl
    {
        [SerializeField] private GameObject dashLineCase;
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
                foreach (Transform child in dashLineCase.transform)
                {
                    GameObject.Destroy(child.gameObject);
                    errorMessage = "erased dashed line";
                    displayMessage.text = errorMessage;
                    isWaitingToShot = true;
                    StartCoroutine(RemoveDash());
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

        IEnumerator RemoveDash()
        {
            yield return new WaitForSeconds(1.5f);
            errorMessage = "";
            displayMessage.text = errorMessage;
            isWaitingToShot = false;
        }
    }
}