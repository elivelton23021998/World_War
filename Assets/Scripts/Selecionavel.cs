using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecionavel : MonoBehaviour
{
	public Material ativado;
	public Material desativado;
	public string texto;

	Renderer render;

	void Start()
	{
		render = GetComponent<Renderer>();
	}

    public void Ativar()
	{
		if (render.material != ativado) render.material = ativado;
	}

	public void Desativar()
	{
		if (render.material != desativado) render.material = desativado;
	}
}
