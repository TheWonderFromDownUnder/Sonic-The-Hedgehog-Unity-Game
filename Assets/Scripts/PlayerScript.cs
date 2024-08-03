using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public Animator animator;
    public float groundCheckDistance = 0.1f;
    public float wallRaycastDistance = 0.6f;
    public ContactFilter2D groundCheckFilter;
    public int Respawn;
    [SerializeField] private float invulDuration;
    [SerializeField] private int invulFlashes;

    private Rigidbody2D rb;
    private Collider2D collider2d;
    private SpriteRenderer spriteRend;
    private List<RaycastHit2D> groundHits = new List<RaycastHit2D>();
    private List<RaycastHit2D> wallHits = new List<RaycastHit2D>();

    public AudioSource jumpSound;
    public RingManager rm;
    public ScoreManager sm;
    public PlayerJumpBehaviour jb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        jumpSound = GetComponent<AudioSource>();
        spriteRend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw(PAP.axisXInput);
        animator.SetFloat(PAP.moveX, moveX);
        bool isMoving = !Mathf.Approximately(moveX, 0f);
        animator.SetBool(PAP.isMoving, isMoving);

        // Check and Trigger for On Ground
        bool lastOnGround = animator.GetBool(PAP.isOnGround);
        bool newOnGround = CheckIfOnGround();
        //bool spinActive = false;
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
            //spinActive = true;
        }
        else
        {
            animator.ResetTrigger(PAP.JumpTriggerName);
        }

        // Crouch
        bool isCrouchKeyPressed = Input.GetButtonDown(PAP.crouchKeyName);

        if (isCrouchKeyPressed)
        {
            animator.SetTrigger(PAP.CrouchTriggerName);
        }
        else if (isCrouchKeyPressed && moveX < 0.1)
        {
            animator.SetTrigger(PAP.RollTriggerName);
            AudioManager.PlaySound(SoundType.rollSound);
        }
        else
        {
            animator.ResetTrigger(PAP.CrouchTriggerName);
            animator.ResetTrigger(PAP.RollTriggerName);
        }

        // Lookup
        bool isLookupKeyPressed = Input.GetButtonDown(PAP.lookupKeyName);

        if (isLookupKeyPressed)
        {
            animator.SetTrigger(PAP.LookupTriggerName);
        }
        else
        {
            animator.ResetTrigger(PAP.LookupTriggerName);
        }
    }

    void FixedUpdate()
    {
        float forceX = animator.GetFloat(PAP.forceX);

        if (forceX != 0) rb.AddForce(new Vector2(forceX, 0) * Time.deltaTime);

        float impulseY = animator.GetFloat(PAP.impulseY);
        float impulseX = animator.GetFloat(PAP.impulseX);

        if (impulseY != 0 || impulseX != 0)
        {
            float xDirectionSign = Mathf.Sign(transform.localScale.x);
            Vector2 impulseVector = new Vector2(xDirectionSign * impulseX, impulseY);

            rb.AddForce(impulseVector, ForceMode2D.Impulse);
            animator.SetFloat(PAP.impulseY, 0);
            animator.SetFloat(PAP.impulseX, 0);
        }

        animator.SetFloat(PAP.velocityX, rb.velocity.x);
        animator.SetFloat(PAP.velocityY, rb.velocity.y);

        bool isStopVelocity = animator.GetBool(PAP.stopVelocity);

        if (isStopVelocity)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool(PAP.stopVelocity, false);
        }
    }

    // Uses a collider cast on the attached collider2d to check if there are any
    // gameobjects collided with on the layers set in groundCheckFilter
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

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Ring"))
        {
            AudioManager.PlaySound(SoundType.ringSound);
            Destroy(other.gameObject);
            rm.ringCount++;
            sm.score += 10;
        }

        if(other.gameObject.CompareTag("Finish"))
        {
            AudioManager.PlaySound(SoundType.goalSound);
            StartCoroutine(waitFinish());
        }

        if (other.gameObject.CompareTag("Enemy"))
        {

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("player_jump"))
            {
                Destroy(other.gameObject);
                AudioManager.PlaySound(SoundType.breakSound);
                sm.score += 100;
            }
            else if (rm.ringCount > 0)
            {
                StartCoroutine(Invulnerability());
                rm.ringCount = 0;
                AudioManager.PlaySound(SoundType.ringlossSound);
            }
            else
            {
                StartCoroutine(waitDeath());
                AudioManager.PlaySound(SoundType.deathSound);
            }
            
        }
    }

    private IEnumerator waitDeath()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1.0f);
        SceneManager.LoadScene(Respawn);
        Time.timeScale = 1f;
    }

    private IEnumerator waitFinish()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(6.0f);
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < invulFlashes; i++)
        {
            spriteRend.color = Color.clear;
            yield return new WaitForSeconds(invulDuration / (invulFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(invulDuration / (invulFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
}
