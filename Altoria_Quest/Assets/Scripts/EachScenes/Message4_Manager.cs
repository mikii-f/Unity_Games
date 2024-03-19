using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message4_Manager : MonoBehaviour
{
    public Image backGround;
    BgMover bgmover;
    TMP_Text message_U;
    TMP_Text message_D;
    Message_Manager m_Manager;
    Guide_Manager gu_Manager;
    MainScene_Manager ms_Manager;
    public GameObject window_U;
    public GameObject window_D;
    public GameObject black_U;
    public GameObject black_D;
    public GameObject window_O;
    public GameObject tri_B;
    public GameObject tri_Q;
    public Sprite Wy1;
    public Sprite Wy2;
    public Sprite tp;
    public Image icon;
    public Sprite icon_A;
    public Sprite icon_R;
    public Sprite icon_Q;
    int enemy;

    Having having;
    Status status;
    public Items item_df;
    public Items item_dg;

    readonly string[] message_s4U = {"ワイバーンを倒すとランダムで素材が獲得できます。\nクラス相性、及び防御力に応じてダメージを受けます。", "クラスはアサシンだった！", "クラスはライダーだった！"};
    readonly string[] message_s4D = {"あ、ワイバーンだ！", "やった、楽勝！", "やっべ。", "やめておこう。"};

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        enemy = UnityEngine.Random.Range(0, 2);
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
        window_O.SetActive(false);
        tri_B.SetActive(false);
        tri_Q.SetActive(false);
        StartCoroutine(m_Manager.Setting());
    }

    // Update is called once per frame
    void Update()
    {
        if (bgmover.offset >= 1 && !m_Manager.is_Windowed)
        {
            m_Manager.Conversation_Start();
            StartCoroutine(Scene4());
        }

        if (window_O.activeSelf) Select_Time();
    }

    void Select_Time()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            tri_B.SetActive(true);
            tri_Q.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            tri_B.SetActive(false);
            tri_Q.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (tri_B.activeSelf)
            {
                StartCoroutine(Scene4_B(enemy));
                window_O.SetActive(false);
            }
            if (tri_Q.activeSelf)
            {
                StartCoroutine(Scene4_Q());
                window_O.SetActive(false);
            }
        }
    }

    IEnumerator Scene4()
    {
        black_U.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        message_D.text = message_s4D[0];
        icon.sprite = icon_Q;
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s4U[0];
        yield return new WaitForSeconds(2);
        black_U.SetActive(true);
        message_D.text  += "\n(矢印キーとEnterで選択)";
        window_O.SetActive(true);
        tri_B.SetActive(true);
    }

    IEnumerator Scene4_B(int e)
    {
        yield return new WaitForSeconds(0.5f);
        black_U.SetActive(false);
        if (e == 0)
        {
            bgmover.symbol.sprite = Wy1;
            icon.sprite = icon_A;
        }
        else
        {
            bgmover.symbol.sprite = Wy2;
            icon.sprite = icon_R;
        }
        message_U.text = message_s4U[e+1];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s4D[e+1];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        yield return StartCoroutine(ms_Manager.FadeOut());
        bgmover.symbol.sprite = tp;
        icon.sprite = tp;
        message_U.text = "";
        message_D.text = "";
        int item = UnityEngine.Random.Range(0, 2);
        int n = UnityEngine.Random.Range(1, 3);
        string text = "";
        int damage;
        if (item == 0) text = "獲得素材：" + "竜の牙(攻撃+10) " + n.ToString() + " 個";
        else text = "獲得素材：" + "竜の逆鱗(攻撃+30) " + n.ToString() + " 個";
        damage = Mathf.Max((1 + 3 * e) * 200 - Status.Defense, 0);
        text += "\n体力が " + damage.ToString() + " 減った";
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(ms_Manager.FadeIn());
        yield return new WaitForSeconds(0.5f);
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = text;
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_D.SetActive(false);
        for (int i = 0; i < n; i++)
        {
            if (item == 0) having.Item_Adder(item_df);
            else having.Item_Adder(item_dg);
        }
        status.hp -= damage;
        status.Status_Changer();
        if (Status.HP <= 0)
        {
            StartCoroutine(ms_Manager.GameOver());
            yield return new WaitForSeconds(5);
        }
        gu_Manager.SandIText_Changer();
        window_U.SetActive(false);
        window_D.SetActive(false);
        m_Manager.is_Window = false;
    }

    IEnumerator Scene4_Q()
    {
        yield return null;
        black_D.SetActive(false);
        message_D.text = message_s4D[3];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        window_U.SetActive(false);
        window_D.SetActive(false);
        icon.sprite = tp;
        m_Manager.is_Window = false;
    }
}
