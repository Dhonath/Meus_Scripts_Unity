using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    #region Componentes que serão setados manualmente
    [Header("Velocidade Movimento")]
    private int Velocidade;
    public int velocidadeAndando;
    public int velocidadeCorrendo;

    [Header("Animator Player")]
    public Animator anim;

    [Header("Camera que será usada")]
    public Camera MainCamera;
    #endregion

    #region Inputs_Direcao
    //Esses Inputs serão usados para definir a direção que o player irá andar, conforme onde a camera esteja onlhando.
    float InputX;
    float InputZ;

    //Direção para onde o player vai andar
    Vector3 Direcao;

    #endregion

    #region Detector se pode andar e Rolar
    public static bool andar;
    public static bool rolar;
    private bool correndo;
    #endregion


    void Start()
    {
        //Fazer o cursor sumir da tela quando o jogo iniciar
        Cursor.lockState = CursorLockMode.Locked;


        //Pega o Animator Controller assim que o jogo iniciar, caso esqueça de setar manualmente
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

       

        //Usará os inputs X e Z para setar a direção em que o player irá se mover
        Direcao = new Vector3(InputX, 0, InputZ);
        #endregion

        #region Condicao para se mover e rotacao Player
        //Verifica se algum input foi pressionado e se está permitido andar
        if (InputX != 0 && andar == true && rolar == false || InputZ != 0 && andar == true && rolar == false)
        {
            //cria uma variavel local que pega a rotação atual da camera
            var CamRot = MainCamera.transform.rotation;
            CamRot.x = 0;
            CamRot.z = 0;

            //Move o Player para frente
            transform.Translate(0,0, Velocidade * Time.deltaTime);
            //O Player irá rotacionar conforme a camera, e sempre andará em diração onde a camera está apontando
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Direcao) * CamRot, 5 * Time.deltaTime);
        }
        #endregion

        #region Faz os outros comando fora funcionar
        Animacoes();
        Speed();
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

        //Verifica se todos os inputs estão zerados e se o Player não está fazendo outra ação
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

    //Irá fazer a rolagem do Player e espera o tempo da animação para permitir que o player volte a andar
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

  
}
