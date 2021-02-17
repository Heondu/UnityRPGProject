using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private Movement2D movement2D;
    public GameObject attackBounds;
    private BoxCollider2D collider;

    private void Awake()
    {
        player = GetComponent<Player>();
        movement2D = GetComponent<Movement2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Move();
        Rotate();
        if (Input.GetMouseButtonDown(0))
            Attack();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        movement2D.MoveTo(new Vector2(x, y).normalized);
    }

    private void Rotate()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void Attack()
    {
        Vector2 newPos = new Vector2(0, collider.bounds.max.y);
        GameObject clone = Instantiate(attackBounds, transform);
        clone.transform.localPosition = newPos;

        SkillStatus.DamageCalc(player.status, player, player.skill.skillStatus);
    }

}
