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

    readonly string[] message_s6U = {"����A�A���g���A����Ȃ��B\n�����ɂ�����Ă��Ƃ́c�c�������A�ړI�͓���������H", "������ǂ����ǁc�c�����͏����ɂ��܂��傤�I\n��Ƀ����K����|���������}�X�^�[��Ɛ�ł�����Ă��Ƃŗǂ����ˁH", "���܂�ˁB�Ȃ炠���������Ăق��ŏ�����������ɏo��������I", "�����c�c�����͕����ˁB\n��ɐi�݂Ȃ����B", "�����������ǂ��z���Ă�邯�ǁA������ɂ��ǂ蒅�����炱����g���Ƃ�����B", "�u �m�N�i���A�̃o�C�I���� �v����ɓ��ꂽ(���C+10 �U��+100)", "�悵�I���̏����ˁI", "���ꂶ�Ⴀ��ɍs�����Ă��炤��B\n�ǂ�����悤�撣��Ȃ����H", "���C��5���������B"};
    readonly string[] message_s6D = {"���A�m�N�i���A�����I", "�����K����T���Ă�񂾂�ˁB\n�������������A�ꏏ�ɍs�������H", "�c�c�I\n�ǁA�Ɛ�Ƃ��͂悭������Ȃ����ǁA�����Ȃ畉���Ă��Ȃ���ˁI", "���c�c�c�c�H", "��������I�킽���̏����I", "���������傤�c�c�B"};

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
        characterName.text = "�m�N�i���A";
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
        message_U.text = "���Ⴀ�s�����I";
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
            message_U.text = "����񂯂�c�c";
            message_D.text = "����񂯂�c�c";
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3));
            int n = Janken();
            yield return new WaitForSeconds(1);
            if (n == 1)
            {
                message_U.text = "";
                message_D.text = "�����������āc�c";
            }
            else if (n == -1)
            {
                message_U.text = "�����������āc�c";
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
        message_U.text += "�ۂ�I";
        message_D.text += "�ۂ�I";
        int hand = UnityEngine.Random.Range(1, 4);
        int n = 0;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            text_P.text = "�O�[";
            switch (hand)
            {
                case 1: text_E.text = "�O�["; n = 0; break;
                case 2: text_E.text = "�`���L"; n = 1; break;
                case 3: text_E.text = "�p�["; n = -1; break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            text_P.text = "�`���L";
            switch (hand)
            {
                case 1: text_E.text = "�O�["; n = -1; break;
                case 2: text_E.text = "�`���L"; n = 0; break;
                case 3: text_E.text = "�p�["; n = 1; break;
            }
        }
        else
        {
            text_P.text = "�p�[";
            switch (hand)
            {
                case 1: text_E.text = "�O�["; n = 1; break;
                case 2: text_E.text = "�`���L"; n = -1; break;
                case 3: text_E.text = "�p�["; n = 0; break;
            }
        }
        return n;
    }

    void Hoi(int n)
    {
        message_U.text += "�ق��I";
        message_D.text += "�ق��I";
        int direction = UnityEngine.Random.Range(0, 4);
        switch (direction)
        {
            case 0: text_E.text += "  ��"; break;
            case 1: text_E.text += "  ��"; break;
            case 2: text_E.text += "  ��"; break;
            case 3: text_E.text += "  �E"; break;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            text_P.text += "  ��";
            if (direction == 0) end = n;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            text_P.text += "  ��";
            if (direction == 1) end = n;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            text_P.text += "  ��";
            if (direction == 2) end = n;
        }
        else
        {
            text_P.text += "  �E";
            if (direction == 3) end = n;
        }
    }
}
