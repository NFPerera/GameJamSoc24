using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCState
{
    Idle,
    Wandering,
    Running,
    Attacking,
    Dead
}

public abstract class Condition
{
    public abstract bool CheckCondition();
}

public class BehaviorTreeNode
{
    public Condition condition { get; set; }
    public NPCState state { get; set; }
    public List<BehaviorTreeNode> children { get; set; } = new List<BehaviorTreeNode>();
    public System.Action action { get; set; }

    public BehaviorTreeNode(Condition condition, NPCState state, System.Action action = null)
    {
        this.condition = condition;
        this.state = state;
        this.action = action;
    }
}

public class BehaviorTree
{
    public BehaviorTreeNode root { get; set; }

    public NPCState Evaluate()
    {
        Stack<BehaviorTreeNode> stack = new Stack<BehaviorTreeNode>();
        stack.Push(root);
        BehaviorTreeNode lastValidNode = null;

        // Traverse the tree in a depth-first manner
        while (stack.Count > 0)
        {
            BehaviorTreeNode node = stack.Pop();
            if (node.condition.CheckCondition())
            {
                lastValidNode = node;
            }
            foreach (BehaviorTreeNode child in node.children)
            {
                stack.Push(child);
            }
        }

        if (lastValidNode != null)
        {
            lastValidNode.action?.Invoke();
            return lastValidNode.state;
        }

        return NPCState.Idle;
    }
}

public class npcIA : MonoBehaviour
{
    public bool isAlly = false;
    public Transform target;
    public float health = 100f;
    private Vector3 wanderTarget;
    private float wanderTimer = 0f;
    //private float wanderInterval = 2f; // Change interval every 2 seconds
    private float wanderSpeed = 3f;

    public float minSpeed = 2f;
    public float maxSpeed = 5f;

    private float speed;

    private BehaviorTree behaviorTree;

    // Start is called before the first frame update
    void Start()
    {
        wanderSpeed = Random.Range(minSpeed, maxSpeed);
        wanderTimer = Random.Range(2f, 5f); // Randomize the interval between 2 to 5 seconds
        speed = Random.Range(minSpeed, maxSpeed);
        behaviorTree = new BehaviorTree();
        var root = new BehaviorTreeNode(new IdleCondition(this), NPCState.Idle, HandleIdleState);

        var wanderingNode = new BehaviorTreeNode(new WanderingCondition(this), NPCState.Wandering, HandleWanderingState);
        var runningNode = new BehaviorTreeNode(new RunningCondition(this), NPCState.Running, HandleRunningState);
        var attackingNode = new BehaviorTreeNode(new AttackingCondition(this), NPCState.Attacking, HandleAttackingState);
        var deadNode = new BehaviorTreeNode(new DeadCondition(this), NPCState.Dead, HandleDeadState);

        root.children.Add(deadNode);
        root.children.Add(wanderingNode);
        wanderingNode.children.Add(attackingNode);
        wanderingNode.children.Add(runningNode);
        behaviorTree.root = root;
    }

    // Update is called once per frame
    void Update()
    {
        NPCState currentState = behaviorTree.Evaluate();
    }

    void HandleIdleState()
    {
    }
    void HandleWanderingState()
    {

        // Update the timer
        wanderTimer -= Time.deltaTime;

        // If the timer runs out, pick a new random target position
        if (wanderTimer <= 0f)
        {
            wanderTarget = GetRandomWanderPosition();
            wanderTimer = Random.Range(2f, 5f); // Randomize the interval between 2 to 5 seconds
        }

        // Move towards the target
        Vector3 direction = (wanderTarget - transform.position).normalized;
        transform.position += direction * (wanderSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);

        // If close to the target, reset the timer
        if (Vector3.Distance(transform.position, wanderTarget) < 0.5f)
        {
            wanderTimer = 0f;
        }
    }

    Vector3 GetRandomWanderPosition()
    {
        // Pick a random direction on the horizontal plane (XZ)
        Vector3 randomDirection = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
        return transform.position + randomDirection;
    }

    void HandleRunningState()
    {

        // Variables to store potential targets
        Transform playerTarget = null;
        Transform closestEnemyTarget = null;
        Transform closestTurretTarget = null;

        float closestDistance = Mathf.Infinity;

        // Find potential targets: players, turrets, and enemies
        GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag("NPC");
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] turretObjects = GameObject.FindGameObjectsWithTag("Turret");

        // Find the closest enemy
        foreach (GameObject potentialTarget in potentialTargets)
        {
            npcIA npc = potentialTarget.GetComponent<npcIA>();
            if (npc != null && npc.isAlly != this.isAlly)
            {
                float distance = Vector3.Distance(transform.position, potentialTarget.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemyTarget = potentialTarget.transform;
                }
            }
        }

        if (!isAlly)
        {
            // Find the closest player
            foreach (GameObject playerObj in playerObjects)
            {
                float distance = Vector3.Distance(transform.position, playerObj.transform.position);
                if (distance < closestDistance)
                {
                    playerTarget = playerObj.transform;
                }
            }
        }

        // Find the closest turret (only if NPC is an ally)
        if (isAlly)
        {
            foreach (GameObject turretObj in turretObjects)
            {
                float distance = Vector3.Distance(transform.position, turretObj.transform.position);
                if (distance < closestDistance)
                {
                    closestTurretTarget = turretObj.transform;
                }
            }
        }

        // Decide which target to pursue
        if (playerTarget != null && Random.Range(0f, 1f) < 0.6f)
        {
            // 60% chance to prioritize the player if one is found
            transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, Time.deltaTime * 5f);
        }
        else if (closestTurretTarget != null && isAlly)
        {
            // If allied NPC, prioritize the turret
            transform.position = Vector3.MoveTowards(transform.position, closestTurretTarget.position, Time.deltaTime * 5f);
        }
        else if (closestEnemyTarget != null)
        {
            // If no turret or player, target the closest enemy
            transform.position = Vector3.MoveTowards(transform.position, closestEnemyTarget.position, Time.deltaTime * 5f);
        }
    }

    void HandleAttackingState()
    {

        // Variables to store potential targets
        Transform playerTarget = null;
        Transform closestEnemyTarget = null;
        Transform closestTurretTarget = null;

        float closestDistance = Mathf.Infinity;

        // Find potential targets: players, turrets, and enemies
        GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag("NPC");
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] turretObjects = GameObject.FindGameObjectsWithTag("Turret");

        // Find the closest enemy
        foreach (GameObject potentialTarget in potentialTargets)
        {
            npcIA npc = potentialTarget.GetComponent<npcIA>();
            if (npc != null && npc.isAlly != this.isAlly)
            {
                float distance = Vector3.Distance(transform.position, potentialTarget.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemyTarget = potentialTarget.transform;
                }
            }
        }

        // Find the closest player
        foreach (GameObject playerObj in playerObjects)
        {
            float distance = Vector3.Distance(transform.position, playerObj.transform.position);
            if (distance < closestDistance)
            {
                playerTarget = playerObj.transform;
            }
        }

        // Find the closest turret (only if NPC is an ally)
        if (isAlly)
        {
            foreach (GameObject turretObj in turretObjects)
            {
                float distance = Vector3.Distance(transform.position, turretObj.transform.position);
                if (distance < closestDistance)
                {
                    closestTurretTarget = turretObj.transform;
                }
            }
        }

        // Decide which target to attack
        if (playerTarget != null && Random.Range(0f, 1f) < 0.6f)
        {
            // 60% chance to prioritize the player if one is found
            target = playerTarget;
        }
        else if (closestTurretTarget != null && isAlly)
        {
            // If allied NPC, prioritize the turret
            target = closestTurretTarget;
        }
        else if (closestEnemyTarget != null)
        {
            // If no turret or player, target the closest enemy
            target = closestEnemyTarget;
        }

        // Attack the selected target
        if (target != null)
        {
            npcIA targetNPC = target.GetComponent<npcIA>();
            if (targetNPC != null)
            {
                StartCoroutine(ApplyDamageOverTime(targetNPC));
            }
        }
    }


    IEnumerator ApplyDamageOverTime(npcIA targetNPC)
    {
        while (targetNPC.health > 0)
        {
            yield return new WaitForSeconds(5f);
            targetNPC.health -= 5f;
        }
    }

    void HandleDeadState()
    {
        Destroy(gameObject);
    }
}
