using UnityEngine;
using System.Collections;

namespace LSE.NARRATION
{
    /// <summary>
    /// Event Aktions Klasse, sendet bei Start ein Event aus
    /// </summary>
    public class N303_Event : N300_Action
    {
        public enum EventType { PLOT, SEQUENCE, ACTION, DIALOG };
        public EventType eventType;
        public string command;
        public string param;
        // Soll automatisch zum nächsten Element gegangen werden, oder regelt das das ausgesendete Event?
        public bool finish;

        public override void Enable()
        {
            LSE.EVENT.E000_EventManager.Instance.Event(
                eventType.ToString("G"), command, param);
            // Ist 
            if (finish)
                LSE.EVENT.E000_EventManager.Instance.Event(
                "SEQUENZ", "finish", name);
        }
    }
}