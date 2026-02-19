using UnityEngine;

public class ScrollTexto : MonoBehaviour
{

    public float scrollSpeed = 67;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Pega a posição atual do GameObejct pai
        Vector3 pos = transform.position;

        // posiciona o vetor apontando para a distância
        Vector3 localVectorUp = transform.TransformDirection(0,1,0);

        //Move o objeto de texto na distância para o efeito de scrolling 3D
        pos += localVectorUp * scrollSpeed *Time.deltaTime;
        transform.position = pos;


    }
}
