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
            public Vector3 startPos = new Vector3(0,0,-10);
            public LSE.INTERACTION.I501_ControllableObject.ControllerType cType;
            public float leftBorder, rightBorder, topBorder, lowerBorder;
            public V000_Visual target;
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
                result += rightBorder.ToString("G4") + ";";
                result += topBorder.ToString("G4") + ";";
                result += lowerBorder.ToString("G4") + ";";
                result += cType.ToString() + ";";
                if (target == null) result += "0";
                else result += target.GetInstanceID();
                return result;
            }
        }

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
        protected bool useParallexScrolling = true;
        public bool UseParallexScrolling
        {
            get { return useParallexScrolling; }
            set { useParallexScrolling = value; }
        }
        
        private void OnEnable()
        {
            if (useParallexScrolling)
                E000_EventManager.Instance.AddEventListener("CAMERA", CameraEventListener);
        }

        private void OnDisable()
        {
            E000_EventManager.Instance.DelEventListener("CAMERA", CameraEventListener);
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

        private GameObject CreateNewObject(string _name, Transform _parent = null)
        {
            GameObject obj = new GameObject();
            if (_parent != null)
                obj.transform.parent = _parent;
            obj.name = _name;
            obj.transform.position = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            return obj;
        }

        public void AddBackground()
        {
            GameObject obj = CreateNewObject("Background_" + Backgrounds.Count.ToString("D3"), transform);
            Backgrounds.Add(obj.AddComponent<V310_Background>());
        }

        public void AddSceneobject()
        {
            GameObject obj = CreateNewObject("Foreground_" + SceneObjects.Count.ToString("D3"), transform);
            SceneObjects.Add(obj.AddComponent<V320_Object>());
        }

        public void RemoveBackground(int index)
        {
            Backgrounds.RemoveAt(index);
        }

        public void RemoveSceneobject(int index)
        {
            SceneObjects.RemoveAt(index);
        }
    }
}