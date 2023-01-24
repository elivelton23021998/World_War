using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {
      
        PhotonNetwork.Instantiate("Jogador", transform.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
