using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gem : MonoBehaviour
{
    int score = 0;
    public Text yourText;
    void OnTriggerEnter2D(Collider2D coll)
    {
        
        // Destroy the gem
        Destroy(gameObject);
    }
}
