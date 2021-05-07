using System.Collections;
using UnityEngine;

namespace QRCode.Feedbacks
{
    public class FeedbackInstantiateGameObject : Feedback
    {
        public GameObject Instance = default;
        public bool UseThisPosition = default;
        
        private Quaternion rotation;
        private Vector3 position;
        private Transform parent;
        
        public FeedbackInstantiateGameObject() : base()
        {
            rotation = Quaternion.identity;
            position = new Vector3();
        }

        public FeedbackInstantiateGameObject(MonoBehaviour owner, GameObject instance, Vector3 position, Quaternion rotation, Transform parent) : base(owner)
        {
            UseThisPosition = true;
            this.position = position;
            this.rotation = rotation;
            this.parent = parent;
        }

        public override IEnumerator Describe()
        {
            var pos = UseThisPosition ? Owner.transform.position : position;
            GameObject.Instantiate(Instance, pos, rotation, parent);
            
            yield break;
        }
    }
}
