using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.UI
{
    public class UIView : UIElement
    {
        //--<Public>
        [TabGroup("Settings")] 
        public string viewName = "";
        
        [TabGroup("Settings")]
        public UIAnimationGroup AnimationGroup;

        [TabGroup("Refreshables")]
        public Dictionary<string, UIRefresher> refreshables = new Dictionary<string, UIRefresher>();

        //---<INITIALIZATION>------------------------------------------------------------------------------------------<
        public override void Init()
        {
            UI.RegisterView(this);
            
            AnimationGroup.Init();

            base.Init();
        }

        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override void Show()
        {
            if (AnimationGroup.Enable && !isVisible)
            {
                AnimationGroup.Play(AnimationType.Show);
            }
            
            base.Show();
        }

        public override void Hide()
        {
            if (AnimationGroup.Enable && isVisible)
            {
                AnimationGroup.Play(AnimationType.Hide);
            }

            base.Hide();
        }

        public void TryRefresh<T>(string refreshable, EventArgs args) where T : UIRefresher
        {
            refreshables.TryGetValue(refreshable, out var element);
            if (element is T cast)
            {
                cast.Refresh(args);
            }
        }
    }
}
