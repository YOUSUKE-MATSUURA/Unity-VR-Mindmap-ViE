using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UIExperimentVR
{
    
    public class LineRemoveButton : MonoBehaviour
    {
        //---------------------------------------------------------------
        [SerializeField] protected TextMeshProUGUI displayMessage;
        //----------------------------------------------------------------
        [SerializeField] private GameObject LineCase;
        private bool isStartPt = true;
        private bool isPressed = false;
        private bool isWaitingToShot;
        //----------------------------------------------------------------------
        protected string errorMessage;
        //----------------------------------------------------------------------
        
        //ボタンがクリックされたときに呼ばれる関数
        public void OnSelect()
        {
            if(isWaitingToShot)return;
            
            if (isStartPt == true)
            {
                foreach (Transform child in LineCase.transform)
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
            
            IEnumerator RemoveLine()
            {
                yield return new WaitForSeconds(1.5f);
                errorMessage = "";
                displayMessage.text = errorMessage;
                isWaitingToShot = false;
            }
        }
    }
}