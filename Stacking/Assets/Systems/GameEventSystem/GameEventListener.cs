using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] GameEvent _gameEvent;
        [SerializeField] UnityEvent _unityEvent;

        public void OnEnable()
        {
            _gameEvent.AddListener(this);
        }

        private void OnDisable()
        {
            _gameEvent.RemoveListener(this);
        }

        public void OnEventRaised()
        {
            _unityEvent?.Invoke();
        }
    }


