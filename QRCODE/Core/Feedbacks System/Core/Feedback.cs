using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace QRCode.Feedbacks
{
    [Serializable]
    public abstract class Feedback
    {
        //--<Public>
        [HideInInspector] public MonoBehaviour Owner;

        [HideIfGroup("showGroup")]
        [FoldoutGroup("showGroup/<GENERAL SETTINGS>")] 
        public bool useDurationAsDelayAfter = false;

        [FoldoutGroup("showGroup/<GENERAL SETTINGS>")]
        public bool Loop = false;

        [FoldoutGroup("showGroup/<GENERAL SETTINGS>"),ShowIf("@this.Loop"), LabelText("Infinity ?"), Indent(2)]
        public bool LoopInfinity = false;

        [FoldoutGroup("showGroup/<GENERAL SETTINGS>"),ShowIf("@this.Loop"),
         DisableIf("@this.LoopInfinity"), LabelText("Intensity"), Indent(2)]
        public AnimationCurve intensityAlongLoops = new AnimationCurve();

        [HorizontalGroup("showGroup/<GENERAL SETTINGS>/Loop"),
         FoldoutGroup("showGroup/General Settings"), 
         ShowIf("@this.Loop"), LabelText("Count"), Indent(2),
         DisableIf("@this.LoopInfinity")]
        public int LoopCount = 1;
        
        [HorizontalGroup("showGroup/<GENERAL SETTINGS>/Loop"),
         FoldoutGroup("showGroup/General Settings"),
         ShowIf("@this.Loop"), LabelText("Delay")]
        public float DelayBetweenEachLoop = 0f;
        
        [FoldoutGroup("showGroup/<GENERAL SETTINGS>")]
        public bool useRandomDuration = false;
        
        [FoldoutGroup("showGroup/<GENERAL SETTINGS>")]
        public float DelayBefore = 0f;

        [FoldoutGroup("showGroup/<GENERAL SETTINGS>"), HideIf("@this.useDurationAsDelayAfter")] 
        public float DelayAfter = 0f;
        
        [FoldoutGroup("showGroup/<GENERAL SETTINGS>"), ShowIf("@!this.useRandomDuration")]
        public float Duration = 1f;

        [FoldoutGroup("showGroup/<GENERAL SETTINGS>"), ShowIf("@this.useRandomDuration"), MinMaxSlider(0,50)]
        public Vector2 RandomDuration = new Vector2(1f, 1f);
        
        [FoldoutGroup("showGroup/<GENERAL SETTINGS>")]
        public Ease ease = Ease.InOutSine;

        //--<Protected>
        [SerializeField, BoxGroup("<CORE>", order: -1)]
        protected bool isActive = true;
        protected bool isPlaying = false;
        protected bool useDebugMode = false;
        protected int currentLoop = 0;

        protected Coroutine
            playFeedbackCoroutine,
            describeCoroutine,
            changeColorCoroutine;

        //--<Events>
        [HideInInspector]
        protected Action
            onInit,
            onPlay,
            onUpdate,
            onComplete,
            onKill;

        [HideInInspector]
        protected Action<int>
            onLoopEnd;

        //--<Editor Var>
#if UNITY_EDITOR
        protected bool showGroup => !isActive;
#endif
        
        //---<PROPERTIES>----------------------------------------------------------------------------------------------<
        public FeedbackGeneralSettings GeneralSettings => FeedbackMaker.GeneralSettings;

        /// <summary>
        /// WARNING : This function set Duration !
        /// </summary>
        protected float GetDuration()
        {
            Duration = useRandomDuration ? Random.Range(RandomDuration.x, RandomDuration.y) : Duration;
            return Duration;
        }

        public bool IsActive
        {
            get => isActive;
            set
            {
                if (!value)
                    Kill();

                isActive = value;
            }
        }

        public Ease Ease
        {
            get
            {
                return ease == Ease.Unset ? Ease.InOutSine : ease;
            }
            set
            {
                ease = value;
            }
        }

        protected float IntensityAlongLoops
        {
            get
            {
                var curLoop = currentLoop == 0 ? 0.01f : currentLoop;
                return LoopInfinity ? 1f : intensityAlongLoops != null ? 
                    intensityAlongLoops.Evaluate(curLoop / LoopCount) : 1f;
            } 
        }
        
        //---<CONSTRUCTORS>--------------------------------------------------------------------------------------------<
        public Feedback(MonoBehaviour owner)
        {
            Owner = owner;
            Duration = 1f;
            Init();
        }

        protected Feedback()
        {
            Duration = 1f;
            Init();
        }

        //---<CORE>----------------------------------------------------------------------------------------------------<
        /// <summary>
        /// Call at the instantiation.
        /// </summary>
        public virtual void Init()
        {
            if(useDebugMode) Debug.Log($"<color=blue> FEEDBACK :</color> {GetType().ToString()} is init.");
            onInit?.Invoke();
            currentLoop = 0;
        }

        /// <summary>
        /// Play Feedback.
        /// </summary>
        [ButtonGroup("Testing")]
        public Feedback Play()
        {
            if (!IsActive)
                return this;
            
            if (isPlaying)
                Kill();
            
            if(useDebugMode) Debug.Log($"<color=blue> FEEDBACK :</color> {GetType().ToString()} start playing.");
            GetDuration();
            playFeedbackCoroutine = Owner.StartCoroutine(PlayFeedbackCoroutine());
            onPlay?.Invoke();
            return this;
        }

        /// <summary>
        /// Don't use it! Use Play(). Core of the feedback coroutine.
        /// </summary>
        /// <returns></returns>
        public IEnumerator PlayFeedbackCoroutine()
        {
            yield return new WaitForEndOfFrame();
            if (!isActive)
            {
                Kill();
                yield break;
            }
            isPlaying = true;
            changeColorCoroutine = Owner.StartCoroutine(UpdateCoroutine());
            yield return new WaitForSeconds(DelayBefore / FeedbackMaker.FeedbackTimeScale);
            var loops = Loop ? LoopCount : 1;
            for (int i = 0; i < loops; i++)
            {
                if (LoopInfinity) i = -1;
                yield return Describe();
                if (Loop)
                {
                    currentLoop++;
                    onLoopEnd?.Invoke(currentLoop);
                    yield return new WaitForSeconds(DelayBetweenEachLoop);
                }
            }
            yield return new WaitForSeconds((useDurationAsDelayAfter ? Duration : DelayAfter) / FeedbackMaker.FeedbackTimeScale);
            isPlaying = false;
            OnComplete();
            currentLoop = 0;
            onComplete?.Invoke();
            if(useDebugMode) Debug.Log($"<color=blue> FEEDBACK :</color> {GetType().ToString()} is finish.");
        }

        /// <summary>
        /// Stop the feedback.
        /// </summary>
        /// <returns></returns>
        [ButtonGroup("Testing")]
        public virtual Feedback Kill()
        {
            isPlaying = false;
            if(playFeedbackCoroutine != null) Owner.StopCoroutine(playFeedbackCoroutine);
            if(changeColorCoroutine != null) Owner.StopCoroutine(changeColorCoroutine);
            if(describeCoroutine != null) Owner.StopCoroutine(describeCoroutine);
            currentLoop = 0;
            onKill?.Invoke();
            if(useDebugMode) Debug.Log($"<color=blue> FEEDBACK :</color> {GetType().ToString()} is kill.");
            return this;
        }

        /// <summary>
        /// Describe the effect of the feedback
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerator Describe();

        protected IEnumerator UpdateCoroutine()
        {
            while (isPlaying)
            {
                onUpdate?.Invoke();
                yield return null;
            }
        }

        //---<CALLBACKS>-----------------------------------------------------------------------------------------------<
        protected virtual void OnComplete() { }

        //---<HELPERS>-------------------------------------------------------------------------------------------------<
        /// <summary>
        /// Set the duration of the feedback.
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public Feedback SetDuration(float duration)
        {
            Duration = duration;
            return this;
        }
        
        /// <summary>
        /// Add some debugs in console.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Feedback UseDebugMode(bool value = true)
        {
            useDebugMode = value;
            return this;
        }

        /// <summary>
        /// Set the owner of the feedback.
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public Feedback SetOwner(MonoBehaviour owner)
        {
            Owner = owner;
            return this;
        }

        /// <summary>
        /// TRUE : Can play the feedback || FALSE : Can't play the feedback.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Feedback SetActive(bool value)
        {
            isActive = value;
            return this;
        }

        /// <summary>
        /// Set the main ease of the feedback.
        /// </summary>
        /// <param name="ease"></param>
        /// <returns></returns>
        public Feedback SetEase(Ease ease)
        {
            this.ease = ease;
            return this;
        }

        /// <summary>
        /// Set the number of loop of the feedback.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public Feedback SetLoops(int count)
        {
            Loop = true;
            LoopCount = count;
            return this;
        }
        
        /// <summary>
        /// Set the number of loop and the delay between each loops of the feedback.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public Feedback SetLoops(int count, float delay)
        {
            Loop = true;
            LoopCount = count;
            DelayBetweenEachLoop = delay;
            return this;
        }

        /// <summary>
        /// Allow to the feedback to be looped infinitely.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Feedback SetLoopInfinity(bool value = true)
        {
            Loop = true;
            LoopInfinity = true;
            return this;
        }
        
        /// <summary>
        /// Set a delay between each loops.
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        public Feedback SetDelayBetweenEachLoops(float delay)
        {
            Loop = true;
            DelayBetweenEachLoop = delay;
            return this;
        }
        
        /// <summary>
        /// Add a callback at the initialisation of the feedback.
        /// </summary>
        /// <param name="onInit"></param>
        /// <returns></returns>
        public Feedback OnInit(Action onInit)
        {
            this.onInit += onInit;
            return this;
        }
        
        /// <summary>
        /// Add a callback at the end of the feedback.
        /// </summary>
        /// <param name="onComplete"></param>
        /// <returns></returns>
        public Feedback OnComplete(Action onComplete)
        {
            this.onComplete += onComplete;
            return this;
        }
        
        /// <summary>
        /// Add a callback while the feedback is running.
        /// </summary>
        /// <param name="onUpdate"></param>
        /// <returns></returns>
        public Feedback OnUpdate(Action onUpdate)
        {
            this.onUpdate += onUpdate;
            return this;
        }
        
        /// <summary>
        /// Add a callback when the feedback starts.
        /// </summary>
        /// <param name="onPlay"></param>
        /// <returns></returns>
        public Feedback OnPlay(Action onPlay)
        {
            this.onPlay += onPlay;
            return this;
        }

        /// <summary>
        /// Add a callback if the feedback is killed.
        /// </summary>
        /// <param name="onKill"></param>
        /// <returns></returns>
        public Feedback OnKill(Action onKill)
        {
            this.onKill += onKill;
            return this;
        }

        /// <summary>
        /// Add a callback at the end of the last loop of the feedback.
        /// </summary>
        /// <param name="onLoopEnd"></param>
        /// <returns></returns>
        public Feedback OnLoopEnd(Action<int> onLoopEnd)
        {
            this.onLoopEnd += onLoopEnd;
            return this;
        }
        
        public bool IsPlaying => isPlaying;
    }
}
