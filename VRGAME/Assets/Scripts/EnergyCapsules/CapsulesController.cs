using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CapsulesController : MonoBehaviour
{
    [SerializeField] private List<Pattern> patterns;
    [SerializeField] private List<Capsules> capsules;

    private void Awake()
    {
        InitializePatterns();
    }

    public Pattern GetUniquePattern()
    {
        List<Pattern> availablePatterns = patterns.Where(p => !p.isDisplayed).ToList();
        if (availablePatterns.Count == 0)
        {
            Debug.LogError("No more unique patterns available!");
            return default;
        }

        int index = UnityEngine.Random.Range(0, availablePatterns.Count);
        availablePatterns[index].isDisplayed = true;
        return availablePatterns[index];
    }

    private void InitializePatterns()
    {
        // Initialize your patterns here, possibly reading from a data file or editor settings
    }

    // Call this method to randomly pick a capsule and assign a unique pattern to it
    public void AssignPatternToCapsule()
    {
        if (capsules.Count == 0)
        {
            Debug.LogError("No capsules to assign patterns to!");
            return;
        }

        Capsules capsule = capsules[UnityEngine.Random.Range(0, capsules.Count)];
        Pattern pattern = GetUniquePattern();
        capsule.ActivateCapsule();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
