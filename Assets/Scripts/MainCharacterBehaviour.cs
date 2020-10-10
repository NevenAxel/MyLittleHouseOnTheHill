using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterBehaviour : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    int baseChopForce;
    [SerializeField]
    int chopRandom;
    [SerializeField]
    int woodAdd;
    [SerializeField]
    int woodAddRandom;
    [SerializeField]
    float timeChop;
    float timeLastChop;
    int currentWood;
    void Start()
    {
        timeLastChop = Time.time - timeChop;
    }
    void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += move * speed * Time.deltaTime;
    }

    private void OnTrigerStay(Collider collider)
    {
        if (!Input.GetKey(KeyCode.Space))
            return;
        Wood wood = collider.GetComponent<Wood>();
        if (wood != null)
        {
            if(timeLastChop < Time.time - timeChop)
            {
                Chop(wood);
            }
        }
    }
    void Chop(Wood wood)
    {
        timeLastChop = Time.time;
        int forceChop = baseChopForce + Random.Range(0, chopRandom + 1);
        if (wood.GetChopped(forceChop))
        {
            currentWood += woodAdd + Random.Range(0, woodAddRandom+1);
            Debug.Log(currentWood);
        }

    }
}
