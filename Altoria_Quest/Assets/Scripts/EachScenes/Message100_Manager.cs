using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Message100_Manager : MonoBehaviour
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
    bool is_Windowed;

    Having having;
    Status status;
    public Items SQ;

    readonly string[] message_s100U = { "現バージョンの最後まで到達しました！", "聖晶石(ボーナスアイテム)を獲得" };
    readonly string[] message_s100D = { "おめでとう！" };

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
    }

    // Update is called once per frame
    void Update()
    {
        if (bgmover.offset >= 1 && !is_Windowed)
        {
            window_U.SetActive(true);
            window_D.SetActive(true);
            m_Manager.is_Window = true;
            is_Windowed = true;
            StartCoroutine(Scene100());
        }
    }

    IEnumerator Scene100()
    {
        black_D.SetActive(true);
        message_U.text = message_s100U[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s100D[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s100U[1];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_D.SetActive(false);
        having.Item_Adder(SQ);
        status.Status_Changer();
        gu_Manager.SandIText_Changer();
        window_U.SetActive(false);
        window_D.SetActive(false);
        m_Manager.is_Window = false;
    }
}
