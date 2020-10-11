using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField]
    GameObject good;
    [SerializeField]
    GameObject bad;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<MainCharacterBehaviour>() != null)
        {
            good.SetActive(false);
            bad.SetActive(true);
        }
    }
}
