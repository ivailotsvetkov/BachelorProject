using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotEvent : GameEvent
{
    public bool Success = false;
    public Vector3 Pos;
}

public class SpotTrigger : AncestorBehaviour
{
    public SpotPicture Picture;
    public bool Correct;
    public bool Found = false;

    public void Click()
    {
        if (Correct)
        {
            if (!Found)
            {
                Overmind.EventsManager.Send(new SpotEvent() { Success = true, Pos = transform.position });
                Found = true;
            }
        } else
        {
            Overmind.EventsManager.Send(new SpotEvent() { Success = false });
        }
    }
}
