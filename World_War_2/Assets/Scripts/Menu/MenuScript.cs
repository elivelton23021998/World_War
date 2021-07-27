using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] //telas
    private GameObject logo, menu;

    [SerializeField] //botoes
    private GameObject botoes;// btnNovoJogo, btnContinuar, btnConfiguracoes, btnCreditos, btnSair;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {

    }



    public void OpenMenu()
    {
        logo.SetActive (false);
        menu.SetActive (true);
    }

    public void NovoJogo()
    {
        SceneManager.LoadScene("Game");
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

    
}
