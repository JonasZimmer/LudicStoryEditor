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

        private void GetLSEParent()
        {
            lseParent = GameObject.Find("LSE");
            if (lseParent == null)
            {
                lseParent = new GameObject();
                lseParent.name = "LSE";
                Canvas c = 
                    lseParent.AddComponent<Canvas>();
                c.renderMode = RenderMode.ScreenSpaceOverlay;
            }
        }

        private void GetParent(string name, out GameObject obj)
        {
            Transform _parent = LSEParent.FindChild(name);
            if (_parent == null)
            {
                obj = new GameObject();
                obj.name = name;
                obj.transform.parent = LSEParent;
                obj.transform.position = Vector3.zero;
            }
            else
                obj = _parent.gameObject;
        }

        public GameObject NewCharacter()
        {
            GameObject character = new GameObject();
            character.AddComponent<LSE.VISUALIZATION.V200_Agent>();
            character.transform.parent = AgentParent;
            character.name = "Neuer Charakter";
            return character;
        }

        public GameObject NewStage()
        {
            GameObject stage = new GameObject();
            stage.AddComponent<LSE.VISUALIZATION.V300_Stage>();
            stage.transform.parent = StageParent;
            stage.name = "Neue Bühne";
            return stage;
        }
    }
}