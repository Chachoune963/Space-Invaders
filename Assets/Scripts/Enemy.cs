using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public GameObject bullets;
    public Transform shootPoint;
    bool bottom;
    public float lineLength;

    Rigidbody2D body;
    Vector2 direction;

    public static int ennemiesNum = 0;
    public static event System.Action OnWin;
    public static event System.Action NextLine;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        direction = Vector2.right;
        NextLine += GoNextLine;
        ennemiesNum++;
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        if ((body.position.x >= Camera.main.aspect * Camera.main.orthographicSize - transform.localScale.x && direction == Vector2.right)
            || (body.position.x <= Camera.main.aspect * -Camera.main.orthographicSize + transform.localScale.x && direction == Vector2.left))
        {
            if (NextLine != null)
            {
                NextLine();
            }
        }

    }

    IEnumerator Move()
    {
        while (true)
        {
            body.position += direction * speed * .5f;
            if (CheckIfBottom() && Random.value < .25f)
            {
                Shoot();
            }
            yield return new WaitForSeconds(.5f);
        }
    }

    void Shoot()
    {
        BulletScript bullet = Instantiate(bullets, shootPoint.position, shootPoint.rotation).GetComponent<BulletScript>();
        bullet.direction = Vector2.down;
    }

    bool CheckIfBottom()
    {
        Collider2D[] ennemiesUnder = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y - Camera.main.orthographicSize/2), new Vector2(.75f, Camera.main.orthographicSize), 0);
        foreach (Collider2D entity in ennemiesUnder)
        {
            if (entity.gameObject != gameObject && entity.tag == "Ennemy") return false;
        }
        return true;
    }

    void GoNextLine()
    {
        direction = -direction;
        body.MovePosition(new Vector2(body.position.x, body.position.y - lineLength));
    }

    private void OnDestroy()
    {
        ennemiesNum -= 1;
        if (ennemiesNum == 0) OnWin();
        NextLine -= GoNextLine;
    }
}
