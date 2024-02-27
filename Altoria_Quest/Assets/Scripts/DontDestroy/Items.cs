using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemData")]
public class Items : ScriptableObject
{
    public string Name;
    public int count;
    public int p_mHP;
    public int p_Motivation;
    public int p_Attack;
    public int p_Deffense;
    public int price_S;
}

