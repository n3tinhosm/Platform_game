using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
	public float speed; //velocidade
	public int jumpForce; //força do pulo
	public Transform groundCheck; // saber o local que esta tocando
	public LayerMask layerGround; // qual camada que pertence ao chão
	public float radiusCheck;
	public bool grounded; // saber se esta ou nao tocando o chao
	private bool jumping; //saber se está pulando

	private Rigidbody2D rb2D;
	private Animator anim;
	private bool facingRight = true;
	private bool isAlive = true;
	private bool levelCompleted = false;
	private bool timeIsOver = false;

	public AudioClip fxWin;
	public AudioClip fxDie;
	public AudioClip fxJump;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, layerGround); //retorna se esta no chão ou nao
        if(Input.GetButtonDown("Jump") && grounded) // pega o clique do botao jump
        {
        	//comandos de pulo
        	jumping = true;
        	if(isAlive && !levelCompleted)
        		SoundManager.instance.PlayFxPlayer(fxJump);

        }
        if(((int)GameManager.instance.time <=  0) && !timeIsOver)
        {	
        	timeIsOver = true;
        	PlayerDie();
        }
        PlayAnimations();

    }

    void FixedUpdate()
    {

    	if(isAlive && !levelCompleted)
    	{
	    	float move = Input.GetAxis("Horizontal");
	    	rb2D.velocity = new Vector2 (move*speed, rb2D.velocity.y); //movimenta o personagem na horizontal (correr)


	    	if((move < 0 && facingRight) || (move > 0 && !facingRight))
	    	{
	    		Flip();
	    	}
	    	if(jumping)
	    	{
	    		rb2D.AddForce(new Vector2(0f, jumpForce)); // faz o personagem pular
	    		jumping = false; //seta o valor da variavel para false afim de que sempre faça a verificação

	    	}
	    }
	    else
	    {
	    	rb2D.velocity = new Vector2 (0, rb2D.velocity.y);
	    }
    }

    void PlayAnimations() //faz a animação da ação do personagem
    {
    	if(levelCompleted)
    	{
    		anim.Play("celebration");
    	}
    	else if(!isAlive)
    	{
    		anim.Play("die");
    	}
    	else if(grounded && rb2D.velocity.x != 0)
    	{
    		anim.Play("run");
    	}
    	else if(grounded && rb2D.velocity.x == 0)
    	{
    		anim.Play("idle");
    	}
    	else if(grounded == false)
    	{
    		anim.Play("jump");
    	}

    }
    void Flip()
    {
    	facingRight = !facingRight; //testa se esta olhando para o lado escolhido
    	transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    //faz a colisão entre player e monstro ter uma ação
    void OnCollisionEnter2D (Collision2D other)
    {
    	//player morre
    	if(other.gameObject.CompareTag("Enemy"))
    	{
    		PlayerDie();
    	}

    }

    //mata o player
    void PlayerDie()
    {	
    	isAlive = false;
    	Physics2D.IgnoreLayerCollision(9,10);
    	SoundManager.instance.PlayFxPlayer(fxDie);


    }

    //quando encostar na placa, o jogodor vence a fase atual
  	void OnTriggerEnter2D (Collider2D other)
  	{
  		if(other.CompareTag("Exit"))
  		{
  			levelCompleted = true;
  			SoundManager.instance.PlayFxPlayer(fxWin);
  		}
  	}
  	//funçao que é executada por ultimo
  	//para os casos que sejam de derrota o player, morte ou perder por tempo
  	void DieAnimationFinished()
  	{
  		if(timeIsOver)
  			GameManager.instance.SetOverlay(GameManager.GameStatus.LOSE);
  		else
  			GameManager.instance.SetOverlay(GameManager.GameStatus.DIE);
  		
  	}
  	void celebrateAnimationFinished()
  	{
  		GameManager.instance.SetOverlay(GameManager.GameStatus.WIN);
  	}
}
