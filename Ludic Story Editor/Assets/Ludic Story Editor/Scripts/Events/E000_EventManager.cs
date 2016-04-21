using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LSE.EVENT
{
    /*
     * Event Verwaltungssystem
     * Listener Funktionen müssen mit einer ID registriert werden, danach können Events
     * mithilfe dieser IDs gefeuert werden und die zugehörigen Funktionen werden 
     * ausgeführt
     */ 
    public sealed class E000_EventManager
    {
        private static E000_EventManager instance;
        public static E000_EventManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new E000_EventManager();
                return instance;
            }
        }

        // Delegate Funktion zur Registrierung als EventListener 
        public delegate void EventListener(string command, string parameter = "");
        // Dieses Dictionary enthält eine Liste aller zu einer Event ID registrierten Funktionen
        private Dictionary<string, List<EventListener>> eventList;

        private E000_EventManager()
        {
            eventList = new Dictionary<string, List<EventListener>>();
        }

        // Event Funktion, ruft die Listener Funktionen aus der Liste heraus auf
        public void Event(string eventId, string command, string parameter = "")
        {
            if (eventList.ContainsKey(eventId))
            {
                Debug.Log("Event \"" + eventId + "\" fired. Contact " + eventList[eventId].Count + " Listeners.\n" + "(" + eventId +", " + command + ", " + parameter + ")");
                foreach(EventListener e in eventList[eventId])
                    if (e != null)
                        e(command, parameter);
            }
        }

        // Fügt der Event ID eine neue Listener Funktion hinzu
        public void AddEventListener(string eventId, EventListener func)
        {
            if (eventList.ContainsKey(eventId))
            {
                List<EventListener> list = eventList[eventId];
                list.Add(func);
            }
            else
            {
                List<EventListener> list = new List<EventListener>();
                list.Add(func);
                eventList.Add(eventId, list);
            }
        }

        // Entfernt eine Funktion von der Liste
        public void DelEventListener(string eventId, EventListener func)
        {
            if (eventList.ContainsKey(eventId))
            {
                List<EventListener> list = eventList[eventId];
                foreach (EventListener e in list)
                {
                    if (e == func)
                    {
                        list.Remove(e);
                        return;
                    }
                }
            }
        }

        // Resettet die Event Listener Liste
        public void ClearEventListener()
        {
            eventList.Clear();
        }
    }
}