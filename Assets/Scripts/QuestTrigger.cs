using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class QuestTrigger : MonoBehaviour
{
    public CompletitionQuest _quest;
    QuestHandler questHandler;
    bool activated;

    


    private void Start()
    {
        questHandler = FindObjectOfType<QuestHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!activated)
        {
            activated = true;
            questHandler.OnQuestTook?.Invoke(this, new QuestHandler.OnQuestTookdEventArgs() { quest = _quest });
            _quest.inAction = true;
        }       
    }

    public void GainPointForQuest(int point)
    {
        if(_quest.inAction)
            questHandler.OnGainPoint?.Invoke(this, new QuestHandler.GainPointEventArgs() { pointGained = point, quest = _quest });
    }
}
