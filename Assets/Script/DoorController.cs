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

    private float waitToLoad;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerController.instance != null && PlayerController.instance.comingFrom == doorName)
        {
            PlayerController.instance.transform.position = transform.position + GetOffset();
            UIFade.instance.setLoading(false);
        }
    }

    Vector3 GetOffset()
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
        if (UIFade.instance.loading && UIFade.instance.fadeScreen.color.a == 1)
        {
            UIFade.instance.setLoading(false);
            GameManager.instance.fadingBetweenAreas = false;
            SceneManager.LoadScene(areaToLoad);
            PlayerController.instance.comingFrom = goesTo; 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            UIFade.instance.setLoading(true);
            GameManager.instance.fadingBetweenAreas = true;
        }
    }

}
