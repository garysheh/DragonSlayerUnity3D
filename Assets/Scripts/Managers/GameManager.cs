using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : Singleton<GameManager>
{
     
    public CharacterStats playerStats;
    public Camera mainCamera;

    private CinemachineFreeLook freeLook;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void Update()
    {
        if (!IsPlayerAlive())
        {
            NavMeshAgent agent = playerStats.gameObject.GetComponent<NavMeshAgent>();
            agent.isStopped = true;
        }
    }

    public void RigisterPlayer(CharacterStats player)
    {
        playerStats = player;

        freeLook = FindObjectOfType<CinemachineFreeLook>();
        if(freeLook != null)
        {
            freeLook.Follow = playerStats.transform.GetChild(2);
            freeLook.LookAt = playerStats.transform.GetChild(2);
        } 
    }

    public bool IsPlayerAlive()
    {
        if (playerStats != null && playerStats.CurrentHealth != 0)
            return true;
        else if (playerStats.CurrentHealth == 0)
            return false;
        return false;
    }

    public void SelectWizard()
    {
        GameObject.Destroy(GameObject.Find("DogPolyart"));
        GameObject.Find("PolyArtWizardStandardMat").GetComponent<PlayerController>().enabled = true;
        TurnOnCineBrain();
    }

    public void SelectHero()
    {
        GameObject.Destroy(GameObject.Find("PolyArtWizardStandardMat"));
        GameObject.Find("DogPolyart").GetComponent<PlayerController>().enabled = true;
        TurnOnCineBrain();
    }

    void TurnOnCineBrain()
    {
        mainCamera.GetComponent<CinemachineBrain>().enabled = true;
    }



    //public Transform GetEntrance()
    //{
    //    foreach (var item in FindObjectOfType<TransitionDestination>())
    //    {
    //        if (item.destinationTag == TransitionDestination.DestinationTag.TownFrontEntry)
    //        {
    //            return item.transform;
    //        }
    //    }
    //    return null;
    //}
}
