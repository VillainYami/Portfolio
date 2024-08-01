using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [HideInInspector] Camera cam;
    [HideInInspector] public GameObject ppov;

    void Start()
    {
        cam = GetComponent<Camera>();
        ppov = GameObject.Find("Player");
    }

    void Update()
    {
        cam.transform.position = new Vector3(ppov.transform.position.x,ppov.transform.position.y,-10);
    }
}
