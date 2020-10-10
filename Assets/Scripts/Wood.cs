using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Wood : MonoBehaviour
{
    [SerializeField]
    int baseLife;

    int currentLife;
    [SerializeField]
    GameObject parentLife;
    [SerializeField]
    Image woundBar;
    private void Awake()
    {
        currentLife = baseLife;
    }
    public bool GetChopped(int chopForce)
    {
        currentLife -= chopForce;
        SetUiLife();
        if(currentLife <= 0)
        {
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }
    public void SetUiLife()
    {
        parentLife.SetActive(true);
        woundBar.rectTransform.offsetMin = new Vector2((float)currentLife/(float)baseLife, woundBar.rectTransform.offsetMin.y);
    }
}
