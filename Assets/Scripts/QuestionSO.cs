using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestionSO : ScriptableObject
{
    public JapaneseLetter[] lettersInQuestion;
    public JapaneseLetter[] extraLetters;
    public GameObject foodPrefab;
}
