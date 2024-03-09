using System;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message6_Manager : MonoBehaviour
{
    public Image backGround;
    BgMover bgmover;
    TMP_Text message_U;
    TMP_Text message_D;
    Message_Manager m_Manager;
    Guide_Manager gu_Manager;
    public GameObject window_U;
    public GameObject window_D;
    public GameObject window_B;
    public TMP_Text text_P;
    public TMP_Text text_E;
    public GameObject black_U;
    public GameObject black_D;
    public Image face;
    public Sprite flame;
    public Sprite C1;
    public Sprite C2;
    public Sprite C3;
    public TMP_Text characterName;
    int end = 0;

    Having having;
    Status status;
    public Items item_violin;

    readonly string[] message_s6U = {"あら、アルトリアじゃない。\nここにいるってことは……私たち、目的は同じかしら？", "それも良いけど……ここは勝負にしましょう！\n先にモルガンを倒した方がマスターを独占できるってことで良いわよね？", "決まりね。ならあっち向いてほいで勝った方が先に出発するわよ！", "くっ……負けは負けね。\n先に進みなさい。", "ぜっったい追い越してやるけど、もし先にたどり着いたらこれを使うといいわ。", "「 ノクナレアのバイオリン 」を手に入れた(やる気+10 攻撃+100)", "よし！私の勝ちね！", "それじゃあ先に行かせてもらうわ。\n追いつけるよう頑張りなさい？", "やる気が5下がった。"};
    readonly string[] message_s6D = {"あ、ノクナレアじゃん！", "モルガンを探してるんだよね。\nせっかくだし、一緒に行こっか？", "……！\nど、独占とかはよく分からないけど、勝負なら負けてられないよね！", "え…………？", "やったぁ！わたしの勝ち！", "ちっくしょう……。"};

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
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
        window_B.SetActive(false);
        StartCoroutine(m_Manager.Setting());
    }

    // Update is called once per frame
    void Update()
    {
        if (bgmover.offset >= 1 && !m_Manager.is_Windowed)
        {
            m_Manager.Conversation_Start();
            StartCoroutine(Scene6());
        }
    }

    IEnumerator Scene6()
    {
        black_U.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        face.sprite = C1;
        characterName.text = "ノクナレア";
        message_D.text = message_s6D[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s6U[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s6D[1];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        face.sprite = C2;
        message_U.text = message_s6U[1];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s6D[2];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s6U[2];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s6D[3];
        yield return new WaitForSeconds(2);
        window_B.SetActive(true);
        black_U.SetActive(false);
        message_U.text = "";
        message_D.text = "";
        face.sprite = C1;
        yield return new WaitForSeconds(2);
        message_U.text = "じゃあ行くわよ！";
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Battle());
        window_B.SetActive(false);
        message_U.text = "";
        message_D.text = "";
        if (end == 1) StartCoroutine(Scene6_W());
        else StartCoroutine(Scene6_L());
    }

    IEnumerator Battle()
    {
        while(end == 0)
        {
            message_U.text = "じゃんけん……";
            message_D.text = "じゃんけん……";
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3));
            int n = Janken();
            yield return new WaitForSeconds(1);
            if (n == 1)
            {
                message_U.text = "";
                message_D.text = "あっち向いて……";
            }
            else if (n == -1)
            {
                message_U.text = "あっち向いて……";
                message_D.text = "";
            }
            else
            {
                text_P.text = "";
                text_E.text = "";
                continue;
            }
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow));
            Hoi(n);
            yield return new WaitForSeconds(1);
            text_P.text = "";
            text_E.text = "";
        }
    }

    IEnumerator Scene6_W()
    {
        black_U.SetActive(true);
        message_D.text = message_s6D[4];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        face.sprite = C3;
        message_U.text = message_s6U[3];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        message_U.text = message_s6U[4];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        face.sprite = flame;
        characterName.text = "";
        message_U.text = message_s6U[5];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_D.SetActive(false);
        having.Item_Adder(item_violin);
        status.Status_Changer();
        gu_Manager.SandIText_Changer();
        window_U.SetActive(false);
        window_D.SetActive(false);
        m_Manager.is_Window = false;
    }

    IEnumerator Scene6_L()
    {
        black_D.SetActive(true);
        face.sprite = C2;
        message_U.text = message_s6U[6];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s6D[5];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s6U[7];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        face.sprite = flame;
        characterName.text = "";
        message_U.text = message_s6U[8];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_D.SetActive(false);
        status.motivation -= 5;
        status.Status_Changer();
        gu_Manager.SandIText_Changer();
        window_U.SetActive(false);
        window_D.SetActive(false);
        m_Manager.is_Window = false;
    }

    int Janken()
    {
        message_U.text += "ぽん！";
        message_D.text += "ぽん！";
        int hand = UnityEngine.Random.Range(1, 4);
        int n = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            text_P.text = "グー";
            switch (hand)
            {
                case 1: text_E.text = "グー"; n = 0; break;
                case 2: text_E.text = "チョキ"; n = 1; break;
                case 3: text_E.text = "パー"; n = -1; break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            text_P.text = "チョキ";
            switch (hand)
            {
                case 1: text_E.text = "グー"; n = -1; break;
                case 2: text_E.text = "チョキ"; n = 0; break;
                case 3: text_E.text = "パー"; n = 1; break;
            }
        }
        else
        {
            text_P.text = "パー";
            switch (hand)
            {
                case 1: text_E.text = "グー"; n = 1; break;
                case 2: text_E.text = "チョキ"; n = -1; break;
                case 3: text_E.text = "パー"; n = 0; break;
            }
        }
        return n;
    }

    void Hoi(int n)
    {
        message_U.text += "ほい！";
        message_D.text += "ほい！";
        int direction = UnityEngine.Random.Range(0, 4);
        switch (direction)
        {
            case 0: text_E.text += "  上"; break;
            case 1: text_E.text += "  下"; break;
            case 2: text_E.text += "  左"; break;
            case 3: text_E.text += "  右"; break;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            text_P.text += "  上";
            if (direction == 0) end = n;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            text_P.text += "  下";
            if (direction == 1) end = n;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            text_P.text += "  左";
            if (direction == 2) end = n;
        }
        else
        {
            text_P.text += "  右";
            if (direction == 3) end = n;
        }
    }
}
