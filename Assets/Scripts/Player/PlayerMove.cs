using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3;
    [SerializeField] private float sideSpeed = 3;
    [SerializeField] private Transform muzzleTransform;
    [SerializeField] private ParticleSystem[] gunFireParticles;
    private bool isBarrierLeftReached = false;
    private bool isBarrierRightReached = false;

    private void Awake()
    {
        InputController.LeftMouseButtonClickedEvent += FireGun;
        InputController.LeftMouseButtonReleasedEvent += CeaseFire;
    }

    private void FireGun()
    {
        for (int i = 0; i < gunFireParticles.Length; i++)
            gunFireParticles[i].Play();
    }

    private void CeaseFire()
    {
        for (int i = 0; i < gunFireParticles.Length; i++)
            gunFireParticles[i].Stop();
    }

    private void Update()
    {
        ForwardMovement();
        SideToSideMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.BarrierLeft)) isBarrierLeftReached = true;
        if (other.CompareTag(Tags.BarrierRight)) isBarrierRightReached = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.BarrierLeft)) isBarrierLeftReached = false;
        if (other.CompareTag(Tags.BarrierRight)) isBarrierRightReached = false;
    }

    private void SideToSideMovement()
    {
        if (Input.GetKey(KeyCode.A) && !isBarrierLeftReached)
            transform.Translate(Time.deltaTime * sideSpeed * -Vector3.right, Space.World);
        else if (Input.GetKey(KeyCode.D) && !isBarrierRightReached)
            transform.Translate(Time.deltaTime * sideSpeed * Vector3.right, Space.World);
    }

    private void ForwardMovement() => transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);
}
