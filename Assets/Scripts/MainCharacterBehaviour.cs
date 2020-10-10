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
    [SerializeField]
    float timeComboBase;
    [SerializeField]
    float timeComboEnd;
    [SerializeField]
    int numberHitCombo;
    [SerializeField]
    GameObject PopupCombo;
    [SerializeField]
    Transform parentCombo;
    [SerializeField]
    int buffDamageCombo;
    [SerializeField]
    List<int> stepsCombo;
    [SerializeField]
    CircularCollider circularCollider;
    [SerializeField]
    int comboTreshold;
    List<int> currentSteps;
    GameObject currentComboGo;
    float timeLastChop;
    int currentWood;
    int currentCombo = 0;
    House currentHouse = null;
    int currentBravos = 0;
    int currentBuffAck = 0;
    bool circular = false;
    public EventHandler<OnWoodChoppedEventArgs> onWoodChopped;
    public class OnWoodChoppedEventArgs : EventArgs
    {
        public int wood;
    }
    
         
    void Start()
    {
        timeLastChop = Time.time - timeChop;
        CopySteps();
    }

    void CopySteps()
    {
        currentSteps = new List<int>();
        foreach (int i in stepsCombo)
            currentSteps.Add(i);
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (move.magnitude != 0)
            Tutoriel.instance.HideWalk();
        transform.position += move * speed * Time.deltaTime;
    }
    public void AddBravo(int count)
    {
        currentBravos += count;
        bravoText.text = currentBravos.ToString();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(currentHouse != null)
            {
                Tutoriel.instance.HideBuild();
                currentHouse.Build();
            }

            else if(circular)
            {
                circularCollider.CleanList();
                List<Wood> woods = circularCollider.GetList();
                if (woods.Count > 0)
                {
                    CalculateCombo();
                    foreach (Wood w in woods)
                        Chop(w, false);
                }
            }
        }
    }
    private void OnTriggerStay(Collider collider)
    {
        if (!Input.GetKey(KeyCode.Space) || circular)
            return;
        Wood wood = collider.GetComponent<Wood>();
        if (wood != null)
        {
            if(timeLastChop < Time.time - timeChop)
            {
                Tutoriel.instance.HideChop();
                Chop(wood, true);
            }
        }
    }
    int endOfWorld;
    public bool ShouldFollow()
    {
        return (endOfWorld == 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("EndOfWorld"))
        {
            endOfWorld++;
        }
        House temp = other.GetComponent<House>();
        if (temp != null)
        {
            currentHouse = temp;
            Tutoriel.instance.EnableBuildTutoriel();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EndOfWorld"))
        {
            endOfWorld--;
        }
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

    public void CalculateCombo()
    {
        currentCombo++;
        if (currentComboGo != null)
        {
            currentComboGo.GetComponent<ComboPopup>().Cancel();
            Destroy(currentComboGo);
        }
        
        if(currentSteps.Count> 0 && currentCombo>currentSteps[0])
        {
            currentSteps.Remove(currentSteps[0]);
            if (currentSteps.Count > 0)
            {
                currentBuffAck++;
                baseChopForce += buffDamageCombo;
            }
            else
                circular = true;
        }
        if(currentCombo > comboTreshold)
        {
            currentComboGo = Instantiate(PopupCombo, parentCombo);
            currentComboGo.transform.position = parentCombo.transform.position;
            currentComboGo.GetComponent<ComboPopup>().SetUp(currentCombo, Mathf.Lerp(timeComboBase, timeComboEnd, Mathf.Min(currentCombo / numberHitCombo, 1)));
        }

    }


    public void ResetCombo()
    {
        circular = false;
        AddBravo(currentCombo);
        baseChopForce -= currentBuffAck * buffDamageCombo;
        currentBuffAck = 0;
        currentCombo = 0;
        CopySteps();
    }
    void Chop(Wood wood, bool combo)
    {
        if(combo)
            CalculateCombo();
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
