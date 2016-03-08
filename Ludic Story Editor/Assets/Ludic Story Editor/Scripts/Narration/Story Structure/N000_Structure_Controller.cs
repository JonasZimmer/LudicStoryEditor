using UnityEngine;
using System.Collections;

namespace LudicStoryEditor.NARRATION
{
    /*
     * Narration Basis Klasse N000
     * Struktur Controller
     * Zuständig für Laden/Speichern der Story Struktur
     * Interpretiert die importierten Daten und konstruiert daraus die Struktur
     */
    public sealed class N000_Structure_Controller
    {
        private N000_Structure_Controller instance;
        public N000_Structure_Controller Instance
        {
            get
            {
                if (instance == null)
                    instance = new N000_Structure_Controller();
                return instance;
            }
        }

        //Erstellt die Story Struktur basierend auf den importierten Daten
        public void Initialisieren()
        {

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