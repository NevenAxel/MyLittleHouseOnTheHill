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
    Bar lifeBar;
    private void Awake()
    {
        currentLife = baseLife;
    }
    public bool GetChopped(int chopForce)
    {
        currentLife -= chopForce;
        lifeBar.SetUi((float)currentLife / (float)baseLife);
        if(currentLife <= 0)
        {
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

}
