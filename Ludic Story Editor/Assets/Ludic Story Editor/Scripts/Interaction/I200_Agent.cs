using UnityEngine;
using System.Collections;

namespace LSE.INTERACTION
{
    /// <summary>
    /// Superklasse für alle agierenden Elemente der Geschichte
    /// </summary>
    public class I200_Agent 
    {
        private string agentName;
        public string Name
        {
            get { return agentName; }
            set { agentName = value; }
        }

        private LSE.VISUALIZATION.V200_Agent visual;
    }
}