using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public static class NavigationState
{
    public static bool isContinueGameFlow
    {
        get;
        set;
    }
}

public class NavigationManager : MonoBehaviour
{
    public const int IN_GAME = 1;
    public void NewGame()
    {
        SceneManager.LoadScene(IN_GAME);
    }
}
