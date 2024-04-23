using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSocket : MonoBehaviour
{
    public JapaneseLetter japaneseLetter;
    private LetterBall letterBall;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LetterBall")
        {
            letterBall = other.gameObject.GetComponent<LetterBall>();
            japaneseLetter = letterBall.japaneseLetter;
            Debug.Log(japaneseLetter + " in");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LetterBall")
        {
            Debug.Log(japaneseLetter + " out");
            letterBall = null;
            japaneseLetter = JapaneseLetter.Null;
        }
    }

    public void Reset()
    {
        if (letterBall != null)
        {
            letterBall.gameObject.SetActive(false);
            letterBall = null;
        }
        japaneseLetter = JapaneseLetter.Null;
    }
}
