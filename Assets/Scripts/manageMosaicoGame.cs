using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class manageMosaicoGame : MonoBehaviour
{
    // Dados para gerar a imagem e o local marcado
    public Image parte;
    public Image localMarcado;
    float lmLargura, lmAltura;

    bool partesEmbaralhadas = false;
    public List<string> partesNoLugarCerto;

    //Nome da imagem do mosaico a ser buscada na pasta Resources
    public string NomeImagem;


    //Variaveis temporizador 
    public float cronometro; // Tempo Decorrido
    public float timer; // Tempo restante
    public float tempoLimite; // Tempo limite para montar o quebra cabeça em segundos

    public bool TimerAtivado = false;  //bool que ativa o timer
    public Toggle JogoComLimiteDeTempo; // .isOn = bool que ativa o modo de jogo com tempo limite

    public TextMeshProUGUI TempoTMPro; //GameObject do timer
    public TextMeshProUGUI TempoFinalTMPro; //GameObject do timer - tela final

    //Paineis de fim de jogo
    public GameObject PainelVitoria;
    public GameObject PainelDerrota;
    

    public AudioClip inicioAudio;
    

    void falaPlay(){
        GameObject.Find("totemPlay").GetComponent<tocadorPlay>().playPlay();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0; //Pausa a contagem de tempo na unity
        timer = tempoLimite;
    }

    // Update is called once per frame
    void Update()
    {       
        if(cronometro>= 4 && ! partesEmbaralhadas){
            EmbaralharPartes();
            falaPlay();
            partesEmbaralhadas = true;

            if (JogoComLimiteDeTempo.isOn) //Verifica se a opção de jogar com tempo limite foi marcada
            {
                TimerAtivado = true; // Ativa o temporizador com a contagem regressiva
            }
            
            
        }

        if (partesNoLugarCerto.Count == 25) //Verifica se todas as partes estão no lugar certo
        {            
            PainelVitoria.SetActive(true);
            Time.timeScale = 0; //Pausa a contagem de tempo na unity

            if (JogoComLimiteDeTempo.isOn)
            {
                TempoFinalTMPro.text = "Tempo restante: "+TempoTMPro.text; //Atualiza o texto do painel Vitória com o tempo restante
            }
            else
            {
                TempoFinalTMPro.text = "Tempo de jogo: " + TempoTMPro.text; //Atualiza o texto do painel Vitória com o tempo de jogo
            }
            
        }

        if (timer <= 0) //Caso o modo de jogo seja com tempo limite e o timer chegar a 0, ativa o Painel Derrota
        {
            PainelDerrota.SetActive(true);
            Time.timeScale = 0; //Pausa a contagem de tempo na unity
        }
    }

    #region Controle do Cronometro e do timer
    private void FixedUpdate()
    {
        cronometro += Time.deltaTime; // guarda o tempo de jogo
        if (TimerAtivado)
        {
            timer -= Time.deltaTime; // guarda o tempo restante com base no tempo limite
            ConverterSParaMinEAtualizar(TempoTMPro,timer); //Atualiza o temporizador na tela com o tempo de jogo restante
        }
        else if (!JogoComLimiteDeTempo.isOn)
        {
            ConverterSParaMinEAtualizar(TempoTMPro, cronometro); //Atualiza o temporizador na tela com o tempo de jogo decorrido
        }
    }

    public void ConverterSParaMinEAtualizar(TextMeshProUGUI texto, float t)
    {
        int min = (int)(t / 60);
        int s = (int)(t % 60);

        texto.text = min.ToString("00") + ":" + s.ToString("00");
    }    

    #endregion
    
    //Chamada pelo botão Jogar do painel inicial
    public void IniciarJogo() 
    {        
        Time.timeScale = 1; //Reinicia a contagem de tempo na unity
        criarLocaisMarcados();
        CriarPartes();
        tocadorInicio.instance.PlayAudio(inicioAudio, 1f);
        //EmbaralharPartes();
    }
  
    //Seleção da imagem do mosaico pelos botões do painel inicial
    public void DefinirImagem(string nome)
    {
        NomeImagem = nome;
    }

    void criarLocaisMarcados(){
        lmLargura = 100;
        lmAltura = 100;
        float numLinhas = 5;
        float numColunas = 5;

        float linha, coluna;

        for (int i=0; i<25; i++){
            Vector3 posicaoCentro = new Vector3();
            posicaoCentro = GameObject.Find("ladoDireito").transform.position;

            linha = i% 5;
            coluna = i /5;

            Vector3 lmPosicao = new Vector3(posicaoCentro.x + lmLargura*(linha-numLinhas/2), posicaoCentro.y - lmAltura*(coluna-numColunas/2), posicaoCentro.z);
            Image lm = (Image)(Instantiate(localMarcado,lmPosicao,Quaternion.identity));
            lm.tag = "" + (i+1);
            lm.name = "LM" + (i+1);
            lm.transform.SetParent(GameObject.Find("LocaisMarcados").transform);
        }
    }

    public void CriarPartes(){
        lmLargura = 100;
        lmAltura = 100;
        float numLinhas = 5;
        float numColunas = 5;

        float linha, coluna;

        for (int i=0; i<25; i++){
            Vector3 posicaoCentro = new Vector3();
            posicaoCentro = GameObject.Find("ladoEsquerdo").transform.position;

            linha = i% 5;
            coluna = i /5;

            Vector3 lmPosicao = new Vector3(posicaoCentro.x + lmLargura*(linha-numLinhas/2), posicaoCentro.y - lmAltura*(coluna-numColunas/2), posicaoCentro.z);
            Image lm = (Image)(Instantiate(parte,lmPosicao,Quaternion.identity));
            lm.tag = "" + (i+1);
            lm.name = "Parte" + (i+1);
            lm.transform.SetParent(GameObject.Find("Partes").transform);

            Sprite[] todasSprites = Resources.LoadAll<Sprite>(NomeImagem);
            Sprite s1 = todasSprites[i];
            lm.GetComponent<Image>().sprite = s1;
        }
    }

    public void EmbaralharPartes(){
        int[] novoArray = new int[25];
        for (int i = 0; i < 25; i++) novoArray[i] =i;
        int temp;

        for (int t = 0; t < 25; t++){
            temp = novoArray[t];
            int r = Random.Range(t,10);
            novoArray[t] = novoArray[r];
            novoArray[r] = temp;
        }

        float linha, coluna, numColunas, numLinhas;
        numColunas = numLinhas = 5;

        for (int i=0; i<25; i++){            
            
            linha = (novoArray[i])% 5;
            coluna = (novoArray[i]) /5;
            
            Vector3 posicaoCentro = new Vector3();
            posicaoCentro = GameObject.Find("ladoEsquerdo").transform.position;

            var g = GameObject.Find("Parte" + (i+1));


            Vector3 novaPosicao = new Vector3(posicaoCentro.x + lmLargura*(linha-numLinhas/2), posicaoCentro.y - lmAltura*(coluna-numColunas/2), posicaoCentro.z);
            g.transform.position = novaPosicao;
            g.GetComponent<DragAndDrop>().posicaoInicialPartes();
        }


    }

    public void RegistrarParteNoLugarCerto(string tag_parte)
    {
        if (!partesNoLugarCerto.Contains(tag_parte))
        {
            partesNoLugarCerto.Add(tag_parte);
        }
        
    }

    public void RemoverParteDoLugarCerto(string tag_parte)
    {
        partesNoLugarCerto.Remove(tag_parte);
    }
}
