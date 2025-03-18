using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GhostEnemy : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public float stoppingDistance = 2f;  // Distance at which the ghost stops following
    public float proximityDistance = 5f;
    private NavMeshAgent agent;
    [SerializeField] private bool isPlayerLooking = false;
    public Camera playerCamera;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 currentLocation = transform.position;
        if (isPlayerLooking)  // Only move if the player isn't looking at the ghost
        {
            agent.SetDestination(currentLocation);  // Stop moving
        }
        else
        {
            agent.SetDestination(player.position);
            CheckProximityAndChangeScene();
        }
        
        CheckPlayerLooking();
    }

    void CheckPlayerLooking()
    {
        Vector3 directionToPlayer = transform.position - playerCamera.transform.position;
        float angle = Vector3.Angle(directionToPlayer, playerCamera.transform.forward);

        // Check if the ghost is within the player's view (within 60 degrees)
        if (angle < 90f && IsGhostVisibleToPlayer())
        {
            isPlayerLooking = true;
        }
        else
        {
            isPlayerLooking = false;
        }
    }

    bool IsGhostVisibleToPlayer()
    {
        // Use a raycast to check if there's a clear line of sight between the camera and the ghost
        Ray ray = new Ray(playerCamera.transform.position, transform.position - playerCamera.transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)
            {
                return true;  // Ghost is visible to the player
            }
        }
        return false;
    }
    void CheckProximityAndChangeScene()
    {
        // Calculate the distance between the ghost and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the ghost is close and the player is not looking, change the scene
        if (distanceToPlayer <= proximityDistance && !isPlayerLooking)
        {
            ChangeScene();
        }
    }

    void ChangeScene()
    {
        // Load the specified next scene
        SceneManager.LoadScene("You Lose");
    }
}
