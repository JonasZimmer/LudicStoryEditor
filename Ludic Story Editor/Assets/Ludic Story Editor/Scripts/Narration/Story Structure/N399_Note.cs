using UnityEngine;
using System.Collections;

namespace LSE.NARRATION
{
    /// <summary>
    /// Beinhaltet Anmerkungen aus der importierten Drehbuchdatei.
    /// Beim Aufruf wird einfach zum Folge Element gesprungen.
    /// </summary>
    public class N399_Note : N300_Action
    {
        public N399_Note() { Type = N300_Action_Type.NOTE; }

        public string note;
        
        public override void Enable()
        {
            LSE.EVENT.E000_EventManager.Instance.Event(
                "SEQUENZ", "finish", name);
        }
    }
}