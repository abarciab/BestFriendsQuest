using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour
{

    public List<string> dialogues = new List<string>();
    public TMP_Text dialogueBox;
    public GameObject textBox;

    // Start is called before the first frame update
    void Start()
    {
        //temp dialogues 

        dialogues.Add("Life is like a dream!");
        dialogues.Add("Lorem ipsum- haha Just Kidding Can You Imagine?");
        dialogues.Add("The guy who made me is kinda mid at coding. Can you kill him for me?");
        dialogues.Add("My life is like a video gaaame trying hard to beat the staaage-");
    }

    public void DisplayDialogue()
    {
        //enables the text box and then displays the dialogue
        textBox.SetActive(true);
        dialogueBox.text = RandomDialogue();
    }
    public string RandomDialogue()
    {
        //finds random dialogue string and returns it
        int index = Random.Range(0, dialogues.Count);
        return dialogues[index];
    }
}
