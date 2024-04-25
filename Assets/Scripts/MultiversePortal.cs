using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiversePortal : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public Transform centerTransform;
    [SerializeField] private Transform bottomTransform;

    public void PlayEngineAudio()
    {
        audioSource.Play();
    }

    public float GetHeightAboveGround()
    {
        return centerTransform.position.y - bottomTransform.position.y;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
