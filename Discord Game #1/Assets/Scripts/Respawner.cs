using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawner : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        player = GameObject.Find("Player").transform;

        if (player.position.y < this.transform.position.y)
        {
            int countLoaded = SceneManager.sceneCount;
            Scene[] loadedScenes = new Scene[countLoaded];

            for (int i = 0; i < countLoaded; i++)
            {
                loadedScenes[i] = SceneManager.GetSceneAt(i);
            }
            SceneManager.UnloadSceneAsync(loadedScenes[1]);
            SceneManager.LoadScene(loadedScenes[1].buildIndex, LoadSceneMode.Additive);
        }
    }
}
