using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ProjectV controls;

    void Awake()
    {
        controls = new ProjectV();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("LevelManager"))
        {
            LevelManager levelData = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            Debug.Log("Level Info | Level Name: " + levelData.levelName + " | Player Health: " + levelData.playerHealth.ToString() + " | Is Multiplayer? " + levelData.isMultiplayer.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        controls.Enable();
        controls.UI.Disable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}
