using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farm : Actor, IMakeMoney
{
    public int level;
    public List<Sprite> farmStateSprites;
    public void MakeMoney()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.money += 1; // TODO: Revisar fórmula
        gameManager.moneyText.text = gameManager.money.ToString();
    }

    public override void SetupActions()
    {
        base.SetupActions();
        actions.Add(Actions.MakeMoney);
        actions.Add(Actions.Upgrade);
    }

    public void Upgradeable()
    {
        Debug.Log("Upgradeando granja");
        // Cambiar tile
        if (level < 5)
        {
            level++;
            this.GetComponent<SpriteRenderer>().sprite = farmStateSprites[level];
        }
    }
}
