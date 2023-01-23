using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Jogador : MonoBehaviour
{
	public Image telaMorte;

	[HideInInspector] public Vector3 respawnPos;
	[HideInInspector] public Quaternion respawnRot;

	public Selecionavel selecionado;
	public Text texto, municao, vidaInimigo;

	public Slider lifeBar;

	public float vida =10;

	bool morto=false;

	public bool key;

    void Start()
	{
		municao.text = "";
		vidaInimigo.text = "";

		lifeBar.maxValue = vida;
		lifeBar.value = vida;
		//respawnPos = transform.position;
		//respawnRot = transform.rotation;
    }

    void Update()
    {

		lifeBar.value = vida;

		if (vida < 1 && !morto)
		{
			morto = true;
			StartCoroutine(Morte());
		}

		if (Input.GetKeyDown(KeyCode.R))
        {
			SceneManager.LoadScene(0);
		}

		if (morto) return;// o return cancela o update e evita de fazer o if

		Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(raio.origin, raio.direction * 10, Color.cyan);
		RaycastHit hit;
		Selecionavel selecao = null;


		if (Physics.Raycast(raio, out hit, 10)) {
			selecao = hit.transform.GetComponent<Selecionavel>();
		}

		// Ativa a nova seleção
		if (selecao) {
			selecao.Ativar();
		}

      //  Desativa a seleção anterior

        if (selecionado != selecao)
        {
            if (selecionado) selecionado.Desativar();
            selecionado = selecao;
        }
        if (selecionado)
        {
            texto.text = selecionado.texto;
			
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
				//if (.enabled)
                Clicavel clicado = selecionado.GetComponent<Clicavel>();
			//	Teleporte tele = selecionado.GetComponent<Teleporte>();


				if (clicado)
                {
					if (clicado.teleporte)
					{
						StartCoroutine(Teleportar(clicado.teleSpawn));
					}
					else clicado.Ativar();
                }
				
            }
        }
        else
        {
            texto.text = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
		if (other.CompareTag("Tiro"))
        {
			Dano(2);
        }

		if (other.CompareTag("Agua"))
		{
			vida = 0;
		}
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    void Dano(float valor)
    {
		vida -= valor;
		vida = Mathf.Clamp(vida, lifeBar.minValue, lifeBar.maxValue);// define o maximo e o minimo de algo
		
		

		
    }

	IEnumerator Morte()
    {
		GetComponent<CharacterController>().enabled = false;
		GetComponent<Movimento>().enabled = false;
		GetComponent<Visao>().enabled = false;


		yield return new WaitForSeconds (1);

		Color cor = telaMorte.color;
		cor.a = 0;
		while(cor.a<0.9f)
        {
			cor.a += Time.deltaTime;
			telaMorte.color = cor;
			yield return null;
        }
		telaMorte.color = cor;

		transform.position = respawnPos;
		transform.rotation = respawnRot;

		vida = lifeBar.maxValue;

		yield return new WaitForSeconds(1);

		

		cor.a = 1;
		while (cor.a > 0)
		{
			cor.a -= Time.deltaTime;
			telaMorte.color = cor;
			yield return null;
		}
		telaMorte.color = cor;


		
		GetComponent<CharacterController>().enabled = true;
		GetComponent<Movimento>().enabled = true;
		GetComponent<Visao>().enabled = true;
		morto = false;

	}

	IEnumerator Teleportar(Transform point)
	{
		GetComponent<CharacterController>().enabled = false;
		
		yield return new WaitForSeconds(0.1f);

		transform.position = point.position;
		GetComponent<CharacterController>().enabled = true;
	}
}
