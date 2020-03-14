using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
	private Vector2 velocity; //velocidade da camera
	public Transform target; // alvo da camera
	public Vector2 smoothTime; // movimento da camera
	public Vector2 maxLimite; //posicao maxima da camera
	public Vector2 minLimite; //poisicao minima da camera

    // Start is called before the first frame update
    void Start()
    {
    	transform.position = new Vector3 (target.position.x, target.position.y, transform.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        //perseguir o personagem
        if(target !=  null)
        {
        	float posX = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothTime.x);
        	float posY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothTime.y);

        	transform.position = new Vector3(posX, posY, transform.position.z);

        	transform.position = new Vector3(
        		//eixo x
        		Mathf.Clamp(transform.position.x, minLimite.x, maxLimite.x),
        		//eixo y
        		Mathf.Clamp(transform.position.y, minLimite.y, maxLimite.y),
        		//eixo z
        		transform.position.z);
        }
    }
}
