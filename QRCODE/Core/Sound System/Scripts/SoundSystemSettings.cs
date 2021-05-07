using System.Collections;
using System.Collections.Generic;
using QRCode.Singletons;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine.Audio;

namespace QRCode.Audio
{
    [CreateAssetMenu(menuName = "QRCode/Audio/Settings")]
    public class SoundSystemSettings : ScriptableObjectSingleton<SoundSystemSettings>
    {
        [FolderPath, Title("Folder Path")]
        public string soundPath = "Assets/QRCODE/SoundSystem/Datas";

        [Range(0, 50)]
        [InlineButton("ResetMinimulNumberOfController", "Reset"), Title("General Settings")]
        public int minimulNumberOfController = 3;

        [Title("Settings")]
        public bool autoKillController = true;

        [Title("Database")]
        public Dictionary<string, Sound> soundDatabase = new Dictionary<string, Sound>();
        public Dictionary<SoundName, Sound> soundDatabaseWithEnum = new Dictionary<SoundName, Sound>();

        void ResetMinimulNumberOfController()
        {
            minimulNumberOfController = 3;
        }

        [Button]
        void GenerateDatabase()
        {
            soundDatabase = new Dictionary<string, Sound>();
            soundDatabaseWithEnum = new Dictionary<SoundName, Sound>();
            var sounds = Resources.FindObjectsOfTypeAll<Sound>();
            foreach (var sound in sounds)
            {
                if (!soundDatabase.ContainsKey(sound.name))
                {
                    soundDatabase.Add(sound.name, sound);
                }

                if (!soundDatabaseWithEnum.ContainsKey(sound.soundName))
                {
                    soundDatabaseWithEnum.Add(sound.soundName, sound);
                }
            }

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }
}