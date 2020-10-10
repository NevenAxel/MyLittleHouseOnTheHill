using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ChooseYourHouse : MonoBehaviour
{

    BuildingType buildingChose;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }  

    public void PlayerMadeItsChoice(BuildingType houseType)
    {
        buildingChose = houseType;
        Time.timeScale = 1;
        gameObject.SetActive(false);
        // Send the building type to GameManager ou autre
    }
}
