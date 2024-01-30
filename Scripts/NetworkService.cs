using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkService : MonoBehaviour
{
    private Material mainTexture; // ссылка на текстуру экрана


private void Start()
{
   mainTexture = GetComponent<Renderer>().material; // инициализируем ссылку на текстуру
   StartCoroutine(ShowImages()); // запускаем корутину, сменяющую изображения
}
//массив из 10 изображений для загрузки, замените ссылки на свои!
private string[] webImages =
{
   "https://images8.alphacoders.com/110/1106033.jpg",
   "https://images.alphacoders.com/108/1089087.jpg",
   "https://images7.alphacoders.com/114/1145219.jpg",
   "https://images4.alphacoders.com/112/1128334.jpg"
};


private Texture[] Images = new Texture[4]; // массив из загруженных изображений
private int i = 0; // счетчик, чтобы знать какое изображение показывается


private IEnumerator ShowImages() // корутина смены изображений
{
   while (true)
   {
       if (Images[i] == null) // если требуемой текстуры нет в массиве
       {
           WWW www = new WWW(webImages[i]); // загружаем изображение по ссылке      
           yield return www; // ждем когда изображение загрузится
           Images[i] = www.texture; // записываем загруженную текстуру в массив
       }
     mainTexture.mainTexture = Images[i]; // устанавливаем текстуру из массива изображений
       i++; // увеличиваем счетчик
       if (i == 4)
       {
           i = 0; // если загрузили уже 9, возвращаемся к первому
       }
       yield return new WaitForSeconds(3f); // ждем 3 секунды между сменой изображений
   }
}

}
