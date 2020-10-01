using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int id;
    public double resistance;
    Actor actor;

    List<Actions> tileActions;

    public Actor Actor {
        get => actor;

        set
        {
            actor = value;
            actor.transform.position = spawnPoint.transform.position;
        }
    }

    public List<Tile> neighbors;
    public Transform spawnPoint;

    [SerializeField] bool selectable = true;

    SpriteRenderer sprTile;
    bool selected = false;

    void Awake()
    {
        SetupActions();
    }

    void SetupActions()
    {
        // Sobre escribir en clase heredada
        tileActions = new List<Actions>();
        tileActions.Add(Actions.Build);
    }

    private void Start()
    {
        sprTile = this.GetComponent<SpriteRenderer>();
    }

    private void OnMouseEnter()
    {
        if(selectable && !selected)
        {
            sprTile.color = Color.gray;
        }
    }

    private void OnMouseExit()
    {
        if(!selected)
        {
            sprTile.color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        #region DEBUG
        Debug.Log("OnMouseDown " + this.name);
        Debug.Log("Actor " + Actor);
        Debug.Log("neighbors------");
        foreach (Tile item in neighbors)
        {
            Debug.Log(item.name);
        }
        #endregion

        if (selectable)
        {
            selected = true;
            sprTile.color = Color.red;
            GameManager.instance.SelectedTile = this;
        }
    }

    internal void Unselect()
    {
        selected = false;
        sprTile.color = Color.white;
    }

    //DEBUG
    internal string GetDetails()
    {
        String tileDetails = "";
        // text stream
        tileDetails += "Tile selected: " + this.name;
        tileDetails += "\nActor " + Actor;
        tileDetails += "\nneighbors------";
        foreach (Tile item in neighbors)
        {
            tileDetails += "\n" + item.name;
        }

        return tileDetails;
    }

    internal List<Actions> GetActions()
    {
        if (Actor != null)
        {
            return Actor.GetActions();
        }
        else
        {
            return tileActions;
        }
    }
}
