using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpin : MonoBehaviour
{
    public Animator animator;
    public bool spinActive = false;
    private Collider2D collider2d;
    private List<RaycastHit2D> groundHits = new List<RaycastHit2D>();
    public float groundCheckDistance = 0.1f;
    public ContactFilter2D groundCheckFilter;
    // Start is called before the first frame update
    void Start()
    {
        collider2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check and Trigger for On Ground
        bool lastOnGround = animator.GetBool(PAP.isOnGround);
        bool newOnGround = CheckIfOnGround();
        animator.SetBool(PAP.isOnGround, newOnGround);

        // Sets the condition that the player has touched the ground and should switch to the landing state
        if (lastOnGround == false && newOnGround == true)
        {
            animator.SetTrigger(PAP.landedOnGround);
        }

        // Jump
        bool isJumpKeyPressed = Input.GetButtonDown(PAP.jumpKeyName);

        if (isJumpKeyPressed && newOnGround == true)
        {
            animator.SetTrigger(PAP.JumpTriggerName);
            AudioManager.PlaySound(SoundType.jumpSound);
            spinActive = true;
        }
    }

    bool CheckIfOnGround()
    {
        collider2d.Cast(Vector2.down, groundCheckFilter, groundHits, groundCheckDistance);

        if (groundHits.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
