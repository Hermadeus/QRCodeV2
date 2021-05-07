using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.UI
{
    public class UIElement : UIComponentBase
    {
        //--<Public>
        [BoxGroup("HideOnStart")]
        public bool hideOnStart = true;
        
        [TabGroup("Behaviours")]
        public UIBehaviour show;
        
        [TabGroup("Behaviours")]
        public UIBehaviour hide;

        //--<Protected>
        protected bool isVisible = false;

        //---<INITIALIZATION>------------------------------------------------------------------------------------------<
        public override void Init()
        {
            show.Init();
            hide.Init();
            
            base.Init();
            
            if (hideOnStart)
            {
                ForceHide();
            }
            else
            {
                isVisible = true;
                gameObject.SetActive(true);
            }
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        
        [Button]
        public virtual void Show()
        {
            if (!isVisible)
            {
                gameObject.SetActive(true);
                hide.StopAnimation();
                show.PlayAnimation(this);
            }

            isVisible = true;
        }

        [Button]
        public virtual void Hide()
        {
            if (isVisible)
            {
                show.StopAnimation();
                hide.PlayAnimation(this, () => gameObject.SetActive(false));
            }

            isVisible = false;
        }

        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        
        public void ForceShow()
        {
            isVisible = false;
            Show();
            gameObject.SetActive(true);
        }

        public void ForceHide()
        {
            isVisible = true;
            Hide();
            gameObject.SetActive(false);
        }

        //---<EDITOR>--------------------------------------------------------------------------------------------------<
        
        protected virtual void Reset()
        {
            show = new UIBehaviour();
            show.Animation.AnimationType = AnimationType.Show;
            hide = new UIBehaviour();
            hide.Animation.AnimationType = AnimationType.Hide;
        }
    }
}
