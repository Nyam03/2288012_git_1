using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weapons; // 무기 리스트
    public int currentWeaponIndex = 0; // 현재 무기
    public Text coinText;
    public Transform weaponParent; // 무기 위치
    public GameObject attackEffectPrefab; // 공격 이펙트
    public Transform effectSpawnPoint;    // 이펙트 위치

    public Transform weaponTransform;
    public float swingAngle = 45f;
    public float swingSpeed = 10f;
    private Quaternion initialRotation;
    public float attackCooldown = 1f;
    private float lastAttackTime = 0f;


    public int playerCoin = 0; // 현재 골드

    void Start()
    {
        EquipWeapon(currentWeaponIndex); // 첫 번째 무기 장착
        initialRotation = weaponTransform.localRotation;
    }

    void Update()
    {
        UpdatePlayerCoin();

        // playerCoin이 0보다 큰 경우에만 업그레이드 조건을 확인
        if (playerCoin > 0)
        {
            CheckForUpgrade();
        }

        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                lastAttackTime = Time.time; // 마지막 공격 시간 갱신
                PlayAttackEffect(); // 공격 이펙트 재생
                StartCoroutine(SwingWeapon()); // 무기 휘두르기
            }
        }
    }

    // UI 텍스트에서 골드 값을 업데이트
    void UpdatePlayerCoin()
    {
        if (coinText != null)
        {
            int.TryParse(coinText.text, out playerCoin); // 텍스트 값을 정수로 변환
        }
    }

    // 무기 업그레이드 조건 확인
    void CheckForUpgrade()
    {
        if (currentWeaponIndex < weapons.Count - 1)
        {
            Weapon nextWeapon = weapons[currentWeaponIndex + 1];

            // 조건 충족 시 업그레이드 실행
            if (playerCoin >= nextWeapon.requiredCoin)
            {
                UpgradeWeapon();
            }
        }
    }

    // 무기 업그레이드 실행
    void UpgradeWeapon()
    {
        if (currentWeaponIndex < weapons.Count - 1) // 마지막 무기가 아닌 경우에만 실행
        {
            Weapon nextWeapon = weapons[currentWeaponIndex + 1];

            // 조건 확인: 다시 한 번 코인 충족 여부 확인
            if (playerCoin >= nextWeapon.requiredCoin)
            {
                currentWeaponIndex++;
                EquipWeapon(currentWeaponIndex);
            }
            else
            {

            }
        }
    }

    // 무기를 장착하는 메서드
    void EquipWeapon(int weaponIndex)
    {
        foreach (Transform child in weaponParent) // 기존 무기 제거
        {
            Destroy(child.gameObject);
        }

        Weapon weapon = weapons[weaponIndex];
        GameObject weaponPrefab = Resources.Load<GameObject>($"Weapons/{weapon.weaponName}");

        if (weaponPrefab != null)
        {
            GameObject newWeapon = Instantiate(weaponPrefab, weaponParent.position, weaponParent.rotation, weaponParent);

            // weaponTransform을 새로 생성된 무기의 Transform으로 설정
            weaponTransform = newWeapon.transform;
        }
        else
        {

        }
    }

    void PlayAttackEffect()
    {
        if (attackEffectPrefab != null && effectSpawnPoint != null)
        {
            Instantiate(attackEffectPrefab, effectSpawnPoint.position, effectSpawnPoint.rotation);
        }
    }

    IEnumerator SwingWeapon()
    {
        // 휘두르기
        Quaternion targetRotation = Quaternion.Euler(0, 0, -swingAngle);
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            weaponTransform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * swingSpeed;
            yield return null;
        }

        // 원래 위치로 복원
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            weaponTransform.localRotation = Quaternion.Slerp(targetRotation, initialRotation, elapsedTime);
            elapsedTime += Time.deltaTime * swingSpeed;
            yield return null;
        }
    }
}
