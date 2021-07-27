using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
	public float velocidade = 10;
	public float gravidade = 20;
	public float pulo = 10;
	bool andando;

	CharacterController controlador;
	float moveY;


    void Start()
    {
		controlador = GetComponent<CharacterController>();
    }

    void Update()
    {
		if (controlador.isGrounded) 
		{
			moveY = 0;
			if (Input.GetKey(KeyCode.Space)) moveY = pulo;
		}

		moveY -= gravidade * Time.deltaTime;

		Vector3 move = Vector3.zero;
		move += Input.GetAxis("Horizontal") * transform.right * velocidade * Time.deltaTime;
		move += Input.GetAxis("Vertical") * transform.forward * velocidade * Time.deltaTime;

		if (move.magnitude >= 0.1f && !andando)
		{
			
			GetComponent<AudioSource>().Play();
			andando = true;
		}
		else if (move.magnitude <= 0.1f && andando)
		{
			GetComponent<AudioSource>().Stop();
			andando = false;
		}

		move.y = moveY * Time.deltaTime;
		
	

		controlador.Move(move);
	

	}
}
