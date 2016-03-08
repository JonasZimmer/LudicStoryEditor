using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LSE.EVENT;

namespace LSE.NARRATION
{
    /*
     * Mittleres Hierachieelement der Story Struktur
     * Die Sequenz vereint eine Menge von Aktionen
     */
    public class N200_Sequence : MonoBehaviour
    {
        List<N300_Action> actions;

        private void Start()
        {
            //Beispielhafter Aufruf
            E000_EventManager.Instance.Event(
                "SEQUENCE",
                "activate",
                "Action_001");
        }

        //Event Listener sollen nur verwendet werden, wenn das Objekt aktiv ist
        public void OnEnable()
        {
            E000_EventManager.Instance.AddEventListener("SEQUENCE", SequenceEventListener);
        }

        public void OnDisable()
        {
            E000_EventManager.Instance.DelEventListener("SEQUENCE", SequenceEventListener);
        }

        //Der Event Listener zur eventId "SEQUENCE"
        private void SequenceEventListener(string _com, string _param = "")
        {
            if (_com == "activate")
            {
                if (_param != "")
                    GoTo(_param);
            }
        }

        private void GoTo(string actionId)
        {
            foreach (N300_Action a in actions)
            {
                if (a.name == actionId)
                {
                    a.gameObject.SetActive(true);
                }
            }
        }
    }
}