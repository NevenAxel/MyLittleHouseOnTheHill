using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestHandler : MonoBehaviour
{   
    [SerializeField] AchievementUI questUIprefab;
    CompletitionQuest[] quests;
    public EventHandler<OnQuestTookdEventArgs> OnQuestTook;
    [SerializeField] float progressBarDuration = 0.25f;
    public class OnQuestTookdEventArgs : EventArgs
    {
        public CompletitionQuest quest;
    }
    public EventHandler<GainPointEventArgs> OnGainPoint;
    public class GainPointEventArgs : EventArgs
    {
        public int pointGained;
        public CompletitionQuest quest;
    }

    private void Start()
    {
        OnQuestTook += OnQuestTookHandler;
        OnGainPoint += OnPointGainHandler;
    }

    private void OnPointGainHandler(object sender, GainPointEventArgs e)
    {
        e.quest.pointGained += e.pointGained;
        e.quest.QuestUI.advancementNumber.text = e.quest.pointGained + " / " + e.quest.Milestone;

        float normalizedStartingAdvancement = (float)(e.quest.pointGained - 1) / (float)e.quest.Milestone;
        float normalizedAdvancement = (float)e.quest.pointGained / (float)e.quest.Milestone;
        StartCoroutine(LerpSize(e.quest.QuestUI.progressBar.transform, normalizedStartingAdvancement, normalizedAdvancement, progressBarDuration, progressBarDuration));
        StartCoroutine(LerpSize(e.quest.QuestUI.progressBarTemp.transform, normalizedStartingAdvancement, normalizedAdvancement, progressBarDuration));
        if (e.quest.pointGained >= e.quest.Milestone)
        {
            // animation fade out
            e.quest.QuestUI.advancementNumber.text = e.quest.Milestone + " / " + e.quest.Milestone;
            Destroy(e.quest.QuestUI.gameObject, 0.5f);
            e.quest.inAction = false;
            
        }
    }
    IEnumerator LerpSize(Transform transform, float origin, float endPoint, float duration, float delay = 0f)
    {
        float timer = 0;
        yield return new WaitForSeconds(delay);
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float scale = Mathf.Lerp(origin, endPoint, timer / duration);
            transform.localScale = new Vector3(scale, 1, 1);
            yield return null;
        }
    }

    private void OnQuestTookHandler(object sender, OnQuestTookdEventArgs e)
    {
        e.quest.QuestUI = Instantiate(questUIprefab, gameObject.transform);
        e.quest.QuestUI.achievementDescription.text = e.quest._description;
        e.quest.QuestUI.achievmentTitle.text = e.quest._name;
        e.quest.QuestUI.advancementNumber.text = e.quest.pointGained + " / " + e.quest.Milestone;
        e.quest.QuestUI.progressBar.transform.localScale = new Vector3(0, 0, 0);
        e.quest.QuestUI.progressBarTemp.transform.localScale = new Vector3(0, 0, 0);

    }

}
