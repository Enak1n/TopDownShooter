using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    public int countDownFrom = 2;

    int count;

    private void Start()
    {
        count = countDownFrom;
    }

    public void CountDownFrom()
    {

        GetComponent<TextMeshProUGUI>().text = count.ToString();

        count--;

        if (count <= 0)
        {
            count = countDownFrom;
        }

    }
}
