using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Move : MonoBehaviour
{
    private Animator Anim;
    public float WalkSpeed = 0.01f;
    private bool IsJumping = false;
    private AnimatorStateInfo Player1Layer0;
    private bool CamWalkLeft = true;
    private bool CamWalkRight = true;
    public GameObject Player1;
    public GameObject Opponent;
    public Vector3 OppOpsition;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Listen to the animator
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

        //Cannot exit screen 
        Vector3 ScreenBounds = Camera.main.WorldToScreenPoint(this.transform.position);

        if(ScreenBounds.x > Screen.width - 200)
        {
            CamWalkRight = false;
        
        }
        if (ScreenBounds.x < 200)
        {
            CamWalkLeft = false;

        }
        else if (ScreenBounds.x > 200 && ScreenBounds.x < Screen.width -200)
        {
            CamWalkRight = true;
            CamWalkLeft = true;

        }

        //Get opponent position
        OppOpsition = Opponent.transform.position;

        //Flip around to face opponent 
        if(OppOpsition.x > transform.position.x)
        {
            StartCoroutine(LeftIsTrue());
        }
        if (OppOpsition.x < transform.position.x)
        {
            StartCoroutine(RightIsTrue());
        }

        // Walking left and right
        if (Player1Layer0.IsTag("Motion"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (CamWalkRight == true)
                {
                    Anim.SetBool("Forward", true);
                    transform.Translate(WalkSpeed, 0, 0);
                }
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                if (CamWalkLeft == true)
                {
                    Anim.SetBool("Backward", true);
                    transform.Translate(-WalkSpeed, 0, 0);
                }
            }
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            Anim.SetBool("Forward", false);
            Anim.SetBool("Backward", false);
        }
        //Jumping and Crouching
        if (Input.GetAxis("Vertical") > 0)
        {
            if (IsJumping == false)
            {
                IsJumping = true;
                Anim.SetTrigger("Jump");
                StartCoroutine(JumpPause());
            }
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            Anim.SetBool("Crouch", true);
        }
        if (Input.GetAxis("Vertical") == 0)
        {
            Anim.SetBool("Crouch", false);
        }
    }

    IEnumerator JumpPause()
    {
        yield return new WaitForSeconds(1.0f);
        IsJumping = false;
    }

    IEnumerator LeftIsTrue()
    {
        yield return new WaitForSeconds(0.15f);
        transform.Rotate(0, -180, 0);
    }
    IEnumerator RightIsTrue()
    {
        yield return new WaitForSeconds(0.15f);
        transform.Rotate(0, 180, 0);
    }


}
