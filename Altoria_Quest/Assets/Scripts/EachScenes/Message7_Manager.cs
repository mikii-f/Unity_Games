using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message7_Manager : MonoBehaviour
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
    public GameObject blanca;
    bool oberon_Chance = false;
    bool blanca_Moving = false;
    bool success = false;
    RectTransform blanca_position;
    const int WIDTH = 900;
    const int TIME = 5;
    public Image face;
    public Sprite flame;
    //public Sprite O1;
    //public Sprite O2;
    //public Sprite O3;
    public TMP_Text characterName;

    Having having;
    Status status;
    public Items item_fp;
    public Items item_mant;

    readonly string[] message_s7U = {"�u �d���̗ؕ� �v��������(�h��+30)", "�����ƁA�������Ă��܂������B����ς�d����Ƃ͖��Ȃ��̂��B\n���܂�ւ��C�͂Ȃ������񂾂��ǂȂ��B", "�S���A�}�X�����Ȃ��A���g���A�́B���Ⴀ������B\n�u�����K���͌������肵�߂����v�A�������������H", "����قǋً}���Ԃł��Ȃ��񂾁A�S�������Ă��܂��Ă��ʔ����Ȃ����낤�H", "�����������K���ɏ��Ă邩�S�z�����H\n�Ȃ�A��������܂����Ă����悤�B", "�u �d�����̃}���g(�X�y�A) �v����ɓ��ꂽ(HP+200 ���C+10 �h��+50)"};
    readonly string[] message_s7D = {"�����ɂ͒N�����Ȃ��݂����B\n��H����́c�c�B", "�����A����ȂƂ���܂ŗ��Ă����ĉ������Ă�́c�c�B\n�����m���Ă��ł���H�����Ă�B", "���A�����̃q���g�c�c�B", "�I�x�����������Ȃ炻���Ȃ񂾂낤���ǁc�c�ꉞ��炪�}�X�^�[�̊�@�Ȃ񂾂�H", "�ׁA�ʂɏ����Ă����Ȃ��Ă����Ă邯�ǁA������������������Ƃ��B\n�c�c�c�c���ƈꉞ�A���肪�ƁI"};

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
        blanca_position = blanca.GetComponent<RectTransform>();
        m_Manager.Start_Scene();
    }

    // Update is called once per frame
    void Update()
    {
        if (bgmover.offset >= 1 && !m_Manager.is_Windowed)
        {
            m_Manager.Conversation_Start();
            StartCoroutine(Scene7());
        }
        if (blanca_Moving)
        {
            Vector2 temp_pos = blanca_position.anchoredPosition;
            temp_pos.x -= (Time.deltaTime / TIME) * WIDTH;
            blanca_position.anchoredPosition = temp_pos;
        }
        if (!ms_Manager.eye_used && oberon_Chance && Input.GetKeyDown(KeyCode.Y)) StartCoroutine(Scene7_EX());
    }

    IEnumerator Scene7()
    {
        black_D.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        message_U.text = message_s7U[0];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s7D[0];
        yield return new WaitForSeconds(1);
        blanca_Moving = true;
        yield return new WaitForSeconds(1);
        oberon_Chance = true;
        ms_Manager.eye_possible = true;
        message_D.text = "";
        black_U.SetActive(false);
        having.Item_Adder(item_fp);
        status.Status_Changer();
        gu_Manager.SandIText_Changer();
        window_U.SetActive(false);
        window_D.SetActive(false);
        yield return new WaitForSeconds(TIME - 1);
        blanca_Moving = false;
        oberon_Chance = false;
        ms_Manager.eye_possible = false;
        if (!success) m_Manager.is_Window = false;
    }

    IEnumerator Scene7_EX()
    {
        success = true;
        oberon_Chance = false;
        ms_Manager.eye_possible = false;
        //oberon.SetActive(true);
        //face.sprite = O1;
        characterName.text = "�I�x����";
        yield return new WaitForSeconds(3);
        window_U.SetActive(true);
        window_D.SetActive(true);
        black_D.SetActive(true);
        message_U.text = message_s7U[1];
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s7D[1];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s7U[2];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s7D[2];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s7U[3];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s7D[3];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        message_U.text = message_s7U[4];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(true);
        black_D.SetActive(false);
        message_D.text = message_s7D[4];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_U.SetActive(false);
        black_D.SetActive(true);
        face.sprite = flame;
        characterName.text = "";
        message_U.text = message_s7U[5];
        yield return null;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        black_D.SetActive(false);
        having.Item_Adder(item_mant);
        status.Status_Changer();
        gu_Manager.SandIText_Changer();
        window_U.SetActive(false);
        window_D.SetActive(false);
        m_Manager.is_Window = false;
    }
}
