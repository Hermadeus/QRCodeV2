using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackFlicker : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public MeshRenderer MeshRenderer;
        
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public int MaterialIndex = 0;

        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public InOutColor inOut;

        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]
        public Color InitColor;
        
        //--<Private>
        private Coroutine flickCor;
        private Tween flickTween;

        //---<CONSTRUCTORS>--------------------------------------------------------------------------------------------<
        public FeedbackFlicker() : base()
        {
            inOut = new InOutColor(Color.red);
            InitColor = Color.white;
        }

        public FeedbackFlicker(MonoBehaviour owner, MeshRenderer meshRenderer, int matIndex = 0) : base(owner)
        {
            MeshRenderer = meshRenderer;
            MaterialIndex = matIndex;
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            flickCor = Owner.StartCoroutine(FlickCor());
            yield break;
        }

        IEnumerator FlickCor()
        {
            InitColor = MeshRenderer.materials[MaterialIndex].color;
            flickTween = MeshRenderer.materials[MaterialIndex].DOColor(inOut.amplitude, inOut.inDuration)
                .SetEase(inOut.inEase);
            yield return new WaitForSeconds(inOut.inDuration + inOut.betweenDuration);
            flickTween = MeshRenderer.materials[MaterialIndex].DOColor(InitColor, inOut.outDuration)
                .SetEase(inOut.outEase);
        }

        public override Feedback Kill()
        {
            base.Kill();
            if(flickCor != null) Owner.StopCoroutine(FlickCor());
            flickTween?.Kill();
            return this;
        }
    }
}
