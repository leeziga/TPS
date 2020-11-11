using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 6.0f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform cam;

    Animator animator;

    //Jump Stuff
    Vector3 velocity;
    public float gravity = -9.8f;
    public Transform groundCheck;
    public float groundDist;
    public LayerMask groundMask;
    bool isGrounded;
    public float jumpHeight = 3;

    //Dash & Movement
    public Vector3 moveDir;
    Player.GunType gunType = Player.GunType.Riffle;

    private Player player;
    [HideInInspector] public vThirdPersonCamera tpCamera;

    public GameObject abilityHolder;
    private List<IAbility> abilities = new List<IAbility>();

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        player = GetComponent<Player>();
        tpCamera = FindObjectOfType<vThirdPersonCamera>();
        foreach (var item in abilityHolder.GetComponentsInChildren<IAbility>())
            abilities.Add(item);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        CheckPlayerInput();


        var Y = Input.GetAxis("Mouse Y");
        var X = Input.GetAxis("Mouse X");

        Vector3 dir = new Vector3(h, 0, v).normalized;
        tpCamera.RotateCamera(X, Y);

        if (dir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, dir.normalized, 2.0f * Time.deltaTime, .1f);
            Quaternion _newRotation = Quaternion.LookRotation(desiredForward);

            transform.rotation = _newRotation;// Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(new Vector3(moveDir.normalized.x, 0, moveDir.normalized.z) * speed * Time.deltaTime);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        //Jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && dir.magnitude <= 0.05f)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void CheckPlayerInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //if (Input.GetMouseButtonDown(0) && h == 0.0f && v == 0.0f)
        //{
        //    player.Shoot();
        //}

        if (Input.GetKeyDown(KeyCode.Q))
        {
            gunType = (Player.GunType)(((int)gunType + 1) % (int)Player.GunType.Count);
            player.SwitchGun(gunType);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            player.ReloadGun(1.0f);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            abilities[0].ExecuteAbility();
        }
    }

}
