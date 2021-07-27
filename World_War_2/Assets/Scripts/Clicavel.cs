using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicavel : MonoBehaviour
{

    public GameObject obj;
    public bool destroy;
    public Transform teleSpawn;
    public bool teleporte;
    public bool precisaChave = false;
    public Jogador player;
    public GameObject imageKey;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Ativar()
    {
        if (player.key)
        {
            precisaChave = false;
            player.key = false;
            imageKey.SetActive (false);
}

        ponte movel = obj.GetComponent<ponte>();
        if (movel)
        {
            if (!precisaChave)movel.Ativar();
        }
         obj.SetActive(true);

        if (destroy)
        {
            if (!precisaChave) Destroy(gameObject);
        }
    }
   
}
