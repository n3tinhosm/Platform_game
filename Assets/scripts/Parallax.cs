using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parallax : MonoBehaviour
{
	public Transform[] bgs; //pega as posiçoes dos cenarios
	public float[] parallaxVel; //velocidade de cada cenario
	public float smooth; //suavidade do cenario
	public Transform cam; //posicao da camera

	private Vector3 previewCam;

    // Start is called before the first frame update
    void Start()
    {
    	previewCam = cam.position;
        
    }

    // Update is called once per frame
    void Update()
    {
     	for(int i=0; i < bgs.Length; i++)
     	{
     		float parallax = (previewCam.x - cam.position.x) * parallaxVel[i];
     		float targetPosX = bgs[i].position.x - parallax;
     		Vector3 targetPos = new Vector3(targetPosX, bgs[i].position.y, bgs[i].position.z);
     		bgs[i].position = Vector3.Lerp(bgs[i].position, targetPos, smooth*Time.deltaTime);
     	}   

     	previewCam = cam.position;
    }
}
