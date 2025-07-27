using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzaCoetes : MonoBehaviour
{
    [SerializeField] GameObject balaPrefab;
    [SerializeField] GameObject jugador;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Disparar();
    }

    void Disparar()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 posicionDisparo = transform.position + transform.forward;
            Quaternion rotacion = transform.rotation;

            Instantiate(balaPrefab, posicionDisparo, rotacion);
        }
    }
}
