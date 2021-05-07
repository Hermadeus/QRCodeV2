using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.System //eg: Game.UI || Game.3C
{
    public class TemplateScript : MonoBehaviour
    {
        [TabGroup("References")] 
        [SerializeField]
        private ScriptableObject data;
        
        [TabGroup("References")] 
        [SerializeField]
        private GameObject
            referenceA,
            referenceB;

        [TabGroup("References")] [SerializeField]
        private Transform transformA;

        [TabGroup("Tweakables")]//tweakable -> Datas à tweaker qui ne sont pas génériques et donc ne vont pas dans des ScriptableObjects.
        [SerializeField, Title("Title 1")]
        private float a = 15f;

        [TabGroup("Tweakables")] 
        [SerializeField, Range(0,150), Title("Title 2")]
        private int b = 15;

        [TabGroup("Tweakables")] 
        [SerializeField, Range(0,150)]
        private int c = 15;
        
        [TabGroup("Debug")] 
        [SerializeField, ReadOnly]
        private Vector2 debugValueA;
        [TabGroup("Debug")] 
        [SerializeField, ReadOnly]
        private float debugValueB;
        
        [Button]
        void Debug(){}
        [Button]
        void OtherDebug(){}

        //--<Static>

        //--<Public>

        //--<Private>

        //--<Events>

        //---<PROPERTIES>----------------------------------------------------------------------------------------------<

        //---<CONSTRUCTOR>---------------------------------------------------------------------------------------------<

        //---<INITIALIZATION>------------------------------------------------------------------------------------------<

        //---<CORE>----------------------------------------------------------------------------------------------------<

        //---<CALLBACKS>-----------------------------------------------------------------------------------------------<

        //---<HELPERS>-------------------------------------------------------------------------------------------------<

        //---<EDITOR>--------------------------------------------------------------------------------------------------<
#if UNITY_EDITOR
            
#endif
    }
}

