using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ComboPopup : MonoBehaviour
{
    [SerializeField]
    Text comboText;
    float timeLeft;
    public void SetUp( int combo, float time)
    {
        timeLeft = time;
        comboText.text = combo.ToString();
        Invoke("Destroy", time);
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
