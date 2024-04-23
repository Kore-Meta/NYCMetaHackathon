using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    public QuestionSO questionSO;
    public ChefStation chefStationPrefab;
    [SerializeField] private ChefStation chefStation;

    public bool isCookingDone;
    private GameObject dish;

    // Start is called before the first frame update
    void Start()
    {
        if (chefStation != null && questionSO != null)
        {
            SetOrderText();
            chefStation.EvtCookBtnPressed.AddListener(Cook);
        }
        isCookingDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateChefStation()
    {
        if (chefStation == null)
        {
            chefStation = Instantiate(chefStationPrefab, new Vector3(0.5f, 1f, 0.5f), Quaternion.Euler(0, 90, 0));
            chefStation.EvtCookBtnPressed.AddListener(Cook);
        }
    }

    public void DisableChefStationGrabbable()
    {
        chefStation.DisableGrab();
    }

    public void Cook()
    {
        if (CheckIfCanCook())
        {
            dish = Instantiate(questionSO.foodPrefab, chefStation.plateTransform.position, Quaternion.identity);
            chefStation.successAudio.Play();
            foreach (var socket in chefStation.letterSockets)
            {
                socket.Reset();
            }
            isCookingDone = true;
        }
        else
        {
            chefStation.failedAudio.Play();
        }
    }
    private bool CheckIfCanCook()
    {
        bool canCook = true;
        int i;
        for (i = 0; i < questionSO.lettersInQuestion.Length; i++)
        {
            Debug.Log(questionSO.lettersInQuestion[i] + "; " + chefStation.letterSockets[i].japaneseLetter);
            if (questionSO.lettersInQuestion[i] != chefStation.letterSockets[i].japaneseLetter)
            {
                canCook = false;
                break;
            }
        }
        if (questionSO.lettersInQuestion.Length < chefStation.letterSockets.Length)
        {
            while (i < chefStation.letterSockets.Length)
            {
                if (chefStation.letterSockets[i].japaneseLetter != JapaneseLetter.Null)
                {
                    canCook = false;
                    break;
                }
                i++;
            }
        }
        return canCook;
    }

    public void SetOrderText()
    {
        chefStation.orderText.text = "";
        if (questionSO != null)
        {
            chefStation.orderText.text = "You got order:\n";
            foreach (var letter in questionSO.lettersInQuestion)
            {
                chefStation.orderText.text += letter + " ";
            }
        }
    }

    public void Reset()
    {
        isCookingDone = false;
        Destroy(dish);
    }

    public void ResetAll()
    {
        isCookingDone = false;
        Destroy(dish);
        questionSO = null;
        SetOrderText();
    }
}
