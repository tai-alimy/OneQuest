using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    [SerializeField] GameObject panel1;
    [SerializeField] GameObject panel2;
    [SerializeField] TextMeshProUGUI[] panel2ButtonsTexts;
    [SerializeField] GameObject panel3;
    [SerializeField] Transform player;
    [SerializeField] TextMeshProUGUI panel2Text;

    int panel2CorrectAnswered;
    bool inDialogue;
    bool dialogueDone;

    public static int timeLeft;
    public TMP_Text timeLeftText;
    // Start is called before the first frame update
    void Start()
    {
        panel2CorrectAnswered = 0;
        dialogueDone = false;
        timeLeft = 60;
        timeLeftText.text = "60";
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0)
        {
            dialogueDone = false;
        }
        if (dialogueDone)
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(false);
            player.GetComponent<PlayerMovement>().enabled = true;

            timeLeftText.text = timeLeft.ToString();

        }
        else
        {
            StartDialogue();
            if (inDialogue)
            {
                player.GetComponent<PlayerMovement>().enabled = false;
            }
            else
            {
                player.GetComponent<PlayerMovement>().enabled = true;


            }
        }
        

        
    }

    void StartDialogue()
    {
        if (Vector3.Distance(transform.position, player.position) <=3&&!dialogueDone)
        {
            transform.LookAt(player);
            inDialogue = true;
            if (!panel2.activeSelf && !panel3.activeSelf)
            {
                panel1.SetActive(true);
            }
        }
        else
        {
            inDialogue = false;
           

        }
    }

    public void NothingButtonClicked()
    {
        transform.Translate(0, 0, -3);
    }

    public void HelpButtonClicked()
    {
        panel1.SetActive(false);

        int[] QA = Randomize();
        panel2Text.text = "Only smart people can help me I want to check if you smart enough to help me. how much is" +
                                                                                QA[0] +" + "+QA[1]+"?";
        panel2ButtonsTexts[0].text = Random.Range(6, 300).ToString();
        panel2ButtonsTexts[1].text = Random.Range(6, 300).ToString();
        panel2ButtonsTexts[2].text = QA[2].ToString();

        panel2.SetActive(true);
    }

    public void CorrectAnswerClicked()
    {
        panel2.SetActive(false);
        panel3.SetActive(true);

    }
    public void WrongAnswerClicked()
    {
        //inDialogue = false;
        transform.Translate(0, 0, - 3);

    }

    public void Panel3No()
    {
        inDialogue = false;
        transform.Translate(0, 0, -3);
    }

    public void Panel3Yes()
    {
        inDialogue = false;
        dialogueDone = true;
        timeLeftText.gameObject.SetActive(true);
        InvokeRepeating(nameof(ReduceTime), 0, 1);
    }

    int[] Randomize()
    {
        int num1 = Random.Range(3, 150);
        int num2 = Random.Range(3, 150);
        int ans = num1 + num2;

        return new int[3] { num1, num2, ans };
    }

    IEnumerator TimerMinute()
    {
        yield return null;
    }
    public void ReduceTime()
    {
        timeLeft -= 1;
    }




}
