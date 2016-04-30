using UnityEngine;
using System.Collections;
using LSE.VISUALIZATION;

namespace LSE.INTERACTION
{
    public class I513_FollowControl : I510_Controls
    {
        [SerializeField]
        protected V000_Visual target;
        public V000_Visual Target
        {
            set { target = value; Debug.LogError("I513::SetTarget name " + target.name); }
        }

        protected void Update()
        {
            if (target == null) return;
            Vector3 pos = target.gameObject.transform.position;
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }
}