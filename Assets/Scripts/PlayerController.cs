using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private Movement2D movement2D;
    public GameObject attackBounds;
    private KeyCode item1 = KeyCode.Alpha1;
    private KeyCode item2 = KeyCode.Alpha2;
    private KeyCode item3 = KeyCode.Alpha3;
    private KeyCode item4 = KeyCode.Alpha4;
    private KeyCode skill1 = KeyCode.Mouse0;
    private KeyCode skill2 = KeyCode.Mouse1;
    private KeyCode skill3 = KeyCode.Q;
    private KeyCode skill4 = KeyCode.E;

    private void Awake()
    {
        player = GetComponent<Player>();
        movement2D = GetComponent<Movement2D>();
    }

    private void Update()
    {
        Move();
        Rotate();

        if (Input.GetKeyDown(item1))
            player.Equip(0);
        else if (Input.GetKeyDown(item2))
            player.Equip(1);
        else if (Input.GetKeyDown(item3))
            player.Equip(2);
        else if (Input.GetKeyDown(item4))
            player.Equip(3);

        if (Input.GetKeyDown(skill1))
        {
            player.ChangeSkill();
            Attack(player.skills[0]);
        }
        else if (Input.GetKeyDown(skill2))
        {
            player.ChangeSkill();
            Attack(player.skills[1]);
        }
        else if (Input.GetKeyDown(skill3))
        {
            player.ChangeSkill();
            Attack(player.skills[2]);
        }
        else if (Input.GetKeyDown(skill4))
        {
            player.ChangeSkill();
            Attack(player.skills[3]);
        }
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

    private void Attack(Skill skill)
    {
        Vector2 newPos = new Vector2(0, 1);
        GameObject clone = Instantiate(attackBounds, transform);
        clone.transform.localPosition = newPos;
        HitDetection hitDetection = clone.GetComponent<HitDetection>();
        hitDetection.attacker = gameObject;
        hitDetection.skill = skill;
        hitDetection.Init();
    }

}
