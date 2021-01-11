using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    public static UICanvas instance;
    public GameObject m_popup;
    private void Awake()
    {
        instance = this;
    }

    public void CreatePanel(GameObject prefab, GameObject parent)
    {
        Instantiate(prefab, parent.transform);
    }

}
