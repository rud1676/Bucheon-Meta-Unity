using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppSceneManager
{
    public static void LoadLogin()
    {
        SceneManager.LoadScene("LOGINSCENE");
    }

    public static async void LoadBucheon()
    {
        LoadingManager.Instance.Show();

        await Task.Yield();

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Bucheon");

        while (asyncOperation.isDone == false)
        {
            await Task.Yield();
        }

        LoadingManager.Instance.Hide();
    }
}
