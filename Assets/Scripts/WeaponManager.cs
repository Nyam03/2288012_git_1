using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weapons; // ���� ����Ʈ
    public int currentWeaponIndex = 0;
    public Text coinText;
    public Transform weaponParent; // ���� ��ġ
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
        EquipWeapon(currentWeaponIndex); // ù ��° ���� ����
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
            int.TryParse(coinText.text, out playerCoin); // �ؽ�Ʈ �� ���� ��ȯ
        }
    }

    // ���� ���׷��̵�
    void CheckForUpgrade()
    {
        if (currentWeaponIndex < weapons.Count - 1)
        {
            Weapon nextWeapon = weapons[currentWeaponIndex + 1];

            // ���׷��̵�
            if (playerCoin >= nextWeapon.requiredCoin)
            {
                UpgradeWeapon();
            }
        }
    }

    // ���� ���׷��̵� ����
    void UpgradeWeapon()
    {
        if (currentWeaponIndex < weapons.Count - 1) // ������ ���Ⱑ �ƴ� ���
        {
            Weapon nextWeapon = weapons[currentWeaponIndex + 1];

            if (playerCoin >= nextWeapon.requiredCoin)
            {
                currentWeaponIndex++;
                EquipWeapon(currentWeaponIndex);
            }
        }
    }

    // ���� ����
    void EquipWeapon(int weaponIndex)
    {
        foreach (Transform child in weaponParent) // ���� ���� ����
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
            // ����Ʈ�� ���� �� ���� �ð� �� ����
            GameObject effect = Instantiate(attackEffectPrefab, effectSpawnPoint.position, effectSpawnPoint.rotation);
            Destroy(effect, 0.3f);
        }
    }

    IEnumerator SwingWeapon()
    {
        // �ֵθ���
        Quaternion targetRotation = Quaternion.Euler(0, 0, -swingAngle);
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            weaponTransform.localRotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * swingSpeed;
            yield return null;
        }

        // ���� ��ġ��
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
        currentWeaponIndex = 0; // �⺻ ����� �ʱ�ȭ
        EquipWeapon(currentWeaponIndex);
        Debug.Log("Weapon reset to default.");
    }
}
