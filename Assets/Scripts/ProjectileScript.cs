using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float tempo = 1;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(tempo);
        Destroy(this.gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Player" && this.tag != "PlayerProjectile")
        {
            if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
            obj.GetComponent<PlayerScript>().TakeDamage();
            Destroy(this.gameObject);
        }
        if (obj.tag == "Enemy" && this.tag != "EnemyProjectile")
        {
            if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
            obj.GetComponent<EnemyScript>().TakeDamage();
            Destroy(this.gameObject);
        } 
    }
}
