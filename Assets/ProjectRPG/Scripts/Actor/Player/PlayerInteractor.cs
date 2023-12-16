using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private float interactionRange;

    public InteractableObject curruntNearInteractableObject { get; set; }

    // Update is called once per frame
    private void Update()
    {
        bool findSuccess = false;

        List<Collider> cols = Physics.OverlapSphere(transform.position, interactionRange).ToList();
        for (int i = 0; i < cols.Count; i++)
        {
            InteractableObject interactable = cols[i].GetComponent<InteractableObject>();
            Vector3 interactablePos = cols[i].transform.position;
            if (interactable != null && Vector3.Distance(transform.position, interactablePos) < interactionRange)
            {
                curruntNearInteractableObject = interactable;
                interactable.CallInteract(this);
                findSuccess = true;
                break;
            }
        }
        if (!findSuccess)
        {
            curruntNearInteractableObject = null;
        }
    }

    public void TryInteract()
    {
        curruntNearInteractableObject?.CallInteract(this);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}