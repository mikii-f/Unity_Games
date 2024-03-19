using System.Collections;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Message8_Manager : MonoBehaviour
{
    public Image backGround;
    BgMover bgmover;
    TMP_Text message_U;
    TMP_Text message_D;
    Message_Manager m_Manager;
    Guide_Manager gu_Manager;
    public GameObject window_U;
    public GameObject window_D;
    public GameObject black_U;
    public GameObject black_D;
    public Image face;
    public Sprite flame;
    public Sprite E1;
    public Sprite E2;
    public TMP_Text characterName;
    public GameObject window_P;
    public GameObject window_I1;
    public GameObject window_I2;
    public GameObject window_I3;
    public GameObject window_S;
    public GameObject window_nS;
    public GameObject tri;
    RectTransform tri_position;
    public GameObject bsItem;
    public GameObject bsItem_NP;
    TMP_Text t_bsItem;
    TMP_Text t_bsItem_NP;

    Having having;
    Status status;
    public Items item_onigiri;
    int bs_Count = 1;

    readonly string[] message_s8U = {"おや、アルトリア嬢か。\n現在カルデア食堂出張版の開店中だ。何か食べていくかね？", "それは結構。では行くといい。\nまた食堂で元気に食べるその顔を見せてくれたまえ。"};
    readonly string[] message_s8D = {"どうしてわざわざシミュレーターに？というツッコミはおいといて。\nちょうどお腹すいてたんだよね！", "よし、元気出た！"};

    // Start is called before the first frame update
    void Start()
    {
        bgmover = backGround.GetComponent<BgMover>();
        GameObject Message_U = GameObject.Find("Message_U");
        message_U = Message_U.GetComponent<TMP_Text>();
        GameObject Message_D = GameObject.Find("Message_D");
        message_D = Message_D.GetComponent<TMP_Text>();
        GameObject message_Manager = GameObject.Find("Message_Manager");
        m_Manager = message_Manager.GetComponent<Message_Manager>();
        GameObject guide_Manager = GameObject.Find("Guide_Manager");
        gu_Manager = guide_Manager.GetComponent<Guide_Manager>();
        GameObject i_Manager = GameObject.Find("Item_Manager");
        having = i_Manager.GetComponent<Having>();
        GameObject s_Manager = GameObject.Find("Status_Manager");
        status = s_Manager.GetComponent<Status>();
        t_bsItem = bsItem.GetComponent<TMP_Text>();
        t_bsItem_NP = bsItem_NP.GetComponent<TMP_Text>();
        window_P.SetActive(false);
        window_I1.SetActive(false);
        window_I2.SetActive(false);
        window_I3.SetActive(false);
        window_S.SetActive(false);
        window_nS.SetActive(false);
        tri_position = tri.GetComponent<RectTransform>();
        tri.SetActive(false);
        StartCoroutine(m_Manager.Setting());
    }

    // Update is called once per frame
    void Update()
    {
        if (bgmover.offset >= 1 && !m_Manager.is_Windowed)
        {
            m_Manager.Conversation_Start();
            StartCoroutine(Scene8());
        }

        if (tri.activeSelf)
        {
            Select_Time();
            Weapon_Info();
            if (Input.GetKeyDown(KeyCode.B)) StartCoroutine(Scene8_Q());
            if (!window_nS.activeSelf && Input.GetKeyDown(KeyCode.Return)) Decision_Weapon();
        }

        if (window_nS.activeSelf) Number_Select();
    }

    void Select_Time()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (tri_position.anchoredPosition.y < 41)
            {
                Vector2 p = tri_position.anchoredPosition;
                p.y += 43;
                tri_position.anchoredPosition = p;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (tri_position.anchoredPosition.y > -45)
            {
                Vector2 p = tri_position.anchoredPosition;
                p.y -= 43;
                tri_position.anchoredPosition = p;
            }
        }
    }

    void Weapon_Info()
    {
        switch (tri_position.anchoredPosition.y)
        {
            case 41:
                window_I1.SetActive(true);
                window_I2.SetActive(false);
                break;
            case -2:
                window_I1.SetActive(false);
                window_I2.SetActive(true);
                window_I3.SetActive(false);
                break;
            case -45:
                window_I2.SetActive(false);
                window_I3.SetActive(true);
                break;
        }
    }

    void Decision_Weapon()
    {
        switch (tri_position.anchoredPosition.y)
        {
            case 41:
                if (Having.items[0].count < 1000)
                {
                    if (!window_S.activeSelf) StartCoroutine(CannotBuy());
                    return;
                }
                else
                {
                    t_bsItem.text = "和食セット";
                    NPText_Setter(1000, bs_Count);
                    StartCoroutine(IntervalnS());
                    break;
                }
            case -2:
                if (Having.items[0].count < 1000)
                {
                    if (!window_S.activeSelf) StartCoroutine(CannotBuy());
                    return;
                }
                else
                {
                    t_bsItem.text = "洋食セット";
                    NPText_Setter(1000, bs_Count);
                    StartCoroutine(IntervalnS());
                    break;
                }
            case -45:
                if (Having.items[0].count < 250)
                {
                    if (!window_S.activeSelf) StartCoroutine(CannotBuy());
                    return;
                }
                else
                {
                    t_bsItem.text = item_onigiri.Name;
                    NPText_Setter(250, bs_Count);
                    StartCoroutine(IntervalnS());
                    break;
                }
        }
        message_D.text = "(矢印キーとEnterで選択)\n(Bで戻る)";
        tri.SetActive(false);
    }

    void Number_Select()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            switch (tri_position.anchoredPosition.y)
            {
                case 41:
                    if (Having.items[0].count >= 1000 * (bs_Count + 1))
                    {
                        bs_Count++;
                        NPText_Setter(1000, bs_Count);
                    }
                    break;
                case -2:
                    if (Having.items[0].count >= 1000 * (bs_Count + 1))
                    {
                        bs_Count++;
                        NPText_Setter(1000, bs_Count);
                    }
                    break;
                case -45:
                    if (Having.items[0].count >= 500 * (bs_Count + 1))
                    {
                        bs_Count++;
                        NPText_Setter(250, bs_Count);
                    }
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            switch (tri_position.anchoredPosition.y)
            {
                case 41:
                    if (bs_Count > 1)
                    {
                        bs_Count--;
                        NPText_Setter(1000, bs_Count);
                    }
                    break;
                case -2:
                    if (bs_Count > 1)
                    {
                        bs_Count--;
                        NPText_Setter(1000, bs_Count);
                    }
                    break;
                case -45:
                    if (bs_Count > 1)
                    {
                        bs_Count--;
                        NPText_Setter(250, bs_Count);
                    }
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            window_P.SetActive(false);
            window_nS.SetActive(false);
            window_I1.SetActive(false);
            window_I2.SetActive(false);
            window_I3.SetActive(false);
            StartCoroutine(Scene8_1());
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            message_D.text = "(矢印キーとEnterで選択)\n(Bで店を出る)";
            window_nS.SetActive(false);
            bs_Count = 1;
            tri.SetActive(true);
        }
    }

    IEnumerator Scene8()
    {
        black_D.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        face.sprite = E1;
        characterName.text = "エミヤ";
        message_U.text = message_s8U[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s8D[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_D.SetActive(true);
        message_D.text = "(矢印キーとEnterで選択)\n(Bで店を出る)";
        window_P.SetActive(true);
        tri.SetActive(true);
    }

    IEnumerator Scene8_1()
    {
        black_D.SetActive(false);
        switch (tri_position.anchoredPosition.y)
        {
            case 41:
                message_D.text = "もぐもぐ……";
                status.hp += 200 * bs_Count;
                status.defense += 2 * bs_Count;
                having.QP_Changer(-1000 * bs_Count);
                break;
            case -2:
                message_D.text = "もぐもぐ……";
                status.hp += 200 * bs_Count;
                status.defense += 2 * bs_Count;
                having.QP_Changer(-1000 * bs_Count);
                break;
            case -45:
                message_D.text = "備蓄、大事だよね！";
                for (int i = 0; i < bs_Count; i++) having.Item_Adder(item_onigiri);
                having.QP_Changer(-250 * bs_Count);
                break;
        }
        yield return new WaitForSeconds(1);
        message_D.text = "(矢印キーとEnterで選択)\n(Bで店を出る)";
        black_D.SetActive(true);
        window_P.SetActive(true);
        window_I1.SetActive(true);
        bs_Count = 1;
        tri_position.anchoredPosition = new Vector2(140, 41);
        tri.SetActive(true);
    }

    IEnumerator Scene8_Q()
    {
        window_P.SetActive(false);
        tri.SetActive(false);
        window_I1.SetActive(false);
        window_I2.SetActive(false);
        window_I3.SetActive(false);
        message_D.text = message_s8D[1];
        black_D.SetActive(false);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        face.sprite = E2;
        message_U.text = message_s8U[1];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_D.SetActive(false);
        status.Status_Changer();
        gu_Manager.SandIText_Changer();
        window_U.SetActive(false);
        window_D.SetActive(false);
        m_Manager.is_Window = false;
    }

    void NPText_Setter(int price, int n)
    {
        t_bsItem_NP.text = "× " + n + " 個\n\n" + (price * n).ToString() + "QP";
    }

    IEnumerator CannotBuy()
    {
        window_S.SetActive(true);
        yield return new WaitForSeconds(1);
        window_S.SetActive(false);
    }

    IEnumerator IntervalnS()
    {
        yield return null;
        window_nS.SetActive(true);
    }
}
