using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject bullets;
    public Transform shootPoint;
    public float speed;

    Rigidbody2D body;
    Vector2 velocity;

    public static event System.Action OnLoseDied;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0).normalized;
        velocity = moveInput * speed;

        if (Input.GetKeyDown(KeyCode.Space)) Shoot();

        UpdatePosition();
    }

    void UpdatePosition()
    {
        body.position += velocity * Time.deltaTime;
    }

    void Shoot()
    {
        BulletScript bullet = Instantiate(bullets, shootPoint.position, shootPoint.rotation).GetComponent<BulletScript>();
        bullet.direction = Vector2.up;
    }

    private void OnDestroy()
    {
        OnLoseDied();
    }
}
