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
        // 마우스 클릭으로 타워 설치
        if (selectedTower != null && Input.GetMouseButtonDown(0))  // 좌클릭
        {
            PlaceTower();
        }
    }

    // 타워 설치
    void PlaceTower()
    {
        // 설치할 위치 결정
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // 좌표를 얻어서 타워 배치
            Vector3 placementPosition = hit.point;
            Instantiate(selectedTower, placementPosition, Quaternion.identity);
            Debug.Log("타워 설치 위치: " + placementPosition);

            // 타워를 설치한 후 selectedTower를 null로 설정해 다음 설치 대기
            selectedTower = null;

            // 다시 타워 설치 가능하도록 설정
            TowerPanel.SetActive(true);
        }
    }

    // 1번 타워 선택
    void SelectTower1()
    {
        selectedTower = towerPrefab1;
        Debug.Log("1번 타워 선택됨");
        TowerPanel.SetActive(false);
    }

    // 2번 타워 선택
    void SelectTower2()
    {
        selectedTower = towerPrefab2;
        Debug.Log("2번 타워 선택됨");
        TowerPanel.SetActive(false);
    }
}
