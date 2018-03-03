using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Once the player has cross the plane trigger, load the appropriate room next to the current room
/// </summary>
public class RoomLoader : MonoBehaviour {

    public GameObject RoomLeft;
    public GameObject RoomRight;
    public GameObject RoomFront;
    public GameObject RoomBack;

    public OuverturePorte PorteFront;
    public OuverturePorte PorteRight;
    public OuverturePorte PorteBack;
    public OuverturePorte PorteLeft;

    [HideInInspector] public GameObject CloneLeft;
    [HideInInspector] public GameObject CloneRight;
    [HideInInspector] public GameObject CloneFront;
    [HideInInspector] public GameObject CloneBack;

    public Vector3 PlacementLeft;
    public Vector3 PlacementRight;
    public Vector3 PlacementFront;
    public Vector3 PlacementBack;

    public Direction leftDirection;
    public Direction rightDirection;
    public Direction frontDirection;
    public Direction backDirection;

    public GameObject PreviousRoom;


    public bool isInTheRoom = false;

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
        Debug.Log("Quit");
        if (other.tag == "Player")
            isInTheRoom = true;

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Enter");
        if (other.tag == "Player")
            isInTheRoom = false;
    }

    public void LoadRooms()
    {
        if (!Loaded) {   
            switch ((int)transform.eulerAngles.y / 90 % 4)
            {
                case 0: orientation = Direction.Front; break;
                case 1: orientation = Direction.Right; break;
                case 2: orientation = Direction.Back; break;
                case 3: orientation = Direction.Left; break;
            }

            if (RoomLeft != null)
            {
                CloneLeft = Instantiate(RoomLeft);
                PlaceRoom(CloneLeft, PlacementLeft, leftDirection, Direction.Left);
            }

            if (RoomRight != null)
            {
                CloneRight = Instantiate(RoomRight);
                PlaceRoom(CloneRight, PlacementRight, rightDirection, Direction.Right);
            }


            if (RoomFront != null)
            {
                CloneFront = Instantiate(RoomFront);
                PlaceRoom(CloneFront, PlacementFront, frontDirection, Direction.Front);
            }


            if (RoomBack != null)
            {
                CloneBack = Instantiate(RoomBack);
                PlaceRoom(CloneBack, PlacementBack, backDirection, Direction.Back);
            }


            if (PreviousRoom != null)
            {
                DestroyPreviousRooms();
            }
            Loaded = true;
        }
    }

    protected void PlaceRoom(GameObject room, Vector3 placement, Direction direction, Direction porte)
    {

        RoomLoader loader = room.GetComponent<RoomLoader>();


        switch (direction)
        {
            case Direction.Front: room.transform.eulerAngles = transform.eulerAngles + new Vector3(0, 180, 0); break;
            case Direction.Right: room.transform.eulerAngles = transform.eulerAngles + new Vector3(0, 90, 0); break;
            case Direction.Back: room.transform.eulerAngles = transform.eulerAngles; break;
            case Direction.Left: room.transform.eulerAngles = transform.eulerAngles + new Vector3(0, -90, 0); break;
            default: Debug.LogWarning("Mauvaise orientation de la salle passée en switch"); break;
        }

        switch (porte)
        {

            case Direction.Front: LinkPorte(PorteFront, direction, room.GetComponent<RoomLoader>()); break;
            case Direction.Right: LinkPorte(PorteRight, direction, room.GetComponent<RoomLoader>()); break;
            case Direction.Back: LinkPorte(PorteBack, direction, room.GetComponent<RoomLoader>()); break;
            case Direction.Left: LinkPorte(PorteLeft, direction, room.GetComponent<RoomLoader>()); break;
            default: Debug.LogWarning("Mauvaise porte passée en switch"); break;
        }

        switch (orientation)
        {
            case Direction.Front: room.transform.position = transform.position + new Vector3(placement.x, placement.y, placement.z); break;
            case Direction.Right: room.transform.position = transform.position + new Vector3(-placement.z, placement.y, placement.x); break;
            case Direction.Back: room.transform.position = transform.position + new Vector3(-placement.x, placement.y, -placement.z); break;
            case Direction.Left: room.transform.position = transform.position + new Vector3(placement.z, placement.y, -placement.x); break;
            default: Debug.LogWarning("Mauvaise orientation de la salle passée en switch"); break;
        }

        if (loader != null)
            loader.PreviousRoom = gameObject;
    }

    protected void DestroyPreviousRooms()
    {
        RoomLoader previousLoarder = PreviousRoom.GetComponent<RoomLoader>();
        if(previousLoarder != null)
        {
            if(previousLoarder.CloneFront != gameObject)
                Destroy(previousLoarder.CloneFront);

            if (previousLoarder.CloneRight != gameObject)
                Destroy(previousLoarder.CloneRight);
            if (previousLoarder.CloneBack != gameObject)
                Destroy(previousLoarder.CloneBack);
            if (previousLoarder.CloneLeft != gameObject)
                Destroy(previousLoarder.CloneLeft);
        }
        Destroy(PreviousRoom);
    }

    protected void LinkPorte(OuverturePorte porte, Direction direction, RoomLoader otherRoom)
    {
        if(porte != null)
        {
            switch (direction)
            {
                case Direction.Front: porte.porteVoisine = otherRoom.PorteFront; otherRoom.PorteFront.porteVoisine = porte; break;
                case Direction.Right: porte.porteVoisine = otherRoom.PorteRight; otherRoom.PorteRight.porteVoisine = porte; break;
                case Direction.Back: porte.porteVoisine = otherRoom.PorteBack; otherRoom.PorteBack.porteVoisine = porte; break;
                case Direction.Left: porte.porteVoisine = otherRoom.PorteLeft; otherRoom.PorteLeft.porteVoisine = porte; break;
            }
        }          
    }
}
