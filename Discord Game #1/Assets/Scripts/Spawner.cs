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
        mainCamera = GameObject.Find("CM vcam1").transform;
        CVC = mainCamera.GetComponent<CinemachineVirtualCamera>();
        player.position = transform.position;
        CVC.OnTargetObjectWarped(player, player.position - mainCamera.position);
    }
}
