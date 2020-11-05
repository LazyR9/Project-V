using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadLevel()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }

    public void OpenLink()
    {
        Application.OpenURL("https://forms.gle/59mi3cMasZH35TpRA");
    }
}
