using System;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int hp;
    public int mhp;
    public int motivation;
    public int attack;
    public int defense;

    [NonSerialized]
    public static int HP;
    [NonSerialized]
    public static int mHP;
    [NonSerialized]
    public static int Motivation;
    [NonSerialized]
    public static int Attack;
    [NonSerialized]
    public static int Defense;
    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        HP = hp;
        mHP = mhp;
        Motivation = motivation;
        Attack = attack;
        Defense = defense;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Status_Ps(int a)
    {
        int n = 0;
        int x = Having.items.Count;
        switch (a)
        {
            case 0:
                for (int i = 0; i < x; i++)
                {
                    n += Having.items[i].p_mHP * Having.items[i].count;
                }
                break;
            case 1:
                for (int i = 0; i < x; i++)
                {
                    n += Having.items[i].p_Motivation * Having.items[i].count;
                }
                break;
            case 2:
                for (int i = 0; i < x; i++)
                {
                    n += Having.items[i].p_Attack * Having.items[i].count;
                }
                break;
            case 3:
                for (int i = 0; i < x; i++)
                {
                    n += Having.items[i].p_Deffense * Having.items[i].count;
                }
                break;
        }
        return n;
    }

    public void Status_Changer()
    {
        HP = hp + Status_Ps(0);
        mHP = mhp + Status_Ps(0);
        Motivation = motivation + Status_Ps(1);
        Attack = attack + Status_Ps(2);
        Defense = defense + Status_Ps(3);
        if (HP > mHP)
        {
            int over = HP - mHP;
            hp -= over;
            HP = mHP;
        }
        if (Motivation < 0) Motivation = 0;
    }
}
