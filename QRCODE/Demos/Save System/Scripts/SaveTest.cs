using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.SaveSystem.Demos
{
    public class SaveTest : SerializedMonoBehaviour
    {
        public int intValue = 0;
        public Vector3 vec3Value = new Vector3();

        [Button]
        public void Save()
        {
            Debug.Log("sava");
            SaveDataTest.current.intValue = intValue;
            SaveDataTest.current.vector3Value = vec3Value;
            SerializationManager.Save("saveTest", SaveDataTest.current);
        }

        [Button]
        public void Load()
        {
            var saveData = (SaveDataTest)SerializationManager.Load(Application.persistentDataPath + "/saves/saveTest.save");
            vec3Value = saveData.vector3Value;
            intValue = saveData.intValue;
        }
    }
}
