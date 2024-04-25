using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSocket : MonoBehaviour
{
    public JapaneseLetter japaneseLetter;
    private LetterBall letterBall;
    private Material originalMat;
    public Material interactionMat;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
        originalMat = GetComponent<MeshRenderer>().material;
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
            GetComponent<MeshRenderer>().material = interactionMat;
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
            GetComponent<MeshRenderer>().material = originalMat;
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
