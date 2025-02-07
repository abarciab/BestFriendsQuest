using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationButton : MonoBehaviour
{
    public GameObject newScene;
    public GameObject newSceneUI;

    public TownGameManager gameManager;
    public void ClickNavigation()
    {
        gameManager.ChangeScene(newScene, newSceneUI);    
    }
}
