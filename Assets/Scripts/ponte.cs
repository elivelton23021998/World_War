using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ponte : MonoBehaviour
{
    public Vector3 aberto, fechado;
    public bool fechando=true;

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fechando)
        {
            transform.position = Vector3.MoveTowards(transform.position, fechado, 12 *Time.deltaTime);//toward mostra o qto anda por segundo ou frame
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, aberto, 0.2f * Time.deltaTime);//learp mostra em porcentagem
        }
    }
    public void Ativar()
    {
        fechando = !fechando;
    }
}
