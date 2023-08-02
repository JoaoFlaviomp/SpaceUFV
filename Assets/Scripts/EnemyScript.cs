using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 1;
    public Vector2 screenLimite = new Vector2(9, 5);
    int direction = 1;

    int vida = 5;
    public int vidaMaxima = 5;

    public GameObject projectile;
    public float shootDistance = 1;
    public float shootSpeed = 300;
    public float shootCD = .8f;
    float shootTimer = 0;
    Vector2 shootDirection = Vector2.left;
    public GameObject explosion;
    public int scoreBonus = 20; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        vida = vidaMaxima;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        Move();
        Shoot();
    }

    public void TakeDamage(int damage = 1)
    {
        if (damage < 0) return;
        if (vida - damage > 0) vida -= damage;
        else
        {
            vida = 0;
            Morrer();
        }
    }

    void Morrer()
    {
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
        try
        {
            FindObjectOfType<PlayerScript>().AddScore(scoreBonus);
        }
        catch { }
        
        vida = vidaMaxima;
        transform.position = new Vector2(screenLimite.x, transform.position.y);
    }
    
    void Shoot()
    {
        if(shootTimer > shootCD)
        {
            GameObject shoot = Instantiate(projectile);
            shoot.transform.position = transform.position + Vector3.left * shootDistance;
            shoot.transform.up = shootDirection.normalized;
            shoot.GetComponent<Rigidbody2D>().AddForce(shootDirection.normalized * shootSpeed);
            shootTimer = 0;
        }
    }
    void Move()
    {
        transform.Translate(new Vector2(-speed * Time.deltaTime, direction * speed * 2 * Time.deltaTime));
        if (transform.position.y > screenLimite.y || transform.position.y < -screenLimite.y)
        {
            direction *= -1;
            transform.position = new Vector2(transform.position.x, Mathf.Sign(transform.position.y) * screenLimite.y);
        }
        if (transform.position.x < -screenLimite.x) transform.position = new Vector2(screenLimite.x, transform.position.y);
    }
}
