using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D PlayerRigidbody;
    private float movementSpeed;

    public Animator PlayerAnimator;

    public static PlayerController instance;

    public string comingFrom;

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    public Vector2 inputMovement;

    public bool canMove = true;

    private GameObject currentTarget;

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
        movementSpeed = 4f;
    }

    void Update()
    {
        if (canMove)
        {
            PlayerRigidbody.velocity = inputMovement * movementSpeed;
            if (inputMovement.x == 1 || inputMovement.x == -1 || inputMovement.y == 1 || inputMovement.y == -1)
            {
                PlayerAnimator.SetFloat("lastMoveX", inputMovement.x);
                PlayerAnimator.SetFloat("lastMoveY", inputMovement.y);
            }
        }
        else
        {
            PlayerRigidbody.velocity = Vector2.zero;
        }
        PlayerAnimator.SetFloat("moveX", PlayerRigidbody.velocity.x);
        PlayerAnimator.SetFloat("moveY", PlayerRigidbody.velocity.y);


        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            inputMovement = value.ReadValue<Vector2>();
            
        } else if (value.canceled)
        {
            inputMovement = Vector2.zero;
        }
    }

    public void OnAction(InputAction.CallbackContext value)
    {
        if (value.performed && currentTarget != null)
        {
            currentTarget.GetComponent<DialogActivator>().Action();
        }
    }

    public void OnMenuButton(InputAction.CallbackContext value)
    {
        if(value.performed)
        {
            GameMenu.instance.Action();
        }
    }

    public void setBottomLeftLimit(Vector3 bottomLeftLimitFromLoader)
    {
        bottomLeftLimit = bottomLeftLimitFromLoader;
    }

    public void setTopRightLimit(Vector3 topRightLimitFromLoader)
    {
        topRightLimit = topRightLimitFromLoader;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        currentTarget = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentTarget = null;
    }
}
