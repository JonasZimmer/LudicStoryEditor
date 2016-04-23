using UnityEngine;
using System.Collections;

namespace LSE.INTERACTION
{
    public class I511_HorizontalControl : I510_Controls
    {
        void Update()
        {
            float xAxisValue = Input.GetAxis("Horizontal") / 3.0f;
            transform.Translate(new Vector3(xAxisValue, 0, 0));
        }
    }
}