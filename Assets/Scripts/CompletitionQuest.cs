using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CompletitionQuest : Completition
{
    public float timeToComplete;
    public AchievementUI QuestUI;
    public int pointGained = 0;
    public bool inAction = false;
    public CompletitionQuest(string name, string description, int milestone) : base(name, description, milestone)
    {
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
