using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenu/*, somPanel*/;
    public bool ativado;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        //somPanel.SetActive(false);
        pauseMenu.SetActive(false);
        ativado = pauseMenu.activeSelf;
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (ativado) Cursor.lockState = CursorLockMode.Confined;
        //else Cursor.lockState = CursorLockMode.Locked;




        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ativado)
            {

                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                ativado = false;
            }
            else
            {
                
                pauseMenu.SetActive(true);
                ativado = true;
                Time.timeScale = 0.0f;
            }

        }


        
        

    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }


    //public void Som()
    //{
    //    somPanel.SetActive(true);
    //}

    public void Continue()
    {
        pauseMenu.SetActive(false);
        ativado = false;
    }
}
