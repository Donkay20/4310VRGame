using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsules : MonoBehaviour
{
    [field: SerializeField]
    [field: Tooltip("if this capsule is started (activated)")]
    public bool isStarted { get; protected set; } = false;
    public bool isRisingComplete { get; private set; } = false;
    public bool isDescendingComplete { get; private set; } = false;

    [SerializeField] private float riseDistance = 2.5f; // Amount to rise up
    [SerializeField] private float riseDurationSecs = 5f; // Time in seconds to complete the movement
    private Vector3 originalPosition;
    private Coroutine riseCoroutine;
    private Coroutine descendCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;

        if (isStarted)
        {
            StartMovingUp();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted && !isRisingComplete && riseCoroutine == null)
        {
            StartMovingUp();
        }

        if (!isStarted && isRisingComplete && !isDescendingComplete && descendCoroutine == null)
        {
            StartMovingDown();
        }
    }

    private void StartMovingUp()
    {
        riseCoroutine = StartCoroutine(RiseUp(riseDistance, riseDurationSecs));
    }

    private void StartMovingDown()
    {
        descendCoroutine = StartCoroutine(Descend(riseDistance, riseDurationSecs));
    }

    public void ActivateCapsule()
    {
        isStarted = true;
    }

    private IEnumerator RiseUp(float distance, float time)
    {
        isRisingComplete = false;
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, distance, 0);

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
        isRisingComplete = true;
        riseCoroutine = null;
    }

    private IEnumerator Descend(float distance, float time)
    {
        isDescendingComplete = false;
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = originalPosition;

        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPosition;
        isDescendingComplete = true;
        isRisingComplete = false;
        descendCoroutine = null;
    }
}
