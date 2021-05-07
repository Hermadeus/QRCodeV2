using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace QRCode.UI
{
    public static class UIAnimator
    {
        //---<MOVE>----------------------------------------------------------------------------------------------------<

        public static Tween PlayMove(UIComponentBase componentBase, UIBehaviour behaviour)
        {
            return GetMove(componentBase, behaviour).SetDelay(behaviour.Animation.move.delayBefore);
        }
        
        public static Tween GetMove(UIComponentBase componentBase, UIBehaviour behaviour)
        {
            switch (behaviour.Animation.move.animMove)
            {
                case AnimMove.Translation:
                    return MoveTranslation(
                        componentBase,
                        behaviour.Animation,
                        componentBase.initialPosition);
                case AnimMove.ScreenTranslation:
                    return MoveScreenTranslation(
                        componentBase,
                        behaviour.Animation,
                        componentBase.initialPosition);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public static Tween MoveTranslation(UIComponentBase componentBase, UIAnimation animation, Vector2 initialPosition)
        {
            switch (animation.AnimationType)
            {
                case AnimationType.Undefined:
                    break;
                case AnimationType.Show:
                {
                    componentBase.RectTransform.anchoredPosition = 
                        componentBase.initialPosition + GetScreenOffset(componentBase, animation.move.direction);
                    return componentBase.RectTransform
                        .DOAnchorPos(initialPosition, animation.move.duration)
                        .SetEase(animation.move.ease);
                }
                case AnimationType.Hide:
                {
                    componentBase.RectTransform.anchoredPosition = componentBase.initialPosition;
                    return componentBase.RectTransform
                        .DOAnchorPos(initialPosition + GetScreenOffset(componentBase, animation.move.direction),
                            animation.move.duration)
                        .SetEase(animation.move.ease);
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return null;
        }

        public static Tween MoveScreenTranslation(
            UIComponentBase componentBase,
            UIAnimation animation,
            Vector2 initialPosition)
        {
            var sequence = DOTween.Sequence();
            
            switch (animation.AnimationType)
            {
                case AnimationType.Undefined:
                    break;
                case AnimationType.Show:
                    componentBase.RectTransform.anchoredPosition = Vector2.zero;
                    componentBase.RectTransform.anchoredPosition +=
                        GetScreenOffset(componentBase, animation.move.direction);
                    sequence.Append(componentBase.RectTransform
                        .DOAnchorPos(Vector2.zero, animation.move.duration)
                        .SetEase(animation.move.ease));
                    break;
                case AnimationType.Hide:
                    componentBase.RectTransform.anchoredPosition = Vector2.zero;
                    GetScreenOffset(componentBase, animation.move.direction);
                    sequence.Append(componentBase.RectTransform
                        .DOAnchorPos(GetScreenOffset(componentBase, animation.move.direction), animation.move.duration)
                        .SetEase(animation.move.ease));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return sequence;
        }
        
        
        //---<ROTATE>--------------------------------------------------------------------------------------------------<

        public static Tween PlayRotate(UIComponentBase component, UIBehaviour behaviour)
        {
            return GetRotate(component, behaviour).SetDelay(behaviour.Animation.move.delayBefore);;
        }

        public static Tween GetRotate(UIComponentBase component, UIBehaviour behaviour)
        {
            switch (behaviour.Animation.rotate.animRotate)
            {
                case AnimRotate.SimplePingPong:
                    return RotateSimplePingPong(component, behaviour.Animation,
                        behaviour.Animation.rotate.initialRotation);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public static Tween RotateSimplePingPong(UIComponentBase component, UIAnimation animation, Vector3 initialRotation)
        {
            var sequence = DOTween.Sequence();

            switch (animation.AnimationType)
            {
                case AnimationType.Undefined:
                    break;
                case AnimationType.Show:
                    sequence.Append(component.RectTransform
                        .DORotate(animation.rotate.targetRotation, animation.rotate.duration / 2)
                        .SetEase(animation.rotate.ease));
                    sequence.Append(component.RectTransform
                        .DORotate(- animation.rotate.targetRotation, animation.rotate.duration / 2)
                        .SetEase(animation.rotate.ease));
                    sequence.Append(component.RectTransform
                        .DORotate(animation.rotate.initialRotation, .2f)
                        .SetEase(animation.rotate.ease));
                    sequence.Play();
                    break;
                case AnimationType.Hide:
                    sequence.Append(component.RectTransform
                        .DORotate(animation.rotate.targetRotation, animation.rotate.duration / 2)
                        .SetEase(animation.rotate.ease));
                    sequence.Append(component.RectTransform
                        .DORotate(- animation.rotate.targetRotation, animation.rotate.duration / 2)
                        .SetEase(animation.rotate.ease));
                    sequence.Append(component.RectTransform
                        .DORotate(animation.rotate.initialRotation, .2f)
                        .SetEase(animation.rotate.ease));
                    sequence.Play();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return sequence;
        }
        
        //---<SCALE>---------------------------------------------------------------------------------------------------<

        public static Tween PlayScale(UIComponentBase component, UIBehaviour behaviour)
        {
            return GetScale(component, behaviour).SetDelay(behaviour.Animation.move.delayBefore);;
        }

        public static Tween GetScale(UIComponentBase component, UIBehaviour behaviour)
        {
            switch (behaviour.Animation.scale.animScale)
            {
                case AnimScale.Stretch:
                    return ScaleStretch(component, behaviour.Animation, behaviour.Animation.scale.initialScale);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static Tween ScaleStretch(UIComponentBase component, UIAnimation animation, Vector3 initialScale)
        {
            var sequence = DOTween.Sequence();
            
            switch (animation.AnimationType)
            {
                case AnimationType.Undefined:
                    break;
                case AnimationType.Show:
                    sequence.Append(component.RectTransform
                        .DOScale(initialScale, animation.scale.duration)
                        .SetEase(animation.scale.ease));
                    break;
                case AnimationType.Hide:
                    sequence.Append(component.RectTransform
                        .DOScale(animation.scale.targetScale, animation.scale.duration)
                        .SetEase(animation.scale.ease));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return sequence;
        }
        
        //---<FADE>----------------------------------------------------------------------------------------------------<

        public static Tween PlayFade(UIComponentBase component, UIBehaviour behaviour)
        {
            return GetFade(component, behaviour).SetDelay(behaviour.Animation.move.delayBefore);;
        }

        public static Tween GetFade(UIComponentBase component, UIBehaviour behaviour)
        {
            switch (behaviour.Animation.fade.animFade)
            {
                case AnimFade.FadeTo:
                    return FadeTo(component, behaviour.Animation, behaviour.Animation.fade.initialAlpha);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static Tween FadeTo(UIComponentBase component, UIAnimation animation, float initialAlpha)
        {
            var sequence = DOTween.Sequence();
            
            switch (animation.AnimationType)
            {
                case AnimationType.Undefined:
                    break;
                case AnimationType.Show:
                   sequence.Append(component.CanvasGroup
                        .DOFade(initialAlpha, animation.fade.duration)
                        .SetEase(animation.fade.ease));
                    break;
                case AnimationType.Hide:
                    sequence.Append(component.CanvasGroup
                        .DOFade(animation.fade.targetAlpha, animation.fade.duration)
                        .SetEase(animation.fade.ease));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return sequence;
        }
        
        //---<HELPERS>-------------------------------------------------------------------------------------------------<

        private static Direction GetInverseDirection(Direction direction)
        {
            return direction switch
            {
                Direction.Left => Direction.Right,
                Direction.Right => Direction.Left,
                Direction.Top => Direction.Bottom,
                Direction.Bottom => Direction.Top,
                Direction.CustomPosition => Direction.CustomPosition,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
        
        private static Vector2 GetScreenOffset(UIComponentBase componentBase, Direction direction)
        {
            var vec2 = new Vector2();
            
            switch (direction)
            {
                case Direction.Left:
                    vec2.x = -componentBase.UICanvas.ScreenSize.x;
                    vec2.y = 0;
                    break;
                case Direction.Right:
                    vec2.x = componentBase.UICanvas.ScreenSize.x;
                    vec2.y = 0;
                    break;
                case Direction.Top:
                    vec2.x = 0;
                    vec2.y = componentBase.UICanvas.ScreenSize.y;
                    break;
                case Direction.Bottom:
                    vec2.x = 0;
                    vec2.y = -componentBase.UICanvas.ScreenSize.y;
                    break;
                case Direction.CustomPosition:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return vec2;
        }
    }
}
