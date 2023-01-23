using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    public GameObject proximo;
    public Text texto;
    public string objetivo;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextObjetivo());
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Jogador>().respawnPos = other.transform.position;
            other.GetComponent<Jogador>().respawnRot = other.transform.rotation;
            if (proximo) proximo.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    IEnumerator NextObjetivo()
    {
     
        texto.text = objetivo;

        yield return new WaitForSeconds(3);
        
        texto.text = "";
    }
}
