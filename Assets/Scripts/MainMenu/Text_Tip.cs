using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Tip : MonoBehaviour
{
    public string[] tip_array = new string[] { "�Ķ� ������ �������Դϴ�!", "���� ��ź�� ���������� ������", "�ð��� �ٳ����� �ʵ��� �ϼ���!" };

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
