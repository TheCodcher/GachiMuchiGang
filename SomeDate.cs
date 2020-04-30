using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Dungeon_Master
{
    enum Direction
    {
		Up = BotAction.Up,
		Right = BotAction.Right,
		Down = BotAction.Down,
		Left = BotAction.Left,
	}
    enum BotAction
    {
        Up,
		Right,
		Down,
        Left,
        Act,
        Stop
    }
	public enum Item : short
	{
		/// <summary>
		/// This is your Bomberman. This is what he usually looks like :)
		/// </summary>
		BOMBERMAN = (short)'☺',

		/// <summary>
		/// Your bomberman is sitting on own bomb
		/// </summary>
		BOMB_BOMBERMAN = (short)'☻',

		/// <summary>
		/// Your dead Bomberman 
		/// </summary>
		/// <remarks>
		/// Don't worry, he will appear somewhere in next move. 
		/// You're getting -200 for each death
		/// </remarks>
		DEAD_BOMBERMAN = (short)'Ѡ',

		/// <summary>
		/// This is other players alive Bomberman
		/// </summary>
		OTHER_BOMBERMAN = (short)'♥',

		/// <summary>
		/// This is other players Bomberman -  just set the bomb
		/// </summary>
		OTHER_BOMB_BOMBERMAN = (short)'♠',

		/// <summary>
		/// Other players Bomberman's corpse 
		/// </summary>
		/// <remarks>
		/// It will disappear shortly, right on the next move. If you've done it you'll get score points
		/// </remarks>
		OTHER_DEAD_BOMBERMAN = (short)'♣',

		/// <summary>
		/// Bomb with timer "5 tacts to boo-o-o-m!"
		/// </summary>
		/// <remarks>
		/// After bomberman set the bomb, the timer starts (5 ticks)
		/// </remarks>
		BOMB_TIMER_5 = (short)'5',

		/// <summary>
		/// Bomb with timer "4 tacts to boom"
		/// </summary>
		BOMB_TIMER_4 = (short)'4',

		/// <summary>
		/// Bomb with timer "3 tacts to boom"
		/// </summary>
		BOMB_TIMER_3 = (short)'3',

		/// <summary>
		/// Bomb with timer "2 tacts to boom"
		/// </summary>
		BOMB_TIMER_2 = (short)'2',

		/// <summary>
		/// Bomb with timer "1 tact to boom"
		/// </summary>
		BOMB_TIMER_1 = (short)'1',

		MY_BOMB_TIMER_5 = (short)'0',
		MY_BOMB_TIMER_4 = (short)'9',
		MY_BOMB_TIMER_3 = (short)'8',
		MY_BOMB_TIMER_2 = (short)'7',
		MY_BOMB_TIMER_1 = (short)'6',

		/// <summary>
		/// Boom! This is what is bomb does, everything that is destroyable got destroyed
		/// </summary>
		BOOM = (short)'҉',

		/// <summary>
		/// Wall that can't be destroyed.
		/// Indestructible wall will not fall from bomb.
		/// </summary>
		WALL = (short)'☼',

		/// <summary>
		/// Destroyable wall. It can be blowed up with a bomb (with score points)
		/// </summary>
		DESTROYABLE_WALL = (short)'#',

		/// <summary>
		/// Walls ruins. This is how broken wall looks like, it will dissapear on next move.
		/// </summary>
		DestroyedWall = (short)'H',

		/// <summary>
		/// Meat chopper. This guys runs over the board randomly and gets in the way all the time. If it will touch bomberman - bomberman dies.
		/// </summary>
		MEAT_CHOPPER = (short)'&',

		/// <summary>
		/// Dead meat chopper. score point for killing.
		/// </summary>
		DeadMeatChopper = (short)'x',

		/// <summary>
		/// Empty space on a map. This is the only place where you can move your Bomberman
		/// </summary>
		Space = (short)' '
	}
	enum Motivate
	{
		OtherBomberman = Item.OTHER_BOMBERMAN,
		OtherBombBomberman = Item.OTHER_BOMB_BOMBERMAN,
		Bomb5 = Item.BOMB_TIMER_5,
		Bomb4 = Item.BOMB_TIMER_4,
		Bomb3 = Item.BOMB_TIMER_3,
		Bomb2 = Item.BOMB_TIMER_2,
		Bomb1 = Item.BOMB_TIMER_1,
		DistroybleWall = Item.DESTROYABLE_WALL,
		Wall = Item.WALL,
		TempLocker,
		Ghost = Item.MEAT_CHOPPER,
		Boom = Item.BOOM,
		Death = Item.DEAD_BOMBERMAN,
		Clear = Item.Space,
		MyBomb543,
		MyBomb2 = Item.MY_BOMB_TIMER_2,
		MyBomb1 = Item.MY_BOMB_TIMER_1,
	}
}
	

