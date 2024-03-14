using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFinal_Manager : MonoBehaviour
{
    public Image backGround;
    public GameObject location;
    BgMover bgmover;
    TMP_Text message_U;
    TMP_Text message_D;
    Message_Manager m_Manager;
    //Guide_Manager gu_Manager;
    MainScene_Manager ms_Manager;
    public GameObject window_U;
    public GameObject window_D;
    public GameObject window_B;
    public TMP_Text text_P;
    public TMP_Text text_E;
    public GameObject black_U;
    public GameObject black_D;
    public Image face;
    public Sprite flame;
    public Sprite Morgan1;
    //public Sprite Morgan2;
    //public Sprite Morgan3;
    public TMP_Text characterName;
    public GameObject status1;
    public GameObject status2;
    public TMP_Text text_S1;
    public TMP_Text text_S2;
    public GameObject window_J;
    public TMP_Text text_J;
    public TMP_Text text_HP1;
    public TMP_Text text_HP2;
    Coroutine coroutine1;
    Coroutine coroutine2;
    int attackSuccess = 0;
    int diffenceSuccess = 0;
    int[] status_m = {2000, 30, 300, 150};
    public GameObject white;
    public TMP_Text text_C;

    Having having;
    Status status;

    readonly string[] message_s8U = {"全く、毎度のことながら騒々しいですね。\nせっかくの夫婦水入らずの時間だというのに。", "あ、あはは……。ごめん、断り切れなくって……。", "(うわあ、アルトリア怒ってるな……。)", "引き下がる気はないようですね。\n仕方ありません、貴方が私に勝てたら、今日のところは我が夫を帰すとしましょう。", "……ん？おまえが持っているそのカップは……。\n…………そう、そうですか。ふふふ…………。", "心配せずとも、貴方にも勝ち目は与えます。\n勝負の内容は……そう、叩いて被ってじゃんけんぽん、です。", "くっ……。ええ、約束は守りましょう。\n名残惜しいですが今日はここまでのようです。", "そうだね、ちょっと強引ではあったけど楽しかったよ、モルガン。\nそれとアルトリアも、頑張ってくれてありがとう！"};
    readonly string[] message_s8D = {"ようやく見つけたぞモルガン！\nさあ、マスターを返してもら……あれ？", "……ふーん、こっちが勝手に心配してる間にマスターはお楽しみだだったんだ。", "まあいいけど？わたしは仕事がたっぷり残ってるマスターをむりやりにでも連れ帰るだけだし。",　"あ、あれ？なんか盛大に勘違いされてるような……。", "妖精國の勝負ってじゃんけんしかなかったっけ！？", "どうだ！", "もう、お気楽なんだから……。\n帰ったら貯めてた分も合わせて報告書提出してね？ちょっとぐらい、手伝ってあげるからさ！"};

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
        //GameObject guide_Manager = GameObject.Find("Guide_Manager");
        //gu_Manager = guide_Manager.GetComponent<Guide_Manager>();
        GameObject mainscene_Manager = GameObject.Find("MainScene_Manager");
        ms_Manager = mainscene_Manager.GetComponent<MainScene_Manager>();
        GameObject i_Manager = GameObject.Find("Item_Manager");
        having = i_Manager.GetComponent<Having>();
        GameObject s_Manager = GameObject.Find("Status_Manager");
        status = s_Manager.GetComponent<Status>();
        window_B.SetActive(false);
        status1.SetActive(false);
        status2.SetActive(false);
        window_J.SetActive(false);
        StartCoroutine(m_Manager.Setting());
    }

    // Update is called once per frame
    void Update()
    {
        if (bgmover.offset >= 1 && !m_Manager.is_Windowed)
        {
            m_Manager.Conversation_Start();
            StartCoroutine(SceneF());
        }
    }

    IEnumerator SceneF()
    {
        black_U.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        face.sprite = Morgan1;
        characterName.text = "モルガン";
        message_D.text = message_s8D[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s8U[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        face.sprite = flame;
        characterName.text = "藤丸立香";
        message_U.text = message_s8U[1];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s8D[1];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        message_D.text = message_s8D[2];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s8U[2];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        face.sprite = Morgan1;
        characterName.text = "モルガン";
        message_U.text = message_s8U[3];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        if (Having.items.FindIndex(x => x.Name == "プリンの空きカップ？") >= 0)
        {
            message_U.text = message_s8U[4];
            for (int i = 0; i < 4; i++) status_m[i] = (int)(status_m[i] * 1.5);
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            black_U.SetActive(true);
            black_D.SetActive(false);
            message_D.text = message_s8D[3];
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            black_U.SetActive(false);
            black_D.SetActive(true);
        }
        message_U.text = message_s8U[5];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s8D[4];
        yield return new WaitForSeconds(2);
        black_U.SetActive(false);
        window_B.SetActive(true);
        window_U.SetActive(false);
        window_D.SetActive(false);
        status1.SetActive(true);
        status2.SetActive(true);
        location.SetActive(false);
        ms_Manager.ponti.SetActive(false);
        text_S1.text = Status.Motivation.ToString() + "\n" + Status.Attack.ToString() + "\n" + Status.Defense.ToString();
        text_S2.text = status_m[1].ToString() + "\n" + status_m[2].ToString() + "\n" + status_m[3].ToString();
        yield return new WaitForSeconds(2);
        face.sprite = flame;
        characterName.text = "藤丸立香";
        window_U.SetActive(true);
        message_U.text = "えっと……2人とも頑張って！";
        yield return new WaitForSeconds(2);
        window_U.SetActive(false);
        face.sprite = Morgan1;
        characterName.text = "モルガン";
        message_U.text = "";
        message_D.text = "";
        text_HP1.text = Status.HP.ToString();
        text_HP2.text = status_m[0].ToString();
        window_J.SetActive(true);
        yield return StartCoroutine(Battle());
        window_B.SetActive(false);
        status1.SetActive(false);
        status2.SetActive(false);
        text_HP1.text = "";
        text_HP2.text = "";
        window_J.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        window_U.SetActive(true);
        window_D.SetActive(true);
        black_U.SetActive(true);
        message_D.text = message_s8D[5];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s8U[6];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        face.sprite = flame;
        characterName.text = "藤丸立香";
        message_U.text = message_s8U[7];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s8D[6];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        StartCoroutine(GameClear());
    }

    IEnumerator Battle()
    {
        while (true)
        {
            text_J.text = "じゃんけん……";
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3));
            int n = Janken();
            if (n == 0)
            {
                yield return new WaitForSeconds(1);
                text_P.text = "";
                text_E.text = "";
                continue;
            }
            else
            {
                float waitTime = UnityEngine.Random.Range(0.5f, 1f);
                coroutine1 = StartCoroutine(AorG1(n));
                coroutine2 = StartCoroutine(AorG2(n, waitTime));
            }
            yield return new WaitForSeconds(3);
            if (attackSuccess > 0)
            {
                int damage;
                if (n == 1)
                {
                    damage = Status.Attack - status_m[3];
                    if (attackSuccess == 2) damage = (int)(damage * 1.5);
                    status_m[0] -= damage;
                    if (status_m[0] < 0) status_m[0] = 0;
                }
                else
                {
                    damage = status_m[2] - Status.Defense;
                    if (attackSuccess == 2) damage = (int)(damage * 1.5);
                    status.hp -= damage;
                    status.Status_Changer();
                }
            }
            attackSuccess = 0;
            diffenceSuccess = 0;
            text_P.text = "";
            text_E.text = "";
            text_J.text = "";
            text_HP1.text = Status.HP.ToString();
            text_HP2.text = status_m[0].ToString();
            if (status_m[0] <= 0)
            {
                text_J.text = "YOU WIN!";
                yield return new WaitForSeconds(3);
                break;
            }
            else if (Status.HP <= 0)
            {
                text_J.text = "YOU LOSE……";
                yield return new WaitForSeconds(2);
                StartCoroutine(ms_Manager.GameOver());
                yield return new WaitForSeconds(5);
            }
        }
    }

    int Janken()
    {
        text_J.text += "ぽん！";
        int hand = UnityEngine.Random.Range(1, 5);
        int n;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            text_P.text = "グー";
            switch (hand)
            {
                case 1: text_E.text = "チョキ"; n = 1; break;
                case 2: text_E.text = "パー"; n = -1; break;
                default: text_E.text = "グー"; n = 0; break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            text_P.text = "チョキ";
            switch (hand)
            {
                case 1: text_E.text = "チョキ"; n = 0; break;
                case 2: text_E.text = "パー"; n = 1; break;
                default: text_E.text = "グー"; n = -1; break;
            }
        }
        else
        {
            text_P.text = "パー";
            switch (hand)
            {
                case 1: text_E.text = "チョキ"; n = -1; break;
                case 2: text_E.text = "パー"; n = 0; break;
                default: text_E.text = "グー"; n = 1; break;
            }
        }
        return n;
    }
    IEnumerator AorG1(int n)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.G));
        if (Input.GetKeyDown(KeyCode.A))
        {
            text_P.text += "　叩く！";
            if (n == 1)
            {
                if (diffenceSuccess == 0)
                {
                    if (UnityEngine.Random.Range(0, 100) < Status.Motivation)
                    {
                        attackSuccess = 2;
                        text_J.text = "クリティカル！";
                    }
                    else
                    {
                        attackSuccess = 1;
                        text_J.text = "";
                    }
                    //ハリセン
                    StopCoroutine(coroutine2);
                }
                else
                {
                    //ガードされる
                }
            }
            else
            {
                text_J.text = "";
                //モルガンむかつき
            }
        }
        else
        {
            text_P.text += "　被る！";
            text_J.text = "";
            if (n == -1) diffenceSuccess = 1;
        }
    }

    IEnumerator AorG2(int n, float t)
    {
        yield return new WaitForSeconds(t);
        if (n == 1)
        {
            text_E.text += "　被る！";
            text_J.text = "";
            diffenceSuccess = 1;
        }
        else
        {
            text_E.text += "　叩く！";
            if (diffenceSuccess == 0)
            {
                if (UnityEngine.Random.Range(0, 100) < status_m[1])
                {
                    attackSuccess = 2;
                    text_J.text = "クリティカル！";
                }
                else
                {
                    attackSuccess = 1;
                    text_J.text = "";
                }
                //ハリセン
                StopCoroutine(coroutine1);
            }
            else
            {
                //ガード成功
            }
        }
    }

    IEnumerator GameClear()
    {
        float fade_time = 2f;
        int loop = 20;
        float wait_time = fade_time / loop;
        float alpha_interval = 255.0f / loop;
        Image fade = white.GetComponent<Image>();
        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            yield return new WaitForSeconds(wait_time);
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
        yield return new WaitForSeconds(1);
        text_C.text = "Game Clear!";
        yield return new WaitForSeconds(3);
        status.Reset_S();
        having.Reset_I();
        SceneManager.LoadScene("Title");
    }
}
