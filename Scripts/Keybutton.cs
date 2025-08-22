using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UIExperimentVR
{
    
    public class Keybutton : MonoBehaviour
    {
        [SerializeField] private GameObject keyboard;

        [SerializeField] private GameObject info;
        //ボタンがクリックされたときに呼ばれる関数
        public void OnSelect()
        {
            keyboard.SetActive(true);
            info.SetActive(false);
        }
    }
}

