using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	public int UserID { get; set; }
	public string Name { get; set; }
	public Color Color { get; set; }
	public Hero[] Heroes { get; set; }
	public Tile[] Tiles { get; set; }
	public bool IsMouseControlled { get; set; }

	private int heroCount = 0;
	private int tileCount = 0;

	public Player(int userID, string name, Color color, bool isMouseControlled)
	{
		UserID = userID;
		Name = name;
		Color = color;
		Heroes = new Hero[5];
		Tiles = new Tile[5];
		IsMouseControlled = isMouseControlled;
	}

	public void AddHero(Hero hero)
	public void AddTile(Tile tile)
	{
		Heroes[heroCount++] = hero;
		hero.Owner = this;
		Tiles[tileCount++] = tile;
		tile.Owner = this;
	}
}
