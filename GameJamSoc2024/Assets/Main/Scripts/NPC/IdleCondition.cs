using UnityEngine;

public class IdleCondition : Condition {
    private npcIA npc;

    public IdleCondition(npcIA npc) {
        this.npc = npc;
    }

    public override bool CheckCondition() {
        return true;
    }
}