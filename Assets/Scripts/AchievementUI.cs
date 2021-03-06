﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour
{
    [SerializeField] public Text achievmentTitle;
    [SerializeField] public Text achievementDescription;
    [SerializeField] public Text advancementNumber;
    [SerializeField] public Text timer;
    [SerializeField] public Image achievementImage;
    [SerializeField] public Transform progressBar;
    [SerializeField] public Transform progressBarTemp;
}
