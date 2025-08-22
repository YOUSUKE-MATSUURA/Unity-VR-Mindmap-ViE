using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UIExperimentVR
{
    
    public class Output : MonoBehaviour
    {
        [SerializeField] private GameObject info;

        [SerializeField] private GameObject keyBoard;
        //ボタンがクリックされたときに呼ばれる関数
        public void OnSelect()
        {
            info.SetActive(true);
            keyBoard.SetActive(false);
        }
    }
}