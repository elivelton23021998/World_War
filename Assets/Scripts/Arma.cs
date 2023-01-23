using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arma : MonoBehaviour
{
    public GameObject tiro;
    Vector2 centro;

    public LayerMask layers;

    public bool equipada, pistola, metralha;
    public GameObject outraArma;

    public float municao, dano;

    public Text texto,inimigoVida;

    float outraMunicao;
    bool atirando;
    //float x;

    // Start is called before the first frame update
    void Start()
    {
        centro = new Vector2(Screen.width / 2, Screen.height / 2);
        equipada = true;
        outraArma.SetActive (false);
        
        
        
    }

    // Update is called once per frame
    void Update()
    {

        if (pistola)texto.text = municao.ToString("000") + " / 20";
        else if (metralha) texto.text = municao.ToString("000") + " / 100";

        TrocaDeArma();

        Ray raio = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(raio, out hit, 50, layers))//so vai colidir nas layerrs selecionadas
        {
            Torreta inimigo = hit.transform.GetComponent<Torreta>();

            if (inimigo)
            {
               // x = inimigo.x;
                inimigoVida.text = inimigo.vida.ToString("00") + " / " + inimigo.x;
            }
            else
            {
                inimigoVida.text = "";
            }
        }



        if (pistola)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Shot(hit, raio);
                if (municao > 0) GetComponent<AudioSource>().Play();
            }
        }

        else if (metralha)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Shot(hit, raio);
                if (municao > 0) if (!atirando) GetComponent<AudioSource>().Play();
                atirando = true;
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                GetComponent<AudioSource>().Stop();
                atirando = false;
            }
        }
        

    }

    void Shot(RaycastHit hit, Ray raio)
    {

        if (Physics.Raycast(raio, out hit, 50, layers))//so vai colidir nas layerrs selecionadas
        {
            if (municao > 0)
            {
                Vector3 posicao = hit.point + hit.normal / 100; //subindo um pouco o hit point pra nao sobrepor a textura
                Quaternion rotacao = Quaternion.FromToRotation(tiro.transform.forward, -hit.normal);// muda a parte da frente do tiro ao contrario do objeto
                GameObject clone = Instantiate(tiro, posicao, rotacao);
                municao--;
                Torreta inimigo = hit.transform.GetComponent<Torreta>();
                if (inimigo)
                {
                    inimigo.vida -= dano;
                    Destroy(clone);
                    if (inimigo.perseguidor)
                    {
                        inimigo.agente.SetDestination(transform.position);
                    }
                    
                }
                Destroy(clone, 1);//5 significa q destroy em 5 segundos
            }
        }
    }


    void TrocaDeArma()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (outraArma.GetComponent<Arma>().equipada) //se a outra arma estiver equipada
            {
                if (metralha)
                {
                    outraArma.SetActive(true);
                    outraArma.GetComponent<Arma>().municao += outraMunicao;
                    outraMunicao = 0;
                    if (outraArma.GetComponent<Arma>().municao > 20) outraArma.GetComponent<Arma>().municao = 20;
                    gameObject.SetActive(false);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (outraArma.GetComponent<Arma>().equipada) //se a outra arma estiver equipada
            {
                if (pistola)
                {
                    outraArma.SetActive(true);
                    outraArma.GetComponent<Arma>().municao += outraMunicao;
                    outraMunicao = 0;
                    if (outraArma.GetComponent<Arma>().municao > 100) outraArma.GetComponent<Arma>().municao = 100;
                    gameObject.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arma"))
        {
            if (pistola) municao += Random.Range (5,35);
            if (metralha) outraMunicao += Random.Range(5, 35);

            if (municao > 20 && pistola) municao = 20;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Metralha"))
        {
            if (metralha) municao += Random.Range(5, 35);
            if (pistola) outraMunicao += Random.Range(5, 35);

            if (municao > 100 && metralha ) municao = 100;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Municao"))
        {
           
            if (metralha) municao = 100;
            if (pistola) municao = 20;
            Destroy(other.gameObject);
        }
    }
  
}
