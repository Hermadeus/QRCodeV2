using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackSetActive : Feedback
    {
        //--<Public>
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]  
        public GameObject GameObjectToSetActive = default;
        
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>")]  
        public bool To = false;

        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>"), HorizontalGroup("showGroup/<FEEDBACK SETTINGS>/<RESET>")]  
        public bool Reset = false;
        
        [FoldoutGroup("showGroup/<FEEDBACK SETTINGS>"), ShowIf("@this.Reset"), 
         HorizontalGroup("showGroup/<FEEDBACK SETTINGS>/<RESET>")]  
        public float ResetDelay = 1f;

        //---<CONSTRUCTORS>--------------------------------------------------------------------------------------------<
        public FeedbackSetActive(MonoBehaviour owner, GameObject toSetActive, bool to = false) : base(owner)
        {
            GameObjectToSetActive = toSetActive;
            To = to;
        }
        
        //---<CORE>----------------------------------------------------------------------------------------------------<
        public override IEnumerator Describe()
        {
            GameObjectToSetActive.SetActive(To);

            if (!Reset) yield break;
            
            yield return new WaitForSeconds(ResetDelay);
            GameObjectToSetActive.SetActive(!To);
        }
    }
}

