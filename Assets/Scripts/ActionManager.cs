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
    //public TriggerAction triggerAction;
    public string triggerAction;
    //public ActionItem(int n_id, string n_label, TriggerAction f_triggerAction)

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

    // Tenemos la referencia de todos los posibles action items, esto igual no es muy prolijo
    private List<ActionItem> actionItems;
    private Dictionary<int, ActionItem> actionItemsDictionary;

    void Start()
    {
        ParentPanel = GameObject.FindWithTag("Panel").GetComponent<RectTransform>();
        actionItems = new List<ActionItem>();
        actionItemsDictionary = new Dictionary<int, ActionItem>();

        // Todas las acciones del juego deben definirse en esta lista
        actionItems.Add(new ActionItem(0, "Build Farm", "ActionBuildFarm", 50));
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

    internal void InstantiateActions(List<Actions> actions)
    {
        ClearActionItems();

        foreach (Actions action in actions)
        {
            ActionItem actionItem;
            if (actionItemsDictionary.TryGetValue((int)action, out actionItem))
            {
                GameObject actionItemButton = (GameObject)Instantiate(actionItemPrefab);
                actionItemButton.transform.SetParent(ParentPanel, false);
                actionItemButton.transform.localScale = new Vector3(1, 1, 1);
                Button tempButton = actionItemButton.GetComponent<Button>();
                ActionProperties actionProperties = actionItemButton.GetComponent<ActionProperties>();

                actionProperties.actionId = (int)action;
                actionProperties.actionCost = actionItem.cost;
                actionItemButton.GetComponentInChildren<Text>().text = actionItem.label;
                tempButton.onClick.AddListener(() => TriggerAction(actionItem.triggerAction));
            }
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
    }
}
