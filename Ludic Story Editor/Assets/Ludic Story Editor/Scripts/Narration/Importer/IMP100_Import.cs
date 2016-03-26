using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LSE.IMPORT
{
    /// <summary>
    /// Base Klasse für die unterschiedlichen Importer
    /// </summary>
    public abstract class IMP100_Import
    {
        protected List<IMP200_ImportDataStruct> importedPlot;
        public abstract void LoadData(string path);
    }
}