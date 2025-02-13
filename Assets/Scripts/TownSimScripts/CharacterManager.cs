using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] public List<CharacterData> allCharacters = new List<CharacterData>();

    // Start is called before the first frame update
    void Start()
    {
        CharacterData Johnny = new CharacterData();
        CharacterData Sally = new CharacterData();
        CharacterData Goobert = new CharacterData();

        Johnny.characterName = "Johnny";
        Sally.characterName = "Sally";
        Goobert.characterName = "Goobert";

        allCharacters.Add(Johnny);
        allCharacters.Add(Sally);
        allCharacters.Add(Goobert); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
