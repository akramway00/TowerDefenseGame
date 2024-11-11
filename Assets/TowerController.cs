using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerController : MonoBehaviour
{
    public GameObject greenSpherePrefab;
    public float greenSphereSpeed = 10f;
    public float launchHeight = 2f;
    public LayerMask johnsonLayerMask;
    public GameObject launchPoint;
    private Camera mainCamera;
    [SerializeField] private TMP_Text points;
    public float energie;
    private AudioSource audioSource;
    public AudioClip sonDefense;
    public AudioClip sonincorect;
    private bool cheatMode = false;


    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(IncreaseNombre());
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        points.text = " " + energie;

        if (Input.GetKeyDown(KeyCode.O))
        {
            cheatMode = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (energie > 0)
            {
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, johnsonLayerMask))
                {

                    GameObject target = hit.collider.gameObject;
                    if (target.CompareTag("johnson"))
                    {
                        LaunchGreenSphere(target);
                        if (!cheatMode)
                        {
                            energie -= 10;
                        }
                    }
                }
            }
            else
            {
                audioSource.PlayOneShot(sonincorect, 0.3f);
            }
        }
    }

    void LaunchGreenSphere(GameObject target)
    {


        GameObject greenSphere = Instantiate(greenSpherePrefab, launchPoint.transform.position, Quaternion.identity);
        Rigidbody rb = greenSphere.GetComponent<Rigidbody>();

        Vector3 targetPosition = target.transform.position;
        Vector3 direction = (targetPosition - greenSphere.transform.position);
        float yOffset = direction.y;
        direction = new Vector3(direction.x, 0f, direction.z);

        float distance = direction.magnitude;
        float angleRadians = Mathf.Deg2Rad * launchHeight;
        float velocityMagnitude = (Mathf.Sqrt(distance) * Mathf.Sqrt(-Physics.gravity.y) / Mathf.Sqrt(2f * Mathf.Tan(angleRadians) - 2f * yOffset / distance));

        Vector3 velocity = direction.normalized * velocityMagnitude;
        velocity.y = velocityMagnitude * Mathf.Tan(angleRadians);

        rb.velocity = velocity;

        audioSource.PlayOneShot(sonDefense, 0.8f);

    }

    IEnumerator IncreaseNombre()
    {
        while (true)
        {
            if (energie < 100)
            {
                energie += 10;
                if (energie > 100)
                {
                    energie = 100;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
}