using UnityEngine;
using System.Collections;

namespace LSE.INTERACTION
{
    public class I512_VerticalControl : I510_Controls
    {
        void Update()
        {
            float yAxisValue = Input.GetAxis("Vertical") / 3.0f;
            transform.Translate(new Vector3(0, yAxisValue, 0));
        }
    }
}