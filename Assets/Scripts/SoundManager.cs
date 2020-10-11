using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource source;
    public static SoundManager instance;
    [SerializeField]
    AudioClip good;
    [SerializeField]
    AudioClip mid;
    [SerializeField]
    AudioClip bad;

    public void PlayEnd(MainCharacterBehaviour.ESuccess success)
    {
        switch(success)
        {
            case MainCharacterBehaviour.ESuccess.eSuccess:
                Play(good);
                break;
            case MainCharacterBehaviour.ESuccess.eMid:
                Play(mid);
                break;
            case MainCharacterBehaviour.ESuccess.eFail:
                Play(bad);
                break;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    public void Play(AudioClip clip)
    {
        if (clip == source.clip)
            return;
        source.time = 0; 
        source.clip = clip;
        source.Play();
    }
}
