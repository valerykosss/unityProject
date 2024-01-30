using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBot : MonoBehaviour
{
    public GameObject player;
    private UnityEngine.AI.NavMeshAgent botagent; // ссылка на агент навигации
    private Animator animbot; // ссылка на аниматор бота
    private float weight = 0;
    
    [SerializeField]
    private GameObject[] points; // массив точек для переходов
    //перечисление состояний бота
    private enum states
    {
        waiting, // ожидает
        going, // идёт
        dialog
    }
    states state = states.waiting; // изначальное состояние ожидания


    private void Start()
    {
    animbot = GetComponent<Animator>(); // берем компонент аниматора
    botagent = GetComponent<UnityEngine.AI.NavMeshAgent>(); // берем компонент агента
    StartCoroutine(Wait()); // запускаем корутину ожидания
    player = FindObjectOfType<PlayerMove>().gameObject;
    }


    private void FixedUpdate()
{
   switch (state)
   {
       case (states.waiting):
       {
           if (PlayerNear())
           {
               PrepareToDialog();
           }
           break;
       }
  
       case states.going:
       {
           if (PlayerNear())
           {
               PrepareToDialog();
           }
           // если дистанция до пункта назначения меньше заданного расстояния (т.е. бот дошел до выданной ему точки)
           else if ((Vector3.Distance(transform.position, botagent.destination)) < 1)
           {
               StartCoroutine(Wait()); // вызываем корутину ожидания
           }
           break;
       }


       case states.dialog:
       {
           if (!PlayerNear())
           {
               StartCoroutine(Wait());
           }
           break;
       }
   }
}
private bool PlayerNear()
{
   return (Vector3.Distance(gameObject.transform.position, player.transform.position) < 5);
}


private void PrepareToDialog()
{
   botagent.SetDestination(transform.position); // обнуляем точку, чтобы бот никуда не шёл
   animbot.SetBool("walk", false); // останавливаем анимацию ходьбы
   state = states.dialog; // устанавливаем состояние подхода к объекту в который попали лучом       
}



    private IEnumerator Wait() // корутина ожидания
    {
    botagent.SetDestination(transform.position); // обнуляем точку, чтобы бот никуда не шёл
    animbot.SetBool("walkBot", false); // останавливаем анимацию ходьбы
    state = states.waiting; // указываем, что бот перешел в режим ожидания


    yield return new WaitForSeconds(3f); // ждем 10 секунд


    botagent.SetDestination(points[Random.Range(0, points.Length)].transform.position);
    // destination – куда идти боту, передаем ему рандомно одну из наших точек
    animbot.SetBool("walkBot", true); // включаем анимацию ходьбы
    }

    private void OnAnimatorIK()
    {
   if (state == states.dialog)
   {
       if (weight < 1)
       {
           weight += 0.1f;
       }
       animbot.SetLookAtWeight(weight); // указываем силу воздействия на голову
       // указываем куда смотреть
       animbot.SetLookAtPosition(player.transform.TransformPoint(Vector3.up * 1.5f));
      
   }
   else if (weight > 0)
   {
       weight -= 0.1f;
       animbot.SetLookAtWeight(weight);
       animbot.SetLookAtPosition(player.transform.TransformPoint(Vector3.up * 1.5f));
   }
}

}

