using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Messenger : MonoBehaviour
{
    public RectTransform panel;
    private Text message; // ссылка на текст
    private static Coroutine RunMessage; // ссылка на запущенную корутину

    // Start is called before the first frame update
    void Start()
    {
        // берем компонент текста, т.к. текст и скрипт находятся на одном объекте
        panel.localScale = new Vector3(1,1);
        message = GetComponent<Text>();
        if (SceneManager.GetActiveScene().name == "MainScene"){
            WriteMessage("Прямо по направлению стоит серый шкаф, откройте его и заберите оттуда сумку, чтобы собрать в нее все необходимое!"); // напишите сюда первое сообщение для пользователя
        }
        else if (SceneManager.GetActiveScene().name == "GymScene"){
            WriteMessage("Найдите тренера, чтобы узнать о тренировке"); // напишите сюда первое сообщение для пользователя
        }
}
        // WriteMessage("Найдите сумку"); // напишите сюда первое сообщение для пользователя
        // Debug.Log("Найдите сумку");

    public void WriteMessage(string text) // метод для запуска корутины с выводом сообщения
    {
        // проверка и остановка корутины, если она уже была запущена
        if (RunMessage != null) 
            StopCoroutine(RunMessage);
        this.message.text = ""; // очистка строки
        // запуск корутины с выводом нового сообщения
        RunMessage = StartCoroutine(Message(text));
    }


    private IEnumerator Message(string message) // корутина для вывода сообщений
    {
        this.message.text = message; // записываем сообщение
        panel.localScale = new Vector3(1,1);
        yield return new WaitForSeconds(7f); // ждем 4 секунды
        this.message.text = ""; // очищаем строку
        panel.localScale = new Vector3(0, 0);

        // this.message.text = message; // записываем сообщение
        // yield return new WaitForSeconds(4f); // ждем 4 секунды
        // this.message.text = ""; // очищаем строку

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
