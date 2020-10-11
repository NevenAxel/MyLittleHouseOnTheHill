using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class Wood : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject fxDestroy;
    [SerializeField] GameObject fxHit;

    [SerializeField] MeshRenderer originalMeshRend;
    [SerializeField] MeshFilter originalMeshFilter;
    [SerializeField] Material tronkMaterial;
    [SerializeField] Mesh tronkMesh;

    [SerializeField]
    int baseLife;
    [SerializeField]
    UnityEvent onChopped;
    Collider col;

    int currentLife;
    [SerializeField]
    Bar lifeBar;
    private void Awake()
    {
        col = GetComponent<Collider>();
        currentLife = baseLife;
    }
    public bool GetChopped(int chopForce)
    {
        animator.SetTrigger("onHit");
        currentLife -= chopForce;
        lifeBar.SetUi((float)currentLife / (float)baseLife);
        GameObject FX = Instantiate(fxHit, gameObject.transform.position, Quaternion.identity);
        Destroy(FX, 2f);
        if (currentLife <= 0)
        {
            originalMeshRend.material = tronkMaterial;
            originalMeshFilter.mesh = tronkMesh;
            // fx
            GameObject newFX = Instantiate(fxDestroy, gameObject.transform.position, Quaternion.identity);
            Destroy(newFX, 2f);
            
            
            onChopped.Invoke();
            lifeBar.gameObject.SetActive(false);
            col.enabled = false;
            this.enabled = false;
            return true;
        }
        return false;
    }

}
