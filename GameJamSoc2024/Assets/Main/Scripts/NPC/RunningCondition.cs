using UnityEngine;
public class RunningCondition : Condition {
    private npcIA npc;
    public float checkDistance = 25f;

    public RunningCondition(npcIA npc) {
        this.npc = npc;
    }

    // TODO Make it modular to avoid code duplication
    public override bool CheckCondition() {
        Collider[] hitColliders = Physics.OverlapSphere(npc.transform.position, checkDistance);
        foreach (var hitCollider in hitColliders) {
            if (IsEnemyOrTurret(hitCollider) || IsEnemyPlayer(hitCollider)) {
                return true; // Enemy, turret, or player nearby, switch to running
            }
        }
        return false; // No enemies nearby
    }

    private bool IsEnemyOrTurret(Collider hitCollider) {
        npcIA otherNPC = hitCollider.GetComponent<npcIA>();
        return otherNPC != null && (otherNPC.isAlly != npc.isAlly || hitCollider.CompareTag("Turret"));
    }

    private bool IsEnemyPlayer(Collider hitCollider) {
        npcIA otherNPC = hitCollider.GetComponent<npcIA>();
        return otherNPC != null && !otherNPC.isAlly && hitCollider.CompareTag("Player");
    }
}