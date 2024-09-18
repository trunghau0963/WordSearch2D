// using UnityEngine;
// using System.IO;

// [System.Serializable]
// public class ScoreData
// {
//     public int score;
// }
// public class Score : MonoBehaviour
// {
//     public int score;

//     string file = "score.json";

//     public void LoadScoreFromJson()
//     {
//         if (File.Exists(file))
//         {
//             try
//             {
//                 string json = File.ReadAllText(file);
//                 score = JsonUtility.FromJson<ScoreData>(json).score;
//             }
//             catch (IOException e)
//             {
//                 Debug.LogError("Error reading score file: " + e.Message);
//             }
//         }
//         else
//         {
//             Debug.LogWarning("Score file not found, initializing score to 0.");
//             score = 0;
//         }
//     }

//     public void SaveScoreToJson()
//     {
//         ScoreData scoreData = new ScoreData { score = score };
//         string json = JsonUtility.ToJson(scoreData);
//         try
//         {
//             File.WriteAllText(file, json);
//         }
//         catch (IOException e)
//         {
//             Debug.LogError("Error writing score file: " + e.Message);
//         }
//     }
// }
