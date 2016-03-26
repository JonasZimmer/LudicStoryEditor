using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LSE.IMPORT
{
    public class IMP200_ImportDataStruct
    {
        public string name;
        public List<string> characters; 
        public class IMP201_ActionDataStruct
        {
            public enum ImportActionType { Character, Action, Dialogue, Parenthetical };
            public ImportActionType type;
            public string data;

            public IMP201_ActionDataStruct(ImportActionType _t, string _d)
            {
                type = _t; data = _d;
            }
        }
        public List<IMP201_ActionDataStruct> actions;

        public IMP200_ImportDataStruct()
        {
            name = "";
            characters = new List<string>();
            actions = new List<IMP201_ActionDataStruct>();
        }
    }
}