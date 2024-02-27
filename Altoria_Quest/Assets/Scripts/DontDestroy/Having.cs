using System.Collections.Generic;
using UnityEngine;

public class Having : MonoBehaviour
{
    public static List<Items> items = new();

    public Items QP;

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        items.Add(QP);
    }

    public void Item_Adder(Items item)
    {
        if (items.FindIndex(x => x.Name == item.Name) >= 0)
        {
            int a = items.FindIndex(x => x.Name == item.Name);
            items[a].count += 1;
        }
        else
        {
            items.Add(item);
        }
    }

    public void QP_Changer(int n)
    {
        QP.count += n;   
    }

    public void Sell(Items item, int n)
    {
        if (item.count - n == 0)
        {
            items.Remove(item);
            item.count = 1;
        }
        else item.count -= n;
        QP_Changer(item.price_S * n);
    }

    private void OnDestroy()
    {
        QP.count = 10000;
        int n = items.Count;
        for (int i=1; i<n; i++)
        {
            items[i].count = 1;
        }
    }
}