
using UnityEngine;

public class PlayerController : Entity
{

    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private ContactFilter2D ground;
    private bool isGrounded => rb.IsTouching(ground); // проверка при которой прыжок работает при соприкосновении с землей
    private bool doubleJump => rb.IsTouching(ground);


    public Transform attackPos;
    public LayerMask enemy;
    public GameObject DeathPanel;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public static PlayerController Instance {get; set;}

    private States State 
    {
        get {return (States)anim.GetInteger("state");} //получает значение из Аниматора
        set {anim.SetInteger("state", (int)value);}  // А вот этим он меняет анимацию 
    }

    private void Awake() 
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update() // метод обновления на каждом кадре
    {
        if (isGrounded) State = States.idle;
        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Run()
    {
        if(isGrounded) State = States.Walk;
        Vector3 dir = transform.right * Input.GetAxis ("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump() //метод прыжка с попыткой реализовать даблджамп. С проверкой, чтобы не было возможности бесконечно спамить прыжком
    {
        if(!isGrounded) State = States.Jump;
        if (isGrounded == true)
        {
          rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);  
        }
        if ( doubleJump == true && rb.velocity.y<0)
         {
             rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
         } 
    }

    public override void GetDamage() 
    {
        lives -=1;
        Debug.Log("Players Health "+lives);
        if (lives <= 0)
        {
            if (!DeathPanel.activeSelf)
            {
               DeathPanel.SetActive(true);
               speed = 0f; 
               Time.timeScale = 0f;
            }
        }
    }

    //public void DeathPlayer()
    //{
    //    if (lives < 1)
    //    {
    //        if (!DeathPanel.activeSelf)
    //        {
    //           DeathPanel.SetActive(true);
    //           speed = 0f; 
    //        }
    //    }
   // }
   
}

public enum States //спиисок состояний
{
    idle,
    Walk,
    Jump,
    Attack
}