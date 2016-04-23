using UnityEngine;
using System.Collections;

namespace LSE.INTERACTION
{
    /// <summary>
    /// Klasse um ein Objekt kontrollierbar zu machen
    /// </summary>
    public class I501_ControllableObject : I500_InteractiveObject
    {
        public enum ControllerType { STATIC, HORIZONTAL, VERTICAL, SPACE, FOLLOW };
        public ControllerType selectedTypeInInspector;
        [SerializeField][HideInInspector]
        private ControllerType cType;
        public ControllerType CType
        {
            get { return cType; }
            set 
            { 
                if (cType == value) return;  
                cType = value;
                SetComponents();
                //Debug.LogError("type set to " + value); 
            }
        }

        private void SetComponents()
        {
            RemoveComponents();
            switch(cType)
            {
                case ControllerType.FOLLOW:
                    break;
                case ControllerType.HORIZONTAL:
                    gameObject.AddComponent<I511_HorizontalControl>();
                    break;
                case ControllerType.SPACE:
                    gameObject.AddComponent<I511_HorizontalControl>();
                    //Workaround, Hide In INspector wird nur beim ersten Element getriggert
                    gameObject.AddComponent<I512_VerticalControl>().hideFlags = HideFlags.HideInInspector;
                    break;
                case ControllerType.STATIC:
                    break;
                case ControllerType.VERTICAL:
                    gameObject.AddComponent<I512_VerticalControl>();
                    break;
                default:
                    Debug.LogError("I501_ControllableObject::SetComponent(), Type " + cType.ToString() + " unknown.");
                    break;
            }
        }

        private void RemoveComponents()
        {
            RemoveComponent("I511_HorizontalControl");
            RemoveComponent("I512_VerticalControl");
        }

        private void RemoveComponent(string c)
        {
            MonoBehaviour m = (MonoBehaviour) gameObject.GetComponent(c);
            if (m != null)
            {
                m.enabled = false;
                DestroyImmediate(m);
                m = null;
            }
        }
    }
}
