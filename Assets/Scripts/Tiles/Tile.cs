﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour, IDestructible
{
    public int level;
    public float resistance = 100f;
    [SerializeField] float maxResistance = 100f;
    Actor actor;
    Fire fire;

    List<ActionItem> tileActions;

    public Actor Actor {
        get => actor;

        set
        {
            actor = value;
            if(actor != null)
            {
                actor.transform.position = spawnPoint.transform.position;
            }
        }
    }

    public Fire Fire
    {
        get => fire;

        set
        {
            fire = value;
            if(fire != null)
            {
                fire.transform.parent = this.transform;
                fire.transform.position = spawnPoint.transform.position;
            }
        }
    }

    public List<Tile> neighbours;
    public Transform spawnPoint;

    [SerializeField] bool selectable = true;

    SpriteRenderer sprTile;
    bool selected = false;

    void Awake()
    {
        SetupActions();
    }

    public void SetupActions()
    {
        tileActions = new List<ActionItem>();
        if (actor == null)
        {
            if (level > 2 && level < 6)
            {
                tileActions.Add(new ActionItem(0, "Build Farm", "ActionBuildFarm", 60));
                tileActions.Add(new ActionItem(1, "Build Factory", "ActionBuildFactory", 100));
            }
            else if (level >= 6)
            {
                tileActions.Add(new ActionItem(2, "Fire", "ActionFire", 0));
            }
        }
        
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
        foreach (Tile item in neighbours)
        {
            Debug.Log(item.name);
        }
        #endregion

        if(this != GameManager.instance.SelectedTile)
        {
            if (selectable)
            {
                selected = true;
                sprTile.color = Color.red;
                GameManager.instance.SelectedTile = this;
            }
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
        foreach (Tile item in neighbours)
        {
            tileDetails += "\n" + item.name;
        }

        return tileDetails;
    }

    internal List<ActionItem> GetActions()
    {
        if(Fire != null)
        {
            return Fire.GetActions();
        }
        else if (Actor != null)
        {
            return Actor.GetActions();
        }
        else
        {
            return tileActions;
        }
    }

    public float GetMaxHealth()
    {
        return maxResistance;
    }

    public float GetCurrentHealth()
    {
        return resistance;
    }

    public float ReceiveDamage(float damage)
    {
        Debug.Log(this.name + " received damage = " + damage);
        Tree tree = Actor as Tree;
        if(tree != null)
        {
            damage = tree.ReceiveDamage(damage);
        }
        else if(IsOnFire())
        {
            Fire.Consume();
        }

        // take remaining damage
        resistance -= damage;

        if(resistance < 0)
        {
            // TODO: DemoteTile();
        }

        // Tile receives all damage
        return 0;
    }

    public bool IsOnFire()
    {
        return this.fire != null ? true : false;
    }

    public void TrySetFire(Fire fire)
    {
        Debug.Log("Try to set fire " + this.name);
        // Si es agua no hagas nada
        if(level < 9)
        {
            if(actor == null)
            {
                bool setFire = true;

                float fireChance = resistance + level - RiskModifier();
                float rndNumber = UnityEngine.Random.Range(0, 99);

                setFire = fireChance < rndNumber ? true : false;

                if (setFire)
                {
                    this.Fire = Instantiate(fire);
                }
            }
        }
    }

    private float RiskModifier()
    {
        float riskModifier = 0f;

        foreach (Tile tile in neighbours)
        {
            if (tile.IsOnFire())
            {
                riskModifier += 1f;
            }
        }

        return riskModifier;
    }
}
