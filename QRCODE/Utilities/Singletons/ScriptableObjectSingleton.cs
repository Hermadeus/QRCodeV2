using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Singletons
{
    public abstract class ScriptableObjectSingleton<T> : SerializedScriptableObject where T : ScriptableObject
    {
        private static T _instance = default(T);
        
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
                }
                return _instance;
            }
        }
    }
}
