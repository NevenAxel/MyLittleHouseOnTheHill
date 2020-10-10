using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Bar : MonoBehaviour
{
    [SerializeField]
    GameObject parent;
    [SerializeField]
    Image redBar;

    public void SetUi(float ratio)
    {
        parent.SetActive(true);
        redBar.rectTransform.offsetMin = new Vector2(ratio, redBar.rectTransform.offsetMin.y);
    }
}
