using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelUpUI : MonoBehaviour
{
    [SerializeField]
    Sprite unlockable;
    [SerializeField]
    Image image;
    bool isUnlockable;
    bool isUnlocked;
    bool isNotChosed;
    [SerializeField]
    string descriptionString;
    [SerializeField]
    string nameString;
    public int indexPallier;
    public void OnMouseOverEvent()
    {
        LevelUpManager.instance.description.SetActive(true);
        if (isUnlockable)
        {
            LevelUpManager.instance.descriptionText.text = descriptionString;
            LevelUpManager.instance.nameText.text = nameString;
        }
        else
        {
            LevelUpManager.instance.descriptionText.text = "???";
            LevelUpManager.instance.nameText.text = "???";
        }
    }

    public void OnMouseExitEvent()
    {
        LevelUpManager.instance.description.SetActive(false);
    }

    public void SetUnlockable()
    {
        isUnlockable = true;
        image.overrideSprite = unlockable;
        image.color = new Color(1, 1, 1, 0.5f);
    }

    public void SetNotChoosed()
    {
        isNotChosed = true;
        image.color = new Color(0, 0, 0, 0.5f);
    }

    public void OnClick()
    {
        if(isUnlockable && !isNotChosed)
        {
            isUnlocked = true;
            image.color = new Color(1, 1, 1, 1f);
            FindObjectOfType<LevelUpManager>().SetChoose(this);
        }
    }
}
