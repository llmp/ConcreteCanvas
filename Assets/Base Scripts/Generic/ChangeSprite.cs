using System;
using System.Reflection;
using System.Collections;
using UnityEngine;

public class ChangeSprite : MonoBehaviour {
	
	public Sprite neutral;
	public Sprite falling;
	public Sprite right;
	public Sprite left;
	public Sprite jumping;
	public Sprite charging;
	public Sprite attacking;
	public Sprite up;
	public Sprite down;
	public Sprite state1;
	public Sprite state2;
	public Sprite state3;
	public Sprite state4;
	public Sprite state5;
	public Sprite state6;
	public Sprite state7;
	public Sprite state8;
	public Sprite state9;
	public Sprite state10;
	public Sprite state11;
	public Sprite state12;

	public void ChangeSpriteTo(SpriteRenderer spriteRenderer, Sprite sprite){
		spriteRenderer.sprite = sprite;
	}
}
