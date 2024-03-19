using System;
using UnityEngine;
using TMPro;

public class Guide_Manager : MonoBehaviour
{
    public GameObject window_L;
    public GameObject window_R;
    Message_Manager m_Manager;
    public GameObject window_M;
    public GameObject item_Content;
    TMP_Text t_Item;
    public TMP_Text t_Status;
    [NonSerialized]
    public bool is_Sidebar;
    // Start is called before the first frame update
    void Start()
    {
        window_L.SetActive(false);
        window_R.SetActive(false);
        t_Item = item_Content.GetComponent<TMP_Text>();
        GameObject message_Manager = GameObject.Find("Message_Manager");
        m_Manager = message_Manager.GetComponent<Message_Manager>();
        is_Sidebar = false;
        if (Having.items.FindIndex(x => x.Name == "ƒ}[ƒŠƒ“‚Ì‰Ô") >= 0)
        {
            GameObject s_Manager = GameObject.Find("Status_Manager");
            Status status = s_Manager.GetComponent<Status>();
            status.hp += 50;
        }
        if (Having.items.FindIndex(x => x.Name == "‚¨‚É‚¬‚è") >= 0 && Status.mHP - Status.HP >= 50)
        {
            int n = Having.items.FindIndex(x => x.Name == "‚¨‚É‚¬‚è");
            int m = Mathf.FloorToInt((Status.mHP - Status.HP) / 50);
            GameObject s_Manager = GameObject.Find("Status_Manager");
            Status status = s_Manager.GetComponent<Status>();
            status.hp += 50 * Mathf.Min(Having.items[n].count, m);
            Having.items[n].count -= Mathf.Min(Having.items[n].count, m);
        }
        else if (Having.items.FindIndex(x => x.Name == "‚¨‚É‚¬‚è") >= 0 && Status.mHP - Status.HP > 0 && Manager.current_Scene == 9)
        {
            int n = Having.items.FindIndex(x => x.Name == "‚¨‚É‚¬‚è");
            GameObject s_Manager = GameObject.Find("Status_Manager");
            Status status = s_Manager.GetComponent<Status>();
            status.hp += 50;
            Having.items[n].count -= 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !m_Manager.is_Window)
        {
            window_L.SetActive(true);
            window_R.SetActive(true);
            is_Sidebar =true;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            window_L.SetActive(false);
            window_R.SetActive(false);
            is_Sidebar =false;
        }

        if (m_Manager.is_Window) window_M.SetActive(false);
        else window_M.SetActive(true);
    }

    public void SandIText_Changer()
    {
        t_Status.text = Status.HP + "\n" + "/" + Status.mHP + "\n" + Status.Motivation + "\n" + Status.Attack + "\n" + Status.Defense;
        string itemtext = "";
        int n = Having.items.Count;
        for (int i = 0; i < n; i++)
        {
            itemtext += Having.items[i].Name + " ~ " + Having.items[i].count + "\n";
        }
        t_Item.text = itemtext;
    }
}
