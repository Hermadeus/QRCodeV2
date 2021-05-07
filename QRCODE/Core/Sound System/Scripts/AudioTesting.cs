using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QRCode.Audio;
using QRCode.Extensions;
using UnityEngine;

namespace MyNamespace
{
    public class AudioTesting : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private Sound _sound;
        
        private void Update()
        {
            transform.position += new Vector3(.1f * Time.deltaTime, 0f, 0f);
            
            if (Input.GetKeyDown(KeyCode.S))
            {
                //Audio.PlaySound("Son Test Simple", transform);
                //_sound.outPut.SetMasterVolume(0f, 1f, Ease.Linear);
                //_sound.PlaySound(_source);
            }
        }
    }
}
