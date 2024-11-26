using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMount : MonoBehaviour
{
    public GameObject player; // 플레이어 오브젝트
    private bool nearMount = false; // 탈것 근처에 있는지 여부
    private GameObject mount;       // 탈것 오브젝트

    void Update()
    {
        if (nearMount && Input.GetKeyDown(KeyCode.F))
        {
            Mount();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mount")) // 탈것 근처에 접근
        {
            nearMount = true;
            mount = other.gameObject;
            Debug.Log("Press F to mount.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mount")) // 탈것에서 멀어짐
        {
            nearMount = false;
            mount = null;
        }
    }

    void Mount()
    {
        if (mount != null)
        {
            player.transform.SetParent(mount.transform); // 플레이어를 탈것에 연결
            player.transform.localPosition = Vector3.zero; // 탈것의 중심으로 위치 설정
            player.GetComponent<FirstPersonController>().enabled = false; // 플레이어 이동 비활성화
            Debug.Log("Mounted!");
        }
    }
}
