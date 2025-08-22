using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace UIExperimentVR
{
    public class DrawLineWithXR : MonoBehaviour
    {
        [SerializeField] private XRRayInteractor _controller;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private LayerMask _layerMask;

        private void Update()
        {
            if (_controller)
            {
                RaycastHit hit;
                if (_controller.TryGetCurrent3DRaycastHit(out hit))
                {
                    _lineRenderer.positionCount = 2;
                    _lineRenderer.SetPosition(0,_controller.transform.position);
                    _lineRenderer.SetPosition(1,hit.point);
                }
                else
                {
                    _lineRenderer.positionCount = 0;
                }
            }
        }
    }
    
}

