using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisLocker : MonoBehaviour
{
    [System.Serializable]
    private enum AxisLockType
    {
        Nothing,
        Horizontal,
        //Vertical
    }

    [SerializeField, Tooltip("固定向き")] private AxisLockType axisLock;

    ///<summary>
    ///定期実行
    /// </summary>
    private void LateUpdate()
    {
        switch (axisLock)
        {
            case AxisLockType.Nothing:
                break;
            case AxisLockType.Horizontal:
                //オブジェクトを水平に維持する
                this.transform.rotation = Quaternion.Euler(0.0f, this.transform.rotation.eulerAngles.y, 0.0f);
                break;
        }
    }
}
