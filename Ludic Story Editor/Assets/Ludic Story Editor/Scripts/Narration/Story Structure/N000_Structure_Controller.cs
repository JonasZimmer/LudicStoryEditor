using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LSE.IMPORT;

namespace LSE.NARRATION
{
    /*
     * Narration Basis Klasse N000
     * Struktur Controller
     * Zuständig für Laden/Speichern der Story Struktur
     * Interpretiert die importierten Daten und konstruiert daraus die Struktur
     */
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
            GameObject plot = new GameObject();
            if (_name != "")
                plot.name = _name;
            else
                plot.name = "Plot";
            plot.AddComponent<N100_Plot>();

            foreach (IMP200_ImportDataStruct _s in data)
            {
                GameObject sequenz = new GameObject();
                sequenz.transform.parent = plot.transform;
                sequenz.name = _s.name;
                //TODO add Characters
                foreach (IMP200_ImportDataStruct.IMP201_ActionDataStruct _a in _s.actions)
                {
                    GameObject action = new GameObject();
                    action.transform.parent = sequenz.transform;
                    action.name = _a.type.ToString("G") + "." + _a.data;
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