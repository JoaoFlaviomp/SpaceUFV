using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public TextMeshProUGUI pontuacaoText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pontuacaoText != null) pontuacaoText.text = "Pontuação maxima\n" + PlayerPrefs.GetInt("Score");
    }

    public void Fechar()
    {
        Time.timeScale = 1;
        StartCoroutine(WaitFechar());
    }
    IEnumerator WaitFechar()
    {
        yield return new WaitForSeconds(.5f);
        Application.Quit();
    }
    
    public void Jogar()
    {
        Time.timeScale = 1;
        StartCoroutine(WaitJogar());
    }

    IEnumerator WaitJogar()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Level");
    }

    public void VoltarAoMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(WaitVoltarAoMenu());
    }

    IEnumerator WaitVoltarAoMenu()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Menu");
    }
}
