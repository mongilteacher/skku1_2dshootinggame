using UnityEngine;

[CreateAssetMenu(fileName = "AttendanceDataSO", menuName = "Scriptable Objects/AttendanceDataSO")]
public class AttendanceDataSO : ScriptableObject
{
    public int Day = 0;
    public CurrencyType RewardCurrencyType;
    public int RewardAmount;
}
