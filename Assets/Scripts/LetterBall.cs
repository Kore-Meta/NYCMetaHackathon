using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterBall : MonoBehaviour
{
    public bool isPickedUp;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private TextMeshPro text;
    public JapaneseLetter japaneseLetter;
    private bool canPlayAudio = true;

    private JapaneseLetterDataLoader letterDataLoader;

    public void PlayAudio()
    {
        if (canPlayAudio)
        {
            audioSource.Play();
            canPlayAudio = false;
        }
    }

    public void SetCanPlayAudio()
    {
        canPlayAudio = true;
    }

    public void BePickedUp()
    {
        isPickedUp = true;
    }

    public void SetUp(JapaneseLetter letter)
    {
        isPickedUp = false;
        canPlayAudio = true;
        letterDataLoader = FindFirstObjectByType<JapaneseLetterDataLoader>();
        audioSource.clip = Resources.Load<AudioClip>(letterDataLoader.lettersDict[letter].audioFilePath);
        text.text = letterDataLoader.lettersDict[letter].japaneseText;
        japaneseLetter = letter;
    }

    // Start is called before the first frame update
    void Start()
    {
        //if (japaneseLetter != JapaneseLetter.Null)
        //{
        //    SetUp(japaneseLetter);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
