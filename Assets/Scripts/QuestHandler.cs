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
        public CompletitionQuest nextQuest = null;
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
            EndQuest(true, e.quest, e.nextQuest);
        }
    }

    private void EndQuest(bool completed, CompletitionQuest quest, CompletitionQuest nextQuest = null)
    {
        // animation fade out
        quest.QuestUI.advancementNumber.text = quest.Milestone + " / " + quest.Milestone;
        Destroy(quest.QuestUI.gameObject, 1f);
        quest.inAction = false;
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
        if(e.quest.QuestUI == null)
            e.quest.QuestUI = Instantiate(questUIprefab, gameObject.transform);
        e.quest.QuestUI.achievementDescription.text = e.quest._description;
        e.quest.QuestUI.achievmentTitle.text = e.quest._name;
        e.quest.QuestUI.advancementNumber.text = e.quest.pointGained + " / " + e.quest.Milestone;
        e.quest.QuestUI.progressBar.transform.localScale = new Vector3(0, 0, 0);
        e.quest.QuestUI.progressBarTemp.transform.localScale = new Vector3(0, 0, 0);
        e.quest.QuestUI.timer.text = Math.Round(e.quest.timeToComplete, 1).ToString();
        if(e.quest.timeToComplete != 0)
            StartCoroutine(CountDownQuest(e.quest));
    }

    IEnumerator CountDownQuest(CompletitionQuest quest)
    {
        float timer = quest.timeToComplete;
        while(timer > 0)
        {
            if(quest.inAction)
                quest.QuestUI.timer.text = Math.Round(timer, 1).ToString();
            timer -= Time.deltaTime;          
            yield return null;
        }
        EndQuest(false, quest);
    }
}
