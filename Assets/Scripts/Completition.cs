using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Completition
{
    public string _name;
    public string _description;
    public Sprite _sprite;
    public int Milestone;
    
    public Completition(string name, string description, int milestone)
    {
        _description = description;
        _name = name;
        Milestone = milestone;
    }
}
