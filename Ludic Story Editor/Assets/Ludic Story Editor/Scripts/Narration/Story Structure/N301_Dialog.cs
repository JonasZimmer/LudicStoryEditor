using UnityEngine;
using System.Collections;

namespace LSE.NARRATION
{
    /// <summary>
    /// Dialogklasse
    /// </summary>
    public class N301_Dialog : N300_Action
    {
        public string text;
        public N301_Dialog() { Type = N300_Action_Type.DIALOG; }
    }
}