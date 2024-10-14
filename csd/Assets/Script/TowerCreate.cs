using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerCreate : MonoBehaviour
{
    public Button towerButton1;
    public Button towerButton2;

    public GameObject towerPrefab1;
    public GameObject towerPrefab2;

    private GameObject selectedTower;
    public GameObject TowerPanel;

    public void PanelOff()
    {
        TowerPanel.SetActive(false);
    }

    public void PanelOn()
    {
        TowerPanel.SetActive(true);
    }

    void Start()
    {
        towerButton1.onClick.AddListener(SelectTower1);
        towerButton2.onClick.AddListener(SelectTower2);
    }

    void Update()
    {
        // ���콺 Ŭ������ Ÿ�� ��ġ
        if (selectedTower != null && Input.GetMouseButtonDown(0))  // ��Ŭ��
        {
            PlaceTower();
        }
    }

    // Ÿ�� ��ġ
    void PlaceTower()
    {
        // ��ġ�� ��ġ ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // ��ǥ�� �� Ÿ�� ��ġ
            Vector3 placementPosition = hit.point;
            Instantiate(selectedTower, placementPosition, Quaternion.identity);
            Debug.Log("Ÿ�� ��ġ ��ġ: " + placementPosition);

            // Ÿ���� ��ġ�� �� selectedTower�� null�� ������ ���� ��ġ ���
            selectedTower = null;

            // �ٽ� Ÿ�� ��ġ �����ϵ��� ����
            TowerPanel.SetActive(true);
        }
    }

    // 1�� Ÿ�� ����
    void SelectTower1()
    {
        selectedTower = towerPrefab1;
        Debug.Log("1�� Ÿ�� ���õ�");
        TowerPanel.SetActive(false);
    }

    // 2�� Ÿ�� ����
    void SelectTower2()
    {
        selectedTower = towerPrefab2;
        Debug.Log("2�� Ÿ�� ���õ�");
        TowerPanel.SetActive(false);
    }
}
