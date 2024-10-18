using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Wave Data", order = 0)]
public class WaveData : ScriptableObject
{
    [field: SerializeField] public int ID { get; private set; }
}