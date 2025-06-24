using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerScore", menuName = "Game/Player Score")]
public class PlayerScoreSO : ScriptableObject
{
    public int score;

    public void SetScore(int value)
    {
        score = value;
    }
}