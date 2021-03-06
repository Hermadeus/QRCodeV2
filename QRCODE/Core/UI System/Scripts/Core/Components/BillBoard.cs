using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace QRCode.UI
{
    public class BillBoard : MonoBehaviour
    {
        [SerializeField, BoxGroup("References")]
        private Camera m_Camera = default;

        [SerializeField, BoxGroup("Settings")]
        private bool useMainCamera = true;

        public Camera Camera
        {
            get => m_Camera;
            set => m_Camera = value;
        }
        
        private void Start()
        {
            if (useMainCamera)
            {
                var cam = Camera.main;
                m_Camera = cam;
            }
        }

        private void LateUpdate()
        {
            var rotation = Camera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward,
                rotation * Vector3.up
                );
        }
    }
}
