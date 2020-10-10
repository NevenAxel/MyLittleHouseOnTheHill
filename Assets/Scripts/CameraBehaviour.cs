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
    private void Awake()
    {
        FindObjectOfType<MainCharacterBehaviour>().onWoodChopped += OnWoodChopped;
    }

    public void OnWoodChopped(object sender, MainCharacterBehaviour.OnWoodChoppedEventArgs e)
    {
        if (e.wood > firstMissionWood)
            isParentedToPlayer = true;
    }

    private void FixedUpdate()
    {
        if(isParentedToPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, positionToFollow.transform.position, lerp);
        }
    }
}
