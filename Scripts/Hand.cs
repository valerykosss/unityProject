using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    private Transform interactObject; // объект для взаимодействия
    public AudioClip impact;
    public AudioSource audioSource;
    private Transform inHand;
    private Transform inHandRigidbody;

    [SerializeField]
    private IKAnimation playerIK; // ссылка на экземпляр скрипта IKAnimation

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ThroughItem();
        }
    }

    private void ThroughItem()
    {
        if (inHand != null) // если персонаж держит объект
        {
            inHand.parent = null; // отвязываем объект     
            StartCoroutine(ReadyToTake()); // запускаем корутину
        }
    }

    private void OnTriggerEnter(Collider other) // рука попадает в триггер
    {
        // если у триггера один из этих тегов
        if (other.CompareTag("item") || other.CompareTag("itemForTransfer"))
        {
            interactObject = other.transform; // записываем объект для взаимодействия
            playerIK.StartInteraction(other.gameObject.transform.position); // сообщаем скрипту IKAnimation о начале взаимодействия для запуска IK-анимации
        }
    }

    private IEnumerator ReadyToTake()
    {
        // yield return null; // ждем один кадр
        // inHand = null; // обнуляем ссылку

        //Destroy(inHandRigidbody.GetComponent<Rigidbody>(), 1f);

        Rigidbody rigidbod = inHand.gameObject.AddComponent<Rigidbody>();
        // код из доп задания, чтобы объект имел гравитацию при отпускании
        inHand.localEulerAngles = new Vector3(0, 0, 0);

        rigidbod.constraints = RigidbodyConstraints.FreezeRotation;
        rigidbod.constraints = RigidbodyConstraints.FreezePosition;

        //rigidbod.constraints = RigidbodyConstraints.FreezeAll;
        rigidbod.isKinematic = false;
        rigidbod.useGravity = true;

        yield return new WaitForSeconds(2f); // ждем 2 секунды
        inHand = null; // обнуляем ссылку

        Destroy(rigidbod);
        interactObject = null; // обнуляем ссылку
    }

    private void FixedUpdate() 
    {
        CheckDistance(); // проверка дистанции с объектом
    }


    // метод для проверки дистанции, чтобы была возможность прекратить взаимодействие с объектом при отдалении
    private void CheckDistance()
    {
        // если происходит взаимодействие и дистанция стала больше 2-ух
        if (interactObject != null &&
            Vector3.Distance(transform.position, interactObject.position) > 2)
        {
            interactObject = null; // обнуляем ссылку на объект
            playerIK.StopInteraction(); // прекращаем IK-анимацию
        }
    }

    private void TakeItemInPocket(GameObject item)
    {
        Debug.Log("покет");
        playerIK.StopInteraction(); // прекращение IK-анимации
        Destroy(interactObject.gameObject); // удалить объект
        MainManager.Messenger.WriteMessage("Вы подобрали "+item.name);
        Debug.Log("Вы подобрали "+ item.name);
        audioSource.PlayOneShot(impact, 0.7F);
        MainManager.Inventory.AddItem(interactObject.gameObject);

    }

    private void OnCollisionEnter(Collision collision) // при коллизии с коллайдером предмета
    {
        Debug.Log("коллижн");
        if (collision.gameObject.CompareTag("item") && inHand != null)
        {
            if (inHand.tag != "itemForTransfer")
            {
                //Debug.Log("!=");
                MainManager.Messenger.WriteMessage("Для начала необходимо найти cумку");
            }
            else if (inHand.tag == "itemForTransfer") // только объекты с тегом item будем удалять
            {
                //Debug.Log("==");
                TakeItemInPocket(collision.gameObject); // передаем в метод объект для удаления
            }
        }
        else if (collision.gameObject.CompareTag("item"))
        {
           MainManager.Messenger.WriteMessage("Для начала необходимо найти cумку");
        }

        // если это объект для перемещения и в руке нет другого предмета
        if (collision.gameObject.CompareTag("itemForTransfer") && !inHand)
        {
            TakeItemInHand(collision.gameObject.transform);
        }
    }

    //для подбора сумки 
    private void TakeItemInHand(Transform item) // добавим метод для переноса объекта
    {
        Debug.Log("хэндд");
        inHand = item; // запоминаем объект для взаимодействия
        inHand.parent = transform; // делаем руку родителем объекта
        inHand.localPosition = new Vector3(-0.09f, 0.39f, 0); // устанавливаем положение
        inHand.localEulerAngles = new Vector3(-187, 0, -7); // устанавливаем поворот
        playerIK.StopInteraction(); // останавливаем IK-анимацию

        MainManager.Messenger.WriteMessage("Вы подобрали "+item.name);
        Debug.Log("Вы подобрали "+ item.name);
    }

}
