using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Balance : MonoBehaviour
{
    public static int accountBalance = 0;
    public static int rareAccountBalance = 0;

    [SerializeField] private TextMeshProUGUI balance;
    [SerializeField] private TextMeshProUGUI rareBalance;

    private float nextTick = 0f;
    private float updateRate = 0.5f;

    private void Update()
    {
        if (Time.time > nextTick)
        {
            nextTick = Time.time + updateRate;
            balance.text = "$ " + accountBalance.ToString();
            rareBalance.text = rareAccountBalance.ToString();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Deposit(10000);
        }
    }

    public static void Deposit(int amount)
    {
        accountBalance += amount;
    }

    public static void Withdraw(int amount)
    {
        accountBalance -= amount;
    }

    public static bool CanWithdraw(int amount)
    {
        if (accountBalance >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void Reset()
    {
        accountBalance = 0;
    }
}
