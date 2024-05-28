using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance { get; private set; }
    public int sceneNumber = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(sceneNumber);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}