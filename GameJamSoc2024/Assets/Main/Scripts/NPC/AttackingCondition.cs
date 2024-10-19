using UnityEngine;

public class AttackingCondition : Condition {
    public float checkDistance = 1.0f;
    public float playerDistanceNearby = 7.5f;
    private npcIA npc;

    public AttackingCondition(npcIA npc) {
        this.npc = npc;
    }

    public override bool CheckCondition() {
        Collider[] hitColliders = Physics.OverlapSphere(npc.transform.position, checkDistance);
        // Check for nearby player if the NPC is not an ally
        if (!npc.isAlly) {
            Debug.Log("Checking for nearby player");
            Collider[] playerColliders = Physics.OverlapSphere(npc.transform.position, playerDistanceNearby);
            foreach (var playerCollider in playerColliders) {
                if (IsEnemyPlayer(playerCollider)) {
                    Debug.Log("Player nearby");
                    return false; // Player nearby, retarget to the player
                }
            }
        }

        foreach (var hitCollider in hitColliders) {
            if (IsEnemyOrTurret(hitCollider)) {
                return true; // Enemy or turret nearby, switch to running
            }
        }


        return false; // No enemies or players nearby
    }
    private bool IsEnemyOrTurret(Collider hitCollider) {
        npcIA otherNPC = hitCollider.GetComponent<npcIA>();
        if (npc.isAlly) {
            return (otherNPC != null && !otherNPC.isAlly) || hitCollider.CompareTag("Turret");
        }
        return otherNPC != null && otherNPC.isAlly != npc.isAlly;
    }

    private bool IsEnemyPlayer(Collider hitCollider) {
        return hitCollider.CompareTag("Player") && !npc.isAlly;
    }
}