using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workout : MonoBehaviour
{
      
    public GameObject cube;
    public GameObject cube1;
    public GameObject cube2;
    // public GameObject psContainer;
    public GameObject psContainer;
    public GameObject ps1Container;
    public GameObject ps2Container;

    public ParticleSystem ps;
    public ParticleSystem ps1;
    public ParticleSystem ps2;

    public Animator anim;
    public int counterBurpee=0;
    public int counterSitup=0;
    public int counterJacks=0;
    public bool flag=false;

	public List<int> collidedObjectIDs = new List<int>();
    private int listSize = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cube")) // Проверяем, что в коллайдер вошел только игрок
        {
           int collidedObjectID = other.gameObject.GetInstanceID();
            collidedObjectIDs.Add(collidedObjectID); // Добавляем ID в список
            Destroy(other.gameObject); // Уничтожаем объект со сцены 
        }
    }

    void Update()
    {
        if (collidedObjectIDs.Count == 1){
                psContainer.SetActive(true);
                MainManager.Messenger.WriteMessage("Нажмите на кнопку J 5 раз, чтобы выполнить 5 киков");
                if (Input.GetKeyDown(KeyCode.J) && counterJacks < 5)
                {
                    anim.SetTrigger("kick"); 
                    counterJacks++;
                }
                else if (counterJacks == 5) {
                    MainManager.Messenger.WriteMessage("Вы выполнили 5 киков! Поспешите в другую зону!");
                    StartCoroutine(ExecuteFirstAfterTime(3)); 
                    // ps.Stop();
                    // cube1.SetActive(true);                    
                    // ps1Container.SetActive(true);
                    cube1.SetActive(true);                    
                    ps1Container.SetActive(true);
                }
        }

        if (collidedObjectIDs.Count == 2){
                // cube.SetActive(false);
                // psContainer.SetActive(false);

                MainManager.Messenger.WriteMessage("Нажмите на кнопку K 5 раз, чтобы выполнить скручивания 5 раз");
                if (Input.GetKeyDown(KeyCode.K) && counterSitup == 0){
                    anim.SetTrigger("situpStart"); 
                    counterSitup++;
                }
                else if (Input.GetKeyDown(KeyCode.K) && counterSitup > 0 && counterSitup < 6)
                {  
                    anim.SetTrigger("situp"); 
                    counterSitup++;
                }
                else if (counterSitup == 6) {
                    anim.SetTrigger("situpEnd"); 
                    MainManager.Messenger.WriteMessage("Вы выполнили скручиания 5 раз, отправляйтесь в следующую зону!");
                    StartCoroutine(ExecuteSecondAfterTime(3)); 
                    // ps1.Stop();
                    cube2.SetActive(true);                    
                    ps2Container.SetActive(true);
                }
        }

        if (collidedObjectIDs.Count == 3){
                // cube1.SetActive(false);
                // ps1Container.SetActive(false);
                MainManager.Messenger.WriteMessage("Нажмите на кнопку B 5 раз, чтобы выполнить берпи 5 раз");
                if (Input.GetKeyDown(KeyCode.B) && counterBurpee < 5)
                {
                    anim.SetTrigger("burpee"); 
                    counterBurpee++;
                }
                else if (counterBurpee == 5) {
                    MainManager.Messenger.WriteMessage("Вы выполнили 5 берпи!");
                    StartCoroutine(ExecuteThirdAfterTime(3)); 
                    ps2.Stop();
                    MainManager.Messenger.WriteMessage("Вы выиграли!");
                    MainManager.game.WinGame(0);
                }
        }

    }
    IEnumerator ExecuteFirstAfterTime(float time)
    {
        yield return new WaitForSeconds(time); // Ждем определенное количество секунд
        ps.Stop();
    }

    IEnumerator ExecuteSecondAfterTime(float time)
    {
        yield return new WaitForSeconds(time); // Ждем определенное количество секунд
        ps1.Stop();
    }

    IEnumerator ExecuteThirdAfterTime(float time)
    {
        yield return new WaitForSeconds(time); // Ждем определенное количество секунд
        ps2.Stop();
    }


}
