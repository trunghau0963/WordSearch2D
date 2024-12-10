using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Extensions;
using Google;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FirebaseManager : MonoBehaviour
{
    public string GoogleAPI = "1000133012177-f0h9ds19spohj02h621duin7jcc62rtl.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;

    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    // public Text Username, UserEmail;

    // public GameObject LoginScreen, ProfileScreen;

    private void Awake() {
        configuration = new GoogleSignInConfiguration{
            WebClientId = GoogleAPI,
            RequestIdToken = true,
        };
    }

    private void Start() {
        InitFirebase();
    }

    void InitFirebase() {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }

    public void GoogleSignInClick() {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticatedFinished);
    }

    void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task) {
        if (task.IsFaulted) {
            Debug.LogError("Faulted");
        } else if (task.IsCanceled) {
            Debug.LogError("Cancelled");
        } else {
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);

            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task => {
                if (task.IsCanceled) {
                    return;
                }

                if (task.IsFaulted) {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                    return;
                }

                user = auth.CurrentUser;

                // Username.text = user.DisplayName;
                // UserEmail.text = user.Email;

                // LoginScreen.SetActive(false);
                // ProfileScreen.SetActive(true);

                // StartCoroutine(LoadImage(CheckImageUrl(user.PhotoUrl.ToString())));
                SceneManager.LoadScene("MainScene");
            });
        }
    }

    // private string CheckImageUrl(string url) {
    //     if (!string.IsNullOrEmpty(url)) {
    //         return url;
    //     }
    //     return imageUrl;
    // }

    // IEnumerator LoadImage(string imageUri) {
    //     WWW www = new WWW(imageUri);
    //     yield return www;

    //     UserProfilePic.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    // }
}