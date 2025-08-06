using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coete : MonoBehaviour
{
    [SerializeField] float velocidad = 5f;
    [SerializeField] GameObject explosion;
    Vector3 direccion;
    [SerializeField] float tiempo;

    CapsuleCollider radioExplosion;

    void Start()
    {
        //Creacion del punto que define el centro de la pantalla
        Vector3 centroPantalla = new Vector3(Screen.width / 2f, Screen.height / 2f, 10f);
        Vector3 puntoMundo = Camera.main.ScreenToWorldPoint(centroPantalla);
        direccion = (puntoMundo - transform.position).normalized;

        //CogerComp
        radioExplosion = GetComponent<CapsuleCollider>();
    }


    void Update()
    {
        //Avanzar constantemente
        transform.position += direccion * velocidad * Time.deltaTime;

        tiempo += Time.deltaTime;

        Autodestruccion();
    }

    private void OnCollisionEnter(Collision other)
    {
        //Destruccion y generacion de particulas
        Vector3 puntoImpacto = other.contacts[0].point;
        Vector3 normal = other.contacts[0].normal;
        Quaternion posicionExplosion = Quaternion.LookRotation(-normal);

        //Particulas
        Instantiate(explosion, puntoImpacto, posicionExplosion);
        //Explosion
        radioExplosion.enabled = true;
        //Destruir
        StartCoroutine(EsperarUnFrame());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            Debug.Log("Dentro de la explosion");
            other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3 (0, 1000, 0));
        }
    }

    void Autodestruccion()
    {
        if (tiempo >= 10)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator EsperarUnFrame()
    {
        yield return null;
        Destroy(gameObject);
    }
}
