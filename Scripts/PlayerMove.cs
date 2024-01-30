using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private AudioSource source;
    private Animator anim; // переменная для ссылки на контроллер анимации 
    private bool walk; // переменная для хранения состояния (движемся или нет)
     private bool walkBack; // переменная для хранения состояния (движемся или нет)
    private bool run; // переменная для хранения состояния (бежим или нет)

    private CharacterController controller; // переменная для обращения к контроллеру
    private float speedMove = 1.6f; // переменная для управления скоростью перемещения
    private float speedTurn = 30f; // переменная для управления скоростью поворота

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        Move(verticalInput);
        Turn(horizontalInput);
        Walk(verticalInput > 0);
        WalkBack(verticalInput < 0);
        Run(verticalInput > 0 && Input.GetKey(KeyCode.LeftShift));

        //маска на лкм
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("stretch");
        }

    }

    private void Walk(bool input)
    {
        if (input != walk) //если состояние изменилось
        {
            walk = input; // сохраняем движемся или нет
            anim.SetBool("walk", walk); // переключаем анимацию
            if(input==true){
                source.Play();
            }
            else {
                source.Stop();
            }
        }
    }

    private void WalkBack(bool input)
    {
        if (input != walkBack) //если состояние изменилось
        {
            walkBack = input; // сохраняем движемся или нет
            anim.SetBool("walk_back", walkBack); // переключаем анимацию
            if(input==true){
                source.Play();
            }
            else {
                source.Stop();
            }
        }
    }

     private void Run(bool input)
    {
        if (input != run) //если состояние изменилось
        {
            run = input; // сохраняем движемся или нет
            anim.SetBool("run", run);
        }
    }

    private void Move(float input)
    {
        // вычисляем вектор направления движения (-1f для эффекта гравитации)
        var movement = new Vector3(0f, -1f, input);
        movement = movement * speedMove * Time.deltaTime; // учитываем скорость и время
        // применяем смещение к контроллеру для передвижения
        controller.Move(transform.TransformDirection(movement));
    }

    private void Turn(float input)
    {
        var turn = input * speedTurn * Time.deltaTime; // выч-м величину поворота
        transform.Rotate(0f, turn, 0f); // поворачиваем на нужный угол
    }
    
    // private void FixedUpdate()
    // {
    //     AudioPlay();
    // }

    // private void AudioPlay(int v)
    // {
    //     if (v != 0 && !source.isPlaying) source.Play();
    //     else if (v == 0 && source.isPlaying) source.Stop();
    // }             
}
