using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace LSE.VISUALIZATION
{
    /// <summary>
    /// Basis Klasse für jedes Visualisierung
    /// </summary>
    public class V000_Visual : MonoBehaviour
    {
        //TODO: Add 3D visuals?
        [SerializeField]
        protected Sprite _s;

        private Image _i;
        protected Image _I
        {
            get 
            {
                if (_i == null)
                {
                    _i = gameObject.GetComponent<Image>();
                    if (_i == null) _i = gameObject.AddComponent<Image>();
                }
                return _i;
            }
        }

        private void Start()
        {
            if (_s != null && _I.sprite != _s)
            {
                _I.sprite = _s;
                _I.SetNativeSize();
            }
        }
    }
}