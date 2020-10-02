using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Actor, IMakeMoney
{
    public void Start()
    {
        InvokeRepeating("MakeMoney", 1f, 1f);
    }
    public void MakeMoney()
    {
        GameManager gameManager = GameManager.instance;
        gameManager.money += 1;
        gameManager.moneyText.text = gameManager.money.ToString();
    }

    public override void SetupActions()
    {
        base.SetupActions();
        actions.Add(Actions.MakeMoney);
    }

    public void Upgradeable()
    {
        // Update de factory
    }
}
