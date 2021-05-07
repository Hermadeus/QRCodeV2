using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace QRCode.Feedbacks
{
    [ShowInInspector]
    public class Sequencer : Feedback
    {
        //--<Public>
        [BoxGroup("showGroup/<FEEDBACKS>")] 
        public List<Feedback> Feedbacks = new List<Feedback>();

        //--<Private>
        private Feedback currentFeedback;

        //---<PROPERTIES>----------------------------------------------------------------------------------------------<
        public Feedback GetCurrentFeedback => currentFeedback;
        public int FeedbacksCount => Feedbacks.Count;
        
        //---<CONSTRUCTORS>--------------------------------------------------------------------------------------------<
        public Sequencer () { }

        public Sequencer(Sequencer sequencer)
        {
            Feedbacks = new List<Feedback>(sequencer.Feedbacks);
            useRandomDuration = sequencer.useRandomDuration;
            Duration = sequencer.Duration;
            RandomDuration = sequencer.RandomDuration;
            useDurationAsDelayAfter = sequencer.useDurationAsDelayAfter;
            DelayAfter = sequencer.DelayAfter;
            DelayBefore = sequencer.DelayBefore;
            Loop = sequencer.Loop;
            LoopInfinity = sequencer.LoopInfinity;
            LoopCount = sequencer.LoopCount;
        }
        
        public Sequencer(MonoBehaviour owner) : base(owner) { }

        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            for (int i = 0; i < Feedbacks.Count; i++)
            {
                currentFeedback = Feedbacks[i];
                yield return Feedbacks[i].PlayFeedbackCoroutine();
                yield return new WaitForSeconds(useDurationAsDelayAfter ? Feedbacks[i].Duration : 0f);
            }

            currentFeedback = null;
        }

        /// <summary>
        /// Add a feedback and return the current sequencer.
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns></returns>
        public Sequencer Append(Feedback feedback)
        {
            Feedbacks.Add(feedback);
            return this;
        }

        /// <summary>
        /// Add a feedback to the sequencer but return the feedback.
        /// </summary>
        /// <param name="feedback"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T AppendSpecific<T>(T feedback) where T : Feedback
        {
            Feedbacks.Add(feedback);
            return feedback;
        }

        /// <summary>
        /// Play the sequencer.
        /// </summary>
        /// <returns></returns>
        public new Sequencer Play()
        {
            var feedback = this as Feedback;
            feedback.Play();
            return this;
        }
        
        /// <summary>
        /// Kill and stop the sequencer.
        /// </summary>
        /// <returns></returns>
        public new Sequencer Kill()
        {
            isPlaying = false;
            if(playFeedbackCoroutine != null) Owner.StopCoroutine(playFeedbackCoroutine);
            if(changeColorCoroutine != null) Owner.StopCoroutine(changeColorCoroutine);
            if(describeCoroutine != null) Owner.StopCoroutine(describeCoroutine);
            
            Feedbacks.ForEach(w => w.Kill());
            
            onKill?.Invoke();
            if(useDebugMode) Debug.Log($"<color=blue> FEEDBACK :</color> {GetType().ToString()} is kill.");

            return this;
        }

        public Sequencer Clear()
        {
            Feedbacks.Clear();
            return this;
        }

        public Sequencer SetOwnerForAll(MonoBehaviour owner)
        {
            Owner = owner;

            foreach (var feedback in Feedbacks)
            {
                feedback.Owner = owner;
                if (feedback is Sequencer)
                {
                    var cast = (Sequencer) feedback;
                    cast.SetOwnerForAll(owner);
                }
            }
            return this;
        }

        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        
        /// <summary>
        /// Return a feedback in the sequencer.
        /// Use index if there are more than 1 researched feedback in the sequencer.
        /// </summary>
        public T GetFeedback<T>(int index = 0) where T : Feedback => Feedbacks.OfType<T>().ToArray()[index];

        /// <summary>
        /// Allow to the sequencer to play each feedback one after another.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Sequencer UseDurationAsDelayAfter(bool value)
        {
            useDurationAsDelayAfter = value;
            foreach (var feedback in Feedbacks)
            {
                feedback.useDurationAsDelayAfter = value;
            }
            return this;
        }
        
        public new Sequencer UseDebugMode(bool value = true)
        {
            useDebugMode = value;
            Feedbacks.ForEach(w => w.UseDebugMode());
            return this;
        }
    }
}
