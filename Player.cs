using System;
using UnityEngine;
using System.Collections.Generic;

public class Player
{

	private Room location;

	public List<GameItem> inventory = new List<GameItem>();

	// Used with if sentences
	public bool Value;
	public bool Value2;

	public Player (Room startLocation)
	{
		this.location = startLocation;
	}

	public Room Move (string direction) {
//		Debug.Log (direction);

		Room nextRoom = location.GetNextRoom (direction);

		// Here we look behind the doors if we can go there and if we need a key
		if (nextRoom != null) {
			if (nextRoom.GetKey() != null ) {
				if (inventory.Contains(nextRoom.GetKey())) {
					location = nextRoom;
					inventory.Clear();
				}
					return location;
			}
			location = nextRoom;
		}
/*
		if (location.GetNextRoom (direction) != null) {
			location = location.GetNextRoom (direction);
*/
		return location;
	}
	public string GetLocationName() {
		return location.GetRoomName ();
	}

	public string GetLocationText1() {
		return location.GetRoomText1 ();
	}

	public string GetLocationText2() {
		return location.GetRoomText2 ();
	}

	public string GetLocationText3() {
		return location.GetRoomText3 ();
	}

	public string GetLocationText4() {
		return location.GetRoomText4 ();
	}

	public string GetLocationText5() {
		return location.GetRoomText5 ();
	}

	public Room GetLocationDoor1() {
		return location.GetRoomDoor1 ();
	}

	public Room GetLocationDoor2() {
		return location.GetRoomDoor2 ();
	}

	public Room GetLocationDoor3() {
		return location.GetRoomDoor3 ();
	}

	public Room GetLocationDoor4() {
		return location.GetRoomDoor4 ();
	}

	public Room GetLocationDoor5() {
		return location.GetRoomDoor5 ();
	}

	public Room GetLocation () {
		return location;
	}
	public void AddGameItem(GameItem gi){
		inventory.Add (gi);
	}

	public string GetGameItemList()
	{
		string temp = "";
		foreach (GameItem item in this.inventory){
			temp = temp + item.GetName ();
		}
		return temp;
	}

}

