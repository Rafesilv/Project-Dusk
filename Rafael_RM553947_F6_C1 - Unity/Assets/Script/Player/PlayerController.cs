using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D colliderPlayer;
    private float moveX;
    private AudioSource sound;

    [Header ("Atributtes")]
    public float speed;
    public float jumpForce;
    public int addJumps;
    public int life;

    [Header ("Ground Detection")]
    public LayerMask groundLayer;
    public float groundCheckDistance;
    public Vector2 groundCheckOffsetLeft;
    public Vector2 groundCheckOffsetRight;

    [Header ("Bool")]
    public bool isGrounded;
    [HideInInspector] public bool isPause;
    
    [Header ("UI")]
    public TextMeshProUGUI TextLife;

    [Header ("GameObjects")]
    public GameObject gameOver;
    public GameObject canvasPause;

    [Header ("Level")]
    public string levelName;

    private void Awake()
    {
        sound = GetComponent<AudioSource>();

        if(PlayerPrefs.GetInt("wasLoaded") == 1)
        {
            life = PlayerPrefs.GetInt("Life", 0);
            Debug.Log("Game loaded");
        }
    }

    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        colliderPlayer = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        TextLife.text = life.ToString();

        if (life <= 0)
        {
            this.enabled = false;
            colliderPlayer.enabled = false;
            rb.gravityScale = 0;
            anim.Play("Die", -1);
            gameOver.SetActive(true);

        }

        if (Input.GetButtonDown("Cancel"))
        {
            PauseScreen();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            string activeScene = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("LevelSaved", activeScene);
            PlayerPrefs.SetInt("Life", life);
            Debug.Log("Game saved");
            
        }
        
        if (isGrounded)
        {
            addJumps = 2;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && addJumps > 0)
            {
                addJumps--;
                Jump();
            }
        }

        Attack();
    }

    void FixedUpdate()
    {
        Move();

        CheckGroundedStatus();
    }

    #region Move
    void Move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        if(moveX > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.SetBool("IsRun", true);

            groundCheckOffsetLeft.x = -Mathf.Abs(groundCheckOffsetLeft.x);
            groundCheckOffsetRight.x = Mathf.Abs(groundCheckOffsetRight.x);
        }

        else if(moveX < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anim.SetBool("IsRun", true);

            groundCheckOffsetLeft.x = Mathf.Abs(groundCheckOffsetLeft.x);
            groundCheckOffsetRight.x = -Mathf.Abs(groundCheckOffsetRight.x);
        }

        else
        {
            anim.SetBool("IsRun", false);
        }
    }   
    #endregion

    void Jump()
    {
        anim.SetBool("IsJump", true);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        
    }

    void Attack()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            anim.Play("Attack", -1);
            sound.Play();
        }
    }
    void PauseScreen()
    {
        if(isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            canvasPause.SetActive(false);
        }

        else
        {
            isPause = true;
            Time.timeScale = 0;
            canvasPause.SetActive(true);
        }

    }

    public void ResumeGame()
    
    {
        isPause = false;
        Time.timeScale = 1;
        canvasPause.SetActive(false);
    }

    public void BackMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void CheckGroundedStatus()
    {
        Vector2 position = transform.position;
        Vector2 groundCheckPositionLeft = position + groundCheckOffsetLeft;
        Vector2 groundCheckPosisitonRight = position + groundCheckOffsetRight;

        RaycastHit2D hitLeft = Physics2D.Raycast(groundCheckPositionLeft, Vector2.down, groundCheckDistance, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(groundCheckPosisitonRight, Vector2.down, groundCheckDistance, groundLayer);

        isGrounded = hitLeft.collider != null || hitRight.collider != null;
        
        anim.SetBool("IsJump", !isGrounded);
    }

    void OnDrawGizmos()
    {
        Vector3 groundCheckPositionLeft = transform.position + new Vector3(groundCheckOffsetLeft.x, groundCheckOffsetLeft.y, 0f);
        Vector3 groundCheckPositionRight = transform.position + new Vector3(groundCheckOffsetRight.x, groundCheckOffsetRight.y, 0f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheckPositionLeft, groundCheckPositionLeft + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(groundCheckPositionRight, groundCheckPositionRight + Vector3.down * groundCheckDistance);
    }

}