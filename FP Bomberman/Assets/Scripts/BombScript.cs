using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private GameObject player;
    private PlayerInterface playerInterface;

    [SerializeField]
    private GameObject explosionFire;
    private AudioSource explosionSound;

    [SerializeField,Range(0,10)]
    private float fireDuration;
    private bool isDetonated = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInterface = player.GetComponent<PlayerInterface>();

        explosionSound = GameObject.FindGameObjectWithTag("Explosion").GetComponent<AudioSource>();
        StartCoroutine("BombTimer");
    }

    IEnumerator BombTimer() // Starts bomb timer and calls detonation function
    {

        //if (transform.position == playerInterface.GetTilePosition())
        //{
        //    this.GetComponent<SphereCollider>().isTrigger = true;
        //    Debug.Log("walkable bomb");
        //}
        //else if (transform.position != playerInterface.GetTilePosition())
        //{
        //    this.GetComponent<SphereCollider>().isTrigger = false;
        //}
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<SphereCollider>().isTrigger = false; // After 0.5 second the bomb turns solid
        yield return new WaitForSeconds(2);
        DetonateBomb();
             
    }

    public void DetonateBomb()
    {
        if (!isDetonated) // Prevents bombs into entering an infinite loop of detonation
        {
            isDetonated = true;
            for (var i = 0f; i < Mathf.PI * 4; i += Mathf.PI / 8) // Shoots raycast in all four directions
            {
                var direction = new Vector3(Mathf.Cos(i), transform.position.y, Mathf.Sin(i));
                ShootRaycast(transform.position, direction, playerInterface.GetFiringRange(), i);
                explosionSound.Play();
            }
        
            IsDestroyed();
            Destroy(gameObject);
        }
    }

    public void ShootRaycast(Vector3 Position, Vector3 shootingSide, float shootingRange, float I)
    {
        RaycastHit hit;
        for (var fr = 1f; fr <= shootingRange; fr++) // checks for hit along the length of a single ray(depends on shootingRange)
        {
            var firePos = new Vector3(transform.position.x + Mathf.Cos(I) * fr, transform.position.y, transform.position.z + Mathf.Sin(I) * fr);
            var fireDir = new Vector3(shootingSide.x * 90, 90, shootingSide.z * 90);

            if (Physics.Raycast(Position, shootingSide, out hit, shootingRange)) //Checks for hits
            {
                //Debug.DrawLine(transform.position, hit.point, Color.red, 10f);
                if (hit.collider != null && !hit.collider.gameObject.CompareTag("Indestructable")) 
                {
                    GameObject fire = Instantiate(explosionFire, firePos, Quaternion.Euler(fireDir)); 
                    Destroy(fire, fireDuration);
                    WallDestruction(hit);
                    return; //Stops the loop after a destructible wall is destroyed
                }
            }
            else // If no hits instantiates a fire
            {
                GameObject fire = Instantiate(explosionFire, firePos, Quaternion.Euler(fireDir));
                Destroy(fire, fireDuration);
            }
        }    
    }

    private void WallDestruction(RaycastHit hit)
    {
        GameObject hitObject = hit.collider.gameObject;
        if (hitObject.CompareTag("Destructable"))
        {
            Destroy(hitObject);
        }
        if (hitObject.CompareTag("Player"))
        {
            playerInterface.TakeDamage(hitObject);
        }
        if (hitObject.CompareTag("Bomb"))
        {
            //Debug.Log("BOOM");
            hitObject.GetComponent<BombScript>().DetonateBomb();
        }
    }

    private void IsDestroyed()
    {
        playerInterface.numberOfBombs++;
        playerInterface.numberOfBombsPlaced.Remove(gameObject);
    }




}

