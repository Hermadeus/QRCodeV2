using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackInstantiateParticleSystem : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public ParticleSystem ParticleSystemPrefab = default;
        
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public Transform InstantiateAtPosition = default;

        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public float ScaleRatio = 1f;

        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public bool useDurationAsParticleSystemDuration = true;

        //--<Private>
        private ParticleSystem particleSystemInstance = null;

        //---<CONSTRUCTORS>--------------------------------------------------------------------------------------------<
        public FeedbackInstantiateParticleSystem() : base()
        {
            useDurationAsParticleSystemDuration = false;
            ScaleRatio = 1f;
        }
        
        public FeedbackInstantiateParticleSystem(
            MonoBehaviour owner, ParticleSystem particleSystemPrefab, Transform transform)
            : base(owner)
        {
            ParticleSystemPrefab = particleSystemPrefab;
            InstantiateAtPosition = transform;
            Duration = useDurationAsParticleSystemDuration ? ParticleSystemPrefab.main.duration : Duration;
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            particleSystemInstance = Object.Instantiate(
                ParticleSystemPrefab,
                InstantiateAtPosition.position,
                Quaternion.identity);

            particleSystemInstance.transform.localScale *= ScaleRatio;
            
            particleSystemInstance?.Play();
            yield break;
        }

        public override Feedback Kill()
        {
            base.Kill();
            particleSystemInstance?.Stop();
            return this;
        }
    }
}
