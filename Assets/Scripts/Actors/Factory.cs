using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Actor, IMakeMoney
{

    public int level;
    public List<Sprite> factoryStateSprites;
    public void Start()
    {
        level = 0;
        InvokeRepeating("MakeMoney", 1f, 1f);
    }
    public void MakeMoney()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.money += 1; // TODO: Revisar fórmula
        gameManager.moneyText.text = gameManager.money.ToString();
    }

    public void Upgradeable()
    {
        Debug.Log("Upgradeando granja");
        // Cambiar tile
        if (level < 6)
        {
            level++;
            this.GetComponent<SpriteRenderer>().sprite = factoryStateSprites[level];
        }
    }

    public override void SetupActions()
    {
        base.SetupActions();
        actions.Add(Actions.MakeMoney);
        actions.Add(Actions.Upgrade);
    }
}
