using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CanvasType
{
    MainMenu,
    GameMenu,
    EndMenu
}

public class CanvasManager : Singleton<CanvasManager>
{
    List<CanvasController> _canvasControllers;

    CanvasController _lastActiveCanvas;

    protected override void Awake()
    {
        base.Awake();

        _canvasControllers = GetComponentsInChildren<CanvasController>().ToList();

        _canvasControllers.ForEach(x => x.gameObject.SetActive(false));

        SwitchCanvas(CanvasType.MainMenu);
        
    }

    public void SwitchCanvas(CanvasType type)
    {
        if(_lastActiveCanvas != null)
        {
            _lastActiveCanvas.gameObject.SetActive(false);
        }

        CanvasController desiredCanvas = _canvasControllers.Find(x => x.type == type);

        if(desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(true);
            _lastActiveCanvas = desiredCanvas;
        }
    }
}
