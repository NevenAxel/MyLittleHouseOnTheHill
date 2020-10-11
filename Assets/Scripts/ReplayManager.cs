using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ReplayManager : MonoBehaviour
{
    [SerializeField]
    GameObject button;
    public void DisplayButton()
    {
        button.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene("Menu");
    }
}
