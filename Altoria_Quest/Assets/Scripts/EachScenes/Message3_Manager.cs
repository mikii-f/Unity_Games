using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message3_Manager : MonoBehaviour
{
    public Image backGround;
    BgMover bgmover;
    TMP_Text message_U;
    TMP_Text message_D;
    Message_Manager m_Manager;
    Guide_Manager gu_Manager;
    MainScene_Manager ms_Manager;
    ItemView IV;
    public GameObject window_U;
    public GameObject window_D;
    public GameObject black_U;
    public GameObject black_D;
    public Image face;
    public Sprite flame;
    public Sprite MS1;
    public Sprite MS2;
    public Sprite MS3;
    public TMP_Text characterName;
    public GameObject window_O;
    public GameObject window_P;
    public GameObject window_I1;
    public GameObject window_I2;
    public GameObject window_I3;
    public GameObject window_S;
    public GameObject window_nS;
    public GameObject tri;
    RectTransform tri_position;
    public GameObject itemview;
    ScrollRect sr;
    public GameObject item_Content;
    public GameObject item_Price;
    public GameObject bsItem;
    public GameObject bsItem_NP;
    TMP_Text t_item;
    TMP_Text t_price;
    TMP_Text t_bsItem;
    TMP_Text t_bsItem_NP;

    Having having;
    Status status;
    public Items item_namakura;
    public Items item_meto;
    public Items item_yoto;
    List<Items> temp_items = new();
    int bs_Count = 1;

    readonly string[] message_s3U = {"お、誰かと思えば嬢ちゃんじゃねえか。", "今日は気分を変えて外で刀の手入れをな。\n嬢ちゃんの方こそ、何か用でもあんのか？", "おまえさんは聖剣でも何でも作れんだろ……。\nまあちょうど良いのがあるにはある。買ってくかい？", "生憎こちとら商人なんでね。良いもん持ってるなら買い取りもするし、今すぐ金(QP)が要るってんなら質に入れるのもいいが、どうすんだ？", "どれが欲しいんだ？", "どれを引き取りゃいい？" ,"まいどあり！", "まだ見ていくか？", "じゃあ、またな！"};
    readonly string[] message_s3D = {"あ、村正！何やってるの？", "今はモルガンを探してて……。あ、そうだ！村正、何か良い刀ない？", "え、金(QP)とるの！？こっちは万年金欠なんだぞ村正ァ！", "うーん、それじゃあ……",  "(矢印キーとEnterで選択)", "(マウスで選択し、クリックまたはEnterで決定)\n(Bを押すと一つ前の選択に戻る)", "ここまでにしておくよ。" };

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
        GameObject mainscene_Manager = GameObject.Find("MainScene_Manager");
        ms_Manager = mainscene_Manager.GetComponent<MainScene_Manager>();
        GameObject i_Manager = GameObject.Find("Item_Manager");
        having = i_Manager.GetComponent<Having>();
        GameObject s_Manager = GameObject.Find("Status_Manager");
        status = s_Manager.GetComponent<Status>();
        IV = itemview.GetComponentInChildren<ItemView>();
        sr = itemview.GetComponent<ScrollRect>();
        t_item = item_Content.GetComponent<TMP_Text>();
        t_price = item_Price.GetComponent<TMP_Text>();
        t_bsItem = bsItem.GetComponent<TMP_Text>();
        t_bsItem_NP = bsItem_NP.GetComponent<TMP_Text>();
        window_O.SetActive(false);
        window_P.SetActive(false);
        window_I1.SetActive(false);
        window_I2.SetActive(false);
        window_I3.SetActive(false);
        window_S.SetActive(false);
        window_nS.SetActive(false);
        tri_position = tri.GetComponent<RectTransform>();
        tri.SetActive(false);
        itemview.SetActive(false);
        StartCoroutine(m_Manager.Setting());
    }

    // Update is called once per frame
    void Update()
    {
        if (bgmover.offset >= 1 && !m_Manager.is_Windowed)
        { 
            m_Manager.Conversation_Start();
            StartCoroutine(Scene3());
        }

        if (tri.activeSelf)
        {
            Select_Time();
            if (window_P.activeSelf)
            { 
                Weapon_Info();
                if (Input.GetKeyDown(KeyCode.B)) Reset_Window();
            }
        }

        if (window_O.activeSelf && Input.GetKeyDown(KeyCode.Return)) Decision_BoSoQ();

        if (window_P.activeSelf && !window_nS.activeSelf && Input.GetKeyDown(KeyCode.Return)) Decision_Weapon();

        if (itemview.activeSelf)
        { 
            IV.Highlight(temp_items.Count);
            if (Input.GetKeyDown(KeyCode.B)) Reset_Window();
        }

        if (itemview.activeSelf && IV.panel.activeSelf && IV.activepanel >= 0 && (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)))
        {
            message_D.text = message_s3D[4] + "\n(Bを押すと一つ前の選択に戻る)";
            t_bsItem.text = temp_items[Selected_Item()].Name;
            NPText_Setter(temp_items[Selected_Item()].price_S, bs_Count);
            StartCoroutine(IntervalnS());
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

    void Decision_BoSoQ()
    {
        switch (tri_position.anchoredPosition.y)
        {
            case 41:
                StartCoroutine(Scene3_B());
                break;
            case -2:
                StartCoroutine(Scene3_S());
                break;
            case -45:
                StartCoroutine(Scene3_Exit());
                break;
        }
        window_O.SetActive(false);
        tri.SetActive(false);
    }

    void Decision_Weapon()
    {
        switch (tri_position.anchoredPosition.y)
        {
            case 41:
                if (Having.items[0].count < 3000)
                {
                    if (!window_S.activeSelf) StartCoroutine(CannotBuy());
                    return;
                }
                else
                {
                    t_bsItem.text = item_namakura.Name;
                    NPText_Setter(3000, bs_Count);
                    StartCoroutine(IntervalnS());
                    break;
                }
            case -2:
                if (Having.items[0].count < 5000)
                {
                    if (!window_S.activeSelf) StartCoroutine(CannotBuy());
                    return;
                }
                else
                {
                    t_bsItem.text = item_meto.Name;
                    NPText_Setter(5000, bs_Count);
                    StartCoroutine(IntervalnS());
                    break;
                }
            case -45:
                if (Having.items[0].count < 10000)
                {
                    if (!window_S.activeSelf) StartCoroutine(CannotBuy());
                    return;
                }
                else
                {
                    t_bsItem.text = item_yoto.Name;
                    NPText_Setter(10000, bs_Count);
                    StartCoroutine(IntervalnS());
                    break;
                }
        }
        tri.SetActive(false);
    }

    void Number_Select()
    {
        if (tri_position.anchoredPosition.x == 160)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                switch (tri_position.anchoredPosition.y)
                {
                    case 41:
                        if (Having.items[0].count >= 3000 * (bs_Count + 1))
                        { 
                            bs_Count++;
                            NPText_Setter(3000, bs_Count);
                        }
                        break;
                    case -2:
                        if (Having.items[0].count >= 5000 * (bs_Count + 1))
                        {
                            bs_Count++;
                            NPText_Setter(5000, bs_Count);
                        }
                        break;
                    case -45:
                        if (Having.items[0].count >= 10000 * (bs_Count + 1))
                        {
                            bs_Count++;
                            NPText_Setter(10000, bs_Count);
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
                            NPText_Setter(3000, bs_Count);
                        }
                        break;
                    case -2:
                        if (bs_Count > 1)
                        {
                            bs_Count--;
                            NPText_Setter(5000, bs_Count);
                        }
                        break;
                    case -45:
                        if (bs_Count > 1)
                        {
                            bs_Count--;
                            NPText_Setter(10000, bs_Count);
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
                StartCoroutine(Scene3_B1());
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                window_nS.SetActive(false);
                bs_Count = 1;
                tri.SetActive(true);
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (temp_items[Selected_Item()].count > bs_Count)
                {
                    bs_Count++;
                    NPText_Setter(temp_items[Selected_Item()].price_S, bs_Count);
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (bs_Count > 1)
                {
                    bs_Count--;
                    NPText_Setter(temp_items[Selected_Item()].price_S, bs_Count);
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                itemview.SetActive(false);
                window_nS.SetActive(false);
                StartCoroutine(Scene3_S1());
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                window_nS.SetActive(false);
                bs_Count = 1;
            }
        }
    }

    void Reset_Window()
    {
        window_P.SetActive(false);
        window_I1.SetActive(false);
        window_I2.SetActive(false);
        window_I3.SetActive(false);
        itemview.SetActive(false);
        window_O.SetActive(true);
        message_U.text = "";
        message_D.text = message_s3D[4];
        tri.SetActive(true);
        tri_position.anchoredPosition = new Vector2(200, 41);
    }

    IEnumerator Scene3()
    {
        black_D.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        face.sprite = MS1;
        characterName.text = "千子村正";
        message_U.text = message_s3U[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s3D[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s3U[1];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s3D[1];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        face.sprite = MS2;
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s3U[2];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s3D[2];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        face.sprite = MS3;
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s3U[3];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s3D[3];
        yield return new WaitForSeconds(1);
        face.sprite = MS1;
        black_D.SetActive(true);
        message_D.text += "\n(矢印キーとEnterで選択)";
        window_O.SetActive(true);
        tri.SetActive(true);
    }

    IEnumerator Scene3_B()
    {
        yield return null;
        black_U.SetActive(false);
        message_U.text = message_s3U[4];
        yield return new WaitForSeconds(0.5f);
        black_U.SetActive(true);
        message_D.text = message_s3D[4] + "\n(Bを押すと一つ前の選択に戻る)";
        window_P.SetActive(true);
        window_I1.SetActive(true);
        tri_position.anchoredPosition = new Vector2(160, 41);
        tri.SetActive(true);
    }

    IEnumerator Scene3_B1()
    {
        black_U.SetActive(false);
        message_U.text = message_s3U[6];
        switch (tri_position.anchoredPosition.y)
        {
            case 41:
                for(int i = 0; i < bs_Count; i++) having.Item_Adder(item_namakura);
                having.QP_Changer(-3000 * bs_Count);
                break;
            case -2:
                for (int i = 0; i < bs_Count; i++) having.Item_Adder(item_meto);
                having.QP_Changer(-5000 * bs_Count);
                break;
            case -45:
                for (int i = 0; i < bs_Count; i++) having.Item_Adder(item_yoto);
                status.hp -= 100 * bs_Count;
                status.Status_Changer();
                if (Status.HP <= 0) StartCoroutine(ms_Manager.GameOver());
                having.QP_Changer(-10000 * bs_Count);
                break;
        }
        yield return new WaitForSeconds(1);
        message_U.text = message_s3U[7];
        yield return new WaitForSeconds(1);
        black_U.SetActive(true);
        window_P.SetActive(true);
        window_I1.SetActive(true);
        bs_Count = 1;
        tri_position.anchoredPosition = new Vector2(160, 41);
        tri.SetActive(true);
    }

    IEnumerator Scene3_S()
    {
        yield return null;
        black_U.SetActive(false);
        message_U.text = message_s3U[5];
        yield return new WaitForSeconds(1f);
        black_U.SetActive(true);
        message_D.text = message_s3D[5];
        IV.bar.numberOfSteps = Mathf.Max(Text_Setter() - 4, 0);
        itemview.SetActive(true);
    }

    IEnumerator Scene3_S1()
    {
        int n = Selected_Item();
        yield return null;
        black_U.SetActive(false);
        face.sprite = flame;
        characterName.text = "";
        having.Sell(temp_items[n], bs_Count);
        status.Status_Changer();
        message_U.text = temp_items[n].Name + "× " + bs_Count.ToString() + " を引き取ってもらった。\n所持QP:" + Having.items[0].count;
        yield return new WaitForSeconds(1);
        black_U.SetActive(true);
        face.sprite = MS1;
        characterName.text = "千子村正";
        message_U.text = message_s3U[5];
        IV.bar.numberOfSteps = Mathf.Max(Text_Setter() - 4, 0);
        bs_Count = 1;
        message_D.text = message_s3D[5];
        itemview.SetActive(true);
    }

    IEnumerator Scene3_Exit()
    {
        message_D.text = message_s3D[6];
        black_D.SetActive(false);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s3U[8];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_D.SetActive(false);
        status.Status_Changer();
        gu_Manager.SandIText_Changer();
        window_U.SetActive(false);
        window_D.SetActive(false);
        m_Manager.is_Window = false;
    }

    int Text_Setter()
    {
        string itemtext = "";
        string pricetext = "";
        int n = Having.items.Count;
        int active = 0;
        temp_items.Clear();
        for (int i = 0; i < n; i++)
        {
            if (Having.items[i].price_S > 0)
            {
                itemtext += Having.items[i].Name + " × " + Having.items[i].count + "\n";
                pricetext += Having.items[i].price_S + "QP\n";
                temp_items.Add(Having.items[i]);
                active += 1;
            }
        }
        t_item.text = itemtext;
        t_price.text = pricetext;
        return active;
    }

    int Selected_Item()
    {
        int n;
        if (IV.bar.numberOfSteps <= 1) n = IV.activepanel;
        else n = IV.activepanel + Mathf.RoundToInt((1 - sr.verticalNormalizedPosition) * (IV.bar.numberOfSteps - 1));
        return n;
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

