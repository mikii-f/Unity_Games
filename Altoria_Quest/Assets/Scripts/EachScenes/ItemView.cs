using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    Vector2 m_position;
    RectTransform rt;
    public Camera c;
    public GameObject panel;
    [NonSerialized]
    public int activepanel = 0;
    public GameObject baseview;
    public Scrollbar bar;
    [NonSerialized]
    public const float W = 28;

    // Start is called before the first frame update
    void Start()
    {
        rt = baseview.GetComponent<RectTransform>();
        panel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Highlight(int n)
    {
        m_position = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, m_position, c, out Vector2 lp);
        if (lp.y > W * 1.5 && n >= 1)
        {
            panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, W * 2, 0);
            activepanel = 0;
        }
        else if (lp.y > W * 0.5 && n >= 2)
        {
            panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, W * 1, 0);
            activepanel = 1;
        }
        else if (lp.y > W * -0.5 && n >= 3)
        {
            panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, W * 0, 0);
            activepanel = 2;
        }
        else if (lp.y > W * -1.5 && n >= 4)
        {
            panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, W * -1, 0);
            activepanel = 3;
        }
        else if (lp.y > W * -2.5 && n >= 5)
        {
            panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, W * -2, 0);
            activepanel = 4;
        }
        else
        {
            panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, W * 10, 0);
            activepanel = -1;
        }
    }

    public void St()
    {
        panel.SetActive(true);
    }

    public void Sf()
    {
        panel.SetActive(false);
    }
}
