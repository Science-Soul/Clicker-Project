using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig")]
public class GameConfig : ScriptableObject
{
    public long BasePassiveIncome = 5;
    public long InitialMoney = 0;
    public long BaseUpgradePrice = 10;
}
