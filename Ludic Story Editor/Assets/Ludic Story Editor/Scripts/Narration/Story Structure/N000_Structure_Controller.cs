using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LSE.IMPORT;

namespace LSE.NARRATION
{
    /// <summary>
    /// Narration Basis Klasse N000
    /// Struktur Controller
    /// Zuständig für Laden/Speichern der Story Struktur
    /// Interpretiert die importierten Daten und konstruiert daraus die Struktur
    /// </summary>
    public sealed class N000_Structure_Controller
    {
        private static N000_Structure_Controller instance;
        public static N000_Structure_Controller Instance
        {
            get
            {
                if (instance == null)
                    instance = new N000_Structure_Controller();
                return instance;
            }
        }

        //Erstellt die Story Struktur basierend auf den importierten Daten
        public void Initialisieren(List<IMP200_ImportDataStruct> data, string _name = "")
        {
            N100_Plot plot = new GameObject().AddComponent<N100_Plot>();
            if (_name != "")
                plot.name = _name;
            else
                plot.name = "Plot";

            foreach (IMP200_ImportDataStruct _s in data)
            {
                GameObject sObj = new GameObject();
                sObj.transform.parent = plot.transform;
                sObj.name = _s.name;
                N200_Sequence sequence = sObj.AddComponent<N200_Sequence>();
                plot.AddSequence(sequence);
                sequence.StageName = _s.name;
                if (_s.characters.Count > 0)
                    foreach (string name in _s.characters)
                        sequence.AddAgent(name);

                string agent = "NONE";
                foreach (IMP200_ImportDataStruct.IMP201_ActionDataStruct _a in _s.actions)
                {
                    GameObject aObj = new GameObject();
                    aObj.transform.parent = sequence.transform;
                    aObj.name = _a.type.ToString("G") + sequence.Actions.Count.ToString("000");

                    switch (_a.type)
                    {
                        case IMP200_ImportDataStruct.IMP201_ActionDataStruct.ImportActionType.Character:
                            GameObject.DestroyImmediate(aObj);
                            agent = _a.data;
                            if (agent.Contains("(CONT'D)"))
                                agent = agent.Replace("(CONT'D)", "");
                            break;
                        case IMP200_ImportDataStruct.IMP201_ActionDataStruct.ImportActionType.Action:
                            N399_Note n = aObj.AddComponent<N399_Note>();
                            n.note = _a.data;
                            break;
                        case IMP200_ImportDataStruct.IMP201_ActionDataStruct.ImportActionType.Parenthetical:
                            N399_Note no = aObj.AddComponent<N399_Note>();
                            no.note = _a.data;
                            break;
                        case IMP200_ImportDataStruct.IMP201_ActionDataStruct.ImportActionType.Dialogue:
                            N301_Dialog d = aObj.AddComponent<N301_Dialog>();
                            d.text = _a.data;
                            break;
                    }

                    if (aObj != null)
                    {
                        N300_Action _action = aObj.GetComponent<N300_Action>();
                        if (_action != null)
                        {
                            if (_action.agent == null)
                                _action.agent = new LSE.INTERACTION.I200_Agent();
                            _action.agent.Name = agent;
                            sequence.AddAction(_action);
                        }
                    }
                }
            }
        }

        //Speichert die Struktur in eine XML
        public void Speichern()
        {

        }

        //Lädt die Struktur aus der gespeicherten XML
        public void Laden()
        {

        }
    }
}