using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Once the player has cross the plane trigger, load the appropriate room next to the current room
/// </summary>
public class RoomLoader : MonoBehaviour {

    public Vector2 RoomSize;

    public GameObject RoomLeft;
    public GameObject RoomRight;
    public GameObject RoomFront;
    public GameObject RoomBack;

    public Direction leftDirection;
    public Direction rightDirection;
    public Direction frontDirection;
    public Direction backDirection;

    public GameObject PreviousRoom;

    protected Direction orientation;


    public enum Direction
    {
        Front,
        Back,
        Left,
        Right
    }

    protected bool Loaded = false;

    private void OnTriggerEnter(Collider other)
    {
        if(!Loaded)
            if (other.tag == "Player")
            {
                
                switch ((int)transform.eulerAngles.y / 90 % 4)
                {
                    case 0: orientation = Direction.Front; break;
                    case 1: orientation = Direction.Right; break;
                    case 2: orientation = Direction.Back; break;
                    case 3: orientation = Direction.Left; break;
                }

                Debug.Log(orientation);

                if (RoomLeft != null)
                    PlaceRoom(RoomLeft, Direction.Left, leftDirection);             
                

                if (RoomRight != null)
                    PlaceRoom(RoomRight, Direction.Right, rightDirection);

                if (RoomFront != null)
                    PlaceRoom(RoomFront, Direction.Front, frontDirection);

                if (RoomBack != null)
                    PlaceRoom(RoomBack, Direction.Back, backDirection);

                if (PreviousRoom != null)
                    PreviousRoom.transform.position = transform.position + new Vector3(0f, -20f, 0f);
            }
    }

    private void OnTriggerExit(Collider other)
    {
        Loaded = false;
    }

    protected void PlaceRoom(GameObject room, Direction placement, Direction direction)
    {       
        switch (direction)
        {
            case Direction.Front: room.transform.eulerAngles = transform.eulerAngles + new Vector3(0,180,0); break;
            case Direction.Right: room.transform.eulerAngles = transform.eulerAngles + new Vector3(0, 90, 0); break;
            case Direction.Back: room.transform.eulerAngles = transform.eulerAngles; break;
            case Direction.Left: room.transform.eulerAngles = transform.eulerAngles + new Vector3(0, -90, 0); break;
            default: Debug.LogWarning("Mauvaise orientation de la salle passée en switch"); break;
        }

        switch (placement)
        {
            case Direction.Front: room.transform.position =  transform.position + ComputeTranslationFront(); break;
            case Direction.Right: room.transform.position = transform.position + ComputeTranslationRight(); break;
            case Direction.Back: room.transform.position = transform.position + ComputeTranslationBack(); break;
            case Direction.Left: room.transform.position = transform.position + ComputeTranslationLeft(); break;
            default: Debug.LogWarning("Mauvaise orientation de la salle passée en switch"); break;
        }
    }

    private Vector3 ComputeTranslationFront()
    {
        switch (orientation)
        {
            case Direction.Front: return new Vector3(0,0,10.1f); break;
            case Direction.Right: return new Vector3(10.1f, 0, 0); break;
            case Direction.Back: return new Vector3(0, 0, -10.1f); break;
            case Direction.Left: return new Vector3(-10.1f, 0, 0); break;
            default: return new Vector3(0,0,0);
        }
    }

    private Vector3 ComputeTranslationRight()
    {
        switch (orientation)
        {
            case Direction.Left: return new Vector3(0, 0, 10.1f); break;
            case Direction.Front: return new Vector3(10.1f, 0, 0); break;
            case Direction.Right: return new Vector3(0, 0, -10.1f); break;
            case Direction.Back: return new Vector3(-10.1f, 0, 0); break;
            default: return new Vector3(0, 0, 0);
        }
    }

    private Vector3 ComputeTranslationBack()
    {
        switch (orientation)
        {
            case Direction.Back: return new Vector3(0, 0, 10.1f); break;
            case Direction.Left: return new Vector3(10.1f, 0, 0); break;
            case Direction.Front: return new Vector3(0, 0, -10.1f); break;
            case Direction.Right: return new Vector3(-10.1f, 0, 0); break;
            default: return new Vector3(0, 0, 0);
        }
    }

    private Vector3 ComputeTranslationLeft()
    {
        switch (orientation)
        {
            case Direction.Right: return new Vector3(0, 0, 10.1f); break;
            case Direction.Front: return new Vector3(10.1f, 0, 0); break;
            case Direction.Left: return new Vector3(0, 0, -10.1f); break;
            case Direction.Back: return new Vector3(-10.1f, 0, 0); break;
            default: return new Vector3(0, 0, 0);
        }
    }
}
