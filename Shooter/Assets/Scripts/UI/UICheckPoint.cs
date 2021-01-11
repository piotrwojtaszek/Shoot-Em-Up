using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICheckPoint : MonoBehaviour
{
    public TextMeshProUGUI checkPointNumber;

    private void Start()
    {
        checkPointNumber.text = GameManager.instance.points.ToString() + " / " + GameManager.instance.maxPoints.ToString();
    }

    public void SetNumber(string text)
    {
        checkPointNumber.text = text;
    }
}
