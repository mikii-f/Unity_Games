using System;
using System.Collections;
using UnityEngine;

public class Message_Manager : MonoBehaviour
{
    public GameObject window_U;
    public GameObject window_D;
    public GameObject black_U;
    public GameObject black_D;
    public GameObject go_Area;
    [NonSerialized]
    public bool is_Window;
    public bool is_Windowed;
    Guide_Manager gu_Manager;
    Status status;
    Manager manager;


    // Start is called before the first frame update
    void Start()
    {
        is_Window = false;
        GameObject guide_Manager = GameObject.Find("Guide_Manager");
        gu_Manager = guide_Manager.GetComponent<Guide_Manager>();
        GameObject s_Manager = GameObject.Find("Status_Manager");
        status = s_Manager.GetComponent<Status>();
        GameObject ga_Manager = GameObject.Find("Game_Manager");
        manager = ga_Manager.GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Conversation_Start()
    {
        window_U.SetActive(true);
        window_D.SetActive(true);
        is_Window = true;
        is_Windowed = true;
    }
    public IEnumerator Setting()
    {
        yield return null;
        manager.Area_Text(go_Area);
        window_U.SetActive(false);
        window_D.SetActive(false);
        black_U.SetActive(false);
        black_D.SetActive(false);
        status.Status_Changer();
        gu_Manager.SandIText_Changer();
        yield return new WaitForSeconds(2);
        go_Area.SetActive(false);
    }
}
