using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LSE.VISUALIZATION
{
    /// <summary>
    /// Basisklasse eines visuellen Objektes in der Szene
    /// </summary>
    public class V100_VisualObject : MonoBehaviour
    {
        //TODO: Add 3D visuals?
        [SerializeField]
        protected Sprite _s;
        [SerializeField]
        protected Vector3 startPosition;
        protected Vector3 curPosition;
        public    Vector3 Position
        {
            get { return curPosition; }
        }
        [SerializeField]
        protected float parallexScrollingFactor;

        protected void Start()
        {
            curPosition = startPosition = transform.position;
        }
        /// <summary>
        /// Bewegt das Objekt basierend auf dem parallexScrollingFactor
        /// </summary>
        /// <param name="deltaX">Delta X der Kameraposition, Grundlage der KameraBewegung</param>
        public void Move(float deltaX)
        {
            curPosition = startPosition + 
                new Vector3(deltaX * parallexScrollingFactor, 0, 0);
        }

        protected void Update()
        {
            transform.position = curPosition;
        }
    }
}