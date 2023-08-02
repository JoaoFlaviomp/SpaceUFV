using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float speed = 1;
    public Vector2 screenLimite;
    public GameObject projectile;
    public Transform[] shootposition;
    public Vector2[] shootDirection;
    public float shootSpeed = 300;
    public float shootCD = .5f;
    float shootTimer = 0;
    public int vida = 10; 
    public int vidaMaxima = 10;
    int score = 0;
    float gameTimer = 0;
    public Image lifebar;
    public TextMeshProUGUI lifeText,scoreText;
    public TextMeshProUGUI newScoreText;
    public GameObject explosion;
    public GameObject menu;
    bool pause = false, morto = false;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        vida = vidaMaxima;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        gameTimer += Time.deltaTime;
        UpdateUI();
        shootTimer += Time.deltaTime;
        Moviment();
        Shoot();
        if (Input.GetButtonDown("Cancel"))
        {
            pause = !pause;
            if (pause && !morto)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
            else if (!morto)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }  
    }

    void UpdateUI()
    {
        lifebar.fillAmount = (float) vida / vidaMaxima;
        lifeText.text = vida + "/" + vidaMaxima;
        scoreText.text = "Score: " + ((int)gameTimer + score);
    }

    public void AddScore(int value = 20)
    {
        score += value;
    }

    void Shoot()
    {
        if(Input.GetAxisRaw("Jump") != 0 && shootTimer >= shootCD)
        {
            for (int i = 0; i < shootposition.Length; i++)
            {
                GameObject shoot = Instantiate(projectile);
                shoot.transform.position = shootposition[i].position;
                shoot.transform.up = shootDirection[i].normalized;
                shoot.GetComponent<Rigidbody2D>().AddForce(shootDirection[i].normalized * shootSpeed);
            }
            shootTimer = 0;
        }
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
        UpdateUI();
    }

    void Morrer()
    {
        morto = true;
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
        vida = vidaMaxima;
        transform.position = Vector2.zero;
        int oldScore = PlayerPrefs.GetInt("Score");
        int newScore = (int)gameTimer + score;
        if(newScore >= oldScore)
        {
            PlayerPrefs.SetInt("Score", newScore);
        }
        if (newScoreText != null) newScoreText.text = "Sua Pontuação: " + newScore.ToString() + "\nPontuação Maxima:" + PlayerPrefs.GetInt("Score");
        menu.SetActive(true);
        Time.timeScale = 0;
    }
    
    void Moviment()
    {
        float hMove = Input.GetAxisRaw("Horizontal");
        float vMove = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector3(hMove, vMove).normalized * speed * Time.deltaTime);
        if (transform.position.x > screenLimite.x) transform.position = new Vector3(screenLimite.x, transform.position.y);
        //if (transform.position.x < screenLimite.x) transform.position = new Vector3(screenLimite.x - .2f, transform.position.y);
        if (transform.position.x < -screenLimite.x) transform.position = new Vector3(-screenLimite.x, transform.position.y);
        //if (transform.position.x < -screenLimite.x) transform.position = new Vector3(screenLimite.x - .2f, transform.position.y);
        if (transform.position.y > screenLimite.y) transform.position = new Vector3(transform.position.x, -screenLimite.y + .2f);
        if (transform.position.y < -screenLimite.y) transform.position = new Vector3(transform.position.x, screenLimite.y -+ .2f);

    }
}
