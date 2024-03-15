using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeView : MonoBehaviour, IInteractable
{
    public Vilige.viewState state;
    Vilige vilige;
    private void Start()
    {
        vilige = FindObjectOfType<Vilige>();
    }

    public void Interact()
    {
        vilige.changeView(transform.position, 3, state);
    }

}
