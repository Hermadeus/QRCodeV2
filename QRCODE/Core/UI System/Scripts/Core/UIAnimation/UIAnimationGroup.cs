using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QRCode.Gameplay;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.UI
{
    [Serializable]
    public class UIAnimationGroup
    {
        //--<Publics>
        
        [BoxGroup("Enable")]
        public bool Enable = false;

        [ShowIf("@this.Enable")]
        public List<UIElement> Elements = new List<UIElement>();

        [FoldoutGroup("Settings"), ShowIf("@this.Enable")]
        public float delayBefore = 0f;
        
        [FoldoutGroup("Settings"), ShowIf("@this.Enable")]
        public float timeBetweenEachAnimation = .2f;
        
        [FoldoutGroup("Settings"), ShowIf("@this.Enable")]
        public bool waitTheEndOfPreviousAnimation = false;
        
        [FoldoutGroup("Settings"), ShowIf("@this.Enable")]
        public bool animateRandomly = false;

        //--<Privates>
        private Dictionary<UIElement, UIBehaviour> elements = new Dictionary<UIElement, UIBehaviour>();
        
        public UIAnimationGroup()
        {
            elements = new Dictionary<UIElement, UIBehaviour>();
            
            timeBetweenEachAnimation = .2f;
            waitTheEndOfPreviousAnimation = false;
            animateRandomly = false;
        }

        public void Init()
        {
            for (int i = 0; i < Elements.Count; i++)
            {
                elements.Add(Elements[i], Elements[i].show);
            }
        }
        
        public void Play(AnimationType animationType)
        {
            Coroutiner.Instance.Play(PlayAnimationCoroutine(animationType));
        }
        
        public IEnumerator PlayAnimationCoroutine(AnimationType animationType)
        {
            yield return new WaitForSeconds(delayBefore);
            
            for (int i = 0; i < elements.Count; i++)
            {
                var element = elements.ElementAt(i).Key;
                var anim = elements[element].Animation;
                switch (animationType)
                {
                    case AnimationType.Undefined:
                        break;
                    case AnimationType.Show:
                        element.Show();
                        break;
                    case AnimationType.Hide:
                        element.Hide();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                if (!waitTheEndOfPreviousAnimation)
                    yield return new WaitForSeconds(timeBetweenEachAnimation);
                else
                    yield return new WaitUntil(() => anim.IsPlaying);
            }
        }
    }
}
