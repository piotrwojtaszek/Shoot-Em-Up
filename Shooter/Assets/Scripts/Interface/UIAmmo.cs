using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAmmo : MonoBehaviour
{
    public static UIAmmo instance;
    TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        instance = this;
    }
    public void TextUpdate(int current, int wholeAmmo)
    {
        text.text = current.ToString() + " / " + wholeAmmo.ToString();
    }
}
