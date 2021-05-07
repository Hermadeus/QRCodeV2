using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackMaterialProperty : Feedback
    {
        public MeshRenderer TargetRenderer;
        public int matIndex = 0;
        public string PropertyName = "";
        public Color TargetColor = Color.white;

        public FeedbackMaterialProperty() : base()
        {
            TargetColor = Color.white;
            matIndex = 1;
        }
        
        public FeedbackMaterialProperty(MonoBehaviour owner) : base(owner)
        {
            
        }
        
        public override IEnumerator Describe()
        {
            TargetRenderer.sharedMaterials[matIndex].DOColor(TargetColor, Duration);
            yield break;
        }
    }
}
