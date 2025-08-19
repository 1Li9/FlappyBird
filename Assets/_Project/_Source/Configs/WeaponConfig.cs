using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "NewWeaponConfig", order =51)]
public class WeaponConfig : ScriptableObject
{
    [SerializeField] private float _xBulletSpawnGap;
    [SerializeField] private float _weaponCooldownTime;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Vector3 _bulletSpawnPosition;
    [SerializeField] private Vector3 _bulletDirection;

    public float XBulletSpawnGap => _xBulletSpawnGap;
    public float CooldownTime => _weaponCooldownTime;
    public float BulletSpeed => _bulletSpeed;
    public Vector3 BulletSpawnPosition => _bulletSpawnPosition;
    public Vector3 BulletDirection => _bulletDirection;

    private void OnValidate()
    {
        _bulletDirection.Normalize();   
    }
}