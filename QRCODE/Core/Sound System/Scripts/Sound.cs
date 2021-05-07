using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace QRCode.Audio
{
    public abstract class Sound : SerializedScriptableObject
    {
        [BoxGroup("References"), InlineEditor(InlineEditorModes.SmallPreview)]
        public AudioClip audioClip = default;
        
        [BoxGroup("References"), EnumPaging] 
        public SoundName soundName = SoundName.NULL_Name;

        [InlineEditor(InlineEditorModes.GUIOnly), BoxGroup("References"), EnumPaging] 
        public AudioMixerGroup outPut = null;
        
        [FoldoutGroup("Settings"), EnumPaging] 
        public SoundType soundType = SoundType.Play;

        [FoldoutGroup("Settings"), ShowIf("@useRandomVolume"), MinMaxSlider(0f, 1f, true)]
        public Vector2 rdmVolume = new Vector2(1f, 1f);

        [FoldoutGroup("Settings"), ShowIf("@!useRandomVolume"), Range(0f,1f)]
        public float volume = 1f;

        [FoldoutGroup("Settings"), ShowIf("@useRandomPitch"), MinMaxSlider(-3f, 3f, true)]
        public Vector2 rdmPitch = new Vector2(1f,1f);

        [FoldoutGroup("Settings"), ShowIf("@!useRandomPitch"), Range(0f,1f)]
        public float pitch = 1f;

        [FoldoutGroup("Settings"), HorizontalGroup("Settings/More")]
        public bool useRandomVolume = false;
        
        [FoldoutGroup("Settings"), HorizontalGroup("Settings/More")]
        public bool useRandomPitch = false;

        [FoldoutGroup("Settings"), Range(0f, 1f)] public float spatialBlend = 0f;

        [FoldoutGroup("Settings")]
        public bool loop = false;

        public abstract void PlaySound(AudioSource source, float delay = 0f);
    }

    public enum SoundType
    {
        Play,
        PlayDelayed,
        PlayOneShot
    }
}