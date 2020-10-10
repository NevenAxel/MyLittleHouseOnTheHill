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
    [SerializeField]
    Text levelText;
    int currentPallier = 0;
    int currentXp;
    bool isLevelUp = false;
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
            levelText.text = "Niveau : " + currentPallier;
            isLevelUp = true;
            Time.timeScale =  0 ;
            foreach (LevelUpUI lui in levels)
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
        if (isLevelUp)
            return;
        popup.SetActive(false);
        UILevelUp.SetActive(!UILevelUp.activeSelf);
        Time.timeScale = (UILevelUp.activeSelf ? 0 : 1);
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
        UILevelUp.SetActive(false);
        isLevelUp = false;
        Time.timeScale = 1;
    }
}
