using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject cubeCase;
    [SerializeField] private Material[] _materials = new Material[1];
    [SerializeField] private GameObject sponArea;
    private int count;
    [SerializeField] private GameObject bordInput;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject keyButton;
    [SerializeField] private GameObject Del1;
    [SerializeField] private GameObject Del2;
    [SerializeField] private GameObject Del3;
    [SerializeField] private GameObject Del4;
    [SerializeField] private GameObject canvases;
    [SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject info;



    //ボタンがクリックされたときに呼ばれる関数
    public void OnSelect()
    {
        Transform spawnArea = sponArea.GetComponent<Transform>();
        var instantiateObject = Instantiate(cubePrefab, spawnArea.position, Quaternion.identity);
        instantiateObject.GetComponent<MeshRenderer>().enabled = true;
        instantiateObject.GetComponentInChildren<Canvas>().enabled = true;
        instantiateObject.GetComponent<MeshRenderer>().material = _materials[count];
        TMP_InputField inputField = bordInput.GetComponentInChildren<TMP_InputField>();
        instantiateObject.GetComponentInChildren<TMP_InputField>().text = inputField.text;
        instantiateObject.transform.parent = cubeCase.transform;
        bordInput.GetComponentInChildren<TMP_InputField>().text = "";
        
        keyButton.SetActive(false);
        Del1.SetActive(false);
        Del2.SetActive(false);
        Del3.SetActive(false);
        Del4.SetActive(false);
        canvases.SetActive(false);
        closeButton.SetActive(false);
        startButton.SetActive(true);
        info.SetActive(false);
    }
}
