using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenteDeCena : MonoBehaviour
{
   public void ReiniciarJogo()
    {
        SceneManager.LoadScene("MosaicoAudios"); //Reinicia a cena do mosaico
    }

    public void Sair()
    {
        print("Saiu");
        Application.Quit(); //Sai do jogo, mas não funciona nos testes na unity
    }
}
