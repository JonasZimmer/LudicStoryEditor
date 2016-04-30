using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

        private GameObject storyParent;
        public Transform StoryParent
        {
            get { if (storyParent == null) GetParent("LSE.Story", out storyParent); return storyParent.transform; }
        }

        private GameObject stageParent;
        public Transform StageParent
        {
            get { if (stageParent == null) GetParent("LSE.Stages", out stageParent); return stageParent.transform; }
        }

        private GameObject agentParent;
        public Transform AgentParent
        {
            get { if (agentParent == null) GetParent("LSE.Agents", out agentParent); return agentParent.transform; }
        }

        private GameObject CreateNewObject(string _name, Transform _parent = null)
        {
            GameObject obj = new GameObject();
            if (_parent != null)
                obj.transform.parent = _parent;
            obj.name = _name;
            obj.transform.position   = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            return obj;
        }

        private void GetLSEParent()
        {
            lseParent = GameObject.Find("LSE");
            if (lseParent == null)
            {
                lseParent = CreateNewObject("LSE");
                Canvas c = 
                    lseParent.AddComponent<Canvas>();
                c.renderMode = RenderMode.ScreenSpaceOverlay;
               /* CanvasScaler cs =
                    lseParent.AddComponent<CanvasScaler>();
                cs.uiScaleMode = 
                    CanvasScaler.ScaleMode.ScaleWithScreenSize;*/
            }
        }

        private void GetParent(string name, out GameObject obj)
        {
            Transform _parent = LSEParent.FindChild(name);
            if (_parent == null)
            {
                obj = CreateNewObject(name, LSEParent);
            }
            else
                obj = _parent.gameObject;
        }

        public GameObject NewCharacter()
        {
            GameObject character = CreateNewObject("Neuer Charakter", AgentParent);
            character.AddComponent<LSE.VISUALIZATION.V200_Agent>();
            return character;
        }

        public GameObject NewStage()
        {
            GameObject stage = CreateNewObject("Neue Bühne", StageParent);
            stage.AddComponent<LSE.VISUALIZATION.V300_Stage>();
            return stage;
        }
    }
}