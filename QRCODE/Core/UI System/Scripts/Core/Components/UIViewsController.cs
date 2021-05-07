using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QRCode.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace QRCode.UI
{
    public class UIViewsController : SerializedMonoBehaviour
    {
        public List<UIView> Views = new List<UIView>();

        public UIView currentView = null;

        private int currentViewId = 0;
        public int Count => Views.Count;

        private IEnumerator Start()
        {
            int i = 0;
            for (int j = 0; j < Views.Count; j++)
            {
                if (Views[i] == currentView)
                    i = j;
            }

            yield return new WaitForEndOfFrame();
            
            OpenAt(i);
        }
        
        [Button]
        public void Next()
        {
            currentViewId++;

            if (currentViewId > Count - 1)
                currentViewId = 0;
            
            CloseAll(Get(currentViewId));
        }

        [Button]
        public void Previous()
        {
            currentViewId--;

            if (currentViewId < 0)
                currentViewId = Count - 1;
            
            CloseAll(Get(currentViewId));
        }

        public void CloseAll(params UIView[] exceptions)
        {
            foreach (var view in Views)
            {
                var isException = false;
                
                for (int i = 0; i < exceptions.Length; i++)
                {
                    if (exceptions[i] == view)
                    {
                        currentView = view;
                        view.Show();
                    }
                    else
                        view.Hide();
                }
            }
        }
        
        public void OpenAt(int index) => CloseAll(Get(index));

        public UIView Get(int index) => Views[index];
    }
}
