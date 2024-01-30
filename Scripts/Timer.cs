using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField]
    Text text; // ссылка куда выводить текст времени
    private DateTime timer = new DateTime(1, 1, 1, 0, 4, 00); // задаем стартовое время таймера


    private void Start()
    {
        StartCoroutine(Timerenumerator());
    }

    // Update is called once per frame
    void Update () {
		
	}

    private IEnumerator Timerenumerator() // корутина будет запускаться
    {
        while (true)
        {
            text.text = timer.Minute.ToString() + ":" + timer.Second.ToString(); // вывод в строку
            timer = timer.AddSeconds(-1); // отнимает от времени одну секунду


            if (timer.Second == 0 && timer.Minute == 0) // когда времени не осталось
            {
                //text.text = "Время вышло!"; // пишем, что время вышло
                text.color = new Color(1, 0, 0); // красим текст красным
                MainManager.game.LoseGame(0); // вызываем конец игры!
                break; // завершаем корутину
            }
            yield return new WaitForSeconds(1); // ждем секунду
        }
    }

}
