using System;
using UnityEngine;
using UnityEngine.UI;

public class BgMover : MonoBehaviour
{
    [NonSerialized]
    public const float MIN = 0f;
    [NonSerialized]
    public const float MAX = 1.5f;
    public float offsetSpeed;
    float pushTime = 0f;
    [NonSerialized]
    public float offset;
    Material material;
    Message_Manager m_Manager;
    public GameObject guide_Manager;
    Guide_Manager gu_Manager;
    public Image symbol;
    RectTransform symbol_p;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Image>().material;
        GameObject message_Manager = GameObject.Find("Message_Manager");
        m_Manager = message_Manager.GetComponent<Message_Manager>();
        gu_Manager = guide_Manager.GetComponent<Guide_Manager>();
        symbol_p = symbol.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Manager.is_Window == false && gu_Manager.is_Sidebar == false)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (pushTime * offsetSpeed <= MAX)
                {
                    pushTime += Time.deltaTime;
                }
                offset = pushTime * offsetSpeed;
                material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
                symbol_p.anchoredPosition = new Vector2(1800 - offset * 1600, -30);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (pushTime * offsetSpeed >= MIN)
                {
                    pushTime -= Time.deltaTime;
                }
                offset = pushTime * offsetSpeed;
                material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
                symbol_p.anchoredPosition = new Vector2(1800 - offset * 1600, -30);
            }
        }
    }

    void OnDestroy()
    {
        material.SetTextureOffset("_MainTex", Vector2.zero);
    }
}
