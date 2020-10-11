using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTravelling : MonoBehaviour
{
    [SerializeField]
    List<Transform> points;
    [SerializeField]
    CameraBehaviour camera;
    [SerializeField]
    float timeLerp;
    [SerializeField]
    float timeWait;
    private void Start()
    {
        FindObjectOfType<House>().onWin += OnWin;
    }

    private void OnWin(object sender, EventArgs e)
    {
        camera.Unparent();
        Destroy(FindObjectOfType<MainCharacterBehaviour>());
        StartCoroutine("GoToNext");
    }

    IEnumerator GoToNext()
    {
        FindObjectOfType<SoundManager>().PlayEnd(FindObjectOfType<MainCharacterBehaviour>().GetSuccess());
        foreach (Transform t in points)
        {
            Vector3 basePos = camera.transform.position;
            float current = 0;
            while (current < timeLerp)
            {
                current += Time.deltaTime;
                camera.transform.position = Vector3.Lerp(camera.transform.position, Vector3.Lerp(basePos, t.position, current / timeLerp), 0.01f);
                yield return null;
            }
            current = 0;
            while (current < timeWait)
            {
                current += Time.deltaTime;
                camera.transform.position = Vector3.Lerp(camera.transform.position, t.position, 0.01f);
                yield return null;
            }
        }
        FindObjectOfType<ReplayManager>().DisplayButton();
    }
}
