using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    private void Awake()
    {
        //Singleton patterns
        // todo : Çok basit mantığı vardır. Awake herşeyden önce çalışır.
        // todo : ScenePersist'leri arıyoruz.
        // todo : Eğer bu objeler 1'den büyük ise, birini destroy ediyoruz.
        // todo : Eğer değilse Bunu destryo etme diyip, var olan objelerimiz korumunu sağlıyoruz.
        // todo : Bu şekilde enemies ve coins'ler aynı levelde olsak bile, yok edilten sonra tekrar yaratılmıyor.
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        Debug.LogError(numScenePersists);
        if (numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }
}