using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D PlayerRigidbody;
    public float movementSpeed;

    public Animator PlayerAnimator;

    public static PlayerController instance;

    public string comingFrom;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * movementSpeed;
        
        PlayerAnimator.SetFloat("moveX", PlayerRigidbody.velocity.x);
        PlayerAnimator.SetFloat("moveY", PlayerRigidbody.velocity.y);

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            PlayerAnimator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            PlayerAnimator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }

    }
}
