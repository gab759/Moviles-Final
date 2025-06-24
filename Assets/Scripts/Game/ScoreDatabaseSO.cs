
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreDatabase", menuName = "Game/Score Database")]
public class ScoreDatabaseSO : ScriptableObject
{
    [Header("Top 5 Puntajes")]
    public PlayerScoreSO[] topScores = new PlayerScoreSO[5];

    public void AddNewScore(int newScore)
    {
        var list = new System.Collections.Generic.List<PlayerScoreSO>(topScores);

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null)
            {
                list[i] = ScriptableObject.CreateInstance<PlayerScoreSO>();
                list[i].score = 0;
            }
        }

        var newEntry = ScriptableObject.CreateInstance<PlayerScoreSO>();
        newEntry.SetScore(newScore);
        list.Add(newEntry);

        list.Sort((a, b) => b.score.CompareTo(a.score));

        for (int i = 0; i < 5; i++)
            topScores[i] = list[i];
    }
}

