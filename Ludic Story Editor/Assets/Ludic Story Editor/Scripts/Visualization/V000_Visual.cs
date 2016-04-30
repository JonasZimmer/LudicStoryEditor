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
        /*protected V000_Visual()
        {
            if (sortOrdMax != sortOrdMin)
                SortingOrder = sortOrdMax / 2;
            else
                SortingOrder = sortOrdMax;
        }*/

        //TODO: Add 3D visuals?
        [SerializeField]
        protected Sprite _s;
        public Sprite _S
        {
            get { return _s; }
            set
            {
                _s = value;
                if (_s != null)
                {
                    _I.sprite = _s;
                    _I.SetNativeSize();
                    name = _s.name;
                }
            }
        }

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

        [SerializeField]//[HideInInspector]
        private Canvas canvas;
        protected Canvas _Canvas
        {
            get 
            {
                return null;
                if (canvas == null)
                {
                    canvas = gameObject.GetComponent<Canvas>();
                    if (canvas == null)
                        canvas = gameObject.AddComponent<Canvas>();
                    canvas.overrideSorting = true;
                    //canvas.hideFlags = HideFlags.HideInInspector;
                } 
                return canvas;
            }
        }

        [SerializeField]//[HideInInspector]
        private int sortingOrder;
        public int SortingOrder
        {
            get { return sortingOrder; } // _Canvas.sortingOrder; }
            set 
            { 
                sortingOrder = value;
                //_Canvas.sortingOrder = sortingOrder;
                CalcParallexScrollingFactor();
            }
        }

        [SerializeField]//[HideInInspector]
        protected int sortOrdMin = 0;
        public int SortOrdMin { get { return sortOrdMin; } }
        [SerializeField]//[HideInInspector]
        protected int sortOrdMax = 0;
        public int SortOrdMax { get { return sortOrdMax; } }

        private void Start()
        {
            if (_s != null && _I.sprite != _s)
            {
                _I.sprite = _s;
                _I.SetNativeSize();
            }
        }

        [SerializeField]
        protected float parallexScrollingFactor;
        protected virtual void CalcParallexScrollingFactor() { }
    }
}