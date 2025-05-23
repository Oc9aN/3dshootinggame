using System.Collections;
using Drakkar.GameUtils;
using UnityEngine;

public class Weapon_Knife : Weapon
{
    [SerializeField]
    private float _attackRange;

    [SerializeField]
    private float _attackAngle;

    private DrakkarTrail _trail;
    
    private IEnumerator _trailCoroutine;

    protected override void Awake()
    {
        base.Awake();
        _trail = GetComponentInChildren<DrakkarTrail>();
    }
    
    private void OnBlade()
    {
        _trail.Begin();
    }
    
    private void EndBlade()
    {
        _trail.End();
    }

    public override void Attack()
    {
        if (_fireRate <= 0 && CurrentAmmo > 0)
        {
            // 블레이드 이펙트
            if (!ReferenceEquals(_trailCoroutine, null))
            {
                StopCoroutine(_trailCoroutine);
                EndBlade();
            }
            OnBlade();
            _trailCoroutine = Trail_Coroutine();
            StartCoroutine(_trailCoroutine);
            
            // 부채꼴 범위 안에서 공격
            _fireRate = _data.FireRate;
            // 공격 방향
            Vector3 attackDirection = _weaponAimStrategy.GetWeaponAimingDirection(this);
            Debug.DrawRay(_attackPosition.position, attackDirection * _attackRange, Color.yellow, 10f);
            // 최대 각도의 한쪽 방향
            Quaternion halfAngle = Quaternion.Euler(0, _attackAngle / 2f, 0);
            Vector3 rangeDirection = halfAngle * attackDirection;
            // 점 곱으로 방향의 기준 잡기
            float angleThreshold = Vector3.Dot(attackDirection, rangeDirection);
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _attackRange, ~(1 << LayerMask.NameToLayer("Player")));
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out IDamageable damageableObject))
                {
                    // 데미지를 입는 객체인 경우
                    // 정면 방향을 기준으로 부채꼴 범위 안인지 체크
                    Vector3 targetDirection = (hitCollider.transform.position - _attackPosition.position).normalized;
                    float dot = Vector3.Dot(targetDirection, attackDirection);
                    if (dot >= angleThreshold)
                    {
                        Data.Damage.From = gameObject;
                        damageableObject.TakeDamage(Data.Damage);
                    }
                }
            }
            
            TriggerAnimation();
        }
    }

    protected override void Reloading()
    {
        return;
    }

    private IEnumerator Trail_Coroutine()
    {
        yield return new WaitForSeconds(1f);
        EndBlade();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 center = transform.position;
        Vector3 forward = transform.forward;
        forward.y = 0;

        float halfAngle = _attackAngle * 0.5f;
        Quaternion leftRot = Quaternion.Euler(0, -halfAngle, 0);
        Quaternion rightRot = Quaternion.Euler(0, halfAngle, 0);

        Vector3 leftDir = leftRot * forward;
        Vector3 rightDir = rightRot * forward;

        // 중심선
        Gizmos.DrawLine(center, center + leftDir * _attackRange);
        Gizmos.DrawLine(center, center + rightDir * _attackRange);

        // 원호
        float deltaAngle = _attackAngle / 50f;
        for (int i = 0; i < 50f; i++)
        {
            float currentAngle = -halfAngle + deltaAngle * i;
            float nextAngle = currentAngle + deltaAngle;

            Vector3 dir1 = Quaternion.Euler(0, currentAngle, 0) * forward;
            Vector3 dir2 = Quaternion.Euler(0, nextAngle, 0) * forward;

            Vector3 point1 = center + dir1 * _attackRange;
            Vector3 point2 = center + dir2 * _attackRange;

            Gizmos.DrawLine(point1, point2);
        }
    }
}