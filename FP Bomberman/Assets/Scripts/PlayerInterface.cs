using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInterface : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 7f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundcheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    public float firingRange = 2f;
    public int numberOfBombs;
    public int maxNumberOfBombs;
    public int temp;
    public List<GameObject> numberOfBombsPlaced = new List<GameObject>();

    // Bomb
    public GameObject Bomb;
    public Transform player;
    private void Start()
    {
        temp = maxNumberOfBombs;
        numberOfBombs = maxNumberOfBombs;
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Walk(x,z);

        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.E) && numberOfBombsPlaced.Count < maxNumberOfBombs )
        {
            PlaceBomb();
        }

        CheckMaxBombCount();

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public float GetFiringRange()
    {
        return firingRange;
    }
    public int GetBombCount()
    {
        return numberOfBombs;
    }

    public void Walk(float x, float z)
    {
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded & velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void PlaceBomb()
    {
        GameObject bomb = Instantiate(Bomb, GetTilePosition(), Quaternion.Euler(-90, 0, 0));
        //bomb.GetComponent<SphereCollider>().isTrigger = false;
        numberOfBombsPlaced.Add(bomb);
        numberOfBombs--;
    }

    private void CheckMaxBombCount()
    {
        if (temp < maxNumberOfBombs)
        {
            temp = maxNumberOfBombs;
            numberOfBombs++;
        }
    }

    public void TakeDamage(GameObject player)
    {
        Destroy(player);
    }

    public Vector3 GetTilePosition()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit);
        var tilePos = hit.collider.transform.position;
        tilePos.y += 1;
        return tilePos;
    }
}
