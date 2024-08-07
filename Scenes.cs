using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void ChangeScenes(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
    }

    public void Exit()
    {
        Debug.Log("Game is closed");
        Application.Quit();
    }
}
