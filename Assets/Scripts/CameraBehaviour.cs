using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    bool isParentedToPlayer;
    [SerializeField]
    GameObject positionToFollow;
    [SerializeField]
    float lerp;
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
            isParentedToPlayer = true;
    }

    public void Unparent()
    {
        isParentedToPlayer = false;
    }

    private void FixedUpdate()
    {
        if(isParentedToPlayer && character.ShouldFollow())
        {
            transform.position = Vector3.Lerp(transform.position, positionToFollow.transform.position, lerp);
        }
    }
}
