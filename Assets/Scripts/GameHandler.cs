using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    int _playerHP = 3;
    float _playerEnergy = 1.0f; // 100%


    public int PlayerHP
    {
        get { return _playerHP; }
        set 
        { 
            _playerHP = value;
            Debug.LogFormat("Player HP = {0}", _playerHP); 
        }
    }

    GameObject[] _enemies;
    Queue<EnemyController2D> _movingEnemies = new Queue<EnemyController2D>();

    private void Awake()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Debug.Log("Enemy array length: " + _enemies.Length);

        for (int i = 0; i < _enemies.Length; i++)
        {
            if (_enemies[i] != null)
            {
                _movingEnemies.Enqueue(_enemies[i].GetComponent<EnemyController2D>());
                Debug.Log("Enemies in queue: " + _movingEnemies.Count);
            }
        }
    }
}
