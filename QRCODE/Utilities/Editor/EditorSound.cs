using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace QRCode.Editor.Utilities
{
    public static class EditorSound
    {
        public static void PlayPreviewClip(AudioClip clip, int startSample = 1, bool loop = false)
        {
            Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
            Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "PlayPreviewClip",
                BindingFlags.Static | BindingFlags.Public,
                null,
                new System.Type[] {
                    typeof(AudioClip),
                    typeof(Int32),
                    typeof(Boolean)
                },
                null
            );
            method.Invoke(
                null,
                new object[] {
                    clip,
                    startSample,
                    loop
                }
            );
        }
        
        public static void PausePreviewClip()
        {
            Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
            Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "PausePreviewClip",
                BindingFlags.Static | BindingFlags.Public,
                null,
                new System.Type[] {
                },
                null
            );
            method.Invoke(null, null);
        }
        
        public static void ResumePreviewClip()
        {
            Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
            Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "ResumePreviewClip",
                BindingFlags.Static | BindingFlags.Public,
                null,
                new System.Type[] {
                },
                null
            );
            method.Invoke(null, null);
        }
        
        public static int GetPreviewClipSamplePosition() {
            Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
            Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "GetPreviewClipSamplePosition",
                BindingFlags.Static | BindingFlags.Public
            );
			
            int position = (int)method.Invoke(
                null,
                new object[] {}
            );
			
            return position;
        }

        public static void StopAllClips()
        {
            Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
            Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "StopAllClips",
                BindingFlags.Static | BindingFlags.Public,
                null,
                new System.Type[] { },
                null
            );
            method.Invoke(
                null,
                new object[] { }
            );
        }
        
        [Obsolete("Unity Crash")]
        public static bool HasPreview(AudioClip clip) {
            Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
            Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "GetSoundSize",
                BindingFlags.Static | BindingFlags.Public
            );
			
            bool hasPreview = (bool)method.Invoke(
                null,
                new object[] {
                    clip
                }
            );
			
            return hasPreview;
        }
        
        public static double GetDuration(AudioClip clip) {
            Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
            Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
            MethodInfo method = audioUtilClass.GetMethod(
                "GetDuration",
                BindingFlags.Static | BindingFlags.Public
            );
			
            double duration = (double)method.Invoke(
                null,
                new object[] {
                    clip
                }
            );
			
            return duration;
        }
    }
}
