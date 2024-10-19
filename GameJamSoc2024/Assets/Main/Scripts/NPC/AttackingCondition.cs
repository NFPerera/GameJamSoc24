using UnityEngine;

public class AttackingCondition : Condition {
    private npcIA npc;
    public float checkDistance = 1.0f;

    public AttackingCondition(npcIA npc) {
        this.npc = npc;
    }

    public override bool CheckCondition() {
        Collider[] hitColliders = Physics.OverlapSphere(npc.transform.position, checkDistance);
        foreach (var hitCollider in hitColliders) {
            npcIA otherNPC = hitCollider.GetComponent<npcIA>();
            if (otherNPC != null && otherNPC.isAlly != npc.isAlly) {
                return true; // Enemy nearby, switch to running
            }
        }
        return false; // No enemies nearby
    }
}