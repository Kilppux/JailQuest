using System;

public class Room
{

	private string roomName;
	private string text1;
	private string text2;
	private string text3;
	private string text4;
	private string text5;
	private string imagePath;
	private Room doorA;
	private Room doorB;
	private Room doorC;
	private Room doorD;
	private Room doorE;
	private GameItem key;

	private bool showButton;

	// Room is made here
	public Room (string roomName, string text1, string text2, string text3, string text4, string text5, string imagePath)
	{
		this.roomName = roomName;
		this.text1 = text1;
		this.text2 = text2;
		this.text3 = text3;
		this.text4 = text4;
		this.text5 = text5;
		this.imagePath = imagePath;
		this.showButton = true;
	}
	// Set the doors for a room
	public void SetDoors (Room doorA, Room doorB, Room doorC, Room doorD, Room doorE)
	{
		this.doorA = doorA;
		this.doorB = doorB;
		this.doorC = doorC;
		this.doorD = doorD;
		this.doorE = doorE;
	}
/*

*/
	public bool GetShowButton() {
		return this.showButton;
	}
	public void AddKey(GameItem key) {
		this.key = key;
	}
	public GameItem GetKey() {
		return key;
	}
	public void DeactivateShowButton() {
		this.showButton = false;
	}

	public Room GetNextRoom (string direction)
	{
		if (direction.Equals ("a")) {
			return this.doorA;
		}
		if (direction.Equals ("b")) {
			return this.doorB;
		}
		if (direction.Equals ("c")) {
			return this.doorC;
		}
		if (direction.Equals ("d")) {
			return this.doorD;
		}
		if (direction.Equals ("e")) {
			return this.doorE;
		}

		return null;
	}

	public string GetRoomName ()
	{
		return roomName;
	}

	public string GetRoomText1 ()
	{
		return text1;
	}

	public string GetRoomText2 ()
	{
		return text2;
	}

	public string GetRoomText3 ()
	{
		return text3;
	}

	public string GetRoomText4 ()
	{
		return text4;
	}

	public string GetRoomText5 ()
	{
		return text5;
	}
	public Room GetRoomDoor1 ()
	{
		return doorA;
	}

	public Room GetRoomDoor2 ()
	{
		return doorB;
	}

	public Room GetRoomDoor3 ()
	{
		return doorC;
	}

	public Room GetRoomDoor4 ()
	{
		return doorD;
	}

	public Room GetRoomDoor5 ()
	{
		return doorE;
	}

	public string GetImagePath ()
	{
		return imagePath;
	}
}
