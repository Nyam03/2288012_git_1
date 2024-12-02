using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weapons; // 무기 리스트
    public int currentWeaponIndex = 0;
    public Text coinText;
    public Transform weaponParent; // 무기 위치
    public GameObject attackEffectPrefab;
    public Transform effectSpawnPoint;

    public Transform weaponTransform;
    public float swingAngle = 45f;
    public float swingSpeed = 10f;
    private Quaternion initialRotation;
    public float attackCooldown = 1f;
    private float lastAttackTime = 0f;


    public int playerCoin = 0;

    void Start()
    {
        EquipWeapon(currentWeaponIndex); // 첫 번째 무기 장착
        initialRotation = weaponTransform.localRotation;
    }

    void Update()
    {
        UpdatePlayerCoin();

        if (playerCoin > 0)
        {
            CheckForUpgrade();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                lastAttackTime = Time.time;
                PlayAttackEffect();
                StartCoroutine(SwingWeapon());
            }
        }
    }

    void UpdatePlayerCoin()
    {
        if (coinText != null)
        {
            int.TryParse(coinText.text, out playerCoin); // 텍스트 값 정수 변환
        }
    }

    // 무기 업그레이드
    void CheckForUpgrade()
    {
        if (currentWeaponIndex < weapons.Count - 1)
        {
            Weapon nextWeapon = weapons[currentWeaponIndex + 1];

            // 업그레이드
            if (playerCoin >= nextWeapon.requiredCoin)
            {
                UpgradeWeapon();
            }
        }
    }

    // 무기 업그레이드 실행
    void UpgradeWeapon()
    {
        if (currentWeaponIndex < weapons.Count - 1) // 마지막 무기가 아닌 경우
        {
            Weapon nextWeapon = weapons[currentWeaponIndex + 1];

            if (playerCoin >= nextWeapon.requiredCoin)
            {
                currentWeaponIndex++;
                EquipWeapon(currentWeaponIndex);
            }
        }
    }

    // 무기 장착
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
            weaponTransform = newWeapon.transform;
        }
    }

    void PlayAttackEffect()
    {
        if (attackEffectPrefab != null && effectSpawnPoint != null)
        {
            // 이펙트를 생성 및 일정 시간 후 삭제
            GameObject effect = Instantiate(attackEffectPrefab, effectSpawnPoint.position, effectSpawnPoint.rotation);
            Destroy(effect, 0.3f);
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

        // 원래 위치로
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            weaponTransform.localRotation = Quaternion.Slerp(targetRotation, initialRotation, elapsedTime);
            elapsedTime += Time.deltaTime * swingSpeed;
            yield return null;
        }
    }

    public void ResetToDefaultWeapon()
    {
        currentWeaponIndex = 0; // 기본 무기로 초기화
        EquipWeapon(currentWeaponIndex);
        Debug.Log("Weapon reset to default.");
    }
}
