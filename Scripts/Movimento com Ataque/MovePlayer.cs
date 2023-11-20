using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    #region Componentes que ser�o setados manualmente
    [Header("Velocidade Movimento")]
    private int Velocidade;
    public int velocidadeAndando;
    public int velocidadeCorrendo;

    [Header("Animator Player")]
    public Animator anim;

    [Header("Camera que ser� usada")]
    public Camera MainCamera;
    #endregion

    #region Inputs_Direcao
    //Esses Inputs ser�o usados para definir a dire��o que o player ir� andar, conforme onde a camera esteja onlhando.
    float InputX;
    float InputZ;

    //Dire��o para onde o player vai andar
    Vector3 Direcao;

    #endregion

    #region Detector se pode andar e Rolar
    public static bool andar;
    public static bool rolar;
    public static bool correndo;
    private bool ataquePesado;
    #endregion


    void Start()
    {
        //Fazer o cursor sumir da tela quando o jogo iniciar
        Cursor.lockState = CursorLockMode.Locked;


        //Pega o Animator Controller assim que o jogo iniciar, caso esque�a de setar manualmente
        anim = GetComponent<Animator>();

        //Precisa iniciar como "true" para permitir que o player ande
        andar = true;
    }

    // Update is called once per frame
    void Update()
    {
        #region Inputs e Direcao
        //Pega o valor dos inputs de andar como "WASD" ou "Analogico"
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

       

        //Usar� os inputs X e Z para setar a dire��o em que o player ir� se mover
        Direcao = new Vector3(InputX, 0, InputZ);
        #endregion

        #region Condicao para se mover e rotacao Player
        //Verifica se algum input foi pressionado e se est� permitido andar
        if (InputX != 0 && andar == true && rolar == false || InputZ != 0 && andar == true && rolar == false)
        {
            //cria uma variavel local que pega a rota��o atual da camera
            var CamRot = MainCamera.transform.rotation;
            CamRot.x = 0;
            CamRot.z = 0;

            //Move o Player para frente
            transform.Translate(0,0, Velocidade * Time.deltaTime);
            //O Player ir� rotacionar conforme a camera, e sempre andar� em dira��o onde a camera est� apontando
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Direcao) * CamRot, 5 * Time.deltaTime);
        }
        #endregion

        #region Faz os outros comando fora funcionar
        Animacoes();
        Speed();
        Attack();
        VerificarBotao();
        #endregion
    }



    
    public void Animacoes()
    {
        if(InputX != 0 && andar == true && rolar == false && correndo == false || InputZ != 0 && andar == true && rolar == false && correndo == false)
        {
            anim.SetInteger("moviment", 1);
        }

        #region Rolagem Player
        if (Input.GetKeyDown(KeyCode.Space) && andar == true && rolar == false)
        {
            StartCoroutine(TempoRolagem());
        }
        #endregion

        #region Player Correndo
        if (InputX != 0 && andar == true && rolar == false && correndo == true || InputZ != 0 && andar == true && rolar == false && correndo == true)
        {
            anim.SetInteger("moviment", 3);
        }
        #endregion

        //Verifica se todos os inputs est�o zerados e se o Player n�o est� fazendo outra a��o
        if (InputX == 0 && InputZ == 0 && andar == true && rolar == false)
        {
            anim.SetInteger("moviment", 0);
        }
    }

    public void Speed()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Velocidade = velocidadeCorrendo;
            correndo = true;
        }
        else
        {
            Velocidade = velocidadeAndando;
            correndo = false;
        }
    }

    public void Attack()
    {
        //Ataques Leves
        if (Input.GetButtonDown("Fire1") && ataquePesado == false)
        {
            StartCoroutine(TempoAtaqueSimples());
        }
        if (Input.GetButtonDown("Fire2") && ataquePesado == false)
        {
            StartCoroutine(TempoAtaqueSubindo());
        }
        //ataques Pesado
        if (Input.GetButtonDown("Fire1") && ataquePesado == true)
        {
            StartCoroutine(TempoAtaquePesado1());
        }
        if(Input.GetButtonDown("Fire2") && ataquePesado == true)
        {
            StartCoroutine(TempoAtaquePesado2());
        }
    }

    public void VerificarBotao()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ataquePesado = true;
        }
        else
        {
            ataquePesado = false;
        }
    }

    //Ir� fazer a rolagem do Player e espera o tempo da anima��o para permitir que o player volte a andar
    IEnumerator TempoRolagem()
    {
        andar = false;
        rolar = true;
        anim.SetInteger("moviment", 2);
        yield return new WaitForSeconds(2.0f);
        andar = true;
        rolar = false;
        anim.SetInteger("moviment", 0);
        yield return null;
    }

    IEnumerator TempoAtaqueSimples()
    {
        andar = false;
        anim.SetInteger("moviment", 4);
        yield return new WaitForSeconds(1.50f);
        andar = true;
        anim.SetInteger("moviment", 0);
        yield return null;
    }

    IEnumerator TempoAtaqueSubindo()
    {
        andar = false;
        anim.SetInteger("moviment", 5);
        yield return new WaitForSeconds(2.20f);
        andar = true;
        anim.SetInteger("moviment", 0);
        yield return null;
    }

    IEnumerator TempoAtaquePesado1()
    {
        andar = false;
        anim.SetInteger("moviment", 6);
        yield return new WaitForSeconds(3.30f);
        andar = true;
        anim.SetInteger("moviment", 0);
        yield return null;
    }

    IEnumerator TempoAtaquePesado2()
    {
        andar = false;
        anim.SetInteger("moviment", 7);
        yield return new WaitForSeconds(2.0f);
        andar = true;
        anim.SetInteger("moviment", 0);
        yield return null;
    }
  
}
