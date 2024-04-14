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
    public bool isAvailable { get; private set; } = true;

    [SerializeField] private float riseDistance = 2.5f; // Amount to rise up
    [SerializeField] private float riseDurationSecs = 5f; // Time in seconds to complete the movement
    private Vector3 originalPosition;
    private Coroutine riseCoroutine;
    private Coroutine descendCoroutine;

    public Pattern currentPattern { get; private set; }
    private GameObject currentModel;
    private Transform complexModels;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        complexModels = this.transform.GetChild(0);
        complexModels.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isStarted)
        {
            isAvailable = false;
        }
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

    public void ActivateCapsule(Pattern pattern)
    {
        currentPattern = pattern;
        if (currentPattern.model != null)
        {
            // Destroy the previous model if it exists
            if (currentModel != null)
            {
                Destroy(currentModel);
            }

            currentModel = Instantiate(currentPattern.model, transform.position, Quaternion.identity);
            currentModel.transform.SetParent(transform);
        }
        isStarted = true;
        complexModels.gameObject.SetActive(true);
    }
    public void DeactivateCapsule()
    {
        isStarted = false;
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
        isDescendingComplete = false;
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
        isAvailable = true;
        complexModels.gameObject.SetActive(false);
        if (currentPattern != null)
        {
            CapsulesController.Instance.ResetPattern(currentPattern);
            currentPattern = null;
        }
        if (currentModel != null)
        {
            Destroy(currentModel);
            currentModel = null;
        }
        descendCoroutine = null;
    }
}
