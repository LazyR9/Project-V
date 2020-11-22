using UnityEngine;
using Cinemachine;

public class Spawner : MonoBehaviour
{
    public Transform player;
    public Transform mainCamera;
    public CinemachineVirtualCamera CVC;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        player.position = transform.position;
        if (GameObject.Find("CM vcam1"))
        {
            mainCamera = GameObject.Find("CM vcam1").transform;
            CVC = mainCamera.GetComponent<CinemachineVirtualCamera>();
            CVC.OnTargetObjectWarped(player, player.position - mainCamera.position);
        }
    }
}
