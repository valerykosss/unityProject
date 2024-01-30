using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ТУТ ДОПИСАТЬ НА КАКУЮ СЦЕНУ ПЕРЕХОДИТ 

    public void OpenNewScene() // метод для смены сцены
    {
        //Time.timeScale = 1f;
        int index = SceneManager.GetActiveScene().buildIndex; // берем индекс запущенной сцены
        index = (index == 0) ? 1 : 0; // меняем индекс с 0 на 1 или с 1 на 0
        StartCoroutine(AsyncLoad(index)); // запускаем асинхронную загрузку сцены
    }

    public void OpenNewScene(int index) // метод для смены сцены
    {
        int ind = index;
        StartCoroutine(AsyncLoad(ind)); // запускаем асинхронную загрузку сцены
        //Time.timeScale = 1f;
    }
    private IEnumerator AsyncLoad (int index)
    {
        AsyncOperation ready = null;
        ready = SceneManager.LoadSceneAsync(index);
        while (!ready.isDone) // пока сцена не загрузилась
        {
            yield return null; // ждём следующий кадр
        }
    } 

}

