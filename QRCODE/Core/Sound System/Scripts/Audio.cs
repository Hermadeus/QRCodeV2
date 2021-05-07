using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QRCode.Audio
{
    public static class Audio
    {
        public static Dictionary<string, Sound> SoundsTable => AudioSettings.soundDatabase;

        public static Dictionary<SoundName, Sound> SoundsTableWithEnum => AudioSettings.soundDatabaseWithEnum;

        public static List<AudioController> Controllers = new List<AudioController>();

        public static SoundSystemSettings AudioSettings => SoundSystemSettings.Instance;

        private static Transform _controllerFolder = null;

        public static void PlaySound(string soundName, float delay = 0)
        {
            SoundsTable.TryGetValue(soundName, out var sound);
            Play(sound, GetUnusedAudioController(), null, delay);
        }

        public static void PlaySound(SoundName soundName, float delay = 0)
        {
            SoundsTableWithEnum.TryGetValue(soundName, out var sound);
            Play(sound, GetUnusedAudioController(), null, delay);
        }

        public static void PlaySound(string soundName, Transform target, float delay = 0)
        {
            SoundsTable.TryGetValue(soundName, out var sound);
            Play(sound, GetUnusedAudioController(), target, delay);
        }
        
        public static void PlaySound(SoundName soundName, Transform target, float delay = 0)
        {
            SoundsTableWithEnum.TryGetValue(soundName, out var sound);
            Play(sound, GetUnusedAudioController(), target, delay);
        }
        
        public static void PlaySound(SoundName soundName, Transform target, out AudioController controller, float delay = 0)
        {
            SoundsTableWithEnum.TryGetValue(soundName, out var sound);
            controller = GetUnusedAudioController();
            Play(sound, controller, target, delay);
        }

        private static void Play(Sound sound, AudioController controller, Transform target = null, float delay = 0)
        {
            if (sound == null)
            {
                Debug.LogError($"There is no sound named {sound} in the soundTable.");
            }

            controller.name = $"Audio Controller [{sound.name}]";
            sound.PlaySound(controller.audioSource, delay);
            controller.ResetUsability(target);
        }

        private static AudioController GetUnusedAudioController()
        {
            if (Controllers.Count != 0)
            {
                foreach (var controller in Controllers.Where(controller => !controller.isCurrentlyUsed))
                {
                    controller.isCurrentlyUsed = true;
                    return controller;
                }
            }

            return AddNewControllerToScene();
        }

        private static AudioController AddNewControllerToScene()
        {
            if (Controllers.Count == 0)
                _controllerFolder = new GameObject("Audio Controllers []").transform;

            var controller = new GameObject("AudioController [Unused]").AddComponent<AudioController>();
            controller.Init();
            controller.isCurrentlyUsed = true;
            Controllers.Add(controller);
            _controllerFolder.name = $"Audio Controllers [{Controllers.Count}]";
            controller.transform.SetParent(_controllerFolder);
            return controller;
        }
    }
}

