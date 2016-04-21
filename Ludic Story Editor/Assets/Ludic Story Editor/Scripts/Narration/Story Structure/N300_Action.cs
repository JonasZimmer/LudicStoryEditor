using UnityEngine;
using System.Collections;
using LSE.INTERACTION;

namespace LSE.NARRATION
{
    /// <summary>
    /// Abstrakte Superklasse für die unterschiedlichen Aktionen 
    /// </summary>
    public abstract class N300_Action : MonoBehaviour
    {
        protected string nextActionId = "";
        public virtual string NextActionID
        {
            get { return nextActionId; }
            set { nextActionId = value;  }
        }

        public I100_Interaction interaction;
        public I200_Agent       agent;

        public enum N300_Action_Type { DIALOG, POI, EVENT, NOTE };
        protected N300_Action_Type type;
        public N300_Action_Type Type
        {
            get { return type; }
            set { type = value; }
        }

        public virtual void Enable()
        {
            gameObject.SetActive(true);
        }

        public virtual void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
