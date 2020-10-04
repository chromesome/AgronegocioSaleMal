using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Actor, IMakeMoney, IHarmful
{
    [SerializeField] float damage = 0.1f;
    [SerializeField] int levelToxic = 3;
    bool isSafe = true;


    public int maxUpgradeLevel;
    public int level;
    public int multiplier;
    public List<Sprite> factoryStateSprites;
    GameManager gameManager;
    public void Start()
    {
        level = 0;
        maxUpgradeLevel = 6;
        multiplier = 20;
        gameManager = GameManager.instance;
        InvokeRepeating("MakeMoney", 1f, 1f);
        InvokeToxicity();
    }

    public void MakeMoney()
    {
        gameManager.money += 2 * (level+1);
        gameManager.moneyText.text = gameManager.money.ToString();
    }

    public void Upgradeable()
    {
 
        // Cambiar tile
        if (level < maxUpgradeLevel)
        {
            this.GetComponent<SpriteRenderer>().sprite = factoryStateSprites[level];
        }
        gameManager.money -= level * multiplier;
        gameManager.moneyText.text = gameManager.money.ToString();
        Debug.Log("Upgradeando granja");
        level++;
        InvokeToxicity();
        SetupActions();
    }

    public override void SetupActions()
    {
        base.SetupActions();
        // if (level < maxUpgradeLevel)     TODO: GDD dice que se upgradea para x100pre
        {
            actions.Add(new ActionItem(6, "Mejorar", "ActionUpgrade", (level+1)* multiplier));
        }
    }

    public void MakeDamage()
    {
        Tile tile = this.GetComponentInParent<Tile>();
        tile.ReceiveDamage(damage * level);
    }

    private void InvokeToxicity()
    {
        if(isSafe && level >= levelToxic)
        {
            isSafe = false;
            InvokeRepeating("MakeDamage", 1f, 1f);
        }
    }
}
