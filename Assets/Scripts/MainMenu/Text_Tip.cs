using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Tip : MonoBehaviour
{
    public string[] tip_array = new string[] { "파란 구슬은 아이템입니다!", "빨간 폭탄을 무서워하지 마세요", "시간이 다끝나지 않도록 하세요!" };

    public Text Tip_Text;

    private float second_time = 6;


    private void Awake()
    {
        Tip_Text.text = "tip : " + tip_array[Random.Range(0, 2)];
    }
    private void Update()
    {
        second_time -= Time.deltaTime;

        if (second_time < 0)
        {
            Tip_Text.text = "tip : " + tip_array[Random.Range(0, 2)];

            second_time = 6;
        }
    }
}
