using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{

    public GameObject player;
    public GameObject UI;
    public bool isStartingPoint;
    private GameObject spawnedPlayer;
    public GameObject gameManager;

    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance is null)
        {
            spawnedPlayer = Instantiate(player);
                
        } else
        {
           spawnedPlayer = GameObject.FindWithTag("Player");
        }

        if (UIFade.instance is null)
        {
            Instantiate(UI);
        }

        if (GameManager.instance is null)
        {
            Instantiate(gameManager);
        }

        if (isStartingPoint)
        {
            Transform PlayerTransform = spawnedPlayer.GetComponent<Transform>();
            PlayerTransform.position = transform.position;
        }
        PlayerController playerController = spawnedPlayer.GetComponent<PlayerController>();
        CameraController playerCamera = Camera.main.GetComponent<CameraController>();
        playerCamera.target = spawnedPlayer.transform;
        playerController.setBottomLeftLimit(playerCamera.tileMap.localBounds.min + new Vector3(0.5f, 1f, 0f));
        playerController.setTopRightLimit(playerCamera.tileMap.localBounds.max + new Vector3(-0.7f, -0.7f, -0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
