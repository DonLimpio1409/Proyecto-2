using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coete : MonoBehaviour
{
    [SerializeField] float velocidad = 5f;
    [SerializeField] GameObject explosion;
    Vector3 direccion;

    void Start()
    {
        //Creacion del punto que define el centro de la pantalla
        Vector3 centroPantalla = new Vector3(Screen.width / 2f, Screen.height / 2f, 10f);
        Vector3 puntoMundo = Camera.main.ScreenToWorldPoint(centroPantalla);
        direccion = (puntoMundo - transform.position).normalized;
    }


    void Update()
    {
        //Avanzar constantemente
        transform.position += direccion * velocidad * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        //Destruccion y generacion de particulas
        Vector3 puntoImpacto = other.contacts[0].point;
        vector3 normal = puntoImpacto.point;
        Quaternion posicionExplosion = Quaternion.LookRotation(-normal);

        Instantiate(explosion, puntoImpacto, posicionExplosion);
        Destroy(gameObject);
    }
}
