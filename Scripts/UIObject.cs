using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObject : MonoBehaviour {

    public GameObject objectInScene; // соответствующий объект на сцене
    [SerializeField]
    private Image imagePlace; // место для картинки
    [SerializeField]
    private Sprite image; // картинка
    private Image redApple; // место для обводки
                               // ссылки на текстуры для обводки
    [SerializeField]
    private Image border; // красная обводка
    [SerializeField]
    public bool State { get; set; } // автоматич свойство состояние подобран/не подобран объект


    private void OnEnable()
    {
        redApple = gameObject.GetComponent<Image>();
        // инициализация должна произойти до отключения объекта,
        // поэтому OnEnable, а не Start       
    }


    public void UpdateImage() // обновить картинку в зависимости от состояния
    {
        if (State) // если объект активен (подобран)
        {
            imagePlace.sprite = image; // отобразить картинку
            imagePlace.color = new Color32(255, 255, 255, 255);
            
        }
        else // если объект еще не подобран
        {
            imagePlace.color = new Color32(255, 255, 255, 125);
           
        }
    }

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
		
	}
}
