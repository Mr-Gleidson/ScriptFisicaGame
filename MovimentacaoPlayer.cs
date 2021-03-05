using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Definindo velocidade da rotação do personagem
    public float turnSpeed = 20f;
    
    //Referência para animação
    Animator m_Animator;

    //Para parte do sistema da física do personagem, movimentação do corpo
    Rigidbody m_Rigidbody;
    
    //Vetor de movimento
    Vector3 m_Movement;

    //Armazenamento de rotações
    Quaternio m_Rotation = Quaternion.indentity;
    
    void Start()
    {
        //Um dos primeiros métodos a serem iniciados em um MonoBehaviour está no Start
        //Obtendo uma referência a um 'Animator' para atribuir a var chamada de m_Animator
        m_Animator = GetComponent<Animator> ();

        //Referência ao Rigidbody
        m_Rigibody = GetComponent<m_Rigidbody>();
    }

   
    void FixedUpdate()
    {
        //Variável Float para horizontal e vertical.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Vetor criado, definição para variável
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();

        //Variável Booleana, definindo o valor de retorno de um método. !Operador de negação, inverte um bool,  tornando um true como false
        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        
        //Valor definido ao parâmetro Animator
        m_Animator.SetBool("IsWalking", isWalking);

        //Rotação do personagem
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);

        //Definição da var de rotação
        m_Rotation = Quaternion.LookRotation(desiredForward);
        
    }

    //Movimento
    void OnAnimatorMove ()
    {
        //Mudança na posição
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);

        //Rotação
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
