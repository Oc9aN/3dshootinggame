using UnityEngine;

public class Weapon_Knife : Weapon
{
    [SerializeField]
    private float _attackRange;

    [SerializeField]
    private float _attackAngle;

    public override void Attack()
    {
        if (_fireRate <= 0 && CurrentAmmo > 0)
        {
            // 부채꼴 범위 안에서 공격
            _fireRate = _data.FireRate;
            // 공격 방향
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _attackRange);
            Vector3 attackDirection = _camera.transform.forward;
            attackDirection.y = 0;
            attackDirection.Normalize();
            Debug.DrawRay(_attackPosition, attackDirection * _attackRange, Color.yellow, 10f);
            // 최대 각도의 한쪽 방향
            Quaternion halfAngle = Quaternion.Euler(0, _attackAngle / 2f, 0);
            Vector3 rangeDirection = halfAngle * attackDirection;
            // 점 곱으로 방향의 기준 잡기
            float angleThreshold = Vector3.Dot(attackDirection, rangeDirection);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out IDamageable damageableObject))
                {
                    // 데미지를 입는 객체인 경우
                    // 정면 방향을 기준으로 부채꼴 범위 안인지 체크
                    Vector3 targetDirection = (hitCollider.transform.position - _attackPosition).normalized;
                    float dot = Vector3.Dot(targetDirection, attackDirection);
                    if (dot >= angleThreshold)
                    {
                        Data.Damage.From = gameObject;
                        damageableObject.TakeDamage(Data.Damage);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 center = transform.position;
        Vector3 forward = transform.forward;

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