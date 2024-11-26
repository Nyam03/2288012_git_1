using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMount : MonoBehaviour
{
    public GameObject player; // �÷��̾� ������Ʈ
    private bool nearMount = false; // Ż�� ��ó�� �ִ��� ����
    private GameObject mount;       // Ż�� ������Ʈ

    void Update()
    {
        if (nearMount && Input.GetKeyDown(KeyCode.F))
        {
            Mount();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mount")) // Ż�� ��ó�� ����
        {
            nearMount = true;
            mount = other.gameObject;
            Debug.Log("Press F to mount.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mount")) // Ż�Ϳ��� �־���
        {
            nearMount = false;
            mount = null;
        }
    }

    void Mount()
    {
        if (mount != null)
        {
            player.transform.SetParent(mount.transform); // �÷��̾ Ż�Ϳ� ����
            player.transform.localPosition = Vector3.zero; // Ż���� �߽����� ��ġ ����
            player.GetComponent<FirstPersonController>().enabled = false; // �÷��̾� �̵� ��Ȱ��ȭ
            Debug.Log("Mounted!");
        }
    }
}
