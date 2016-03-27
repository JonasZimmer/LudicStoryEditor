using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace LSE.IMPORT
{
    /// <summary>
    /// Importiert Daten aus einem Final Draft Dokument *.fdx
    /// </summary>
    public sealed class IMP001_FDX : IMP100_Import
    {
        private static IMP001_FDX instance;
        public static IMP001_FDX Instance
        {
            get
            {
                if (instance == null)
                    instance = new IMP001_FDX();
                return instance;
            }
        }

        IMP200_ImportDataStruct current;

        public override void LoadData(string path)
        {
            importedPlot = new List<IMP200_ImportDataStruct>();
            current = null;

            FileStream f = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Inheritable);
            StreamReader str = new StreamReader(f);
            //StreamReader str = File.OpenText(path);
            string data = str.ReadToEnd();
            
            f.Dispose();
            str.Dispose();
            /*
            int start= data.IndexOf("<Content>");
            int end = data.IndexOf("</Content>");
            data = data.Substring(start, end-start+10);/**/

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(data);
            XmlNodeList content = xmlDoc.GetElementsByTagName("Paragraph");

            foreach (XmlNode paragraph in content)
            {
                if (paragraph.Attributes["Type"] != null)
                    switch (paragraph.Attributes["Type"].Value)
                    {
                        case "Scene Heading":
                            if (current != null)
                                importedPlot.Add(current);
                            current = new IMP200_ImportDataStruct();
                            if (paragraph["Text"] != null)
                                current.name = paragraph["Text"].InnerText;
                            else
                                Debug.LogError("IMP101::LoadData, Scene Heading not containing a <Text> Element");
                            if (paragraph["SceneProperties"]["SceneArcBeats"] != null)
                                foreach (XmlNode sceneArcBeat in paragraph["SceneProperties"]["SceneArcBeats"])
                                {
                                    string c = sceneArcBeat.Attributes["Name"].Value;
                                    current.characters.Add(c);
                                }
                            else
                                Debug.LogError("IMP101::LoadData, Scene Heading of " + current.name + " not containing a <SceneArcBeats> Attribute");
                            break;
                        case "Action":
                            if (paragraph["Text"] != null)
                                current.actions.Add(
                                    new IMP200_ImportDataStruct.IMP201_ActionDataStruct(
                                        IMP200_ImportDataStruct.IMP201_ActionDataStruct.ImportActionType.Action,
                                        paragraph["Text"].InnerText));
                            break;
                        case "Character":
                            if (paragraph["Text"] != null)
                                current.actions.Add(
                                    new IMP200_ImportDataStruct.IMP201_ActionDataStruct(
                                        IMP200_ImportDataStruct.IMP201_ActionDataStruct.ImportActionType.Character,
                                        paragraph["Text"].InnerText));
                            break;
                        case "Dialogue":
                            if (paragraph["Text"] != null)
                                current.actions.Add(
                                    new IMP200_ImportDataStruct.IMP201_ActionDataStruct(
                                        IMP200_ImportDataStruct.IMP201_ActionDataStruct.ImportActionType.Dialogue,
                                        paragraph["Text"].InnerText));
                            break;
                        case "Parenthetical":
                            if (paragraph["Text"] != null)
                                current.actions.Add(
                                    new IMP200_ImportDataStruct.IMP201_ActionDataStruct(
                                        IMP200_ImportDataStruct.IMP201_ActionDataStruct.ImportActionType.Parenthetical,
                                        paragraph["Text"].InnerText));
                            break;
                    }               
            }
            if (current != null)
                importedPlot.Add(current);

            LSE.NARRATION.N000_Structure_Controller.Instance.Initialisieren(importedPlot, path.Substring(path.LastIndexOf("/")+1));
        }
    }
}