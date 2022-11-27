using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class main_menu_controll : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

