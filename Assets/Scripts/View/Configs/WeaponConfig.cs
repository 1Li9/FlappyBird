using UnityEngine;

[CreateAssetMenu(fileName ="WeaponConfig", menuName = "NewWeaponConfig", order = 51)]
public class WeaponConfig : ScriptableObject, IWeaponConfig
{
    [SerializeField] private int _poolCapacity;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _releaseXPosition;

    public int PoolCapacity => _poolCapacity;
    public float CooldownTime => _cooldownTime;
    public float ReleaseXPosition => _releaseXPosition;
}
