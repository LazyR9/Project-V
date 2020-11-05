using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {   
        player = GameObject.Find("Player").transform;
        player.position = this.transform.position;
    }
}
