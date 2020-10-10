using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    [SerializeField]
    string scene;
    [SerializeField]
    GameObject options;
    public void LoadGame()
    {
        SceneManager.LoadScene(scene);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void HideOptions()
    {
        options.SetActive(false);
    }

    public void ShowOptions()
    {
        options.SetActive(true);
    }
}
