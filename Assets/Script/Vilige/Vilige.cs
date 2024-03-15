using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vilige : MonoBehaviour, IInteractable
{
    public enum viewState
    {
        VILIGE,
        INN,
        BAR,
        SHOP,
        MERCENARY,
        CHURCH,
        DUNGEON
    }

    public float speed = 0.1f;

    public viewState state { get; set; }

    private void Start()
    {
        state = viewState.VILIGE;
    }

    private void Update()
    {
        if (InteractManager.canInteract && Input.GetKeyDown(KeyCode.Escape))
        {
            if (state != viewState.VILIGE)
            {
                state = viewState.VILIGE;
                StartCoroutine(changeViewAnim(transform.position, 5));
            }
        }
    }

    public void changeView(Vector3 pos, float size, viewState _state)
    {
        state = _state;
        StartCoroutine(changeViewAnim(pos, size));
    }

    public void Interact()
    {
        if (state != viewState.VILIGE)
        {
            state = viewState.VILIGE;
            StartCoroutine(changeViewAnim(transform.position, 5));
        }
    }

    IEnumerator changeViewAnim(Vector3 Pos, float size)
    {
        InteractManager.canInteract = false;

        // 카메라를 myPos로 이동
        while (Vector3.Distance(Camera.main.transform.position, Pos + new Vector3(0, 0, -10f)) > 0.05f)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, Pos + new Vector3(0, 0, -10f), speed);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, size, speed);
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.05f);

        Camera.main.transform.position = Pos + new Vector3(0, 0, -10f);
        Camera.main.orthographicSize = size;

        InteractManager.canInteract = true;
    }
}
