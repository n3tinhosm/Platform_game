using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//faz toda a configuração de score e time do jogo.

public class GameManager : MonoBehaviour
{

	public static GameManager instance;

	public Sprite[] overlaySprites;
	public Image overlay;
	public Text timeHud;
	public Text scoreHud;

	public float time;
	public int score;

	public enum GameStatus {WIN, LOSE, DIE, PLAY}
	public GameStatus status;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy (gameObject);
		}
	}
    // Start is called before the first frame update
    void Start()
    {
    	//inicializa as variaveis

        time = 30f;
        score = 0;
        status = GameStatus.PLAY;
        overlay.enabled = false;
        Physics2D.IgnoreLayerCollision(9,10, false); //quando o jogador voltar a vida, ele volta a colidir cm os monstros

    }

    // Update is called once per frame
    void Update()
    {
        if(status == GameStatus.PLAY)
        {
        	time -= Time.deltaTime; //faz o tempo diminuir o tempo em 1 segundo por vez.
        	int timeInt = (int)time; //converte de float para inteiro;

        	if(timeInt >= 0)
        	{
        		timeHud.text = "Time: " + timeInt.ToString (); //o text do game é atualizado com o valor correto e exibido na tela
        		scoreHud.text = "Score: " + score.ToString ();
        	}

        }
        else if(Input.GetButtonDown("Jump"))
        {

        	 if(status == GameStatus.WIN)
	        {
	        	if(SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1)
	        		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //pega o index da fase atual e soma 1.
	       		else
	       		{
	        		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2); //pega o index da fase atual e soma 1.	       			
	        	}
	        }
	        else
	        {
	        	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	        }
	    }
    }

    //função que recebe do script do player se passou de fazer ou não
    public void SetOverlay(GameStatus parStatus)
    {
    	status = parStatus;
    	overlay.enabled = true;
    	overlay.sprite = overlaySprites[(int)parStatus];
    }
}
