using UnityEngine;
using System.Collections;

namespace LSE.VISUALIZATION
{
    /// <summary>
    /// Basisklasse für die Hintergrundelemente der Stage
    /// </summary>
    public class V310_Background : V100_VisualObject
    {
        private V310_Background()
        {
            sortOrdMin = 000;
            sortOrdMax = 100;
        }

        protected override void CalcParallexScrollingFactor()
        {
            parallexScrollingFactor = -(float)SortingOrder / (float)sortOrdMax;
        }
    }
}