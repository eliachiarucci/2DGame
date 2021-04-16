using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{

    public string areaToLoad;

    public string doorName;

    public string goesTo;

    public enum Direction
    {
        Top,
        Right,
        Down,
        Left
    }

    public int offsetAmount;

    public Direction exitDirection;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerController.instance != null && PlayerController.instance.comingFrom == doorName)
        {
            PlayerController.instance.transform.position = transform.position + getOffset();
        }
    }

    Vector3 getOffset()
    {
        switch (exitDirection)
        {
            case Direction.Top:
                return new Vector3(0, offsetAmount, 0);
            case Direction.Right:
                return new Vector3(offsetAmount, 0, 0);
            case Direction.Down:
                return new Vector3(0, -offsetAmount, 0);
            case Direction.Left:
                return new Vector3(-offsetAmount, 0, 0);
            default:
                return new Vector3(0, 1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(areaToLoad);
            PlayerController.instance.comingFrom = goesTo;
        }
    }

}
