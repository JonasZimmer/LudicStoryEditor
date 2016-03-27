using UnityEngine;
using System.Collections;

namespace LSE.NARRATION
{
    /*
     * Event Aktions Klasse, sendet bei Start ein Event aus
     */
    public class N399_Note : N300_Action
    {
        public string note;

        public override void Enable()
        {
            LSE.EVENT.E000_EventManager.Instance.Event(
                "SEQUENZ", "finish", name);
        }
    }
}