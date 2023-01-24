using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    
    float masterVol;

    Slider masterSlider;

    // Start is called before the first frame update
    void Start()
    {
        masterSlider = GameObject.FindGameObjectWithTag("Musica").GetComponent<Slider>();
        masterSlider.value = PlayerPrefs.GetFloat("MasterSond");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MasterVol (float volume)
    {
        masterVol = volume;
        AudioListener.volume = masterVol;
        PlayerPrefs.SetFloat("MasterSond",masterVol);
    }
    
}
