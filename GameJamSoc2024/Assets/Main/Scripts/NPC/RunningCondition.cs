using UnityEngine;
public class RunningCondition : Condition {
    private npcIA npc;
    public float checkDistance = 10f;

    public RunningCondition(npcIA npc) {
        this.npc = npc;
    }

    public override bool CheckCondition() {
        // Check if there are no enemies nearby
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