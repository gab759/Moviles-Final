using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Firestore;
using Firebase.Auth;
using UnityEngine;

public class FirestoreService : MonoBehaviour
{
    private FirebaseFirestore firestore;
    private FirebaseAuth auth;

    [SerializeField] private ScoreDatabaseSO scoreDatabase;

    private void Awake()
    {
        firestore = FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
    }

    public async void UploadPlayerScore(int score)
    {
        var user = auth.CurrentUser;
        if (user == null)
        {
            Debug.LogWarning("Usuario no autenticado.");
            return;
        }

        string uid = user.UserId;

        PlayerScoreNewData newEntry = new PlayerScoreNewData(score);

        CollectionReference personalScores = firestore
            .Collection("ranking")
            .Document(uid)
            .Collection("puntajes");

        await personalScores.AddAsync(newEntry);

        QuerySnapshot snapshot = await personalScores
            .OrderByDescending("score")
            .Limit(5)
            .GetSnapshotAsync();

        List<PlayerScoreNewData> top5 = new List<PlayerScoreNewData>();

        foreach (var doc in snapshot.Documents)
        {
            if (doc.Exists)
            {
                PlayerScoreNewData data = doc.ConvertTo<PlayerScoreNewData>();
                top5.Add(data);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (i < top5.Count)
                scoreDatabase.topScores[i].SetScore(top5[i].score);
            else
                scoreDatabase.topScores[i].SetScore(0);
        }

        await LimpiarExceso(uid, snapshot.Documents.Select(d => d.Id).ToList());
    }

    private async Task LimpiarExceso(string uid, List<string> top5IDs)
    {
        var allDocs = await firestore
            .Collection("ranking")
            .Document(uid)
            .Collection("puntajes")
            .GetSnapshotAsync();

        foreach (var doc in allDocs.Documents)
        {
            if (!top5IDs.Contains(doc.Id))
            {
                await doc.Reference.DeleteAsync();
            }
        }
    }
}
[FirestoreData]
public class PlayerScoreNewData
{
    [FirestoreProperty] public int score { get; set; }

    public PlayerScoreNewData() { }

    public PlayerScoreNewData(int score)
    {
        this.score = score;
    }
}