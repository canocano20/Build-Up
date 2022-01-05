using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
 
    #region Serialize Fields
    [SerializeField] private GameObject _stackPrefab;
    [SerializeField] private float _spawnTime;
    [SerializeField] private float _spawnHeightChange;

    

    [Header("Events")]
    [SerializeField] GameEvent _onMouseClick;
    [SerializeField] GameEvent _onAfterMouseClick;

    #endregion

    #region Private Fields
    
    private bool _canSpawn = true;
    private float _spawnCount = 0;

    #endregion

    #region Unity Callbacks
    private void Update()
    {
        if(Input.GetButtonDown("Fire1") && _canSpawn && GameManager.GetInstance().CanGameStart)
        {
            EventManager.GetInstance().RaiseEvent(_onMouseClick);
            EventManager.GetInstance().RaiseEvent(_onAfterMouseClick);
        }
    }

    #endregion

    #region Enumerators
    IEnumerator Spawn(float time, GameObject go)
    {
        yield return new WaitForSeconds(time);

        _spawnCount += 1;

        GameObject newStack = Instantiate(go, go.transform.position, go.transform.rotation);

        newStack.transform.localScale = StackController.PreviousStack.transform.localScale;
        newStack.transform.position = StackController.PreviousStack.transform.position + new Vector3(0f, .4f, 0f);
        
        if(_canSpawn == false)
        {
            Destroy(newStack);
        }
    }

    #endregion

    #region Public Funtions
    public void SpawnStack()
    {
        
        StartCoroutine(Spawn(_spawnTime, _stackPrefab));
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
        _canSpawn = false;
    }

    public void StartSpawn()
    {
        _canSpawn = true;
    }

    public void SpawnFirstStack()
    {
        GameObject go = Instantiate(_stackPrefab, _stackPrefab.transform.position, _stackPrefab.transform.rotation);
        go.name = "First";
    }
    
    #endregion
}
