using System.Collections;
using System.Collections.Generic;
using QRCode.Singletons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode
{
    public class ScriptableObjectHolder : SerializedMonoBehaviour
    {
        [SerializeField] private List<ScriptableObject> _objects = new List<ScriptableObject>();

    }
}

