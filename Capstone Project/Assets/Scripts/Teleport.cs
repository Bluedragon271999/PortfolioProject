using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform teleportTarget;
    public GameObject thePlayer;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public Camera cam4;
    private void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
        cam3.enabled = false;
        cam4.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        thePlayer.transform.position = teleportTarget.transform.position;
        cam1.enabled = false;
        cam2.enabled = true;
        cam3.enabled = false;
        cam4.enabled = false;
    }
}
