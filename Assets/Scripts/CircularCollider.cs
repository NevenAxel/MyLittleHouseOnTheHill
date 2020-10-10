using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularCollider : MonoBehaviour
{
    List<Wood> near = new List<Wood>();
    [SerializeField]
    GameObject player;
    public List<Wood> GetList()
    {
        return near;
    }

    private void Update()
    {
        transform.position = player.transform.position;
    }
    public void CleanList()
    {
        List<Wood> temp = new List<Wood>();
        foreach (Wood w in near)
            if (w == null)
                temp.Add(w);
        foreach(Wood w in temp)
                near.Remove(w);
    }
    private void OnTriggerEnter(Collider other)
    {
        Wood temp = other.GetComponent<Wood>();
        if (temp != null)
            near.Add(temp);
    }

    private void OnTriggerExit(Collider other)
    {
        Wood temp = other.GetComponent<Wood>();
        if (temp != null)
            near.Remove(temp);
    }
}
