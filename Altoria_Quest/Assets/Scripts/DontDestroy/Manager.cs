using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Manager : MonoBehaviour
{
    const int SCENE_NUMBER = 10;
    int[] scene_Order = new int[SCENE_NUMBER];
    [NonSerialized]
    public static int current_Scene;

    static Manager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        scene_Order[0] = 0;
        scene_Order[1] = 1;
        scene_Order[SCENE_NUMBER-1] = 100;
        current_Scene = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Order_Setter()
    {
        if (current_Scene != 0)
        {
            current_Scene = 0;
            for (int j=2; j < SCENE_NUMBER-1; j++) scene_Order[j] = 0;
        }
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        int i = 2;
        while (i < SCENE_NUMBER-1)
        {
            int n = UnityEngine.Random.Range(2, SCENE_NUMBER-1);
            if (!scene_Order.Contains(n))
            {
                scene_Order[i] = n;
                i++;
            }
            else continue;
        }
    }

    public void Scene_Changer()
    {
        current_Scene++;
        switch(scene_Order[current_Scene])
        {
            case 1:
                SceneManager.LoadScene("MainScene1");
                break;
            case 2:
                SceneManager.LoadScene("MainScene2");
                break;
            case 3:
                SceneManager.LoadScene("MainScene3");
                break;
            case 4:
                SceneManager.LoadScene("MainScene4");
                break;
            case 5:
                SceneManager.LoadScene("MainScene5");
                break;
            case 6:
                SceneManager.LoadScene("MainScene6");
                break;
            case 7:
                SceneManager.LoadScene("MainScene7");
                break;
            case 8:
                SceneManager.LoadScene("MainScene8");
                break;
            case 100:
                SceneManager.LoadScene("GrandBattle");
                break;
        }
    }

    public void Area_Text(GameObject go_Area)
    {
        TMP_Text area_Text = go_Area.GetComponentInChildren<TMP_Text>();
        string text = "�G���A" + current_Scene.ToString() + "\n";
        switch (scene_Order[current_Scene])
        {
            case 1:
                text += "�`���[�g���A��";
                break;
            case 2:
                text += "�C�x���g";
                break;
            case 3:
                text += "���퉮";
                break;
            case 4:
                text += "�퓬";
                break;
            case 5:
                text += "�C�x���g";
                break;
            case 6:
                text += "�Ό�";
                break;
            case 7:
                text += "�C�x���g";
                break;
            case 8:
                text += "�x�e";
                break;
            case 100:
                text = "�ŏI�G���A\n�{�X";
                break;
        }
        area_Text.text = text;
    }
    public IEnumerator Skip()
    {
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.S));
        if (Input.GetKey(KeyCode.Alpha2)) SceneManager.LoadScene("MainScene2");
        else if (Input.GetKey(KeyCode.Alpha3)) SceneManager.LoadScene("MainScene3");
        else if (Input.GetKey(KeyCode.Alpha4)) SceneManager.LoadScene("MainScene4");
        else if (Input.GetKey(KeyCode.Alpha5)) SceneManager.LoadScene("MainScene5");
        else if (Input.GetKey(KeyCode.Alpha6)) SceneManager.LoadScene("MainScene6");
        else if (Input.GetKey(KeyCode.Alpha7)) SceneManager.LoadScene("MainScene7");
        else if (Input.GetKey(KeyCode.Alpha8)) SceneManager.LoadScene("MainScene8");
        else if (Input.GetKey(KeyCode.Alpha0)) SceneManager.LoadScene("GrandBattle");
    }
}
