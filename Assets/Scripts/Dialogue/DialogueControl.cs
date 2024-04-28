using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj; //janela do dialogo
    public Image profileSprite; //sprite do perfil
    public Text speechText; //texto da fala
    public Text actorNameText; //nome do npc

    [Header("Settings")]
    public float typingSpeed; //velocidade da fala

    //Variáveis de controle
    public bool isShowing; //se a janela está visivel
    private int index; //index das sentenças
    private string[] sentences;
    private string[] currentActorName;
    private Sprite[] actorSprite;

    private Player player;

    public static DialogueControl instance;

    //awake é chamado antes de todos os Start() na hierarquia de execução de scripts
    private void Awake()
    {
        instance = this;
    }


    //é chamado ao inicializar
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        
    }

    IEnumerator TypeSentence()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    //pular pra próxima frase/fala
    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            if(index < sentences.Length -1)
            {
                index++;
                profileSprite.sprite = actorSprite[index];
                actorNameText.text = currentActorName[index];
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else //quando terminam os textos
            {
                speechText.text = "";
                actorNameText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false;
                player.isPaused = false;
            }
        }
    }

    //chamar a fala do npc
    public void Speech(string[] txt, string[] actorName, Sprite[] actorProfile)
    {
        if(!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            currentActorName = actorName;
            actorSprite = actorProfile;
            profileSprite.sprite = actorSprite[index];
            actorNameText.text = currentActorName[index];
            StartCoroutine(TypeSentence());
            isShowing = true;
            player.isPaused = true;
        }
    }
}
