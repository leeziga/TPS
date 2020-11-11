using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    ThirdPersonMovement moveScript;

    public float dashSpeed;
    public float dashTime;
    Animator animator;



    void Start()
    {
        moveScript = GetComponent<ThirdPersonMovement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            StartCoroutine(PlayerDash());
            animator.SetBool("isDashing", true);
        }
        else
        {
            animator.SetBool("isDashing", false);
        }
    }

    IEnumerator PlayerDash()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            //moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
