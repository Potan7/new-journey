using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class InteractManager : MonoBehaviour
{
    static public bool canInteract { get; set; }

    private void Awake()
    {
        canInteract = true;
    }

    // ���콺 Ŭ���� ���� raycast �߻��� IInteractable �������̽��� ���� ������Ʈ�� ������ Interact() ȣ��
    private void Update()
    {
        if (canInteract && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray2D ray = new Ray2D(mousePos, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }
        }
    }
}
