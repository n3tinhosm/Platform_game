using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public float speed; //velocidade
	public Transform groundCheck; // saber o local que esta tocando
	public LayerMask layerGround; // qual camada que pertence ao chão
	public float radiusCheck;
	public bool grounded; // saber se esta ou nao tocando o chao

	private Rigidbody2D rb2D;
	private Animator anim;
	private bool facingRight = true;
	private bool isVisible = false;
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
    	if(!grounded)
    	{
    		Flip();
    	}
        
    }
    void FixedUpdate()
    {
    	if(isVisible) //se o monster aparecer na tela, ele se move 
    	{
    		rb2D.velocity = new Vector2 (speed, rb2D.velocity.y);
    	}
    	else
    	{
    		rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
    	}
    }
    void Flip()
    {
    	facingRight = !facingRight; //testa se esta olhando para o lado escolhido
    	transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    	speed*= -1;
    }
    void OnBecameVisible()
    {
    	Invoke ("MoveEnemy", 1f);
    }
    void OnBecameInvisible()
    {
    	Invoke ("StopEnemy", 3f);
    }

    void MoveEnemy()
    {
    	isVisible = true;
    	anim.Play("run");
    }
    void StopEnemy()
    {
    	isVisible = false;
    	anim.Play("MosterA");
    }
}
