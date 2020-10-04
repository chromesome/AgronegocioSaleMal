using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Actions
{
    BuildFactory, BuildFarm, Fire, Deforest, MakeMoney, Mitigate, Upgrade
}

public class ActionItem
{
    public int id;
    public string label;
    public int cost;

    public delegate void TriggerAction();
    public string triggerAction;

    public ActionItem(int n_id, string n_label, string f_triggerAction, int n_cost)
    {
        id = n_id;
        label = n_label;
        triggerAction = f_triggerAction;
        cost = n_cost;
    }
    
}

public class ActionManager : MonoBehaviour
{
    public GameObject actionItemPrefab;
    private RectTransform ParentPanel;

    public List<ActionItem> actionItems;
    public Dictionary<int, ActionItem> actionItemsDictionary;

    void Start()
    {
        ParentPanel = GameObject.FindWithTag("Panel").GetComponent<RectTransform>();

        actionItems = new List<ActionItem>();
        actionItemsDictionary = new Dictionary<int, ActionItem>();

        // Podríamos usar esta lista diccionario para invocar actionitems de acá, en vez de instanciar nuevos por doquier.
        actionItems.Add(new ActionItem(0, "Build Farm", "ActionBuildFarm", 60));
        actionItems.Add(new ActionItem(1, "Build Factory", "ActionBuildFactory", 100));
        actionItems.Add(new ActionItem(2, "Fire", "ActionFire", 0));
        actionItems.Add(new ActionItem(3, "Deforest", "ActionDeforest", 0));
        actionItems.Add(new ActionItem(4, "MakeMoney", "ActionMakeMoney", 0));
        actionItems.Add(new ActionItem(5, "Mitigate", "ActionMitigate", 0));
        actionItems.Add(new ActionItem(6, "Upgrade", "ActionUpgrade", 10));

        foreach (ActionItem item in actionItems)
        {
            actionItemsDictionary.Add(item.id, item);
        }
    }

    internal void InstantiateActions(List<ActionItem> actions)
    {
        if (ParentPanel == null)
            ParentPanel = GameObject.FindWithTag("Panel").GetComponent<RectTransform>();

        ClearActionItems();

        foreach (ActionItem action in actions)
        {
            GameObject actionItemButton = (GameObject)Instantiate(actionItemPrefab);
            actionItemButton.transform.SetParent(ParentPanel, false);
            actionItemButton.transform.localScale = new Vector3(1, 1, 1);
            Button tempButton = actionItemButton.GetComponent<Button>();
            ActionProperties actionProperties = actionItemButton.GetComponent<ActionProperties>();

            actionProperties.actionId = action.id;
            actionProperties.actionCost = action.cost;
            actionItemButton.GetComponentInChildren<Text>().text = action.label + "($ " + actionProperties.actionCost + ")";
            tempButton.onClick.AddListener(() => TriggerAction(action.triggerAction));
        }
    }

    public void ClearActionItems()
    {
        foreach (Transform button in ParentPanel)
        {
            Destroy(button.gameObject);
        }
    }

    private void TriggerAction(string triggerAction)
    {
        // Hacemos esto aca para limpiar los action items.. hay que ver si es la mejor forma de resolverlo
        GameManager.instance.Invoke(triggerAction, 0f);
        // Averiguar como pasar parametros o si es necesario usar Coroutine
    }
}
