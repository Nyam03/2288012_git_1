using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.UI.CanvasScaler;
using UnityEngine.UI;
using UnityEditor;

public class MapCreate : MonoBehaviour
{
    [Header("길 종류") ]
    public GameObject straight_road;
    public GameObject turn_road;
    public GameObject select_road;

    private string targetWord_1 = "Straight";
    private string targetWord_2 = "Turn";

    [Header("길 선택 버튼")]
    public Button[] road_button;
    void Start()
    {
        // Resources 폴더 내의 오브젝트 불러오기
        straight_road = Resources.Load<GameObject>("Straight");
        turn_road = Resources.Load<GameObject>("Turn");
    }

    void Update()
    {
        OnMouseDown();
    }

    private void OnMouseDown()
    {
        // 좌클릭 : 길 설치
        if (Input.GetMouseButtonDown(0))
        {
            // ray를 통해 바닥 확인
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // 감지한 오브젝트의 중앙 좌표 확인
                Collider hitcollider = hit.collider;
                Vector3 center = hitcollider.bounds.center;
                Debug.Log(center);

                // 충돌한 오브젝트의 이름 확인
                string objectname = hit.collider.gameObject.name;

                // 길을 선택 했는지, 이미 길이 설치되어 있지 않은지 확인
                if (select_road != null && !objectname.Contains(targetWord_1) && !objectname.Contains(targetWord_2))
                {
                    if (select_road.name == "Straight")
                    {
                        center.x -= 2;
                        center.y += 0.5f;
                        center.z += 6;
                        Instantiate(straight_road, center, Quaternion.identity);

                    }
                    else if (select_road.name == "Straight_90")
                    {
                        center.x += 6;
                        center.y += 0.5f;
                        center.z += 2;
                        Instantiate(straight_road, center, Quaternion.Euler(0, 90, 0));
                    }
                    else if (select_road.name == "Turn")
                    {
                        center.x -= 2;
                        center.y += 0.5f;
                        center.z += 4;
                        Instantiate(turn_road, center, Quaternion.identity);
                    }
                    else if (select_road.name == "Turn_90")
                    {
                        center.x += 4;
                        center.y += 0.5f;
                        center.z += 2;
                        Instantiate(turn_road, center, Quaternion.Euler(0, 90, 0));
                    }
                    else if (select_road.name == "Turn_180")
                    {
                        center.x += 2;
                        center.y += 0.5f;
                        center.z -= 4;
                        Instantiate(turn_road, center, Quaternion.Euler(0, 180, 0));
                    }
                    else if (select_road.name == "Turn_270")
                    {
                        center.x -= 4;
                        center.y += 0.5f;
                        center.z -= 2;
                        Instantiate(turn_road, center, Quaternion.Euler(0, 270, 0));
                    }
                }
                else
                    Debug.Log("선택된 길이 없거나 이미 길이 설치되어 있습니다.");
            }
        }

        // 우클릭 : 길 제거
        if (Input.GetMouseButtonDown(1))
        {
            // ray를 통해 바닥 확인
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 충돌한 오브젝트의 이름 확인
                string objectname = hit.collider.gameObject.name;
                Debug.Log(hit.collider.gameObject.name);

                if (objectname.Contains(targetWord_1) || objectname.Contains(targetWord_2))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    public void RoadSelect() // 버튼으로 설치할 길 선택
    {
        // 어떤 버튼을 눌렀는지 확인
        GameObject checkroad = EventSystem.current.currentSelectedGameObject;
        select_road = checkroad;
        Button roadButton = checkroad.GetComponent<Button>();

        // 녹색
        ColorBlock colorblock = roadButton.colors;
        colorblock.normalColor = new Color(0f, 1f, 0f, 1f);
        colorblock.selectedColor = new Color(0f, 1f, 0f, 1f);

        // 흰색
        ColorBlock normalblock = roadButton.colors;
        normalblock.normalColor = new Color(1f, 1f, 1f, 1f);
        normalblock.selectedColor = new Color(1f, 1f, 1f, 1f);

        // 버튼 색깔 초기화
        for (int i = 0; i < road_button.Length; i++)
        {
            road_button[i].colors = normalblock;
        }

        // 선택된 버튼을 초록색으로
        if (checkroad.name == "Straight")
        {
            roadButton.colors = colorblock;
        }

        if (checkroad.name == "Straight_90")
        {
            roadButton.colors = colorblock;
        }

        if (checkroad.name == "Turn")
        {
            roadButton.colors = colorblock;
        }

        if (checkroad.name == "Turn_90")
        {
            roadButton.colors = colorblock;
        }

        if (checkroad.name == "Turn_180")
        {
            roadButton.colors = colorblock;
        }

        if (checkroad.name == "Turn_270")
        {
            roadButton.colors = colorblock;
        }
    }
}
