using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Questions : MonoBehaviour
{
    Dictionary<int, string[]> questions = new Dictionary<int, string[]>();

    [SerializeField]
    Sprite[] images;

    SpriteRenderer questionRenderer;

    [SerializeField]
    Button questionButton, button1, button2, button3, button4, submitButton;

    List<int> remainingQuestions = new List<int>();

    string[] currentQuestion;

    bool correct;
    bool[] btns = new bool[4];

    private void Start()
    {
        questionRenderer = GetComponent<SpriteRenderer>();

        string[] strings;
        questions.Clear();

        strings = new string[] { "Šioje situacijoje 10 valandą ryto jūs raudonu automobiliu galite:",
            "?Sukti į dešinę.", "?Apsisukti.", "!Važiuoti tiesiai.", "?Važiuoti tiesiai ir į kairę." };
        questions.Add(0, strings);

        strings = new string[] { "Kuris (-ie) kelio ženklas (-ai) nedraudžia sukti į kairę pusę?",
            "!1", "!2", "!3", "!4" };
        questions.Add(1, strings);

        strings = new string[] { "Kokia eilės tvarka automobiliai važiuos per sankryžą?",
            "!Geltonas, žalias, raudonas.", "?Žalias, raudonas, geltonas.", "?Raudonas vienu metu su geltonu, žalias.", "?Žalias, raudonas vienu metu su geltonu." };
        questions.Add(2, strings);

        // klausimu sablonas
        strings = new string[] { "Klausimas3",
            "?Atsakymas", "!Atsakymas2", "?Atsakymas3", "?Atsakymas4" };

        foreach (var entry in questions)
        {
            remainingQuestions.Add(entry.Key);
        }


        ExecuteQuestion();
    }

    void Update()
    {
        
    }

    string[] PickRandomQuestion()
    { 
        int randInt = remainingQuestions.Count;
        randInt = Random.Range(0, randInt);

        int value = remainingQuestions[randInt];
        remainingQuestions.RemoveAt(randInt);


        
        Sprite newSpirte = images[value];
        questionRenderer.sprite = newSpirte;

        return questions[value];
    }

    void ExecuteQuestion()
    {
        currentQuestion = PickRandomQuestion();
        TextMeshProUGUI texts; 

        texts = questionButton.GetComponentInChildren<TextMeshProUGUI>();
        texts.text = currentQuestion[0];

        texts = button1.GetComponentInChildren<TextMeshProUGUI>();
        texts.text = currentQuestion[1].Substring(1);

        texts = button2.GetComponentInChildren<TextMeshProUGUI>();
        texts.text = currentQuestion[2].Substring(1);

        texts = button3.GetComponentInChildren<TextMeshProUGUI>();
        texts.text = currentQuestion[3].Substring(1);

        texts = button4.GetComponentInChildren<TextMeshProUGUI>();
        texts.text = currentQuestion[4].Substring(1);
    }

    public void CheckAnswer()
    {
        string selection = (btns[0] ? "!" : "?") + (btns[1] ? "!" : "?") + (btns[2] ? "!" : "?") + (btns[3] ? "!" : "?");

        string answer = string.Format("{0}{1}{2}{3}", currentQuestion[1][0], currentQuestion[2][0], currentQuestion[3][0], currentQuestion[4][0]);

        correct = string.Compare(selection, answer) == 0;

        Debug.Log(string.Format("\nSelection: {0}, Correct:  {1}, Is correct?: {2}", selection, answer, correct));

        if (correct)
        {
            levelCheck.updateClearData();
            GlobalVariables.showResults = true;
        }

        mainManager.ChangeScene(0);
    }

    public void SelectButton(Button btn)
    { 
        var clr = btn.colors;

        int index = (int)btn.name[btn.name.Length - 1] - 49;

        btns[index] = !btns[index];

        if (btns[index])
        {
            clr.normalColor = Color.green;
        }
        else
        {
            clr.normalColor = Color.white;
        }

        btn.colors = clr;

        btn.interactable = false;
        btn.interactable = true;
    }
}
