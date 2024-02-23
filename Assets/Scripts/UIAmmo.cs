using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAmmo : MonoBehaviour
{
    [SerializeField]
    private Text text = null;

    public void UpdateBulletsText(int bulletCount)
    {
        if (bulletCount == 0)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }
        text.text = bulletCount.ToString();
    }
}