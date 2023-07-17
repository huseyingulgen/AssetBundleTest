
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private int maxSceneIndex = 5; // Belirlediğiniz maksimum sahne indeksi
    [SerializeField] private int sceneStartIndex = 0; // Başlama sahnesi indeksi
    public int currentSceneIndex = 0; // Mevcut sahne indeksi
    public int outputSceneIndex = 0; // Çıktı sahne indeksi

    void Start()
    {
        
        LoadSceneIndexes();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(SceneTransition());
    }

    IEnumerator SceneTransition()
    {
        // Sahnenin süresi için bekleyin (örneğin 5 saniye)
        yield return new WaitForSeconds(3f);

        // Sonraki sahneye geç
        NextScene();

       
    }

    void NextScene()
    {
        currentSceneIndex++;
        if (currentSceneIndex > maxSceneIndex)
        {
            currentSceneIndex = sceneStartIndex;
        }

        // Çıktı sahne indeksini artır ve PlayerPrefs ile kaydet
        outputSceneIndex++;
        PlayerPrefs.SetInt("OutputSceneIndex", outputSceneIndex);

        // Sahne geçişini gerçekleştir
        SceneManager.LoadScene(currentSceneIndex);

        // İndeksleri kaydet
        SaveSceneIndexes();
    }



    private void SaveSceneIndexes()
    {
        PlayerPrefs.SetInt("CurrentSceneIndex", currentSceneIndex);
        PlayerPrefs.SetInt("OutputSceneIndex", outputSceneIndex);
        PlayerPrefs.Save();
    }

    private void LoadSceneIndexes()
    {
        currentSceneIndex = PlayerPrefs.GetInt("CurrentSceneIndex", sceneStartIndex);
        outputSceneIndex = PlayerPrefs.GetInt("OutputSceneIndex", 0);
    }
}
