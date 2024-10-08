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
    [Header("�� ����") ]
    public GameObject straight_road;
    public GameObject turn_road;
    public GameObject select_road;

    private string targetWord_1 = "Straight";
    private string targetWord_2 = "Turn";

    [Header("�� ���� ��ư")]
    public Button[] road_button;
    void Start()
    {
        // Resources ���� ���� ������Ʈ �ҷ�����
        straight_road = Resources.Load<GameObject>("Straight");
        turn_road = Resources.Load<GameObject>("Turn");
    }

    void Update()
    {
        OnMouseDown();
    }

    private void OnMouseDown()
    {
        // ��Ŭ�� : �� ��ġ
        if (Input.GetMouseButtonDown(0))
        {
            // ray�� ���� �ٴ� Ȯ��
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // ������ ������Ʈ�� �߾� ��ǥ Ȯ��
                Collider hitcollider = hit.collider;
                Vector3 center = hitcollider.bounds.center;
                Debug.Log(center);

                // �浹�� ������Ʈ�� �̸� Ȯ��
                string objectname = hit.collider.gameObject.name;

                // ���� ���� �ߴ���, �̹� ���� ��ġ�Ǿ� ���� ������ Ȯ��
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
                    Debug.Log("���õ� ���� ���ų� �̹� ���� ��ġ�Ǿ� �ֽ��ϴ�.");
            }
        }

        // ��Ŭ�� : �� ����
        if (Input.GetMouseButtonDown(1))
        {
            // ray�� ���� �ٴ� Ȯ��
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // �浹�� ������Ʈ�� �̸� Ȯ��
                string objectname = hit.collider.gameObject.name;
                Debug.Log(hit.collider.gameObject.name);

                if (objectname.Contains(targetWord_1) || objectname.Contains(targetWord_2))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    public void RoadSelect() // ��ư���� ��ġ�� �� ����
    {
        // � ��ư�� �������� Ȯ��
        GameObject checkroad = EventSystem.current.currentSelectedGameObject;
        select_road = checkroad;
        Button roadButton = checkroad.GetComponent<Button>();

        // ���
        ColorBlock colorblock = roadButton.colors;
        colorblock.normalColor = new Color(0f, 1f, 0f, 1f);
        colorblock.selectedColor = new Color(0f, 1f, 0f, 1f);

        // ���
        ColorBlock normalblock = roadButton.colors;
        normalblock.normalColor = new Color(1f, 1f, 1f, 1f);
        normalblock.selectedColor = new Color(1f, 1f, 1f, 1f);

        // ��ư ���� �ʱ�ȭ
        for (int i = 0; i < road_button.Length; i++)
        {
            road_button[i].colors = normalblock;
        }

        // ���õ� ��ư�� �ʷϻ�����
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
