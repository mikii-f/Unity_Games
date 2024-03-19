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
    public static int current_Scene = 0;
    public static int BaobhanSithWatched = 0;
    public static int OberonFound = 0;
    public static int SumOfBoughtSwordPrice = 0;
    public int total = 0;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Order_Setter()
    {
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

    public IEnumerator Score(TMP_Text viewpoint, TMP_Text score)
    {
        int property = Having.items[0].count + SumOfBoughtSwordPrice;
        for (int i = 1; i < Having.items.Count; i++) property += Having.items[i].price_S;
        total += property;
        yield return new WaitForSeconds(0.7f);
        viewpoint.text = "���Y(QP���Z)";
        score.text = "=�@" +  property.ToString();
        yield return new WaitForSeconds(0.7f);
        viewpoint.text += "\n�c��̗́@�~�@10";
        score.text += "\n=�@" + (Status.HP * 10).ToString();
        total += Status.HP * 10;
        yield return new WaitForSeconds(1);
        if (OberonFound == 0)
        {
            viewpoint.text += "\n�H�H�H�@�~�@0";
            score.text += "\n=�@0";
        }
        else
        {
            viewpoint.text += "\n�I�x�������������@�~�@1";
            score.text += "\n=�@2000";
            total += 2000;
        }
        yield return new WaitForSeconds(0.7f);
        if (BaobhanSithWatched == 0)
        {
            viewpoint.text += "\n�H�H�H�@�~�@0";
            score.text += "\n=�@0";
        }
        else
        {
            viewpoint.text += "\n�o�[���@���E�V�[�̖{���������@�~�@1";
            score.text += "\n=�@5000";
            total += 5000;
        }
        yield return new WaitForSeconds(1.5f);
        viewpoint.text += "\n���v�X�R�A";
        score.text += "\n" + total.ToString();
    }

    public void Reset_M()
    {
        current_Scene = 0;
        for (int j = 2; j < SCENE_NUMBER - 1; j++) scene_Order[j] = 0;
        BaobhanSithWatched = 0;
        OberonFound = 0;
        SumOfBoughtSwordPrice = 0;
        total = 0;
    }
}
