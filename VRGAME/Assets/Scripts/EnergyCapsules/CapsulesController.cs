using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CapsulesController : MonoBehaviour
{
    public static CapsulesController Instance;
    [SerializeField] private List<Pattern> patterns;
    [SerializeField] private List<Capsules> capsules = new List<Capsules>();
    [SerializeField] private float activationCooldown = 7.5f;
    [SerializeField] private GunShoot laserEnergyScript;
    [SerializeField] private PlayerStats playerStatsScript;
    private float cooldownTimer = 0f;

    private void Awake()
    {
        Instance = this;

        capsules = new List<Capsules>(GetComponentsInChildren<Capsules>());
    }

    public Pattern GetUniquePattern()
    {
        List<Pattern> availablePatterns = patterns.Where(p => !p.isDisplayed).ToList();
        if (availablePatterns.Count == 0)
        {
            return default;
        }
        int index = UnityEngine.Random.Range(0, availablePatterns.Count);
        availablePatterns[index].isDisplayed = true;
        return availablePatterns[index];
    }

    // Call this method to randomly pick a capsule and assign a unique pattern to it
    public void ActivateOneRandomCapsule()
    {
        List<Capsules> availableCapsules = capsules.Where(c => c.isAvailable).ToList();
        if (availableCapsules.Count == 0)
        {
            Debug.LogError("No capsules to assign patterns to!");
            return;
        }

        Capsules capsule = availableCapsules[UnityEngine.Random.Range(0, capsules.Count)];
        Pattern pattern = GetUniquePattern();
        if (pattern == null)
        {
            Debug.LogError("No unique pattern available to assign!");
            return;
        }
        capsule.ActivateCapsule(pattern);
    }

    public void ResetPattern(Pattern pattern)
    {
        pattern.isDisplayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= activationCooldown)
        {
            cooldownTimer = 0f;  // Reset the cooldown timer
            ActivateOneRandomCapsule();
        }

        // if (Input.GetKeyDown("g"))
        // {
        //     ActivateOneRandomCapsule();
        // }
    }
    public void CheckPatternsInCapsules(Vector3[] inputPattern)
    {
        
        foreach (Capsules capsule in capsules)
        {
            if (capsule.isStarted && ComparePatterns(capsule.currentPattern.pattern, inputPattern))
            {
                capsule.DeactivateCapsule();
                laserEnergyScript.ChargeEnergy(25f);

                //add level up here
                playerStatsScript.LevelUp();

                break;
            }
        }
    }
    private bool ComparePatterns(Vector3[] pattern1, Vector3[] pattern2)
    {
        if (pattern1.Length != pattern2.Length)
            return false;

        for (int i = 0; i < pattern1.Length; i++)
        {
            if (!Vector3Equals(pattern1[i], pattern2[i]))
                return false;
        }
        return true;
    }

    private bool Vector3Equals(Vector3 a, Vector3 b)
    {
        return a.x == b.x && a.y == b.y && a.z == b.z;
    }
}
