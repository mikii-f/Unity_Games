using System.Collections;
using UnityEngine;

public class Scene0_Manager : MonoBehaviour
{
    public GameObject window_M;
    public GameObject black;
    Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        GameObject ga_Manager = GameObject.Find("Game_Manager");
        manager = ga_Manager.GetComponent<Manager>();
        window_M.SetActive(false);
        black.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(GameStart());
        }
    }

    IEnumerator GameStart()
    {
        black.SetActive(true);
        manager.Order_Setter();
        yield return new WaitForSeconds(2);
        manager.Scene_Changer();
    }
}
