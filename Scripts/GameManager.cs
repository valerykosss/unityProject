using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private Coroutine end; // ссылка на запущенную корутину, чтобы не проиграть после выигрыша
    public void WinGame(int i) // в случае выигрыша
    {
        if (end == null) // проверяем, была ли уже выиграна или проиграна игра
        {
            MainManager.Messenger.WriteMessage("Поздравляем, вы выиграли!"); 
            //MainManager.sceneChanger.OpenNewScene(2);
            end = StartCoroutine(NewScene(i)); // запускаем окончание игры через 4 секунды
        }
    }


    public void LoseGame(int i) // в случае проигрыша
    {
        if (end == null)
        {
            MainManager.Messenger.WriteMessage("Вы проиграли!");
            end = StartCoroutine(NewScene(i)); // запускаем окончание игры через 4 секунды
        }
    }


    public void ExitGame() // выход из игры
    {
        Application.Quit();
    }


    // private IEnumerator BeforeExit() // корутина перед выходом для прочтения последних сообщений
    // {
    //     yield return new WaitForSeconds(4f);
    //     MainManager.sceneChanger.OpenNewScene(); // выходим в главное меню
    // }

    private IEnumerator NewScene(int i) // корутина перед выходом для прочтения последних сообщений
    {
        yield return new WaitForSeconds(4f);
        MainManager.sceneChanger.OpenNewScene(i); // выходим в главное меню
    }

}
