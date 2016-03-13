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
        N300_Action activeAction;

        private void Start()
        {
            activeAction = null;
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
            switch (_com)
            {
                case "activate":
                    if (_param != "")
                        ActivateAction(_param);
                    break;
                case "finish":
                    if (_param != "")
                        FinishAction(_param);
                    break;
            }
        }

        private void ActivateAction(N300_Action a)
        {
            if (a != null)
            {
                if (activeAction != null)
                    activeAction.Disable();
                a.Enable();
                activeAction = a;
            }
            else
                Debug.LogError("N300_Sequence::ActivateAction, Parameter a is null.");
        }

        private void ActivateAction(string indicatorId)
        {
            N300_Action a = FindByID(indicatorId);
            if (a != null)
                ActivateAction(a);
        }

        private N300_Action FindByID(string actionId)
        {
            foreach (N300_Action a in actions)
            {
                if (a.name == actionId)
                    return a;
            }
            Debug.LogError("N300_Sequence::FindByID, can't find ID " + actionId + " in List.");
            return null;
        }

        private void FinishAction(string actionId)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i].name == actionId)
                {
                    // Wenn eine Folge Aktion definiert ist, diese ausführen
                    if (actions[i].NextActionID != "")
                    {
                        ActivateAction(actions[i].NextActionID);
                        return;
                    }
                    // Sonst das nächste Element der Liste ausführen
                    else if (i+1 < actions.Count)
                    {
                        ActivateAction(actions[i + 1]);
                        return;
                    }
                }
            }
        }
    }
}