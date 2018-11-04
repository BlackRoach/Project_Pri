using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : InteractionObject, IInteractive
{

    private InGamemanager inGameManager;
    private Transform player_trans;
    void Start()
    {
        inGameManager = InGamemanager.Instance;
        player_trans = inGameManager.PlayerDataContainer_readonly.PlayerTrans;
    }

    protected override void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            base.OnTriggerExit2D(col);
            inGameManager.CloseTeleportWindow();

        }
    }
    void Update()
    {

    }
    public void Teleportation(int pos)
    {
        player_trans.position = inGameManager.trans_list[pos].position;
        // position_money[pos]를 이용해 돈 깎기
    }
    void OpenTeleportWindow()
    {
        inGameManager.OpenTeleportWindow();
    }
    void IInteractive.Interaction()
    {
        OpenTeleportWindow();
    }
}
