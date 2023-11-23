using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomPassos : MonoBehaviour
{
    [Header("AudioSource")]
    public AudioSource As;
    [Header("AudioClip com base no chão")]
    public AudioClip[] SomGramaAndando;
    public AudioClip[] SomGramaCorrendo;
    [Header("Som Passos em chão de pedra ou pedregulho")]
    public AudioClip[] SomPedraAndando;
    public AudioClip[] SomPedraCorrendo;
    void Start()
    {
        As = GetComponent<AudioSource>(); 
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        #region Som andando Grama
        if (other.gameObject.tag == "Grama" && MovePlayer.correndo == false)
        {
            As.PlayOneShot(SomGramaAndando[Random.Range(0,SomGramaAndando.Length)]);
        }
        if (other.gameObject.tag == "Grama" && MovePlayer.correndo == true)
        {
            As.PlayOneShot(SomGramaCorrendo[Random.Range(0, SomGramaCorrendo.Length)]);
        }
        #endregion
        #region Sons andando Pedra
        if (other.gameObject.tag == "Pedra" && MovePlayer.correndo == true)
        {
            As.PlayOneShot(SomPedraCorrendo[Random.Range(0, SomPedraCorrendo.Length)]);
        }
    else if(other.gameObject.tag == "Pedra" && MovePlayer.correndo == false)
        {
            As.PlayOneShot(SomPedraAndando[Random.Range(0, SomPedraAndando.Length)]);
        }
        #endregion
    }
}
