using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Template : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;

    private Vector2 mousePos;

    //voy a poner un comentario aca porque puedo
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y));
    }

}