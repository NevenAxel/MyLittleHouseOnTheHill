using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class House : MonoBehaviour
{
    [SerializeField]
    List<GameObject> stepsHouse;
    [SerializeField]
    List<GameObject> stepsMill;
    [SerializeField]
    int numberOfTapBeforeNextStep;
    [SerializeField]
    Bar bar;
    int step = 0;
    int minorStep = 0;
    public EventHandler<EventArgs> onWin;
    public void Build()
    {
        minorStep++;
        if (minorStep > numberOfTapBeforeNextStep)
        {
            minorStep = 0;
            step++;
            if(step < stepsHouse.Count)
            {
                stepsHouse[step - 1].SetActive(false);
                stepsHouse[step].SetActive(true);
                if(step == stepsHouse.Count - 1)
                {
                    onWin?.Invoke(this, new EventArgs());
                    Debug.Log("Win");
                }
            }
        }
        bar.SetUi((float)minorStep / (float)numberOfTapBeforeNextStep);
    }
}
