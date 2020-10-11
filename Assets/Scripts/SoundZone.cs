using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundZone : MonoBehaviour
{
    [SerializeField]
    AudioClip clip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MainCharacterBehaviour>() != null)
        {
            SoundManager.instance.Play(clip);
        }
    }
}
