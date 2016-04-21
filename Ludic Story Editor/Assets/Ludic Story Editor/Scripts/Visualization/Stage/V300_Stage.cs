using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LSE.EVENT;

namespace LSE.VISUALIZATION
{
    /// <summary>
    /// Basis Klasse für das Bühnenbild
    /// </summary>
    public class V300_Stage : MonoBehaviour
    {

        [System.Serializable]
        public class V301_CameraSettings
        {
            public Vector3  startPos;
            public float    leftBorder;
            public float    rightBorder;
            /// <summary>
            /// Formatiert die Kameraeinstellungen um sie als Parameter
            /// für das initialisierende Event zu nutzen
            /// </summary>
            /// <returns>formatierter String</returns> 
            public override string ToString()
            {
                string result = "";
                result += startPos.x.ToString("G4") + ";";
                result += startPos.y.ToString("G4") + ";";
                result += startPos.z.ToString("G4") + ";";
                result += leftBorder.ToString("G4") + ";";
                result += rightBorder.ToString("G4");
                return result; 
            }
        }
        [SerializeField]
        private V301_CameraSettings cameraSetting;

        [SerializeField]
        private List<V310_Background> backgrounds;
        public List<V310_Background> Backgrounds
        {
            get
            {
                if (backgrounds == null)
                    backgrounds = new List<V310_Background>();
                return backgrounds;
            }
        }

        [SerializeField]
        private List<V320_Object> sceneObjects;
        public List<V320_Object> SceneObjects
        {
            get
            {
                if (sceneObjects == null)
                    sceneObjects = new List<V320_Object>();
                return sceneObjects;
            }
        }

        [SerializeField]
        protected bool useParallexScrolling;
        
        private void OnEnable()
        {
            if (useParallexScrolling)
                E000_EventManager.Instance.AddEventListener("CAMERA", CameraEventListener);
        }

        private void OnDisable()
        {
            E000_EventManager.Instance.DelEventListener("CAMERA", CameraEventListener);
        }

        private void Start()
        {
            E000_EventManager.Instance.Event("CAMERA", "Init", cameraSetting.ToString());
        }

        private void CameraEventListener(string _command, string _param = "")
        {
            if (_command == "Move")
            {
                float deltaX = float.Parse(_param);
                if (deltaX != 0)
                {
                    foreach (V310_Background b in backgrounds)
                        b.Move(deltaX);
                    foreach (V320_Object o in sceneObjects)
                        o.Move(deltaX);
                }
            }
        }
    }
}