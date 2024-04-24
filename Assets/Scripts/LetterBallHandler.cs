using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LetterBallHandler : MonoBehaviour
{
    public LetterBall ballPrefab;
    public float speed = 0.1f;
    public float interval = 0.15f;

    private int nBalls;
    private float[] ballPositions;
    private LetterBall[] balls;
    private float ballPathLength;

    public ConveyorBeltBuilder beltBuilder;

    public TextMeshPro text4Debugging;

    public QuestionSO questionSO;

    void Start()
    {
        beltBuilder.Evt_OnBeltBuilt.AddListener(OnBeltBuilt);
        beltBuilder.Evt_OnBeltDestroyed.AddListener(Reset);
    }

    void FixedUpdate()
    {
        if (beltBuilder.belt != null && balls != null)
        {
            MoveBallsOnBelt();
        }
    }

    public void OnBeltBuilt()
    {
        if (beltBuilder.isBeltBuilt && questionSO != null)
        {
            SetUpBalls();
#if UNITY_EDITOR
            Debug.Log("length is " + beltBuilder.belt.pathLength.ToString());
#else
            //if (text4Debugging != null)
            //    text4Debugging.text = belt.pathLength.ToString();
#endif
        }
    }

    public void SetUpBalls()
    {
        int n = (int)(beltBuilder.belt.pathLength / interval);
        nBalls = questionSO.lettersInQuestion.Length + questionSO.extraLetters.Length;
        if (nBalls < n)
            nBalls = n;
        ballPositions = new float[nBalls];
        balls = new LetterBall[nBalls];
        JapaneseLetter[] letters = GenerateRandomizedLetters(nBalls);
        //text4Debugging.text = letters[0].ToString();
        for (int i = 0; i < nBalls; i++)
        {
            ballPositions[i] = i * interval - beltBuilder.belt.pathLength / 2f;
            balls[i] = Instantiate(ballPrefab);
            balls[i].transform.position = GetBallPositionOnBelt(ballPositions[i]);
            balls[i].SetUp(letters[i]);
        }
        ballPathLength = Mathf.Max(beltBuilder.belt.pathLength, nBalls * interval);
        //text4Debugging.text = FindFirstObjectByType<JapaneseLetterDataLoader>().lettersDict[letters[0]].japaneseText;

        beltBuilder.belt.speed = speed;
    }

    private JapaneseLetter[] GenerateRandomizedLetters(int n)
    {
        JapaneseLetter[] letters = new JapaneseLetter[n];
        int i = 0;
        while (i < questionSO.lettersInQuestion.Length)
        {
            letters[i] = questionSO.lettersInQuestion[i];
            i++;
        }
        while (i < questionSO.lettersInQuestion.Length + questionSO.extraLetters.Length)
        {
            letters[i] = questionSO.extraLetters[i - questionSO.lettersInQuestion.Length];
            i++;
        }
        while (i < n)
        {
            letters[i] = letters[Random.Range(0, questionSO.lettersInQuestion.Length + questionSO.extraLetters.Length)];
            i++;
        }
        Shuffle(letters);
        return letters;
    }

    // Function to shuffle the array
    private void Shuffle<T>(T[] array)
    {
        // Create a random number generator
        System.Random rng = new System.Random();

        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }

    private Vector3 GetBallPositionOnBelt(float ballPosition)
    {
        Vector3 pos, norm;
        beltBuilder.belt.GetPositionOnPath(ballPosition, out pos, out norm);
        return pos + 0.1f * norm;
    }

    private void MoveBallsOnBelt()
    {
        for (int i = 0; i < nBalls; i++)
        {
            if (balls[i].isPickedUp)
            {
                continue;
            }
            ballPositions[i] += speed * Time.deltaTime;
            if (ballPositions[i] > ballPathLength)
            {
                ballPositions[i] -= ballPathLength;
            }
            balls[i].transform.position = GetBallPositionOnBelt(ballPositions[i]);
            // TODO: I'm not able to fix this for now
            //if (ballPositions[i] < 0 || ballPositions[i] > beltBuilder.belt.pathLength)
            //{
            //    balls[i].gameObject.SetActive(false);
            //}
            //else
            //{
            //    balls[i].gameObject.SetActive(true);
            //}
        }
    }

    public void Reset()
    {
        DestroyBalls();
    }

    public void ResetAll()
    {
        DestroyBalls();
        questionSO = null;
    }

    public void DestroyBalls()
    {
        foreach (var ball in balls)
        {
            Destroy(ball.gameObject);
        }
        balls = null;
    }
}
