using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movSpeed;
    private bool isMoving;
    private Vector2 speed;

    private Animator animator;

    public LayerMask solidObjectsLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            speed.x = Input.GetAxisRaw("Horizontal");
            speed.y = Input.GetAxisRaw("Vertical");

            if (speed.x != 0) speed.y = 0;

            if (speed != Vector2.zero)
            {

                animator.SetFloat("moveX", speed.x);
                animator.SetFloat("moveY", speed.y);

                Debug.Log("This is speedX" + speed.x);
                Debug.Log("This is speedY" + speed.y);

                var targetPos = transform.position;
                targetPos.x += speed.x;
                targetPos.y += speed.y;

                if(isWalkable(targetPos))

                StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);
    }
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos -transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, movSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool isWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }

}
