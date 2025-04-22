using UnityEngine;

public class Bomb : MonoBehaviour
{
    // 목표: 마우스의 오른쪽 버튼으로 수류탄 투척
    // 1. 수류탄 오브젝트
    // 2. 오른쪽 버튼 입력 받기
    // 3. 발사 위치에 수류탄 생성하기
    // 4. 생성된 수류탄을 카메라 방향으로 물리적인 힘 가하기

    [SerializeField]
    private GameObject _explosionEffectPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject effectObject = Instantiate(_explosionEffectPrefab);
        effectObject.transform.position = transform.position;
        
        Destroy(gameObject);
    }
}
