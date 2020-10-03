using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Actor, IMakeMoney
{

    public int level;
    public int multiplier;
    public List<Sprite> factoryStateSprites;
    GameManager gameManager;
    public void Start()
    {
        level = 0;
        multiplier = 20;
        gameManager = GameManager.instance;
        InvokeRepeating("MakeMoney", 1f, 1f);
    }
    public void MakeMoney()
    {
        gameManager.money += 1; // TODO: Revisar fórmula
        gameManager.moneyText.text = gameManager.money.ToString();
    }

    public void Upgradeable()
    {
 
        // Cambiar tile
        if (level < 6)
        {
            this.GetComponent<SpriteRenderer>().sprite = factoryStateSprites[level];
            gameManager.money -= level * multiplier;
            gameManager.moneyText.text = gameManager.money.ToString();
            Debug.Log("Upgradeando granja");
            level++;
            SetupActions();
        }
    }

    public override void SetupActions()
    {
        base.SetupActions();
        if (level < 6)
        {
            actions.Add(new ActionItem(6, "Upgrade", "ActionUpgrade", (level+1)*20));
        }
    }
}
