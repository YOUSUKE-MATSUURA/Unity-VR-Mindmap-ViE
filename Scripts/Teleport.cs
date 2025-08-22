using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static  UIExperimentVR.LibraryForVRTextbook;

namespace UIExperimentVR
{
    public class Teleport : ActionToControl
    {
        [SerializeField] private GameObject cubeCase;
        void Start()
        {
            if (!isReady) { return; }
        }
        
        protected override void OnActionPerformed(InputAction.CallbackContext ctx) => UpdateValue(ctx);

        //protected override void OnActionCanceled(InputAction.CallbackContext ctx) => UpdateValue(ctx);

        private void UpdateValue(InputAction.CallbackContext ctx)
        {
            var grabs = cubeCase.GetComponentsInChildren<XRGrabInteractable>();
            foreach (var g in grabs)
            {
                g.enabled = false;
            }

            var teles = cubeCase.GetComponentsInChildren<TeleportationAnchor>();
            foreach (var t in teles)
            {
                t.enabled = true;
            }
        }
    }
}