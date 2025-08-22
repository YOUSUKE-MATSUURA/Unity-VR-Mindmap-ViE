using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class StartTitle : MonoBehaviour
{
    
    //ボタンがクリックされたときに呼ばれる関数
    public void OnSelect()
    {
        SceneManager.LoadScene("UITestVR3");
    }
}
