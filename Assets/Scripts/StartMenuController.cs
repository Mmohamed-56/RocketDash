using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void StartGame(){
        SceneManager.LoadScene("Level 1");
    }

    void QuitGame(){
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
