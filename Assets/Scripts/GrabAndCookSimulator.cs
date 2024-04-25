using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndCookSimulator : MonoBehaviour
{
    public LetterBall[] letterBalls;
    public Transform[] socketTransforms;

    private void FindAllLetterBalls()
    {
        letterBalls = FindObjectsOfType<LetterBall>();
    }

    public void MoveBallToSocket0(int i)
    {
        MoveBallToSocket(i, 0);
    }
    public void MoveBallToSocket1(int i)
    {
        MoveBallToSocket(i, 1);
    }
    public void MoveBallToSocket2(int i)
    {
        MoveBallToSocket(i, 2);
    }
    public void MoveBallToSocket3(int i)
    {
        MoveBallToSocket(i, 3);
    }
    public void MoveBallToSocket4(int i)
    {
        MoveBallToSocket(i, 4);
    }

    private void MoveBallToSocket(int i, int j)
    {
        if (i >= letterBalls.Length || j >= 6)
        {
            return;
        }
        letterBalls[i].GetComponent<Rigidbody>().MovePosition(socketTransforms[j].position);
    }

    public void MoveBallOutOfSocket(int i)
    {
        if (i >= letterBalls.Length)
        {
            return;
        }
        letterBalls[i].GetComponent<Rigidbody>().MovePosition(new Vector3(0, 10, 0));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (letterBalls == null && FindFirstObjectByType<LetterBall>() != null)
        {
            FindAllLetterBalls();
        }
    }
}
