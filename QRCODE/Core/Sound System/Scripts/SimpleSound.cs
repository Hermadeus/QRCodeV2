using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor;
using Random = UnityEngine.Random;

namespace QRCode.Audio
{
    [CreateAssetMenu(menuName = "QRCode/Audio/Simple Sound")]
    public class SimpleSound : Sound
    {
        public override void PlaySound(AudioSource source, float delay = 0f)
        {
            var clip = source.clip = audioClip;
            var vol = source.volume = useRandomVolume ? Random.Range(rdmVolume.x, rdmVolume.y) : volume;
            source.pitch = useRandomPitch ? Random.Range(rdmPitch.x, rdmPitch.y) : pitch;
            source.outputAudioMixerGroup = outPut != null ? outPut : null;
            source.spatialBlend = spatialBlend;
            
            switch (soundType)
            {
                case SoundType.Play:
                    source.Play();
                    break;
                case SoundType.PlayDelayed:
                    source.PlayDelayed(delay);
                    break;
                case SoundType.PlayOneShot:
                    source.PlayOneShot(clip, vol);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
