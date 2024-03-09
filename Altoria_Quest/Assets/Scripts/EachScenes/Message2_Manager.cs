using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message2_Manager : MonoBehaviour
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
    public Image face;
    public Sprite flame;
    public Sprite BS1;
    public Sprite BS2;
    public Sprite BS3;
    public TMP_Text characterName;
    public GameObject window_O;
    public GameObject tri_H;
    public GameObject tri_B;
    public GameObject window_R;

    Having having;
    Status status;
    public Items item_heel;
    public Items item_boots;

    readonly string[] message_s2U = { "��H�N���Ǝv���Γc�ɗd������˂����B\n�^���ǂ��Ȃ��܂��B", "�C�̐V�삪���������񂾂�B����ŁA�������������郄�c��T���Ă��炨�܂����������ă��P�B", "���傤�ǃT�C�Y�͍����Ă�͂������A���ʂɎ������Ă��B", "���܂ɂ��ƁI�H�c�c���₢���A���͋C�����ǂ�����ȁB\n�q�[��(�U����)�ƃu�[�c(�h���)�A�ǂ����ɂ���񂾁H", "�q�[���c�c���������������悤�w�͂��ȁB\n�O�̂��ߌ����Ƃ�����Ή󂷂Ȃ�B���ƂŃ��r���[����������ȁB", "�u �o�[���@���E�V�[�̃q�[�� �v����ɓ��ꂽ(���C+5 �U��+50)", "�u�[�c���B�܁A���܂��炵���ȁB\n���ƂŃ��r���[���������ȁB", "�u �o�[���@���E�V�[�̃u�[�c �v����ɓ��ꂽ(���C+5 �h��+50)"};
    readonly string[] message_s2D = { "����A�o�[���@���E�V�[�H�Ȃ�ł���ȂƂ���Ɂc�c�B\n����ƁA�^���ǂ����āH", "���A��������I�o�[���@���E�V�[�����܂ɂ͗ǂ����Ƃ��邶���I", "����Ȃ�c�c", "���������ȁB" };

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
        window_O.SetActive(false);
        tri_H.SetActive(false);
        tri_B.SetActive(false);
        window_R.SetActive(false);
        StartCoroutine(m_Manager.Setting());
    }

    // Update is called once per frame
    void Update()
    {
        if (bgmover.offset >= 1 && !m_Manager.is_Windowed)
        {
            m_Manager.Conversation_Start();
            StartCoroutine(Scene2());
        }

        if (window_O.activeSelf) Select_Time();

        if (!ms_Manager.eye_used && ms_Manager.eye_possible && Input.GetKeyDown(KeyCode.Y)) StartCoroutine(Real_Voice());
    }

    void Select_Time()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            tri_H.SetActive(true);
            tri_B.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            tri_H.SetActive(false);
            tri_B.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (tri_H.activeSelf) StartCoroutine(Scene2_R(0));
            if (tri_B.activeSelf) StartCoroutine(Scene2_R(2));
            window_O.SetActive(false);
            ms_Manager.eye_possible = false;
        }
    }

    IEnumerator Scene2()
    {
        black_D.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        face.sprite = BS1;
        characterName.text = "�o�[���@���E�V�[";
        message_U.text = message_s2U[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s2D[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        face.sprite = BS2;
        message_U.text = message_s2U[1];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        message_U.text = message_s2U[2];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s2D[1];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        face.sprite = BS3;
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s2U[3];
        ms_Manager.eye_possible = true;
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s2D[2];
        yield return new WaitForSeconds(1);
        face.sprite = BS1;
        black_D.SetActive(true);
        message_D.text += "\n(���L�[��Enter�őI��)";
        window_O.SetActive(true);
        tri_H.SetActive(true);
    }

    IEnumerator Scene2_R(int n)
    {
        yield return null;
        black_D.SetActive(false);
        message_D.text = message_s2D[3];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s2U[n+4];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        face.sprite = flame;
        characterName.text = "";
        message_U.text = message_s2U[n+5];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_D.SetActive(false);
        if (n == 0) having.Item_Adder(item_heel);
        else having.Item_Adder(item_boots);
        status.Status_Changer();
        gu_Manager.SandIText_Changer();
        window_U.SetActive(false);
        window_D.SetActive(false);
        m_Manager.is_Window = false;
    }

    IEnumerator Real_Voice()
    {
        yield return null;
        window_R.SetActive(true);
        ms_Manager.eye_possible = false;
        yield return new WaitForSeconds(4);
        window_R.SetActive(false);
    }
}
