using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LSE.EVENT;

namespace LSE.NARRATION
{
    /// <summary>
    /// Mittleres Hierachieelement der Story Struktur
    /// Die Sequenz vereint eine Menge von Aktionen
    /// </summary>
    [System.Serializable]
    public class N200_Sequence : MonoBehaviour
    {
        /// <summary>
        /// Zuweisung des Bühnenbilds der Sequenz
        /// string stageName dient dabei als Hinweis
        /// welche Bühne zu verwenden ist, wird aus
        /// dem Originaldokument gezogen
        /// </summary>
        [SerializeField]
        private string stageName;
        public string StageName
        {
            get { return stageName; }
            set { stageName = value; }
        }

        [System.Serializable]
        public class N200_Sequence_StageAttributes
        {
            public LSE.VISUALIZATION.V300_Stage stage;
            public LSE.VISUALIZATION.V300_Stage.V301_CameraSettings cameraSettings;
        }
        [SerializeField]
        private N200_Sequence_StageAttributes stageAttributes;
        public N200_Sequence_StageAttributes StageAttributes
        {
            get { return stageAttributes; }
            set { stageAttributes = value; }
        }

        /// <summary>
        /// Aus der Originaldatei werden die Bezeichner für die
        /// in der Sequenz auftretenden Agenten gezogen. Diese
        /// Liste verknüpft den Bezeichner mit einer reellen
        /// Visualisierung eines Agenten.
        /// </summary>
        [System.Serializable]
        public class N200_Sequence_AgentStruct
        {
            public string agentName;
            public LSE.VISUALIZATION.V200_Agent agent;
        }
        [SerializeField]
        private List<N200_Sequence_AgentStruct> agents;
        public List<N200_Sequence_AgentStruct> Agents
        {
            get
            {
                if (agents == null)
                    agents = new List<N200_Sequence_AgentStruct>();
                return agents;
            }
        }
        public void AddAgent(string _n)
        {
            N200_Sequence_AgentStruct a = new N200_Sequence_AgentStruct();
            a.agentName = _n;
            Agents.Add(a);
        }

        /// <summary>
        /// Liste mit allen Aktionen der Sequenz
        /// activeAction beinhaltet die gerade aktivierten
        /// Aktionen.
        /// </summary>
        [SerializeField]
        private List<N300_Action> actions;
        public List<N300_Action> Actions
        {
            get
            {
                if (actions == null)
                    actions = new List<N300_Action>();
                return actions;
            }
        }
        //TODO Liste
        private N300_Action activeAction;

        /// <summary>
        /// Sequenz die nach dieser ausgeführt werden soll
        /// </summary>
        protected string nextSequenceId = "";
        public virtual string NextSequenceId
        {
            get { return nextSequenceId; }
            set { nextSequenceId = value; }
        }

        private void Start()
        {
            activeAction = null;
            E000_EventManager.Instance.Event("CAMERA", "Init", StageAttributes.cameraSettings.ToString());
        }
        
        /// <summary>
        /// Event Listener sollen nur verwendet werden, wenn das Objekt aktiv ist
        /// </summary>
        public void Enable()
        {
            E000_EventManager.Instance.AddEventListener("SEQUENCE", SequenceEventListener);
        }

        public void Disable()
        {
            E000_EventManager.Instance.DelEventListener("SEQUENCE", SequenceEventListener);
        }

        /// <summary>
        /// Der Event Listener zur eventId "SEQUENCE"
        /// </summary>
        /// <param name="_command">Auszuführender Befehl</param>
        /// <param name="_param">Optionale Parameter</param>
        private void SequenceEventListener(string _command, string _param = "")
        {
            switch (_command)
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

        /// <summary>
        /// Fügt eine Aktion zur Aktionsliste der Sequenz hinzu
        /// </summary>
        /// <param name="_a">Hinzuzufügende Aktion</param>
        public void AddAction(N300_Action _a)
        {
            Actions.Add(_a);
        }

        /// <summary>
        /// Aktiviert eine Aktion und deaktiviert die zuletzt aktive.
        /// </summary>
        /// <param name="a">Zu aktivierende Aktion</param>
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

        /// <summary>
        /// Überladene Funktion: Aktiviert eine Aktion basierend auf ihrer ID
        /// </summary>
        /// <param name="actionId">Die ID der zu aktivierenden Funktion</param>
        private void ActivateAction(string actionId)
        {
            N300_Action a = FindByID(actionId);
            if (a != null)
                ActivateAction(a);
        }

        /// <summary>
        /// Findet eine Aktion anhand ihrer ID
        /// </summary>
        /// <param name="actionId">Die ID der zu findenden Aktion</param>
        /// <returns>Aktion mit der mitgelieferten ID</returns>
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

        /// <summary>
        /// Beendet eine Aktion und aktiviert die Folgeaktion
        /// </summary>
        /// <param name="actionId">Vollendete Aktion</param>
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