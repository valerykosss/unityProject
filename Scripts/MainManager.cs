using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static GameManager game;
    public static Messenger messenger; // ссылка на скрипт для вывода сообщений
    public static SceneChanger sceneChanger;
    public static Messenger Messenger
    {
       get
       {
           if (messenger == null) // инициализация по запросу
           {
               messenger = FindObjectOfType<Messenger>();
           }
           return messenger;
       }
       private set
        {
           messenger = value;
        }
    }
    private static InventoryManager inventory;
    public static InventoryManager Inventory
    {
        get
        {
            if (inventory == null)
            {
                inventory = FindObjectOfType<InventoryManager>();
            }
            return inventory;
        }
        private set
        {
            inventory = value;
        }
    }



    private void OnEnable() // метод будет вызван при активации объекта
    {
        // находим скрипт на сцене и инициализируем ссылку
        //messenger = FindObjectOfType<Messenger>();
        DontDestroyOnLoad(gameObject);
        sceneChanger = GetComponent<SceneChanger>(); 
        game = GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
