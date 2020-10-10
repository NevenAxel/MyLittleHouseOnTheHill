using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementHandler : MonoBehaviour
{
    [SerializeField] AchievementUI achievementUIprefab;
    [SerializeField] float achievementStayDuration = 1f;

    MainCharacterBehaviour player;

    [SerializeField] TextAsset woodcutAchievementData;
    int woodCut;
    [SerializeField] List<Achievement> woodCutAchievements = new List<Achievement>();

    [System.Serializable]
    class Achievement
    {
        
        public string _name;
        public string _description;
        public Sprite _sprite;
        public int Milestone;
        public Achievement(string name, string description, int milestone)
        {
            _description = description;
            _name = name;
            Milestone = milestone;
        }
    }

    void Start()
    {
        player = FindObjectOfType<MainCharacterBehaviour>();
        player.onWoodChopped += OnWoodChopped;
        
    }

    [ContextMenu("TranslateWoodCutAchievement")]
    private void TranslateAllAchievementData()
    {
        TranslateAchievmentData(woodcutAchievementData, woodCutAchievements);
    }


    private void TranslateAchievmentData(TextAsset data, List<Achievement> achievementList)
    {
        string[] lines = data.text.Split(new char[] { '\n' });
        // skip first line
        for (int i = 1; i < lines.Length; i++)
        {
            string[] cases = lines[i].Split(new char[] { ',' });

            Achievement achievement = new Achievement(cases[0], cases[1], Int32.Parse(cases[2]));
            achievementList.Add(achievement);
        }
    }

    private void OnWoodChopped(object sender, MainCharacterBehaviour.OnWoodChoppedEventArgs e)
    {
        woodCut++;
        achievementCheck(woodCutAchievements, woodCut);
    }

    private void achievementCheck(Achievement achievement, int currentAdvancement)
    {
        if (achievement.Milestone == currentAdvancement)
        {
            achievementUnlocked(achievement);
        }
        else
        {
            achievementUpdated(achievement);
        }
    }

    private void achievementCheck(List<Achievement> achievements, int currentAdvancement)
    {
        foreach(Achievement achievement in achievements)
        {
            if(achievement.Milestone == currentAdvancement)
            {
                achievementUnlocked(achievement);
                return;
            }
            if (achievement.Milestone > currentAdvancement)
            {
                achievementUpdated(achievement);
                return;
            }
        }
    }

    private void achievementUpdated(Achievement achievement)
    {
        AchievementUI achievementPrefab = Instantiate(achievementUIprefab, gameObject.transform);
        achievementPrefab.achievementDescription.text = achievement._description;
        achievementPrefab.achievmentTitle.text = achievement._name;
        achievementPrefab.achievementImage.sprite = achievement._sprite;
        Animation anim = achievementPrefab.GetComponent<Animation>();
        anim.Play();
        Destroy(achievementPrefab.gameObject, achievementStayDuration);
    }

    private void achievementUnlocked(Achievement achievement)
    {
        AchievementUI achievementPrefab = Instantiate(achievementUIprefab, gameObject.transform);
        achievementPrefab.achievementDescription.text = achievement._description;
        achievementPrefab.achievmentTitle.text = achievement._name;
        achievementPrefab.achievementImage.sprite = achievement._sprite;
        Animation anim = achievementPrefab.GetComponent<Animation>();
        anim.Play();
        Destroy(achievementPrefab.gameObject, achievementStayDuration);
    }
}
