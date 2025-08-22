using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UIExperimentVR
{
    
    public class StartButton : MonoBehaviour
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
        //ボタンがクリックされたときに呼ばれる関数
        public void OnSelect()
        {
            keyButton.SetActive(true);
            Del1.SetActive(true);
            Del2.SetActive(true);
            Del3.SetActive(true);
            Del4.SetActive(true);
            keyboard.SetActive(true);
            canvases.SetActive(true);
            closeButton.SetActive(true);
            startButton.SetActive(false);
        }
    }
}