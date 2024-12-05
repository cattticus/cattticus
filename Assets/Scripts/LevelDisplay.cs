using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelDisplay : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    void Start()
    {
        levelText.gameObject.SetActive(true); //displays text at start of level
        Invoke("HideText", 2.5f); //number can be adjusted if need be
    }

    void HideText()
    {
        levelText.gameObject.SetActive(false);
    }
}
