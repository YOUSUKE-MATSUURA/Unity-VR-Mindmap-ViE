using System;
using UnityEngine;
using UnityEngine.InputSystem;

using static  UIExperimentVR.LibraryForVRTextbook;

namespace UIExperimentVR
{
    public class ActionFormCube : ActionToControl
    {
        [SerializeField] private GameObject cubePrefab;
        [SerializeField] private GameObject cubeCase;
        void Start()
        {
            if (!isReady) { return; }
        }
        
        protected override void OnActionPerformed(InputAction.CallbackContext ctx) => UpdateValue(ctx);

        //protected override void OnActionCanceled(InputAction.CallbackContext ctx) => UpdateValue(ctx);

        private void UpdateValue(InputAction.CallbackContext ctx)
        {
            var instantiateObject = Instantiate(cubePrefab, new Vector3(0, 2, 3), Quaternion.identity);
            instantiateObject.GetComponent<MeshRenderer>().enabled = true;
            instantiateObject.transform.parent = cubeCase.transform;
        }
    }
}