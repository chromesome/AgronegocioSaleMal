using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int id;
    public double resistance;

    int x;
    int y;

    public List<ActionItem> actions;

    public int X {
        get => x;
        set
        {
            x = value;
            SortingLayer();
        }
    }

    

    public int Y
    {
        get => y;
        set
        {
            y = value;
            SortingLayer();
        }
    }

    void Awake()
    {
        
        SetupActions();
    }

    protected virtual void SortingLayer()
    {
        SpriteRenderer sprRenderer = GetComponent<SpriteRenderer>();
        if(sprRenderer != null)
        {
            sprRenderer.sortingOrder = y == 0 ? y + 1 : y + 4;
        }
    }

    public virtual void SetupActions()
    {
        actions = new List<ActionItem>();
        // Sobreescribir en métodos que hereden de esta clase
    }

    internal List<ActionItem> GetActions()
    {
        // TODO Lista de acciones
        // Obtener lista de acciones en actors
        return actions;
    }

}
