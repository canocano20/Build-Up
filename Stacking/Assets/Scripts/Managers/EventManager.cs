using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    [SerializeField] GameEvent _onGameStart;
    [SerializeField] GameEvent _onGameEnd;
    [SerializeField] GameEvent _onMouseClick;
    
    public void RaiseEvent(GameEvent gameEvent)
    {
        gameEvent.Raise();
    }
}
