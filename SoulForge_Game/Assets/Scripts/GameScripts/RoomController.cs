using UnityEngine;
using System.Collections.Generic;

public class RoomController : MonoBehaviour
{
    // Assign these in the Inspector or via code
    public List<Ghoul_Behaviour> room1Ghouls;
    public Wizard_Behavior room1Wizard;
    public Golem_Behaviour room2Golem;

    public enum RoomLog { None, Room1Cleared, Room2Cleared }

    private bool room1AlreadyCleared = false;
    private bool room2AlreadyCleared = false;

    void Update()
    {
        // Check Room 1
        if (!room1AlreadyCleared)
        {
            bool room1Cleared = true;
            foreach (var ghoul in room1Ghouls)
                if (ghoul != null && ghoul.currentHealth > 0) room1Cleared = false;
            if (room1Wizard != null && room1Wizard.currentHealth > 0) room1Cleared = false;
            if (room1Cleared)
            {
                room1AlreadyCleared = true;
                Debug.Log("Room1Cleared!");
                // TODO: open door 1
            }
        }
        // Check Room 2
        if (!room2AlreadyCleared)
        {
            bool room2Cleared = true;
            if (room2Golem != null && room2Golem.currentHealth > 0) room2Cleared = false;
            if (room2Cleared)
            {
                room2AlreadyCleared = true;
                Debug.Log("Room2Cleared!");
                // TODO: open door 2
            }
        }
    }
}