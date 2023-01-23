using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cerca : MonoBehaviour
{
    public Transform abre;
    Vector3 aberto;
    public bool cerca, chave, fechadura, vida, medicamentos, tanque, respawn,morte;
    public GameObject keyImage;
    // Start is called before the first frame update
    void Start()
    {
       if (abre != null) aberto = abre.position;
    }

    // Update is called once per frame
    void Update()
    {
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Jogador player = other.GetComponent<Jogador>();

            if (cerca) player.vida -= 1;
            if (tanque) player.vida -= 2;
            if (vida)
            {
                player.vida += Random.Range(2, 6);
                Destroy(gameObject);
            }
            if (medicamentos)
            {
                player.vida = player.lifeBar.maxValue;
                Destroy(gameObject);
            }
            if (respawn)
            {
                player.respawnPos = transform.position;
                player.respawnRot = transform.rotation;
            }
            if (chave)
            {
                player.key = true;
                keyImage.SetActive(true);
                Destroy(gameObject);
            }
            if (fechadura && player.key)
            {
                player.key = false;
                keyImage.SetActive(false);
                abrindo();
            }
            if (morte) player.vida = 0;
        }
    }
    void abrindo()
    {
        transform.position = Vector3.Lerp(transform.position, aberto, 0.2f * Time.deltaTime);
    }
}
