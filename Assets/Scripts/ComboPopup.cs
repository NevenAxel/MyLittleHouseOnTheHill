using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ComboPopup : MonoBehaviour
{
    [SerializeField]
    Text comboText;
    float timeLeft;
    public enum ETypePopup
    {
        eBase,
        eFaster,
        eCircular
    }
    [SerializeField]
    GameObject parentBase;
    [SerializeField]
    GameObject parentFaster;
    [SerializeField]
    GameObject parentCircular;
    public void SetUp( int combo, float time, ETypePopup type)
    {
        timeLeft = time;
        comboText.text = combo.ToString();
        Invoke("Destroy", time);
        parentBase.SetActive(false);
        parentFaster.SetActive(false);
        parentCircular.SetActive(false);
        switch(type)
        {
            case ETypePopup.eBase:
                parentBase.SetActive(true);
                break;
            case ETypePopup.eCircular:
                parentCircular.SetActive(true);
                break;
            case ETypePopup.eFaster:
                parentFaster.SetActive(true);
                break;

        }
    }
    public void Cancel()
    {
        CancelInvoke();
    }
    private void Destroy()
    {
        FindObjectOfType<MainCharacterBehaviour>().ResetCombo();
        Destroy(gameObject);
    }

}
