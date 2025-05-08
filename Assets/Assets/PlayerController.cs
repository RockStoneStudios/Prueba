using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform rifleStart;
    [SerializeField] private TextMeshProUGUI HpText;

    [SerializeField] private GameObject GameOver;
    [SerializeField] private GameObject Victory;

    public float health = 100;
    private CharacterController controller;
    private Vector3 moveDirection;
    public float moveSpeed = 5f;
    public float gravity = 9.81f;
    public float jumpHeight = 1.5f;
    private float yVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        ChangeHealth(0);
    }

    void Update()
    {
        // Movimiento
         float horizontal = Input.GetAxis("Horizontal");
         float vertical = Input.GetAxis("Vertical");
          Vector3 move = transform.right * horizontal + transform.forward * vertical;

           controller.Move(move * moveSpeed * Time.deltaTime);
        // Gravedad
        if (controller.isGrounded)
        {
            yVelocity = -1f;
            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = Mathf.Sqrt(jumpHeight * 2f * gravity);
            }
        }
        else
        {
            yVelocity -= gravity * Time.deltaTime;
        }

        move.y = yVelocity;
        controller.Move(move * Time.deltaTime);

        // Disparo
        if (Input.GetMouseButtonDown(0))
        {
            GameObject buf = Instantiate(bullet);
            buf.transform.position = rifleStart.position;
            buf.GetComponent<Bullet>().setDirection(transform.forward);
            buf.transform.rotation = transform.rotation;
        }

        // Golpe cuerpo a cuerpo (clic derecho)
        if (Input.GetMouseButtonDown(1))
        {
            Collider[] tar = Physics.OverlapSphere(transform.position, 2);
            foreach (var item in tar)
            {
                if (item.CompareTag("Enemy"))
                {
                    Destroy(item.gameObject);
                }
            }
        }

        // Detección de objetos cercanos
        Collider[] targets = Physics.OverlapSphere(transform.position, 3);
        foreach (var item in targets)
        {
            if (item.CompareTag("Heal"))
            {
                ChangeHealth(50);
                Destroy(item.gameObject);
            }
            if (item.CompareTag("Finish"))
            {
                Win();
            }
            if (item.CompareTag("Enemy"))
            {
                Lost();
            }
        }
    }

    public void ChangeHealth(int hp)
    {
        health += hp;
        if (health > 100)
            health = 100;
        else if (health <= 0)
            Lost();

        HpText.text = health.ToString();
    }

    public void Win()
    {
        Victory.SetActive(true);
        Destroy(GetComponent<PlayerLook>());
        Cursor.lockState = CursorLockMode.None;
    }

    public void Lost()
    {
        GameOver.SetActive(true);
        Destroy(GetComponent<PlayerLook>());
        Cursor.lockState = CursorLockMode.None;
    }
}
