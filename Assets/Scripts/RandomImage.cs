using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomImage : MonoBehaviour
{
    List<Sprite> sprites;
    Image img;
    private void Awake()
    {
        img.overrideSprite = sprites[Random.Range(0, sprites.Count)];
    }
}
