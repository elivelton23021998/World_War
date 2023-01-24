using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Visao : MonoBehaviourPunCallbacks
{
	public float sensibilidade = 1;
	Transform cabeca;
	

	void Start()
	{
		cabeca = transform.Find("Cabeca");
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		if (photonView.IsMine)
		{
			float rotY = Input.GetAxis("Mouse X") * sensibilidade;
			float rotX = Input.GetAxis("Mouse Y") * sensibilidade;
			transform.Rotate(0, rotY, 0);
			cabeca.Rotate(-rotX, 0, 0);

			//rotX -= Input.GetAxis("Mouse Y") * sensibilidade;
			//rotX = Mathf.Clamp(rotX, -60, 60);
			//cabeca.localEulerAngles = new Vector3(rotX, 0, 0);
		}
	}
}
