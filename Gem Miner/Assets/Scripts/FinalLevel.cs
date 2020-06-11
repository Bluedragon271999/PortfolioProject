using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalLevel : MonoBehaviour
{
    public Text score;
    public void OnTriggerEnter2D()
    {
        if (score.text == "Score: 1000")
        {
            SceneManager.LoadScene("Win");
        }
        else if (score.text != "Score: 1000")
        {
            SceneManager.LoadScene("Lose");
        }
    }
}
