using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Framework.Events {
    public class UnityEventTyped : UnityEvent<GameEvent> { }
    
    public class EventManager : MonoBehaviour {
        private Dictionary<Type, UnityEventTyped> eventDictionary;
        private static EventManager eventManager;

        private static EventManager Instance {
            get {
                if (!eventManager) {
                    eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                    if (eventManager) {
                        if (eventManager != null) eventManager.Init();
                    }
                    else {
                        Debug.LogError(
                            "There needs to be one active EventManger script on a GameObject in your scene.");
                    }
                }

                return eventManager;
            }
        }

        private void Init() {
            if (eventDictionary == null) {
                eventDictionary = new Dictionary<Type, UnityEventTyped>();
            }
        }

        public static void StartListening<T>(UnityAction<GameEvent> listener) where T : GameEvent {
            if (Instance.eventDictionary.TryGetValue(typeof(T), out var thisEvent)) {
                thisEvent.AddListener(listener);
            }
            else {
                thisEvent = new UnityEventTyped();
                thisEvent.AddListener(listener);
                Instance.eventDictionary.Add(typeof(T), thisEvent);
            }
        }

        public static void StopListening<T>(UnityAction<GameEvent> listener) where T : GameEvent {
            if (eventManager == null) return;
            if (Instance.eventDictionary.TryGetValue(typeof(T), out var thisEvent)) {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent<T>(T e) where T : GameEvent{
            if (Instance.eventDictionary.TryGetValue(e.GetType(), out var thisEvent)) {
                thisEvent.Invoke(e);
            }
        }
    }
}