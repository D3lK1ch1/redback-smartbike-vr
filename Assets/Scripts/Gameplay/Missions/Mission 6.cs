using System.Collections;
using TMPro;
using UnityEngine;

public class Mission6 : MonoBehaviour
{
    public string missionName; // Defines the mission goal to be displayed to the user
    public TextMeshProUGUI missionNameText; // References the UI text that displays the mission goal
    public TextMeshProUGUI timerText; // References the UI text that displays the remaining time
    public TextMeshProUGUI pointsText; // References the UI text that displays the current points
    public TextMeshProUGUI missionStatusText; // References the UI text that displays the mission status (e.g., "Mission Complete!")

    private int points;
    private float remainingTime = 30f;
    private bool missionCompletion;

    void Awake()
    {
        // Declares initial state of the mission
        missionCompletion = false;
        points = 0;

        // Sets UI to default
        UpdateUI();
        StartCoroutine(CountdownTimer());
    }

    void Update()
    {
        if (remainingTime <= 0f && !missionCompletion)
        {
            // Time's up and mission ends
            missionCompletion = true;
            EndMission();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the tag on the triggered collision is "GoldStar" or "RedStar"
        if (other.CompareTag("1"))
        {
            CollectItem(other.gameObject, 1, 5f);
        }
        else if (other.CompareTag("2"))
        {
            CollectItem(other.gameObject, 3, 10f);
        }
    }

    // Handles what to do with the item once collision is triggered with it
    private void CollectItem(GameObject itemCollectable, int pointsEarned, float timeAdded)
    {
        Debug.Log(itemCollectable.name + " collected");

        // Destroy collected object from the scene
        Destroy(itemCollectable);
        points += pointsEarned;
        remainingTime += timeAdded;

        // Update new points and time
        UpdateUI();
    }

    // Changes the UI for the player based on the defined mission 
    private void UpdateUI()
    {
        missionNameText.text = "Mission: " + missionName; // Tells player the goal
        timerText.text = "Time: " + Mathf.Ceil(remainingTime) + "s"; // Display remaining time
        pointsText.text = "Points: " + points; // Display current points
    }

    private IEnumerator CountdownTimer()
    {
        while (remainingTime > 0 && !missionCompletion)
        {
            yield return new WaitForSeconds(1f);
            remainingTime--;
            UpdateUI();
        }
    }

    private void EndMission()
    {
        // Display final points and mission completion message
        missionStatusText.text = "Mission Complete! Final Points: " + points;
        StartCoroutine(HideMissionStatusText());

        // Optionally, hide other UI elements
        missionNameText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        pointsText.gameObject.SetActive(false);


    }

    private IEnumerator HideMissionStatusText()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);
        // Hide the mission status text
        missionStatusText.gameObject.SetActive(false);
    }



}
