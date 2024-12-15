using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Court", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
