using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace QRCode.Audio
{
    [CreateAssetMenu(menuName = "QRCode/Audio/Multiple Sound")]
    public class MultipleSound : Sound
    {
        [BoxGroup("References"), InlineButton("ClearPool")]
        public List<AudioClip> clips = new List<AudioClip>();

        [BoxGroup("References"), Sirenix.OdinInspector.ReadOnly, ShowInInspector] 
        private List<AudioClip> pool = new List<AudioClip>();

        [FoldoutGroup("Settings"), EnumPaging]
        public MultipleSoundType multipleSoundType = MultipleSoundType.Random;
        
        public override void PlaySound(AudioSource source, float delay = 0f)
        {
            AudioClip clip = null;
            
            switch (multipleSoundType)
            {
                case MultipleSoundType.Random:
                    clip = clips[Random.Range(0, clips.Count - 1)];
                    break;
                case MultipleSoundType.Pooling:
                    if (clips.Count == 0)
                    {
                        clips = new List<AudioClip>(pool);
                        pool.Clear();
                    }

                    clip = clips[0];
                    clips.RemoveAt(0);
                    pool.Add(clip);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            var vol = source.volume = useRandomVolume ? Random.Range(rdmVolume.x, rdmVolume.y) : volume;
            source.pitch = useRandomPitch ? Random.Range(rdmPitch.x, rdmPitch.y) : pitch;
            source.outputAudioMixerGroup = outPut != null ? outPut : null;
            source.spatialBlend = spatialBlend;
            source.clip = clip;
            
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

        public void ClearPool()
        {
            if(pool == null || pool.Count == 0)
                return;
            
            foreach (var clip in pool)
            {
                clips.Add(clip);
            }
            pool.Clear();
        }
    }

    public enum MultipleSoundType
    {
        /// <summary>
        /// Get a random sound in the list.
        /// </summary>
        Random,
        /// <summary>
        /// Pool a sound in the list and re inject all sounds when the list is empty.
        /// </summary>
        Pooling,
    }
}
