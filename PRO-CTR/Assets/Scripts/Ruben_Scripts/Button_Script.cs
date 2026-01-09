using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Script : MonoBehaviour
{
    public void LoadLevel(string p = "")
    {
        SceneManager.LoadScene(p);
        Debug.Log("Loaded level!!");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
