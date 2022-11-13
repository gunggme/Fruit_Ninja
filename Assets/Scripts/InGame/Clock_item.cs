    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock_item : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody clockRigidbody;
    private Collider clockCollider;
    private ParticleSystem clockEffect;

    public int points = 0;

    private void Awake()
    {
        clockRigidbody = GetComponent<Rigidbody>();
        clockCollider = GetComponent<Collider>();
        clockEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        FindObjectOfType<GameManager>().IncreaseScore(points);

        // Disable the whole fruit
        clockCollider.enabled = false;
        whole.SetActive(false);

        // Enable the sliced fruit
        sliced.SetActive(true);
        clockEffect.Play();

        // Rotate based on the slice angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        // Add a force to each slice based on the blade direction
        foreach (Rigidbody slice in slices)
        {
            slice.velocity = clockRigidbody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.time += 5;



            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}
