using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public DontDestroy instance;
    void Awake()
    {
        /*GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        if (canvas != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);*/
        // Esto lo hacemos para que la instancia de GameManager este a lo largo de todo el juego.
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
