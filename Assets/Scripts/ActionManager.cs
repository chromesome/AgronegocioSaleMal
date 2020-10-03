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

    void Start()
    {
        ParentPanel = GameObject.FindWithTag("Panel").GetComponent<RectTransform>();
    }

    internal void InstantiateActions(Tile tile, List<ActionItem> actions)
    {
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
    }
}
