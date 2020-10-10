using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LevelUpDestroyRandomWood : LevelUpEffectParent
{
    [SerializeField]
    int destroyCount;
    public override void OnActivate()
    {
        Wood[] trees = FindObjectsOfType<Wood>();
        List<Wood> treeList = new List<Wood>(trees);
        treeList = treeList.OrderBy(i => Random.value).ToList();
        int count = Mathf.Min(treeList.Count, destroyCount);
        for (int i = 0; i < count; i++)
            Destroy(treeList[i].gameObject);
    }
}
