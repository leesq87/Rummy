using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class StaticList {

	public static List<Tile> Tiles = new List<Tile> ();

	public static List<Tile> Player1 = new List<Tile> ();
	public static List<Tile> Player2 = new List<Tile> ();
	public static List<Tile> Player3 = new List<Tile> ();
	public static List<Tile> Player4 = new List<Tile> ();

	[System.SerializableAttribute]
	public class Tile
	{
		public int Number;
		public int Color;
		public int Continuous = 0;
		public bool register = false;

		public int Start = 0;
		public int End = 0;
	}
}
