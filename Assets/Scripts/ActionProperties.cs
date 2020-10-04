using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionProperties : MonoBehaviour
{
    public int actionCost;
    public int actionId;

    public Image actionButtonImage;

    [SerializeField]
    public List<Sprite> actionButtonSprites;

    private int moneyRef = 0;

    void Start()
    {
        actionButtonImage.sprite = actionButtonSprites[actionId];
    }

    void Update()
    {
        // Si este código fuese medicina me costaría la matrícula.
        switch (actionId)
        {
            case 0: // Build farm
            case 1: // Build factory
            case 2: // Set fire
            case 3: // Deforest
            case 5: // Mitigate
            case 6: // Upgrade
                bool isInteractable = this.GetComponent<Button>().interactable;
                moneyRef = GameManager.instance.money;
                if (moneyRef < actionCost && isInteractable == true)
                {
                    this.GetComponent<Button>().interactable = false;
                }
                else if (moneyRef >= actionCost && isInteractable == false)
                {
                    this.GetComponent<Button>().interactable = true;
                }
                break;

        }
    }
}
