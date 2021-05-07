using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QRCode.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioController : MonoBehaviour
    {
        public AudioSource audioSource = default;

        public bool isCurrentlyUsed = false;

        private Transform target = null;

        public void Init()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void ResetUsability(Transform target = null)
        {
            StartCoroutine(ResetUsabilityIE(target));
        }

        IEnumerator ResetUsabilityIE(Transform target = null)
        {
            this.target = target;
            while (audioSource.isPlaying)
            {
                transform.position = target != null ? target.position : transform.position;
                yield return null;
            }
            name = "AudioController [Unused]";
            isCurrentlyUsed = false;
        }

        public void Stop()
        {
            audioSource.Stop();
        }
    }
}
