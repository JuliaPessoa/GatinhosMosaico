using UnityEngine;
using UnityEngine.SceneManagement;

public class ChamaMosaico : MonoBehaviour
{
    bool primeira;
    float t;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        primeira = true;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        print(t);
        t += Time.deltaTime;
        if (t > 20 && primeira) // 35 = tempo em segundos do áudio de abertura
        {
            primeira = false;
            SceneManager.LoadScene("MosaicoAudios");
        } 
                            


    }
}
