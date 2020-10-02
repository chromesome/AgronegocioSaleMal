using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int id;
    public double resistance;

    public List<Actions> actions;

    void Awake()
    {
        
        SetupActions();
    }

    public virtual void SetupActions()
    {
        actions = new List<Actions>();
        // Sobreescribir en métodos que hereden de esta clase
        /*actions.Add(Actions.Fire);
        actions.Add(Actions.Deforest);
        actions.Add(Actions.MakeMoney);
        actions.Add(Actions.Mitigate);*/
    }

    internal List<Actions> GetActions()
    {
        // TODO Lista de acciones
        // Obtener lista de acciones en actors
        return actions;
    }

}
