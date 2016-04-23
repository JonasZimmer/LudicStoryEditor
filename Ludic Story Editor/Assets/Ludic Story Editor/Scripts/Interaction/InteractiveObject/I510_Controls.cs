using UnityEngine;
using System.Collections;

namespace LSE.INTERACTION
{
    /// <summary>
    /// Super Klasse für alle Kontrollmechanismen auf einem Objekt.
    /// Der Konstruktor triggert die Hide Flags, sodass die abgeleiteten
    /// Klassen nicht sichtbar im Inspector angezeigt werden. 
    /// </summary>
    public class I510_Controls : MonoBehaviour
    {
        [ExecuteInEditMode]
        protected I510_Controls()
        {
            gameObject.GetComponent<I510_Controls>().hideFlags = 
                HideFlags.HideInInspector;
        }
    }
}