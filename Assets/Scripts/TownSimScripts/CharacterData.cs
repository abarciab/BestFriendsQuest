using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [Header("Profile")]
    private string characterName;
    private int age;

    [Header("Happiness")]
    private float happiness;

    [Header("Relationships")]
    private Dictionary<string, float> items = new Dictionary<string, float>();

}
