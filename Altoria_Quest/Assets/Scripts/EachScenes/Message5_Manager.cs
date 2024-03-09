using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message5_Manager : MonoBehaviour
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
    public Sprite M1;
    public Sprite M2;
    public Sprite M3;
    public TMP_Text characterName;
    public GameObject window_O;
    public GameObject tri_R;
    public GameObject tri_Q;
    int trash;

    Having having;
    Status status;
    public Items item_flower;
    public Items item_trash;

    readonly string[] message_s5U = {"����A�L���X�^�[�̌N�B\n����ȂƂ���ŏo��Ƃ͊�����ˁB", "�Ȃ񂾂��A�o����ɉ��b�Ȋ������Ȃ�āB\n�����K����T���Ă���񂾂낤�H�����菕�����Ă����悤����Ȃ����B", "����A�M�p���Ă��炦�Ă��Ȃ��悤���ˁB\n����̓A������A���̃����K���͂��Ȃ��l�����Ƃ͂����A��肷�����Ȃ炱�炵�߂Ȃ��Ƃ����Ȃ��B", "������ł��邾�������̎���������ɂ���Ȃ瓦����͂Ȃ����낤�H\n�N�̊�Ȃ�R����Ȃ��ƕ�����͂����B", "�͂́A���������Ď��͖Z�����g�łˁB\n���ꂶ�Ⴀ������󂯎���Ă����Ƃ����B", "�u �}�[�����̉� �v����ɓ��ꂽ\n(�̗́E�ő�̗�+200 �U���E�h��+20 �V�����G���A�ɓ��邲�Ƃɑ̗�50��)", "�u �v�����̋󂫃J�b�v�H �v����ɓ��ꂽ", "����͎c�O���B\n�܂��A���������撣��Ƃ����B"};
    readonly string[] message_s5D = {"���A�}�[�����c�c�B", "(�Ȃ񂩉������C������񂾂�Ȃ��c�c�B)\n���[��A�}�[�����͂ǂ����Ď菕�����Ă�����ł����H", "(�m���ɉR�͌����Ă��Ȃ��݂��������ǁc�c�B\n�}�[�����̂������A�C�e���A�ǂ�����ׂ����ȁH)", "�����K���͋��G�����A������Ă����܂��B\n����͂���Ƃ��āA���l�ɉ�������̂͗ǂ��Ȃ��Ǝv���܂��B", "����A����́c�c�H", "�Ȃ񂾂����������Ă�݂����Ō��Ȃ̂ŁA���͂Ŋ撣��܂��B"};

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        trash = UnityEngine.Random.Range(0, 2);
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
        window_O.SetActive(false);
        tri_R.SetActive(false);
        tri_Q.SetActive(false);
        StartCoroutine(m_Manager.Setting());
    }

    // Update is called once per frame
    void Update()
    {
        if (bgmover.offset >= 1 && !m_Manager.is_Windowed)
        {
            m_Manager.Conversation_Start();
            StartCoroutine(Scene5());
        }

        if (window_O.activeSelf) Select_Time();
    }

    void Select_Time()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            tri_R.SetActive(true);
            tri_Q.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            tri_R.SetActive(false);
            tri_Q.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (tri_R.activeSelf)
            {
                StartCoroutine(Scene5_R(trash));
                window_O.SetActive(false);
            }
            if (tri_Q.activeSelf)
            {
                StartCoroutine(Scene5_Q());
                window_O.SetActive(false);
            }
        }
    }

    IEnumerator Scene5()
    {
        black_D.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        face.sprite = M1;
        characterName.text = "�}�[����";
        message_U.text = message_s5U[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s5D[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        face.sprite = M2;
        message_U.text = message_s5U[1];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s5D[1];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s5U[2];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        face.sprite = M3;
        message_U.text = message_s5U[3];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s5D[2];
        yield return new WaitForSeconds(2);
        black_D.SetActive(true);
        face.sprite = M1;
        message_D.text += "\n(���L�[��Enter�őI��)";
        window_O.SetActive(true);
        tri_R.SetActive(true);
    }

    IEnumerator Scene5_R(int t)
    {
        yield return null;
        black_D.SetActive(false);
        message_D.text = message_s5D[3];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        face.sprite = M2;
        message_U.text = message_s5U[4];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        face.sprite = flame;
        characterName.text = "";
        message_U.text = message_s5U[5];
        if (t == 1)
        {
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            black_U.SetActive(true);
            black_D.SetActive(false);
            message_D.text = message_s5D[4];
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            black_U.SetActive(false);
            black_D.SetActive(true);
            message_U.text = message_s5U[6];
        }
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_D.SetActive(false);
        having.Item_Adder(item_flower);
        if (t == 1) having.Item_Adder(item_trash);
        status.Status_Changer();
        gu_Manager.SandIText_Changer();
        window_U.SetActive(false);
        window_D.SetActive(false);
        m_Manager.is_Window = false;
    }

    IEnumerator Scene5_Q()
    {
        yield return null;
        black_D.SetActive(false);
        message_D.text = message_s5D[5];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s5U[7];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_D.SetActive(false);
        window_U.SetActive(false);
        window_D.SetActive(false);
        m_Manager.is_Window = false;
    }
}
