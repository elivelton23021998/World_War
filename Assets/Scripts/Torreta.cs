using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Torreta : MonoBehaviour
{
    public float vida = 10;

    public GameObject tiro, jogador, spawn, recompensa;

    public float angulo = 45, distanceRay = 30, rotVel = 120;

    float tempo;

    public bool torreta, perseguidor, tanque, chefe;

    public NavMeshAgent agente;
    bool drop = false, atirando;

    public Image telaFim;
    public GameObject textos;
    public float x;
    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        x = vida;
        StartCoroutine(ProcuraJogador());
        //jogador = GameObject.Find("Jogador");  
    }

    // Update is called once per frame
    void Update()
    {
      

      



        if (chefe)
        {
            if (vida < 25 && !drop)
            {

                recompensa = Instantiate(recompensa, transform.position, recompensa.transform.rotation);
                drop = true;
            }
            if (vida <= 0)
            {
                StartCoroutine(EndGame());
            }
        }
        if (vida <= 0 && !chefe)
        {
            recompensa = Instantiate(recompensa, transform.position, recompensa.transform.rotation);
            Destroy(gameObject);
        }

        if (JogadorProximo() && tanque)
        {
            agente.SetDestination(jogador.transform.position);
        }

        if (JogadorProximo() && !tanque)// jogador perto de 30 unidades
        {
            if (perseguidor) agente.SetDestination(jogador.transform.position);

            transform.LookAt(jogador.transform);// a rotacao vai virar pro jogador, mas tem q ta a frente pro eixo z (azul)
            if (tempo < Time.time)
            {
                if (chefe) tempo = Time.time + 0.1f;
                else tempo = Time.time + 0.5f;// esse 0.f é uma chamada de tempo pra se fazer um delay

                Quaternion rotacao = Quaternion.FromToRotation(tiro.transform.up, transform.forward);//eixo Y (up) vira pra frente (foward ou eixo Z) da torreta
                GameObject copia = Instantiate(tiro, spawn.transform.position, rotacao);//identity nao aplica rotacao
                copia.GetComponent<Rigidbody>().AddForce(transform.forward * 500);//foward na direcao z
                Destroy(copia, 3);

                if (!atirando)
                {
                    GetComponent<AudioSource>().Play();
                    atirando = true;
                }


            }
        }
        else
        {
            if (atirando)
            {
                GetComponent<AudioSource>().Stop();
                atirando = false;
            }
            transform.Rotate(0, rotVel * Time.deltaTime, 0);//player roda 180 graus por segundo
        }

    }

    bool JogadorProximo()
    {

        if (Vector3.Distance(transform.position, jogador.transform.position) < distanceRay)
        {

            Vector3 alvo = jogador.transform.position - transform.position;
            if (Vector3.Angle(transform.forward, alvo) <= angulo)// se o angulo entre a visao da torreta e o caminho do plauer ser a msm
            {
                Ray raio = new Ray(transform.position, alvo);
                Debug.DrawRay(raio.origin, raio.direction, Color.red);
                RaycastHit hit;
                if (Physics.Raycast(raio, out hit, distanceRay))
                {
                    if (hit.transform == jogador.transform)
                    {
                        return true;
                    }
                }

            }
        }
        return false;
    }

    IEnumerator EndGame()
    {
        Color cor = telaFim.color;
        cor.a = 0;

        while (cor.a < 0.9f)
        {
            cor.a += Time.deltaTime;
            telaFim.color = cor;
            yield return null;
        }
        textos.SetActive(true);
        telaFim.color = cor;
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }

    IEnumerator ProcuraJogador()
    {
        GameObject[] jogadores = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject bola in jogadores)
        {
            if (Vector3.Distance(bola.transform.position, transform.position) < Vector3.Distance(jogador.transform.position, transform.position))
            {
                jogador= bola;
            }
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(ProcuraJogador());
    }
}
            
        



