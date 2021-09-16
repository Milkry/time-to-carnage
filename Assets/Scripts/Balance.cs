using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Balance : MonoBehaviour
{
    public static int accountBalance = 0;
    public static int rareAccountBalance;

    [SerializeField] private TextMeshProUGUI balance;
    [SerializeField] private TextMeshProUGUI rareBalance;

    private float nextTick = 0f;
    private float updateRate = 0.5f;

    private void Start()
    {
        GameData data = SaveSystem.LoadData();

        rareAccountBalance = data.gems;
    }

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
            DepositGems(5);
        }
    }

    public static void Deposit(int amount)
    {
        accountBalance += amount;
    }
    
    public static void DepositGems(int amount)
    {
        rareAccountBalance += amount;
    }

    public static void Withdraw(int amount)
    {
        accountBalance -= amount;
    }

    public static void WithdrawGems(int amount)
    {
        rareAccountBalance -= amount;
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

    public static bool CanWithdrawGems(int amount)
    {
        if (rareAccountBalance >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void CalculateGems(int minAmountInclusive, int maxAmountInclusive, int chance)
    {
        if (chance > 0 && chance <= 100)
        {
            if (chance <= Random.Range(1, 100))
            {
                int amount = Random.Range(minAmountInclusive, maxAmountInclusive);
                DepositGems(amount);
            }
        }
        else
        {
            Debug.LogWarning($"Invalid 'Chance' number provided ({chance}). Number must be between (1 - 100)");
        }
    }

    public static void Reset()
    {
        accountBalance = 0;
    }
}
