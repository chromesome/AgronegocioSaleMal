using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    public int kills;
    public int moneyWon;
    public Text endText;
    public int iterator;
    public GameObject retryButton;

    void Start()
    {
        iterator = 0;
        kills = GameManager.instance.kills;
        moneyWon = GameManager.instance.money;
        endText.text = "¡Felicidades! Tu patrimonio se valúa en $ " + moneyWon.ToString() + " y solo nos costó la vida del planeta.";
    }
    public void TryAgainMsg()
    {
        switch (iterator)
        {
            case 0:
                endText.text = "Solamente cuando se haya envenenado el último río, cortado el último árbol y matado el último pez, el hombre se dará cuenta de que no puede comerse el dinero";
                iterator++;
                break;
            case 1:
                endText.text = "En la vida real no hay segundas oportunidades, tampoco en este juego. Hacé algo antes de que sea demasiado tarde.";
                iterator++;
                break;
            case 2:
                Destroy(retryButton);
                break;
            default:
                break;
        }
        
    }
}
