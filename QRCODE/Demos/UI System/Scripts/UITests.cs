using System;
using System.Collections;
using System.Collections.Generic;
using QRCode.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace QRCode.UI.Demos
{
    public class UITests : MonoBehaviour
    {
        public UIView view;

        public Sprite[] Sprites;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                view.TryRefresh<TextMeshProUGUIRefresher>("Text A",
                    new TextMeshProEventArgs(){value = "Salut A"});
                view.TryRefresh<TextMeshProUGUIRefresher>("Text B",
                    new TextMeshProEventArgs(){value = "Salut B"});
                view.TryRefresh<TextMeshProUGUIRefresher>("Text C",
                    new TextMeshProEventArgs(){value = "Salut C"});
                view.TryRefresh<TextMeshProUGUIRefresher>("Text D",
                    new TextMeshProEventArgs(){value = "Salut D"});
            }
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                
            }
        }

        [Button]
        public void RefreshElement()
        {
            view.TryRefresh<TextMeshProUGUIRefresher>("text", new TextMeshProEventArgs(){value = $"{Random.Range(0,150)}"});
            view.TryRefresh<ImageRefresher>("image", new ImageEventArgs(){Sprite = Sprites[Random.Range(0, Sprites.Length)]});
        }

        [Button]
        public void OpenView(string name)
        {
            UI.ShowViewAndHideOthers(name);
        }
    }
}
