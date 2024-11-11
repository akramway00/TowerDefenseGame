using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Johnson : MonoBehaviour
{
    public float johnsonSpeed = 3f;
    public float stopRayon = 1f;
    public GameObject spherePrefab;
    public float sphereSpeed = 10f;
    public float sphereLaunchInterval = 2f;
    public float launchAngleDegrees = 45f;
    public float sphereLaunchHeight = 2f;

    private Animator johnsonAnimator;
    private bool isAttacking = false;
    private Coroutine sphereLaunchCoroutine;
    private GameObject tower;
    private AudioSource audioSource;
    public AudioClip sonattaque;
    public float johnsonrestant;
    


    void Start()
    {
        johnsonAnimator = GetComponent<Animator>();


        tower = GameObject.FindGameObjectWithTag("Tower");

        audioSource = gameObject.AddComponent<AudioSource>();


    }

    void Update()
    {
        



        Vector3 direction = (tower.transform.position - transform.position).normalized;
        direction.y = 0;
        float distanceToTower = Vector3.Distance(transform.position, tower.transform.position);

        if (distanceToTower <= stopRayon && !isAttacking)
        {
            johnsonAnimator.SetBool("Run", false);
            johnsonAnimator.SetTrigger("Attack");
            isAttacking = true;

            if (sphereLaunchCoroutine == null)
            {
                sphereLaunchCoroutine = StartCoroutine(LaunchSphereRoutine());
            }
        }
        else if (distanceToTower > stopRayon && isAttacking)
        {
            johnsonAnimator.SetBool("Run", true);
            isAttacking = false;

            if (sphereLaunchCoroutine != null)
            {
                StopCoroutine(sphereLaunchCoroutine);
                sphereLaunchCoroutine = null;
            }
        }

        if (!isAttacking)
        {
            float step = johnsonSpeed * Time.deltaTime;
            Vector3 newPosition = transform.position + direction * step;
            transform.position = newPosition;
        }

        transform.LookAt(new Vector3(tower.transform.position.x, transform.position.y, tower.transform.position.z));
    }

    IEnumerator LaunchSphereRoutine()
    {
        while (true)
        {
            LaunchSphere();
            yield return new WaitForSeconds(sphereLaunchInterval);
        }
    }

    void LaunchSphere()
    {
        GameObject sphere = Instantiate(spherePrefab, transform.position, Quaternion.identity);
        Rigidbody rb = sphere.GetComponent<Rigidbody>();

        Vector3 direction = (tower.transform.position - transform.position).normalized;
        sphere.transform.LookAt(tower.transform);

        float launchHeight = sphereLaunchHeight;
        Vector3 velocity = direction * sphereSpeed + Vector3.up * launchHeight;
        rb.velocity = velocity;

        audioSource.PlayOneShot(sonattaque, 0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("sphereverte"))
        {
            Destroy(gameObject);
        }
    }
}
