using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System;

public class MenuScript :MonoBehaviourPunCallbacks
{
    static string nomeJogador;
    [SerializeField] InputField texto;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {

    }

    public void NovoJogo()
    {
        nomeJogador = texto.text;

        PhotonNetwork.AutomaticallySyncScene = true; // sincronizar cena dos jogadores

        PhotonNetwork.ConnectUsingSettings();// conecta ao servidor

        
    }

    public void Continuar()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void Mute()
    {
        AudioListener.pause=true;
    }
    public void UnMute()
    {
        AudioListener.pause = false;
    }



    ////////////////////////////////////// parte online 
    
    
    
    
    
    public override void OnConnectedToMaster() //uma vez conectado ao servidor...
    {
        base.OnConnectedToMaster();
        print("OnConnectedToMaster");
         
        PhotonNetwork.JoinRandomRoom();// joga o jogador a uma sala qualquer que ja esteja criada
    }

    public override void OnJoinRandomFailed(short returnCode, string message) // se nao tiver sala disponivel...
    {
        base.OnJoinRandomFailed(returnCode, message);
        print("OnJoinRandomFailed" + message);
        
        PhotonNetwork.CreateRoom(null); // criar nova sala com o nome
    }

    public override void OnJoinedRoom()//se entrou na sala...
    {
        base.OnJoinedRoom();
        print("OnJoinedRoom()");
        PhotonNetwork.LoadLevel("Game");

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        print("OnDisconnected" + cause);
        
        PhotonNetwork.LoadLevel("Menu");
    }
}
