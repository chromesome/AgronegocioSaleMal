using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Actor, IMakeMoney
{
    public int level;
    public List<Sprite> farmStateSprites;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
    }
    public void MakeMoney()
    {
        gameManager.money += 1; // TODO: Revisar fórmula
        gameManager.moneyText.text = gameManager.money.ToString();
    }

    public override void SetupActions()
    {
        base.SetupActions();
        actions.Add(new ActionItem(4, "MakeMoney", "ActionMakeMoney", 0));
        if (level < 5)
        {
            actions.Add(new ActionItem(6, "Upgrade", "ActionUpgrade", 10));
        }
    }

    public void Upgradeable()
    {
        // Cambiar tile
        if (level < 5)
        {
            this.GetComponent<SpriteRenderer>().sprite = farmStateSprites[level];
            gameManager.money -= 10;
            gameManager.moneyText.text = gameManager.money.ToString();
            level++;
            SetupActions();
        }
    }
}
