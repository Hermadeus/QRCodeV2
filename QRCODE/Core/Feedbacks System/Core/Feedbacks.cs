using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks
{
    /// <summary>
    /// Stock all feedbacks handler in a dictionary.
    /// </summary>
    public class Feedbacks
    {
        private Dictionary<string, IFeedbackHandler> table;
        
        public void Init(GameObject gameObject)
        {
            var handlers = gameObject.GetComponentsInChildren<IFeedbackHandler>();

            if(handlers == null)
                return;
            
            table = new Dictionary<string, IFeedbackHandler>();

            foreach (var handler in handlers)
            {
                var owner = (MonoBehaviour) handler;
                table.Add(owner.gameObject.name, handler as IFeedbackHandler);
            }
        }
        
        public void Call(string key) => table[key].Play();
    }
}
