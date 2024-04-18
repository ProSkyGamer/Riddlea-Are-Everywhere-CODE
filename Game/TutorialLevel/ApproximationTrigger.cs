using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproximationTrigger : MonoBehaviour
{
    [SerializeField] protected float interactableDistance = 1f;
    [SerializeField] private bool isInteractDistanceShow = true;

    //DELETE
    [Header("Temporary")]
    [SerializeField] protected BoxCollider2D collision;
    private bool isTriggered = false;
    public event EventHandler OnApproximationTrigger;

    protected virtual void Awake()
    {
        collision = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        if (!isTriggered)
        {
            Vector3 castPosition = transform.position + (Vector3)collision.offset;
            Vector2 castCubeLenght = collision.size + new Vector2(interactableDistance, interactableDistance);
            float cubeRotation = 0f;
            Vector2 cubeDirection = Vector2.up;
            float distance = 0f;

            RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(castPosition, castCubeLenght,
                cubeRotation, cubeDirection, distance);
            foreach (RaycastHit2D raycastHit in raycastHits)
            {
                if (raycastHit)
                    if (raycastHit.collider.gameObject.TryGetComponent<PlayerController>(out PlayerController interactedPlayer))
                    {
                        if (OnApproximationTrigger != null)
                        {
                            isTriggered = true;
                            OnApproximationTrigger?.Invoke(this, EventArgs.Empty);
                        }
                            break;
                    }
            }
        }

    }


    protected virtual void OnDrawGizmosSelected()
    {
        if (isInteractDistanceShow)
        {
            Gizmos.color = Color.blue;
            Vector3 castPosition = collision.transform.position + (Vector3)collision.offset;
            Vector2 castCubeLenght = collision.size + new Vector2(interactableDistance, interactableDistance);
            Gizmos.DrawCube(castPosition, castCubeLenght);
        }
    }
}
