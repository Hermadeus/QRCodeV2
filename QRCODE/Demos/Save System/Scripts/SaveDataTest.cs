using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRCode.SaveSystem.Demos
{
    [Serializable]
    public class SaveDataTest
    {
        private static SaveDataTest _current;

        public static SaveDataTest current
        {
            get
            {
                if (_current == null)
                {
                    _current = new SaveDataTest();
                }

                return _current;
            }
        }

        public int intValue;
        public Vector3 vector3Value;
    }
}
