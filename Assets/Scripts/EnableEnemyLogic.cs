using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEnemyLogic : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private float _distantToPlayer;
    void Update()
    {
        _distantToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        if (_distantToPlayer > 30f)
        {
            gameObject.GetComponent<EnemyLogic>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<EnemyLogic>().enabled = true;
        }
    }
}
