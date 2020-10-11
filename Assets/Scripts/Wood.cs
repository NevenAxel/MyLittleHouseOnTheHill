using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Wood : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject fx;
    [SerializeField]
    int baseLife;
    [SerializeField]
    UnityEvent onChopped;

    int currentLife;
    [SerializeField]
    Bar lifeBar;
    private void Awake()
    {
        currentLife = baseLife;
    }
    public bool GetChopped(int chopForce)
    {
        animator.SetTrigger("onHit");
        currentLife -= chopForce;
        lifeBar.SetUi((float)currentLife / (float)baseLife);
        if(currentLife <= 0)
        {
            // fx

            GameObject newFX = Instantiate(fx, gameObject.transform.position, Quaternion.identity);
            Destroy(newFX, 2f);
            Destroy(animator.gameObject, 2);
            Destroy(gameObject);

            onChopped.Invoke();
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

}
