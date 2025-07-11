using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class RoomController : MonoBehaviour
{
    public List<Ghoul_Behaviour> room1Ghouls;
    public Wizard_Behavior room1Wizard;
    public Golem_Behaviour room2Golem;

    public GameObject room1Door; // <-- Referenz zur Tür (optional: separate für "Tür zu" und "Tür auf")
    public GameObject room2Door;

    public GameObject room3Door;

    [SerializeField] private CardGameManager cardGameManager;
    [SerializeField] private PlayerMovement player; // Direkt gesetzt oder per Find

    private bool room1AlreadyCleared = false;
    private bool room2AlreadyCleared = false;

    void Update()
    {
        // Raum 1 prüfen
        if (!room1AlreadyCleared)
        {
            bool cleared = true;
            foreach (var ghoul in room1Ghouls)
                if (ghoul != null && ghoul.currentHealth > 0) cleared = false;
            if (room1Wizard != null && room1Wizard.currentHealth > 0) cleared = false;

            if (cleared)
            {
                room1AlreadyCleared = true;
                Debug.Log("Room 1 Cleared!");

                StartCoroutine(ShowCardsAfterDelay(1f, () =>
                {
                    cardGameManager.TriggerCardChoice(player, onFinish: () =>
                    {
                        OpenDoor(room2Door);
                    });
                }));
            }

        }

        // Raum 2 prüfen
        if (!room2AlreadyCleared)
        {
            bool cleared = (room2Golem == null || room2Golem.currentHealth <= 0);

            if (cleared)
            {
                room2AlreadyCleared = true;
                Debug.Log("Room 2 Cleared!");

                StartCoroutine(ShowCardsAfterDelay(1.5f, () =>
                {
                    cardGameManager.TriggerCardChoice(player, onFinish: () =>
                    {
                        OpenDoor(room3Door);
                    });
                }));
            }

        }
    }

    private void OpenDoor(GameObject door)
    {
        if (door != null) door.SetActive(false);
    }

    private void CloseDoor(GameObject door)
    {
        if (door != null) door.SetActive(true);
    }

    private IEnumerator ShowCardsAfterDelay(float delaySeconds, System.Action callback)
    {
        yield return new WaitForSeconds(delaySeconds);
        callback?.Invoke();
    }

    public void ResetRoomStates()
    {
        room1AlreadyCleared = false;
        room2AlreadyCleared = false;

        // Türen ggf. schließen oder öffnen (je nachdem wie es sein soll)
        if (room1Door != null) OpenDoor(room1Door);
        if (room2Door != null) CloseDoor(room2Door);
    }



}
