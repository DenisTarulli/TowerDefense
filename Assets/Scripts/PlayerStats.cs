using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    [SerializeField] private int startingMoney;

    public static int Lives;
    [SerializeField] private int startingLives;

    private void Start()
    {
        Money = startingMoney;
        Lives = startingLives;
    }
}
