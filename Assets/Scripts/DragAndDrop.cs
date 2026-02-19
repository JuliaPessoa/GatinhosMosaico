using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    Vector3 posicaoOriginal;
    private string Tag ;
    public bool lugarCerto;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicaoOriginal = transform.position;
        Tag = gameObject.tag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Drag(){
        GameObject.Find("Parte"+Tag).transform.position = Mouse.current.position.ReadValue();
        print("Arrastando: " + gameObject.name);
        if (lugarCerto)
        {
            GameObject.Find("manageMosaicoGame").GetComponent<manageMosaicoGame>().RemoverParteDoLugarCerto(Tag);
        }
    }

    public void Drop()
    {
        checkMatch();
    }

    public void moveBack()
    {
        transform.position = posicaoOriginal;
    }

    public void Snap(GameObject img, GameObject lm)
    {
        img.transform.position = lm.transform.position;
        GameObject.Find("manageMosaicoGame").GetComponent<manageMosaicoGame>().RegistrarParteNoLugarCerto(Tag);
        lugarCerto = true;
    }

    public void checkMatch()
    {
        //GameObject lm1 =  GameObject.Find("LM1");
        //GameObject img =  GameObject.Find("Image");


        
        GameObject lm1 =  GameObject.Find("LM"+Tag);

        GameObject img =  gameObject;

        float distance = Vector3.Distance(lm1.transform.position, img.transform.position);

        print("Distância: "+ distance);

        if (distance<= 50)
        {
            Snap(img, lm1);
        }
        else
        {
            moveBack();
        }


    }

    public void posicaoInicialPartes(){

        posicaoOriginal = transform.position;

    }
    
    }
