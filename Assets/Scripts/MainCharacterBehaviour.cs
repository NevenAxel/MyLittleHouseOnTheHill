using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
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
    [SerializeField]
    Text woodText;
    [SerializeField]
    Text bravoText;
    float timeLastChop;
    int currentWood;
    House currentHouse = null;
    int currentBravos = 0;
    public EventHandler<OnWoodChoppedEventArgs> onWoodChopped;
    public class OnWoodChoppedEventArgs : EventArgs
    {
        public int wood;
    }
    
         
    void Start()
    {
        timeLastChop = Time.time - timeChop;
    }
    void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += move * speed * Time.deltaTime;
    }
    public void AddBravo(int count)
    {
        currentBravos += count;
        bravoText.text = currentBravos.ToString();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && currentHouse != null)
        {
            currentHouse.Build();
        }
    }
    private void OnTriggerStay(Collider collider)
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

    private void OnTriggerEnter(Collider other)
    {
        House temp = other.GetComponent<House>();
        if (temp != null)
            currentHouse = temp;
    }

    private void OnTriggerExit(Collider other)
    {
        House temp = other.GetComponent<House>();
        if (temp != null)
            currentHouse = null;
    }

    public void buffSpeed(float buff)
    {
        speed *= buff;
    }

    public void buffCutTime(float buff)
    {
        timeChop /= buff;
    }
    void Chop(Wood wood)
    {
        timeLastChop = Time.time;
        int forceChop = baseChopForce + UnityEngine.Random.Range(0, chopRandom + 1);
        if (wood.GetChopped(forceChop))
        {
             Debug.Log("Wood chopped");
            currentWood += woodAdd + UnityEngine.Random.Range(0, woodAddRandom + 1);
            woodText.text = currentWood.ToString();
            onWoodChopped?.Invoke(this, new OnWoodChoppedEventArgs() { wood = currentWood});
        }
    }
}
