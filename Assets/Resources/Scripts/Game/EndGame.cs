using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    public int kills;
    public int destroyedForestSize;
    public int scorchedLandSize;
    public int moneyWon;
    public Text endText;
    public int iterator;
    public GameObject retryButton;

    void Start()
    {
        iterator = 0;
        kills = GameManager.instance.kills;
        destroyedForestSize = GameManager.instance.forestDestructionSize;
        scorchedLandSize = GameManager.instance.scorchedLandSize;
        moneyWon = GameManager.instance.money;
        endText.text = "¡Felicidades! Tu patrimonio se valúa en $ " + moneyWon.ToString() + " y solo nos costó:\n- La muerte de " + kills.ToString() + " animales que habitan los humedales.\n- La destrucción de " + destroyedForestSize + " hectáreas de bosques nativos.\n- La erosión de " + scorchedLandSize + " hectáreas de tierra irrecuperables.";
    }

    public void TryAgainMsg()
    {
        switch (iterator)
        {
            case 0:
                endText.text = "\"Solo cuando se haya envenenado el último río, cortado el último árbol y matado el último pez, el hombre se dará cuenta de que no puede comerse el dinero\".";
                iterator++;
                break;
            case 1:
                endText.text = "Este mundo ya está muerto, pero el nuestro aún no. Hacé algo antes de que sea demasiado tarde.";
                retryButton.GetComponentInChildren<Text>().text = "NO HAY PLANETA B";
                iterator++;
                break;
            case 2:
                endText.text = "#LeyDeHumedalesYa";

                Destroy(retryButton);
                break;
            default:
                break;
        }
        
    }
}
