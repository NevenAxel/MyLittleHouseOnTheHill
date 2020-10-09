using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterBehaviour : MonoBehaviour
{
    [SerializeField]
    float speed;
    void Start()
    {
        
    }
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += move * speed * Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.other.CompareTag("Tree"))
        {

        }
    }
}
