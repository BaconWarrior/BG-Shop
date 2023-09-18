using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private float movementSpeed = 3f;
    private Rigidbody2D rb;
    Vector2 movDirection;
    private Animator anim;
    public UnityEvent OnAction;
    private bool Bussy;
    public bool bussy
    {
        get => Bussy;
    }
    [SerializeField] GameObject interactionUI;
    [SerializeField] GameObject playerRender;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GameManager.Instance.playerController = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            OnAction?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            GameManager.Instance.playerInventory.ReturnInventory().ToggleInventory();
        }
        if (Bussy)
        {
            movDirection = Vector2.zero;
            anim.SetFloat("Velocity", movDirection.magnitude);
            return;
        }
        interactionUI.transform.position = transform.position;
        //Get the input values;
        movDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //Set animator movement values
        anim.SetFloat("Velocity", movDirection.magnitude);
        //Flip model in "X" to face movement direction only if the player is moving in x
        if(movDirection.x != 0)
            playerRender.transform.localScale = movDirection.x > 0 ? new Vector3(1, 1, 1): new Vector3(-1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Interactable"))
        {
            interactionUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interactionUI.SetActive(false);
        }
    }

    public void SetBussy(bool _state)
    {
        Bussy = _state;
    }

    public void PlayPayAnimation()
    {
        anim.SetTrigger("Pay");
    }

    private void FixedUpdate()
    {
        //Apply phisics to move de player
        rb.velocity = movDirection * movementSpeed;
    }
}
