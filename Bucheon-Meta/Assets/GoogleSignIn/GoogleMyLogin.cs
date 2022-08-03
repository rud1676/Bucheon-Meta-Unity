using Google;
using Firebase.Auth;

using UnityEngine;
using System.Threading.Tasks;
using System;

public class GoogleMyLogin : MonoBehaviour
{
    // Auth 용 instance
    FirebaseAuth auth = null;

    // 사용자 계정
    FirebaseUser user = null;

    // 기기 연동이 되어 있는 상태인지 체크한다.
    private bool signedIn = false;

    public static GoogleMyLogin Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (RuntimePlatform.Android == Application.platform || RuntimePlatform.IPhonePlayer == Application.platform)
        {
            // 초기화
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

            // 유저의 로그인 정보에 어떠한 변경점이 생기면 실행되게 이벤트를 걸어준다.
            auth.StateChanged += AuthStateChanged;
            //AuthStateChanged(this, null);
        }
    }

    // 계정 로그인에 어떠한 변경점이 발생시 진입.
    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            // 연동된 계정과 기기의 계정이 같다면 true를 리턴한다. 
            signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

            if (!signedIn && user != null)
            {
                UnityEngine.Debug.Log("Signed out " + user.UserId);
            }

            user = auth.CurrentUser;

            if (signedIn)
            {
                UnityEngine.Debug.Log("Signed in " + user.UserId);
            }
        }
    }

    //////////////
    // 익명 로그인 //
    //////////////
    public void AnonyLogin()
    {
        // 익명 로그인 진행
        auth.SignInAnonymouslyAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAnonymouslyAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                return;
            }

            // 익명 로그인 연동 결과
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    //////////////
    // 구글 로그인 //
    //////////////
    public void GoogleLoginProcessing(Action<FirebaseUser> callback)
    {
        if (GoogleSignIn.Configuration == null)
        {
            // 설정
            GoogleSignIn.Configuration = new GoogleSignInConfiguration
            {
                RequestIdToken = true,
                RequestEmail = true,
                // Copy this value from the google-service.json file.
                // oauth_client with type == 3
                WebClientId = "969168149966-6efajsgic19ke0h6qs5ds4uugouevvfl.apps.googleusercontent.com"
            };
        }

        Task<GoogleSignInUser> signIn = GoogleSignIn.DefaultInstance.SignIn();

        TaskCompletionSource<FirebaseUser> signInCompleted = new TaskCompletionSource<FirebaseUser>();

        signIn.ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.Log("bjh Google Login task.IsCanceled");
                callback(null);
            }
            else if (task.IsFaulted)
            {
                task.ToString();
                Debug.Log("bjh Google Login task.IsFaulted");
                Debug.Log("bjh google Login err:"+ task.Exception.ToString());
                callback(null);
            }
            else
            {
                Credential credential = GoogleAuthProvider.GetCredential(((Task<GoogleSignInUser>)task).Result.IdToken, null);
                auth.SignInWithCredentialAsync(credential).ContinueWith(authTask =>
                {
                    if (authTask.IsCanceled)
                    {
                        signInCompleted.SetCanceled();
                        Debug.Log("Google Login authTask.IsCanceled");
                        return;
                    }
                    if (authTask.IsFaulted)
                    {
                        signInCompleted.SetException(authTask.Exception);
                        Debug.Log("Google Login authTask.IsFaulted");
                        return;
                    }

                    user = authTask.Result;
                    Debug.LogFormat("Google User signed in successfully: {0} ({1})", user.DisplayName, user.UserId);
                    callback(user);
                    return;
                });
            }
        });
    }

    // 연동 해제
    public void SignOut()
    {
        if (RuntimePlatform.Android == Application.platform)
        {
            if (auth.CurrentUser != null)
            {
                auth.SignOut();
                GoogleSignIn.DefaultInstance.SignOut();
            }
        }
        else
        {

        }
    }

    // 연동 계정 삭제
    public void UserDelete()
    {
        if (auth.CurrentUser != null)
            auth.CurrentUser.DeleteAsync();
    }
}