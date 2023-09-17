using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManager { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            _gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public static GameManager Instance
    {
        get => _gameManager;
    }

}
