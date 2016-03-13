using UnityEngine;
using System.Collections;

namespace LSE.NARRATION
{
    /*
     * Abstrakte Superklasse für die unterschiedlichen Aktionen 
     */
    public abstract class N300_Action : MonoBehaviour
    {
        protected string nextActionId = "";
        public virtual string NextActionID
        {
            get { return nextActionId; }
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
