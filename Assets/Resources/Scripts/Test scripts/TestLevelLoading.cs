using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelLoading : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            gameManager.WinLevel();
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            gameManager.LoseGame();
        }
    }
}
