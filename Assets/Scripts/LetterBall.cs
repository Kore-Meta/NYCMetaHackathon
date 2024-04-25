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
        audioSource.clip = Resources.Load<AudioClip>(JapaneseLetterDataLoader.Instance.lettersDict[letter].audioFilePath);
        text.text = JapaneseLetterDataLoader.Instance.lettersDict[letter].japaneseText;
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
