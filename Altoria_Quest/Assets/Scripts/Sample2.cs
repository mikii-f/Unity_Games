using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample2 : MonoBehaviour
{

    Vector2 m_position;
    RectTransform rt;
    public Camera c;
    public GameObject panel;
    public GameObject base_ob;
    const float W = 200/7;

    // Start is called before the first frame update
    void Start()
    {
        rt = base_ob.GetComponent<RectTransform>();
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_position = Input.mousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, m_position, c, out Vector2 lp);
        if (lp.y > W * 2.5) panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, W * 3, 0);
        else if (lp.y > W * 1.5) panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, W * 2, 0);
        else if (lp.y > W * 0.5) panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, W * 1, 0);
        else if (lp.y > W * -0.5) panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, W * 0, 0);
        else if (lp.y > W * -1.5) panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, W * -1, 0);
        else if (lp.y > W * -2.5) panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, W * -2, 0);
        else panel.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, W * -3, 0);
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
