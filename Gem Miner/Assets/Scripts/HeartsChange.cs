using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsChange : MonoBehaviour
{
    public Image img;
    public Image img2;
    public Image img3;
    public Sprite newSprite;
    public int health = 3;
    // Start is called before the first frame update
    public void HealthChange()
    {
            health--;
            if (health == 0)
            {
                img = GetComponent<Image>();
                img.sprite = newSprite;
            }
            else if (health == 1)
            {
                img2 = GetComponent<Image>();
                img2.sprite = newSprite;
            }
            else if (health == 2)
            {
                img3 = GetComponent<Image>();
                img3.sprite = newSprite;
            }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
