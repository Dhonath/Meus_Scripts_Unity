using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camOrbital : MonoBehaviour
{
    [Header("Transformer do player")]
    public Transform Player;
    [Header("Altura da camera")]
    public float YoOffset;
    [Header("Limite de rota��o na vertical/recomendado 30")]
    public float Limiterot;
    [Header("Sensibilidade/velocidade da rota��o da camera")]
    public float sensibility;

    //Pegar� o valor dos Inputs do Mouse
    float rotX;
    float rotY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Cria duas variavel nova para pegar o valor dos Inputs do Mouse
        float Mouse_X = Input.GetAxis("Mouse Y");
        float Mouse_Y = Input.GetAxis("Mouse X");

        //pega os valores e multiplica pela a sensibilidade para se mover e suavizar
        rotX -= Mouse_X * sensibility * Time.deltaTime;
        rotY += Mouse_Y * sensibility * Time.deltaTime;

        //define o limite de rota��o com base no valor setado no Limitrot
        rotX = Mathf.Clamp(rotX, -Limiterot, Limiterot);


        //Colocar os valores das rota��es no transforme da camera
        transform.rotation = Quaternion.Euler(rotX, rotY, 0);


    }

    private void LateUpdate() {
        //Atualiza a posi��o da camera com base na posi��o do player
        transform.position = Player.position + Player.up * YoOffset;
    }
}
