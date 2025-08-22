using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIExperimentVR
{
    public class BillBord : MonoBehaviour
    {
        void Update()
        {
            var c = Camera.main.transform.position;
            var p = transform.position;
            transform.LookAt(2 * p - c);
        }
    }
}
