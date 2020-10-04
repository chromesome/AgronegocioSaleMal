using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    public Text endText;
    public void TryAgainMsg()
    {
        endText.text = "En la vida real no hay segundas oportunidades, tampoco en este juego. Toma acción antes de que sea demasiado tarde.";
    }
}
