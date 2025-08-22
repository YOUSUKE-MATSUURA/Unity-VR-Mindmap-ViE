using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectBounce2 : MonoBehaviour
{
    public  float bounceHeight = 0.5f; //バウンドの高さ
    public float bounceSpeed = 2.0f;　//バウンドの速さ
    //public float scaleSpeed = 2.0f; //伸縮の速さ
    //public float scaleAmount = 0.2f; //伸縮の幅

    private Vector3 startPos;　//オブジェクトの初期位置
    //private Vector3 startScale; //オブジェクトの初期スケール
    //private Vector3 startColliderSize;
    void Start()
    {
        startPos = transform.localPosition;　//オブジェクトの初期位置を保存
        //startScale = transform.localScale; //オブジェクトの初期スケールを保存
        //startColliderSize = GetComponent<BoxCollider>().size;
    }

    void Update()
    {
        //sin関数を使ってオブジェクトを上下に伸び縮みさせる
        float y = startPos.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
        
        //sin関数を使ってオブジェクトを伸縮させる
        //float scaleY = Mathf.Abs(Mathf.Sin(Time.time * scaleSpeed)) * scaleAmount;
        //transform.localScale = new Vector3(startScale.x, startScale.y + 0.3f + scaleY, startScale.z);

        //float scaleX = (startScale.y + scaleY) / startScale.y;
        //startColliderSize =
            //new Vector3(startColliderSize.x * scaleX, startColliderSize.y, startColliderSize.z);
    }
}
