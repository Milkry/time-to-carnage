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
    private float checkRate = 0.5f;

    private void Update()
    {
        if (Time.time > nextTick)
        {
            nextTick = Time.time + checkRate;
            balance.text = "$ " + accountBalance.ToString();
            rareBalance.text = rareAccountBalance.ToString();
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

    public static void Reset()
    {
        accountBalance = 0;
    }
}
