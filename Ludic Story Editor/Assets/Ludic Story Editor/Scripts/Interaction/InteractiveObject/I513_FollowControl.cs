using UnityEngine;
using System.Collections;
using LSE.VISUALIZATION;

namespace LSE.INTERACTION
{
    public class I513_FollowControl : I510_Controls
    {
        [SerializeField]
        protected V000_Visual target;

        protected void Update()
        {
            Vector3 pos = target.gameObject.transform.position;
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }
}