using UnityEngine;
using System.Collections;

namespace LSE
{
    public sealed class LSE100_LSEController
    {
        private static LSE100_LSEController instance;
        public static LSE100_LSEController Instance
        {
            get { if (instance == null) instance = new LSE100_LSEController(); return instance; }
        }

        private GameObject lseParent;
        public Transform LSEParent
        {
            get { if (lseParent == null) GetLSEParent(); return lseParent.transform; }
        }

        private void GetLSEParent()
        {
            lseParent = GameObject.Find("LSE");
            if (lseParent == null)
            {
                lseParent = new GameObject();
                lseParent.name = "LSE";
            }
        }
    }
}