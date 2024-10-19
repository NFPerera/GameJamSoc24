public class DeadCondition : Condition {
    private npcIA npc;

    public DeadCondition(npcIA npc) {
        this.npc = npc;
    }

    public override bool CheckCondition() {
        // Check if the NPC's health is zero or below
        return npc.health <= 0;
    }
}