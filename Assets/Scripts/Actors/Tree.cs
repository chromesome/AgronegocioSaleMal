using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Actor
{
    public override void SetupActions()
    {
        actions = new List<Actions>();
        actions.Add(Actions.Fire);
    }
}
