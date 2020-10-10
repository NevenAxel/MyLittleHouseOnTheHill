using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementHandler : MonoBehaviour
{
    [SerializeField] AchievementUI achievementUIprefab;
    AchievementUI currentAchievementPrefab;
    [SerializeField] float achievementStayDuration = 1f;
    [SerializeField] float progressBarDuration = 0.25f;
    float countDownToHide = 1f;

    MainCharacterBehaviour player;

    [SerializeField] TextAsset woodcutAchievementData;
    int woodCut;
    [SerializeField] List<Completition> woodCutAchievements = new List<Completition>();



    

    void Start()
    {
        player = FindObjectOfType<MainCharacterBehaviour>();
        player.onWoodChopped += OnWoodChopped;
        countDownToHide = achievementStayDuration;
    }

    [ContextMenu("TranslateWoodCutAchievement")]
    private void TranslateAllAchievementData()
    {
        TranslateAchievmentData(woodcutAchievementData, woodCutAchievements);
    }


    private void TranslateAchievmentData(TextAsset data, List<Completition> achievementList)
    {
        string[] lines = data.text.Split(new char[] { '\n' });
        // skip first line
        for (int i = 1; i < lines.Length; i++)
        {
            string[] cases = lines[i].Split(new char[] { ',' });

            Completition achievement = new Completition(cases[0], cases[1], Int32.Parse(cases[2]));
            achievementList.Add(achievement);
        }
    }

    private void OnWoodChopped(object sender, MainCharacterBehaviour.OnWoodChoppedEventArgs e)
    {
        woodCut++;
        achievementCheck(woodCutAchievements, woodCut);
    }

    private void achievementCheck(Completition achievement, int currentAdvancement)
    {
        if (achievement.Milestone == currentAdvancement)
        {
            achievementUnlocked(achievement);
        }
        else
        {
            achievementUpdated(achievement, 0, currentAdvancement);
        }
    }

    private void achievementCheck(List<Completition> achievements, int currentAdvancement)
    {
        for(int i = 0; i < achievements.Count; i++)
        {
            if (achievements[i].Milestone == currentAdvancement)
            {
                achievementUnlocked(achievements[i]);
                return;
            }
            else if(i == 0 && achievements[0].Milestone > currentAdvancement)
            {
                    achievementUpdated(achievements[i], 0, currentAdvancement);
                    return;
            }
            else if(i == achievements.Count && achievements[i - 1].Milestone < currentAdvancement)
            {
                    achievementUpdated(achievements[i], achievements[i-1].Milestone, currentAdvancement);
                    return;
            }
            else if (achievements[i].Milestone < currentAdvancement && achievements[i + 1].Milestone > currentAdvancement)
            {

                achievementUpdated(achievements[i+1], achievements[i].Milestone, currentAdvancement);
                return;
            }
        }
    }

    private void achievementUpdated(Completition achievement, int previousMilestone, int advancement)
    {
        if(currentAchievementPrefab == null)
        {
            StartCoroutine(Fade());
            currentAchievementPrefab = Instantiate(achievementUIprefab, gameObject.transform);           
            Animation anim = currentAchievementPrefab.GetComponent<Animation>();
            anim.Play("AchievementUpdate");
        }
        else
        {
            countDownToHide = achievementStayDuration;
        }
            
        currentAchievementPrefab.achievementDescription.text = achievement._description;
        currentAchievementPrefab.achievmentTitle.text = achievement._name;
        currentAchievementPrefab.achievementImage.sprite = achievement._sprite;

        float normalizedStartingAdvancement = (float)(advancement - 1 - previousMilestone) / (float)(achievement.Milestone - previousMilestone);
        float normalizedAdvancement = (float)(advancement - previousMilestone) / (float)(achievement.Milestone - previousMilestone);
        StartCoroutine(LerpSize(currentAchievementPrefab.progressBar.transform, normalizedStartingAdvancement, normalizedAdvancement, progressBarDuration, progressBarDuration));
        StartCoroutine(LerpSize(currentAchievementPrefab.progressBarTemp.transform, normalizedStartingAdvancement, normalizedAdvancement, progressBarDuration));
    }

    private void achievementUnlocked(Completition achievement)
    {
        if (currentAchievementPrefab == null)
        {
            StartCoroutine(Fade());
            currentAchievementPrefab = Instantiate(achievementUIprefab, gameObject.transform);
            Animation anim = currentAchievementPrefab.GetComponent<Animation>();           
            anim.Play("AchievementUnlockedNew");
        }
        else
        {
            countDownToHide = achievementStayDuration;
            Animation anim = currentAchievementPrefab.GetComponent<Animation>();
            anim.Play("AchievementUnlocked");
        }
            
        currentAchievementPrefab.achievementDescription.text = achievement._description;
        currentAchievementPrefab.achievmentTitle.text = achievement._name;
        currentAchievementPrefab.achievementImage.sprite = achievement._sprite;
        currentAchievementPrefab.progressBar.transform.localScale = new Vector3(1, 1, 1);
    }
    IEnumerator LerpSize(Transform transform, float origin, float endPoint, float duration, float delay = 0f)
    {
        float timer = 0;
        yield return new WaitForSeconds(delay);
        while(timer < duration)
        {
            timer += Time.deltaTime;
            float scale = Mathf.Lerp(origin, endPoint, timer / duration);
            transform.localScale = new Vector3(scale, 1, 1);           
            yield return null;
        }
    }

    IEnumerator Fade()
    {
        for (int i = 0; i < countDownToHide; i++)
        {
            yield return new WaitForSeconds(1f);
        }
        Animation anim = currentAchievementPrefab.GetComponent<Animation>();
        anim.Play("AchievementHide");
        currentAchievementPrefab = null;
        Destroy(currentAchievementPrefab, 1f);
    }
}
