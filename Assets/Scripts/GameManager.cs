using Cinemachine;
using UnityEngine.AI;

public class GameManager : Singleton<GameManager>
{
     
    public CharacterStats playerStats;

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
            freeLook.Follow = playerStats.transform.GetChild(3);
            freeLook.LookAt = playerStats.transform.GetChild(3);
        } 
    }

    public bool IsPlayerAlive()
    {
        return playerStats.CurrentHealth != 0;
    }
}
