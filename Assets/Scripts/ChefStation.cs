using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ChefStation : MonoBehaviour
{
    public LetterSocket[] letterSockets;
    public Transform plateTransform;
    public AudioSource successAudio;
    public AudioSource failedAudio;
    public TextMeshProUGUI orderText;

    public UnityEvent EvtCookBtnPressed;

    [SerializeField] private GameObject grabbableBuildingBlock;

    public void EnableGrab()
    {
        grabbableBuildingBlock.SetActive(true);
    }

    public void DisableGrab()
    {
        grabbableBuildingBlock.SetActive(false);
    }

    public void OnCookBtnPressed()
    {
        EvtCookBtnPressed.Invoke();
    }
}
