using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "New Game Event", menuName = "Game Event")]
    public class GameEvent : ScriptableObject
    {
        private HashSet<GameEventListener> _listeners = new HashSet<GameEventListener>();

        public void Raise()
        {
            foreach (GameEventListener listener in _listeners)
            {
                listener.OnEventRaised();
            }
        }

        public void AddListener(GameEventListener gameEventListener)
        {
            _listeners.Add(gameEventListener);
        }

        public void RemoveListener(GameEventListener gameEventListener)
        {
            _listeners.Remove(gameEventListener);
        }
    }



