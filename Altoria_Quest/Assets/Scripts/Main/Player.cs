using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;

    Message_Manager m_Manager;
    public GameObject guide_Manager;
    Guide_Manager gu_Manager;
    public GameObject background;
    BgMover bgmover;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("run_R", false);
        anim.SetBool("run_L", false);
        GameObject message_Manager = GameObject.Find("Message_Manager");
        m_Manager = message_Manager.GetComponent<Message_Manager>();
        gu_Manager = guide_Manager.GetComponent<Guide_Manager>();
        bgmover = background.GetComponent<BgMover>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Manager.is_Window == false && gu_Manager.is_Sidebar == false
            && bgmover.offset >= BgMover.MIN && bgmover.offset <= BgMover.MAX)
        {
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("run_R", false);
                anim.SetBool("run_L", false);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetBool("run_R", true);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("run_L", true);
            }
            else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("run_R", false);
                anim.SetBool("run_L", false);
            }
            else
            {
                anim.SetBool("run_R", false);
                anim.SetBool("run_L", false);
            }
        }
        else
        {
            anim.SetBool("run_R", false);
            anim.SetBool("run_L", false);
        }
    }
}
