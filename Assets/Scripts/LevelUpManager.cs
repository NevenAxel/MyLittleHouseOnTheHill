using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelUpManager : MonoBehaviour
{
    [SerializeField]
    List<int> xpPalliers;
    [SerializeField]
    List<Sprite> sprites;
    [SerializeField]
    Image pallierImage;
    [SerializeField]
    GameObject UILevelUp;
    [SerializeField]
    GameObject popup;
    [SerializeField]
    List<LevelUpUI> levels;
    int currentPallier = 0;
    int currentXp;
    public GameObject description;
    public Text descriptionText;
    public Text nameText;
    public static LevelUpManager instance;
    private void Awake()
    {
        FindObjectOfType<MainCharacterBehaviour>().onWoodChopped += OnWoodChopped;
        pallierImage.overrideSprite = sprites[0];
        instance = this;
    }

    void OnWoodChopped(object sender, MainCharacterBehaviour.OnWoodChoppedEventArgs e)
    {
        if (currentPallier == xpPalliers.Count - 1)
            return;
        currentXp++;
        if(currentXp >= xpPalliers[currentPallier])
        {
            currentXp -= xpPalliers[currentPallier];
            currentPallier++;
            UILevelUp.SetActive(true);
            popup.SetActive(false);
            pallierImage.overrideSprite = sprites[currentPallier];
            foreach(LevelUpUI lui in levels)
            {
                if(lui.indexPallier == currentPallier)
                {
                    lui.SetUnlockable();
                }
            }
        }
    }

    public void ToggleLevelUp()
    {
        popup.SetActive(false);
        UILevelUp.SetActive(!UILevelUp.activeSelf);
    }

    public void SetChoose(LevelUpUI ui)
    {
        foreach (LevelUpUI lui in levels)
        {
            if (lui.indexPallier == ui.indexPallier && lui != ui)
            {
                lui.SetNotChoosed();
            }
        }
    }
}
