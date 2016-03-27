using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LSE.EVENT;

namespace LSE.NARRATION
{
    /*
     * Oberstes Hierachieelement der Story Struktur
     * Der Plot besteht aus vielen Sequenzen.
     * Er regelt die Reihenfolge, in der diese abgespielt werden
     */
    [System.Serializable]
    public class N100_Plot : MonoBehaviour
    {
        [SerializeField]
        private List<N200_Sequence> sequences;
        public List<N200_Sequence> Sequences
        {
            get
            {
                if (sequences == null)
                    sequences = new List<N200_Sequence>();
                return sequences;
            }
        }
        N200_Sequence activeSequence;

        //Event Listener sollen nur verwendet werden, wenn das Objekt aktiv ist
        public void Enable()
        {
            E000_EventManager.Instance.AddEventListener("PLOT", PlotEventListener);
        }

        public void Disable()
        {
            E000_EventManager.Instance.DelEventListener("PLOT", PlotEventListener);
        }

        //Der Event Listener zur eventId "SEQUENCE"
        private void PlotEventListener(string _command, string _param = "")
        {
            switch (_command)
            {
                case "activate":
                    if (_param != "")
                        ActivateSequence(_param);
                    break;
                case "finish":
                    if (_param != "")
                        FinishAction(_param);
                    break;
            }
        }

        public void AddSequence (N200_Sequence _s)
        {
            Sequences.Add(_s);
        }

        private void ActivateSequence(N200_Sequence s)
        {
            if (s != null)
            {
                if (activeSequence != null)
                    activeSequence.Disable();
                s.Enable();
                activeSequence = s;
            }
            else
                Debug.LogError("N100_Plot::ActivateSequence, Parameter s is null.");
        }

        private void ActivateSequence(string sequenceId)
        {
            N200_Sequence s = FindByID(sequenceId);
            if (s != null)
                ActivateSequence(s);
        }

        private N200_Sequence FindByID(string sequenceId)
        {
            foreach (N200_Sequence s in sequences)
            {
                if (s.name == sequenceId)
                    return s;
            }
            Debug.LogError("N100_Plot::FindByID, can't find ID " + sequenceId + " in List.");
            return null;
        }

        private void FinishAction(string sequenceId)
        {
            for (int i = 0; i < sequences.Count; i++)
            {
                if (sequences[i].name == sequenceId)
                {
                    // Wenn eine Folge Aktion definiert ist, diese ausführen
                    if (sequences[i].NextSequenceId != "")
                    {
                        ActivateSequence(sequences[i].NextSequenceId);
                        return;
                    }
                    // Sonst das nächste Element der Liste ausführen
                    else if (i + 1 < sequences.Count)
                    {
                        ActivateSequence(sequences[i + 1]);
                        return;
                    }
                }
            }
        }
    }
}