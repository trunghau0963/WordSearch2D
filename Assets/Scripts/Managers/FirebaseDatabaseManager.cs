// using Firebase;
// using Firebase.Database;
// using Firebase.Extensions;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class FirebaseDatabaseManager : MonoBehaviour
// {
//     private DatabaseReference reference;
//     private void Awake(){
//         FirebaseApp app = FirebaseApp.DefaultInstance;
//         reference = FirebaseDatabase.DefaultInstance.RootReference;
//     }

//     void Start(){
//         WriteDatabase("1", "Hello World!");
//     }

//     public void WriteDatabase(string id, string message){
//         reference.Child("Users").Child(id).SetRawJsonValueAsync(message).ContinueWithOnMainThread(task => 
//         {
//             if(task.IsCompleted){
//                 Debug.Log("Data written successfully!");
//                 // Update UI or do something with the data
//             }
//             else{
//                 Debug.LogError("Error writing data: " + task.Exception.Message);
//             }

//         });

//     }

//     public void ReadDatabase(string id){

//     }
// }
