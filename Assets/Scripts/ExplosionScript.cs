using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float tempo = .1f;
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
}
