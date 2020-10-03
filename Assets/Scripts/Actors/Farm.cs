using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Actor, IMakeMoney
{
    public int maxUpgradeLevel;
    public int level;
    public int multiplier;
    public int upgradeCost;
    public List<Sprite> farmStateSprites;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
        maxUpgradeLevel = 5;
        multiplier = 1;
        upgradeCost = 10;
    }
    public void MakeMoney()
    {
        gameManager.money += multiplier * (level+1); // Agregamos 1 porque el level arranca en 0
        gameManager.moneyText.text = gameManager.money.ToString();
    }

    public override void SetupActions()
    {
        base.SetupActions();
        actions.Add(new ActionItem(4, "MakeMoney", "ActionMakeMoney", 0));
        if (level < maxUpgradeLevel)
        {
            actions.Add(new ActionItem(6, "Upgrade", "ActionUpgrade", upgradeCost));
        }
    }

    public void Upgradeable()
    {
        // Cambiar tile
        if (level < maxUpgradeLevel)
        {
            this.GetComponent<SpriteRenderer>().sprite = farmStateSprites[level];
            gameManager.money -= 10;
            gameManager.moneyText.text = gameManager.money.ToString();
            level++;
            SetupActions();
        }
    }
}
