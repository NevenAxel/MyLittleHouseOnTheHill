using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChoice : MonoBehaviour
{
    ChooseYourHouse chooseYourHouse;
    [SerializeField] BuildingType buildingType;
    private void Start()
    {
        chooseYourHouse = FindObjectOfType<ChooseYourHouse>();
    }
    public void OnButtonPressed()
    {
        chooseYourHouse.PlayerMadeItsChoice(buildingType);
    }
}
