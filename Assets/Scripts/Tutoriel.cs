using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutoriel : MonoBehaviour
{
    [SerializeField]
    GameObject background;
    [SerializeField]
    GameObject tutoWalk;
    [SerializeField]
    GameObject tutoChop;
    [SerializeField]
    GameObject tutoBuild;
    enum EStateTuto
    {
        eWalk,
        eChop,
        eWaitBuild,
        eBuild,
        eEnd
    }
    EStateTuto currentState = EStateTuto.eWalk;
    public static Tutoriel instance;
    private void Start()
    {
        instance = this;
        background.SetActive(true);
        tutoWalk.SetActive(true);
    }

    public void HideWalk()
    {
        if (currentState != EStateTuto.eWalk)
            return;
        currentState = EStateTuto.eChop;
        tutoWalk.SetActive(false);
        tutoChop.SetActive(true);
    }

    public void HideChop()
    {
        if (currentState != EStateTuto.eChop)
            return;
        currentState = EStateTuto.eWaitBuild;
        tutoChop.SetActive(false);
        background.SetActive(false);
    }

    public void EnableBuildTutoriel()
    {
        if (currentState != EStateTuto.eWaitBuild)
            return;
        currentState = EStateTuto.eBuild;
        tutoBuild.SetActive(true);
        background.SetActive(true);
    }

    public void HideBuild()
    {
        if (currentState != EStateTuto.eBuild)
            return;
        currentState = EStateTuto.eEnd;
        tutoBuild.SetActive(false);
        background.SetActive(false);
    }
}
