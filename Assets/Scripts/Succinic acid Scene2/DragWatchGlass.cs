using System.Collections;
using UnityEngine;

public class DragWatchGlass : MonoBehaviour
{
    private Vector3 offset;
    public StirrerBehaviour stirrerBehaviour; // Add this line to reference the StirrerBehaviour script
    private bool isDragging = false;
    private bool hasRotated = false; // Flag to track if rotation has occurred

    // Target coordinates for the watch glass
    private Vector3 targetCoordinates = new Vector3(-7.53f, 0.090f, 0f);
    private float proximityThreshold = 0.5f; // Adjust this value as needed

    // Reference to the child object
    public GameObject succinicAcidPowder;

    // Reference to the particle system
    public new ParticleSystem particleSystem;

    // Reference to the second particle system
    public ParticleSystem secondParticleSystem;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        // Calculate degrees per second based on total rotation time
        float totalRotationTime = 2.0f; // Example: 2 seconds
        float degreesPerSecond = 180f / totalRotationTime;
    }
    // Add a new private variable to track if the particle system has been played
    private bool hasParticleSystemPlayed = false;

    void Update()
    {

    if (!hasParticleSystemPlayed && succinicAcidPowder != null && !succinicAcidPowder.activeInHierarchy && !particleSystem.isPlaying)
    {
        particleSystem.Play();
        hasParticleSystemPlayed = true; // Set the flag to true after playing the particle system
        StartCoroutine(StartSecondParticleSystemWhenFirstFinishes());
    }
    }

    IEnumerator StartSecondParticleSystemWhenFirstFinishes()
{
    // Wait until the first particle system has stopped playing
    while (particleSystem.isPlaying)
    {
        yield return null; // Wait for the next frame
    }

    // Now start the second particle system
    secondParticleSystem.Play();
}

public void StopSecondParticleSystemAfter4Seconds()
    {
        StartCoroutine(PauseSecondParticleSystemAfterSeconds(0.1f));
    }

IEnumerator PauseSecondParticleSystemAfterSeconds(float seconds)
{
    yield return new WaitForSeconds(seconds);

    // Now stop the second particle system only if the stirrer is animating
        if (stirrerBehaviour.isAnimating)
            {
                secondParticleSystem.Stop();
            }
}

    private void OnMouseDown()
    {
        // Calculate the offset between the touch position and the object's position
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            // Update the object's position based on touch input
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

            // Check if the watch glass is close to the target coordinates
            float distanceToTarget = Vector3.Distance(transform.position, targetCoordinates);
            if (distanceToTarget < proximityThreshold && !hasRotated)
            {
                // Snap the watch glass to the target coordinates
                transform.position = targetCoordinates;

                // Rotate the watch glass by 180 degrees around the Z-axis
                transform.Rotate(Vector3.forward, 180f);

                // Set the flag to prevent further rotation
                hasRotated = true;

                // Make the child object disappear
                if (succinicAcidPowder != null)
                {
                    succinicAcidPowder.SetActive(false);

                    // Activate the particle system and set its position
                    if (particleSystem != null)
                    {
                        particleSystem.gameObject.SetActive(true);
                        particleSystem.transform.position = succinicAcidPowder.transform.position;
                    }
                }
            }
        }
    }
}
