using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private Text Ans;
    public GameObject Door;

    private string Answer = "123456";
    private bool isCorrect = false;
    private bool isCooldown = false;
    
    void Update()
    {
        if (!isCorrect && !isCooldown)
        {
            // ตรวจจับการกดปุ่มตัวเลข 0-9 บน keyboard ปกติ
            for (KeyCode key = KeyCode.Alpha0; key <= KeyCode.Alpha9; key++)
            {
                if (Input.GetKeyDown(key))
                {
                    int number = (int)key - (int)KeyCode.Alpha0;
                    Number(number);
                }
            }

            // ตรวจจับการกดปุ่มตัวเลข 0-9 บน numpad
            for (KeyCode key = KeyCode.Keypad0; key <= KeyCode.Keypad9; key++)
            {
                if (Input.GetKeyDown(key))
                {
                    int number = (int)key - (int)KeyCode.Keypad0;
                    Number(number);
                }
            }

            // ตรวจจับปุ่ม Enter สำหรับยืนยัน
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Execute();
            }

            // ตรวจจับปุ่ม Backspace เพื่อลบทีละตัว และ Delete เพื่อลบทั้งหมด
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                RemoveLastDigit();
            }
            else if (Input.GetKeyDown(KeyCode.Delete))
            {
                cleanAns();
            }
        }
    }
    
    public void Number(int number)
    {
        if (Ans.text.Length >= 6 && Ans.text != "CORRECT" && Ans.text != "INCORRECT") return;
        if (!isCorrect && !isCooldown)
        {
            Ans.text += number.ToString();
        }
    }

    public void Execute()
    {
        if (Ans.text == Answer)
        {
            Ans.text = "CORRECT";
            Door.SetActive(false);
            isCorrect = true;
            StartCoroutine(DisableKeypad());
        }
        else
        {
          StartCoroutine(IncorrectCooldown()); 
        }
    }

    private IEnumerator DisableKeypad()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    
    private IEnumerator IncorrectCooldown()
    {
        Ans.text = "INCORRECT";
        isCooldown = true;
        yield return new WaitForSeconds(1f);
        Ans.text = ""; // ล้างข้อความเพื่อให้ใส่รหัสใหม่
        isCooldown = false;
    }

    // ลบตัวเลขทั้งหมด
    public void cleanAns()
    {
        Ans.text = "";
    }

    // ลบตัวเลขตัวสุดท้าย
    public void RemoveLastDigit()
    {
        if (Ans.text.Length > 0 && Ans.text != "CORRECT" && Ans.text != "INCORRECT")
        {
            Ans.text = Ans.text.Substring(0, Ans.text.Length - 1);
        }
    }
}