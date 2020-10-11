using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    bool isParentedToPlayer;
    [SerializeField]
    GameObject positionToFollow;
    bool following;
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    int firstMissionWood;
    MainCharacterBehaviour character;
    private void Awake()
    {
        character = FindObjectOfType<MainCharacterBehaviour>();
        character.onWoodChopped += OnWoodChopped;
    }

    public void OnWoodChopped(object sender, MainCharacterBehaviour.OnWoodChoppedEventArgs e)
    {
        if (e.wood > firstMissionWood)
        {
            isParentedToPlayer = true;
            Destroy(GameObject.FindGameObjectWithTag("ColliderTuto"));
        }
        character.onWoodChopped -= OnWoodChopped;

    }

    public void Unparent()
    {
        isParentedToPlayer = false;
    }

    private void Update()
    {
        if(isParentedToPlayer && character.ShouldFollow())
        {

                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, positionToFollow.transform.position, step);
            
        }
    }
}
