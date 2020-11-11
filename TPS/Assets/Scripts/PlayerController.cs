using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum JumpState
    {
        Grounded,
        Jumping,
    }
    public float speed = 5.0f;
    public float jumpForce = 150.0f;

    public int playerPoints = 0;
    private Rigidbody rb;
    private JumpState _jumpState = JumpState.Grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed);
        rb.AddForce(movement);
    }

    private void Jump()
    {
        float jumpVal = Input.GetKeyDown(KeyCode.Space) ? jumpForce : 0.0f;
        switch (_jumpState)
        {
            case JumpState.Grounded:
                if (jumpVal > 0.0f)
                {
                    _jumpState = JumpState.Jumping;
                }
                break;
            case JumpState.Jumping:
                if (jumpVal > 0.0f)
                {
                    jumpVal = 0.0f;
                }
                break;
        }
        Vector3 jumping = new Vector3(0.0f, jumpVal, 0.0f);
        rb.AddForce(jumping);
    }

    private void OnCollisionEnter(Collision collision)
    {       
        if (_jumpState == JumpState.Jumping && collision.gameObject.CompareTag("Ground"))
        {
            _jumpState = JumpState.Grounded;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            PickUp pickup = other.gameObject.GetComponent<PickUp>();
            if (pickup != null)
            {
                playerPoints += pickup.Collect();
                ServiceLocator.Get<GameManager>().UpdateScore(playerPoints);
            }
        }
    }
}
