using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarfaceController : MonoBehaviour
{
    // test

    public Transform target;
    private ScarfaceAnimator scarfaceAnimator;
    private ScarfaceMovement scarfaceMovement;
    private bool isRoaring;

    private void Start()
    {
        scarfaceAnimator = GetComponent<ScarfaceAnimator>();
        scarfaceMovement = GetComponent<ScarfaceMovement>();
        isRoaring = false;
    }

    private void Update()
    {
        if (isRoaring)
            return;

        scarfaceMovement.HandleMovement(target);

        scarfaceAnimator.UpdateVelocity(scarfaceMovement.Velocity());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            var x = Random.Range(0, 25);
            var z = Random.Range(0, 25);

            target.position = new Vector3(x, target.position.y, z);

            if (Random.Range(0f, 1f) < 0.5f)
            {
                isRoaring = true;
                scarfaceAnimator.UpdateRoaring(Random.Range(0, 3));
                StartCoroutine(Roar());
            }
        }
    }

    private IEnumerator Roar()
    {
        yield return new WaitForSeconds(4.15f);
        isRoaring = false;
    }
}