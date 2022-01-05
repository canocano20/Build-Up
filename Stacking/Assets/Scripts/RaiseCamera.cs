using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseCamera : MonoBehaviour
{   
    [Header("Raise Attributes")]
    [SerializeField] private float _raiseValue;
    [SerializeField] private float _raiseSpeed;
    private Vector3 _previousePosition;
    private bool _canRaise;

    private void Update()
    {
        if(_canRaise && GameManager.GetInstance().CanGameStart)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + _raiseValue, transform.position.z), Time.deltaTime * _raiseSpeed);

            if(transform.position.y >= _previousePosition.y + _raiseValue)
            {
                transform.position = new Vector3(transform.position.x ,Mathf.Round(transform.position.y * 10f) / 10f, transform.position.z);
                _canRaise = false;
            }
        }
    }

    IEnumerator CanRaise(float time)
    {  
        yield return new WaitForSeconds(time);

        _previousePosition = transform.position;

        _canRaise = true;
    }

    public void RunRaise(float time)
    {
        StartCoroutine(CanRaise(time));
    }
}
