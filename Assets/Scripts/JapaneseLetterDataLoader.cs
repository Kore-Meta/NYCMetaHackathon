using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

[Serializable]
public class Letter
{
    public string name;
    public string audioFilePath;
    public string japaneseText;
}

[Serializable]
public class LettersFromFile
{
    public Letter[] letters;
}

[Serializable]
public class LetterData
{
    public string audioFilePath;
    public string japaneseText;

    public LetterData(string newFilePath, string newText)
    {
        audioFilePath = newFilePath;
        japaneseText = newText;
    }
}

public class JapaneseLetterDataLoader : MonoBehaviour
{
    public Dictionary<JapaneseLetter, LetterData> lettersDict;
    public TextMeshPro text4Debugging;

    void LoadAndInitializeData()
    {
        lettersDict = new Dictionary<JapaneseLetter, LetterData>();

        TextAsset jsonFile = Resources.Load<TextAsset>("JapaneseLetterDict");
        if (jsonFile != null)
        {
            string json = jsonFile.text;
            LettersFromFile lettersFromFile = JsonUtility.FromJson<LettersFromFile>(json);
            //Debug.Log(lettersFromFile.letters[0].name);
            foreach (Letter letter in lettersFromFile.letters)
            {
                JapaneseLetter name;
                if (Enum.TryParse(letter.name, out name))
                {
                    lettersDict.Add(name, new LetterData(letter.audioFilePath, letter.japaneseText));
                }
            }
            //foreach (var key in lettersDict.Keys)
            //{
            //    Debug.Log("Key: " + key);
            //}
            //text4Debugging.text = lettersDict[JapaneseLetter.Ta].audioFilePath + "\n" + lettersDict[JapaneseLetter.Ta].japaneseText;
        }
        else
        {
            if (text4Debugging != null)
            {
                text4Debugging.text = "JSON file not found";
            }
        }
        //Debug.Log(lettersDict[JapaneseLetter.Ta].japaneseText);
    }

    void Start()
    {
        // Initialize the dictionary by loading JSON data
        LoadAndInitializeData();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
