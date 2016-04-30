using UnityEngine;
using System.Collections;

namespace LSE.VISUALIZATION
{
    /// <summary>
    /// Klasse zur Visualisierung der Agenten
    /// </summary>

    public class V200_Agent : V000_Visual
    {
        private V200_Agent()
        {
            sortOrdMin = 100;
            sortOrdMax = 200;
        }

        [SerializeField]
        private LSE.INTERACTION.I200_Agent interaction;
        [SerializeField]
        private string charName;
        public string CharName
        {
            get { return charName; }
            set { charName = value; name = charName; }
        }
        public Sprite _S
        {
            get { return _s; }
            set { _s = value; }
        }
    }
}