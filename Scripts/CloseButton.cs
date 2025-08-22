using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UIExperimentVR
{
    
    public class CloseButton : MonoBehaviour
    {
        [SerializeField] private GameObject keyButton;
        [SerializeField] private GameObject Del1;
        [SerializeField] private GameObject Del2;
        [SerializeField] private GameObject Del3;
        [SerializeField] private GameObject Del4;
        [SerializeField] private GameObject keyboard;
        [SerializeField] private GameObject canvases;
        [SerializeField] private GameObject closeButton;
        [SerializeField] private GameObject startButton;
        [SerializeField] private GameObject info;
        //ボタンがクリックされたときに呼ばれる関数
        public void OnSelect()
        {
            keyButton.SetActive(false);
            Del1.SetActive(false);
            Del2.SetActive(false);
            Del3.SetActive(false);
            Del4.SetActive(false);
            keyboard.SetActive(false);
            canvases.SetActive(false);
            closeButton.SetActive(false);
            startButton.SetActive(true);
            info.SetActive(false);
        }
    }
}