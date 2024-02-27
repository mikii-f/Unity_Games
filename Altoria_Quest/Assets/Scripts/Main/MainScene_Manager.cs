using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainScene_Manager : MonoBehaviour
{
    public GameObject background;
    BgMover bgmover;
    public GameObject ponti;
    public GameObject go_Next;
    public GameObject black;
    public GameObject reverse;
    [NonSerialized]
    public bool eye_used = false;
    [NonSerialized]
    public bool eye_possible = false;
    Image fade;
    bool is_Reached;
    Message_Manager m_Manager;
    Guide_Manager gu_Manager;
    Manager manager;
    Status status;
    const float fade_time = 0.5f;
    const int loop = 20;
    float wait_time = fade_time / loop;
    float alpha_interval = 255.0f / loop;
    // Start is called before the first frame update
    void Start()
    {
        go_Next.SetActive(false);
        reverse.SetActive(false);
        bgmover = background.GetComponent<BgMover>();
        ponti.GetComponent<RectTransform>().anchoredPosition = new Vector3(-272 + Manager.current_Scene * 61f, 210f, 0f);
        is_Reached = false;
        GameObject message_Manager = GameObject.Find("Message_Manager");
        m_Manager = message_Manager.GetComponent<Message_Manager>();
        GameObject guide_Manager = GameObject.Find("Guide_Manager");
        gu_Manager = guide_Manager.GetComponent<Guide_Manager>();
        GameObject ga_Manager = GameObject.Find("Game_Manager");
        manager = ga_Manager.GetComponent<Manager>();
        GameObject s_Manager = GameObject.Find("Status_Manager");
        status = s_Manager.GetComponent<Status>();
        fade = black.GetComponent<Image>();
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_Reached && bgmover.offset >= BgMover.MAX)
        {
            is_Reached = true;
            go_Next.SetActive(true);
            StartCoroutine(Next());
        }

        if (is_Reached && bgmover.offset < BgMover.MAX)
        {
            go_Next.SetActive(false);
        }
        else if (is_Reached && bgmover.offset >= BgMover.MAX)
        {
            go_Next.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Y) && !eye_used && (!m_Manager.is_Window || eye_possible)) StartCoroutine(FairyEyes());

        if (Input.GetKeyDown(KeyCode.S))
        {
            manager.Debug();
        }
    }

    public IEnumerator FadeIn()
    {
        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
        {
            yield return new WaitForSeconds(wait_time);
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
    }

    public IEnumerator FadeOut()
    {
        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            yield return new WaitForSeconds(wait_time);
            Color new_color = fade.color;
            new_color.a = alpha / 255.0f;
            fade.color = new_color;
        }
    }

    IEnumerator Next()
    {
        yield return new WaitUntil((() => (Input.GetKeyDown(KeyCode.Return)) && (bgmover.offset >= BgMover.MAX)));
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(2);
        manager.Scene_Changer();
    }

    IEnumerator FairyEyes()
    {
        yield return null;
        eye_used = true;
        status.motivation -= 1;
        status.Status_Changer();
        gu_Manager.SandIText_Changer();
        reverse.SetActive(true);
        yield return new WaitForSeconds(2);
        reverse.SetActive(false);
    }
}
