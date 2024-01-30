using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public GameObject cube;
    public GameObject psContainer;
    public GameObject player;
    private float weight = 0;
    private UnityEngine.AI.NavMeshAgent botagent; 
    private Animator animbot; 
    [SerializeField]
    private GameObject[] points; 

    //ОЗВУЧКА БОТА
    private int dialog_flag = 0;
    [SerializeField]
    AudioClip bot_1, bot_2, player_1, player_2, bot_last;
    private AudioSource source;
    //ОЗВУЧКА БОТА

    private enum states
    {
        waiting, 
        going, 
        dialog, 
    }
    states state = states.waiting; 

     private void Start()
    {
        player = FindObjectOfType<PlayerMove>().gameObject;
        animbot = GetComponent<Animator>(); 
        botagent = GetComponent<UnityEngine.AI.NavMeshAgent>(); 
        StartCoroutine(Wait()); 
        source = GetComponent<AudioSource>();
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
                
               
                else if ((Vector3.Distance(transform.position, botagent.destination)) < 1)
                {
                    StartCoroutine(Wait()); 
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
        botagent.SetDestination(transform.position); 
        animbot.SetBool("walkBot", false); 
        state = states.dialog; 

        Vector3 relativePos = animbot.transform.position - player.transform.position;
        Debug.Log(relativePos);

        if (dialog_flag == 0)
        {
            StartCoroutine(Dialog());
            dialog_flag = 1;
        }
    }

    private IEnumerator Wait()
    {
        botagent.SetDestination(transform.position);
        animbot.SetBool("walkBot", false); 
        state = states.waiting; 


        yield return new WaitForSeconds(35f); 


        botagent.SetDestination(points[Random.Range(0, points.Length)].transform.position);
        animbot.SetBool("walkBot", true);
        state = states.going; 
    }

    private IEnumerator Dialog()
    {
        source.PlayOneShot(bot_1);
        MainManager.Messenger.WriteMessage("Добрый вечер! К сожалению, уже почти время закрывать зал.");
        yield return new WaitForSeconds(bot_1.length);
        source.PlayOneShot(player_1);
        MainManager.Messenger.WriteMessage("Здравствуйте! Простите, что пришла так поздно. Я не заметила, как летит время.");
        yield return new WaitForSeconds(player_1.length); 
        source.PlayOneShot(bot_2);
        MainManager.Messenger.WriteMessage("Ничего страшного! Если вы хотите успеть сделать тренировку, начинать вам нужно уже сейчас. У вас есть всего 5 минут!");
        yield return new WaitForSeconds(bot_2.length); 
        source.PlayOneShot(player_2);
        MainManager.Messenger.WriteMessage("Спасибо! Я постараюсь быть максимально быстрой и не задерживать вас");
        yield return new WaitForSeconds(player_2.length); 
        source.PlayOneShot(bot_last);
        MainManager.Messenger.WriteMessage("Ищите белые зоны для выполнения интенсивной тренировки на время, удачи вам!");
        cube.SetActive(true);
        psContainer.SetActive(true);
        yield return new WaitForSeconds(bot_last.length);

    }

    private void OnAnimatorIK()
    {
        if (state == states.dialog)
        {
            if (weight < 1)
            {
                weight += 0.1f;
            }
            animbot.SetLookAtWeight(weight); 
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

