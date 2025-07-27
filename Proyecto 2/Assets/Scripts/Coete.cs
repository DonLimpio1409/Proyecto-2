using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coete : MonoBehaviour
{
    [SerializeField] float velocidad = 5f;
    

    void Start()
    {
        Vector3 centroPantalla = new Vector3(Screen.width / 2f, Screen.height / 2f, 10f);
        Vector3 puntoMundo = Camera.main.ScreenToWorldPoint(centroPantalla);
        Vector3 direccion = (puntoMundo - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direccion * velocidad * Time.deltaTime;
    }
}
