using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCState {
    Idle,
    Wandering,
    Running,
    Attacking,
    Dead
}

public abstract class Condition {
    public abstract bool CheckCondition();
}

public class BehaviorTreeNode {
    public Condition condition { get; set; }
    public NPCState state { get; set; }
    public List<BehaviorTreeNode> children { get; set; } = new List<BehaviorTreeNode>();
    public System.Action action { get; set; }

    public BehaviorTreeNode(Condition condition, NPCState state, System.Action action = null) {
        this.condition = condition;
        this.state = state;
        this.action = action;
    }
}

public class BehaviorTree {
    public BehaviorTreeNode root { get; set; }

    public NPCState Evaluate() {
        Stack<BehaviorTreeNode> stack = new Stack<BehaviorTreeNode>();
        stack.Push(root);
        BehaviorTreeNode lastValidNode = null;

        // Traverse the tree in a depth-first manner
        while (stack.Count > 0) {
            BehaviorTreeNode node = stack.Pop();
            if (node.condition.CheckCondition()) {
                lastValidNode = node;
            }
            foreach (BehaviorTreeNode child in node.children) {
                stack.Push(child);
            }
        }

        if (lastValidNode != null) {
            lastValidNode.action?.Invoke();
            return lastValidNode.state;
        }

        return NPCState.Idle;
    }
}

public class npcIA : MonoBehaviour {
    public bool isAlly = false;
    public Transform target;
    public float health = 100f;
private Vector3 wanderTarget;
private float wanderTimer = 0f;
private float wanderInterval = 2f; // Change interval every 2 seconds
private float wanderSpeed = 3f;

public float minSpeed = 2f;
public float maxSpeed = 5f;

private float speed;




    private BehaviorTree behaviorTree;

    // Start is called before the first frame update
    void Start() {
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
    void Update() {
        NPCState currentState = behaviorTree.Evaluate();
        Debug.Log("Current state: " + currentState);
    }

    void HandleIdleState() {
    }
void HandleWanderingState() {
    Debug.Log("Wandering state");

    // Update the timer
    wanderTimer -= Time.deltaTime;

    // If the timer runs out, pick a new random target position
    if (wanderTimer <= 0f) {
        wanderTarget = GetRandomWanderPosition();
        wanderTimer = Random.Range(2f, 5f); // Randomize the interval between 2 to 5 seconds
    }

    // Move towards the target
    Vector3 direction = (wanderTarget - transform.position).normalized;
    transform.position += direction * wanderSpeed * Time.deltaTime;
    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);

    // If close to the target, reset the timer
    if (Vector3.Distance(transform.position, wanderTarget) < 0.5f) {
        wanderTimer = 0f;
    }
}

Vector3 GetRandomWanderPosition() {
    // Pick a random direction on the horizontal plane (XZ)
    Vector3 randomDirection = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
    return transform.position + randomDirection;
}

    void HandleRunningState() {
        Debug.Log("Running state");

        // Find the closest target that is different from their isAlly flag
        GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag("NPC");
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject potentialTarget in potentialTargets) {
            npcIA npc = potentialTarget.GetComponent<npcIA>();
            if (npc != null && npc.isAlly != this.isAlly) {
                float distance = Vector3.Distance(transform.position, potentialTarget.transform.position);
                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestTarget = potentialTarget.transform;
                }
            }
        }

        if (closestTarget != null) {
            // Move towards the closest target
            transform.position = Vector3.MoveTowards(transform.position, closestTarget.position, Time.deltaTime * 5f); // Adjust speed as needed
        }
    }

    void HandleAttackingState() {
        Debug.Log("Attacking state");

        // Find the closest target that is different from their isAlly flag
        GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag("NPC");
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject potentialTarget in potentialTargets) {
            npcIA npc = potentialTarget.GetComponent<npcIA>();
            if (npc != null && npc.isAlly != this.isAlly) {
                float distance = Vector3.Distance(transform.position, potentialTarget.transform.position);
                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestTarget = potentialTarget.transform;
                }
            }
        }

        if (closestTarget != null) {
            target = closestTarget;
            npcIA targetNPC = target.GetComponent<npcIA>();
            if (targetNPC != null) {
                // Apply damage to the target NPC
                StartCoroutine(ApplyDamageOverTime(targetNPC));

                // Check if the target NPC is dead
                if (targetNPC.health <= 0) {
                    targetNPC.health = 0;
                    return;
                }
            }
        }
    }

    IEnumerator ApplyDamageOverTime(npcIA targetNPC) {
        while (targetNPC.health > 0) {
            yield return new WaitForSeconds(5f);
            targetNPC.health -= 5f;
            Debug.Log("Ouch");
        }
    }

    void HandleDeadState() {
        Debug.Log("Dead state");
        Destroy(gameObject);
    }
}
