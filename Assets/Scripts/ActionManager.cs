using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Actions
{
    Build, Fire, Deforest, MakeMoney, Mitigate
}

public class ActionItem
{
    public int id;
    public string label;

    public delegate void TriggerAction();
    //public TriggerAction triggerAction;
    public string triggerAction;
    //public ActionItem(int n_id, string n_label, TriggerAction f_triggerAction)
    public ActionItem(int n_id, string n_label, string f_triggerAction)
    {
        id = n_id;
        label = n_label;
        triggerAction = f_triggerAction;
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
        actionItems.Add(new ActionItem(0, "Build", "ActionBuild"));
        actionItems.Add(new ActionItem(1, "Fire", "ActionFire"));
        actionItems.Add(new ActionItem(2, "Deforest", "ActionDeforest"));
        actionItems.Add(new ActionItem(3, "MakeMoney", "ActionMakeMoney"));
        actionItems.Add(new ActionItem(4, "Mitigate", "ActionMitigate"));

        foreach (ActionItem item in actionItems)
        {
            actionItemsDictionary.Add(item.id, item);
        }
    }
    internal void InstantiateActions(Tile tile, List<Actions> actions)
    {
        //ClearActionItems();

        foreach (Actions action in actions)
        {
            ActionItem actionItem;
            if (actionItemsDictionary.TryGetValue((int)action, out actionItem))
            {
                GameObject actionItemButton = (GameObject)Instantiate(actionItemPrefab);
                actionItemButton.transform.SetParent(ParentPanel, false);
                actionItemButton.transform.localScale = new Vector3(1, 1, 1);
                Button tempButton = actionItemButton.GetComponent<Button>();

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
        GameManager.instance.Invoke(triggerAction, 0f);
    }
}
