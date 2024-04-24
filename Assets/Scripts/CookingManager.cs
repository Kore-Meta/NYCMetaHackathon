using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.Voice.Samples.Dictation;
using TMPro;

public class CookingManager : MonoBehaviour
{
    public QuestionSO questionSO;
    public ChefStation chefStationPrefab;
    [SerializeField] private ChefStation chefStation;

    public bool isCookingDone;
    private GameObject dish;

    private string dictationText;
    public string DictationText
    {
        get { return dictationText; }
        set { dictationText = value; }
    }
    private string questionTxt;
    [SerializeField] private DictationActivation dictationActivation;

    public TextMeshPro text4Debugging;

    // Start is called before the first frame update
    void Start()
    {
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
        }
        chefStation.EvtCookBtnPressed.AddListener(Cook);
    }

    public void DisableChefStationGrabbable()
    {
        chefStation.DisableGrab();
    }

    public void CheckCookResults()
    {
        dictationText = dictationText.Replace(" ", "")  // Remove spaces
                                   .Replace("，", "") // Remove comma
                                   .Replace("、", "") // Remove ideographic comma
                                   .Replace("；", ""); // Remove semicolon
        chefStation.cookButtonText.text = "You said:" + dictationText + "\n";
        Debug.Log(questionTxt + "  " + dictationText);
        bool isVoiceCorrect = false;
        if (dictationText == questionTxt)
        {
             isVoiceCorrect = true;
        }

        bool isIngredientsComplete = CheckIfIngredientsComplete();
        if (!isIngredientsComplete || !isVoiceCorrect)
        {
            chefStation.failedAudio.Play();
            if (!isIngredientsComplete)
            {
                chefStation.cookButtonText.text += "Ingredients ";
                if (!isVoiceCorrect)
                {
                    chefStation.cookButtonText.text += "and pronouciation ";
                }
                chefStation.cookButtonText.text += "not right! Try again!\n";
            }
            else
            {
                chefStation.cookButtonText.text += "Pronounciation not right! Try again!\n";
            }
        }
        else
        {
            dish = Instantiate(questionSO.foodPrefab, chefStation.plateTransform.position, Quaternion.identity);
            chefStation.successAudio.Play();
            foreach (var socket in chefStation.letterSockets)
            {
                socket.Reset();
            }
            chefStation.cookButtonText.text += "Ingredients and pronounciation both correct!";
            isCookingDone = true;
        }
    }

    public void Cook()
    {
        dictationActivation.ToggleActivation();
        Debug.Log("hello?");
        if (text4Debugging != null)
        {
            text4Debugging.text = "hello?";
        }
    }

    private bool CheckIfIngredientsComplete()
    {
        bool isIngredientsComplete = true;
        int i;
        for (i = 0; i < questionSO.lettersInQuestion.Length; i++)
        {
            Debug.Log(questionSO.lettersInQuestion[i] + "; " + chefStation.letterSockets[i].japaneseLetter);
            if (questionSO.lettersInQuestion[i] != chefStation.letterSockets[i].japaneseLetter)
            {
                isIngredientsComplete = false;
                break;
            }
        }
        if (questionSO.lettersInQuestion.Length < chefStation.letterSockets.Length)
        {
            while (i < chefStation.letterSockets.Length)
            {
                if (chefStation.letterSockets[i].japaneseLetter != JapaneseLetter.Null)
                {
                    isIngredientsComplete = false;
                    break;
                }
                i++;
            }
        }
        return isIngredientsComplete;
    }

    public void SetOrderText()
    {
        chefStation.orderText.text = "";
        chefStation.cookButtonText.text = "";
        questionTxt = "";
        if (questionSO != null)
        {
            chefStation.orderText.text = "You got order:\n";
            chefStation.cookButtonText.text = "Press button and say\n";
            foreach (var letter in questionSO.lettersInQuestion)
            {
                Debug.Log(JapaneseLetterDataLoader.Instance.lettersDict);
                Debug.Log(letter);
                chefStation.orderText.text += JapaneseLetterDataLoader.Instance.lettersDict[letter].japaneseText + "(" + letter + ") ";
                chefStation.cookButtonText.text += JapaneseLetterDataLoader.Instance.lettersDict[letter].japaneseText + "(" + letter + ") -- ";
                questionTxt += JapaneseLetterDataLoader.Instance.lettersDict[letter].japaneseText;
            }
            chefStation.cookButtonText.text += "\nto cook";
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
