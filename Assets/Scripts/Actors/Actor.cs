using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int id;
    public double resistance;

    public List<ActionItem> actions;

    void Awake()
    {
        
        SetupActions();
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
