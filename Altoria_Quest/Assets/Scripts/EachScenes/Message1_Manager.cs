using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message1_Manager : MonoBehaviour
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
    public Sprite t2;

    Having having;
    Status status;
    public Items item_df;

    readonly string[] message_s0U = { "悪の女王モルガンにマスターが奪われた！？\nアルトリア・キャスターを操作して取り返しに行こう！" };
    readonly string[] message_s0D = { "よーし、頑張るぞー！" };

    readonly string[] message_s1U = { "「 竜の牙 」を見つけた(攻撃+10)" };
    readonly string[] message_s1D = { "やったね！\nこの調子で進んでいこう！" };

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
        StartCoroutine(m_Manager.Setting());
        StartCoroutine(Scene0());
    }

    // Update is called once per frame
    void Update()
    {
        if (bgmover.offset >= 1 && !m_Manager.is_Windowed)
        {
            m_Manager.Conversation_Start();
            StartCoroutine(Scene1());
        }
    }

    IEnumerator Scene0()
    {
        m_Manager.is_Window = true;
        yield return new WaitForSeconds(2);
        window_U.SetActive(true);
        window_D.SetActive(true);
        black_D.SetActive(true);
        message_U.text = message_s0U[0];
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s0D[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        window_U.SetActive(false);
        window_D.SetActive(false);
        m_Manager.is_Window = false;
        message_U.text = null;
        message_D.text = null;
    }

    IEnumerator Scene1()
    {
        black_D.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        bgmover.symbol.sprite = t2;
        message_U.text = message_s1U[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s1D[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        having.Item_Adder(item_df);
        status.Status_Changer();
        gu_Manager.SandIText_Changer();
        window_U.SetActive(false);
        window_D.SetActive(false);
        m_Manager.is_Window = false;
    }
}
