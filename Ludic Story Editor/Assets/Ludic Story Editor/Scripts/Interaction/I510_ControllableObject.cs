using UnityEngine;
using System.Collections;

namespace LSE.INTERACTION
{
    /// <summary>
    /// Klasse um ein Objekt kontrollierbar zu machen
    /// </summary>
    public class I510_ControllableObject : I100_Interaction
    {
        public enum ControllerType { STATIC, HORIZONTAL, VERTICAL, SPACE, FOLLOW };
        [SerializeField]
        private ControllerType cType;
        public ControllerType CType
        {
            get { return cType; }
            set { cType = value; Debug.LogError("type set to " + value); }
        }
    }
}
