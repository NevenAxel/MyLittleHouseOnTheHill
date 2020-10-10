using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PubManager : MonoBehaviour
{
    [SerializeField]
    GameObject parent;
    [SerializeField]
    Image pub;
    public static PubManager instance;
    private void Awake()
    {
        instance = this;
    }

    public void DisplayPub(Sprite sprite)
    {
        parent.SetActive(true);
        pub.overrideSprite = sprite;
        Time.timeScale = 0;
    }

    public void HidePub()
    {
        parent.SetActive(false);
        Time.timeScale = 1;
    }

}
