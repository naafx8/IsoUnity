﻿using UnityEngine;
using System.Collections;

public class AnimationManager : EventManager {



	public override void receiveEvent (GameEvent ev)
	{
		if(ev.Name == "ShowAnimation"){
			Decoration dec = (ev.getParameter("Objetive") as GameObject).GetComponent<Decoration>();
			GameObject animation = (GameObject)ev.getParameter("Animation");

			GameObject go = (GameObject) GameObject.Instantiate(animation);

			Decoration animation2 = go.GetComponent<Decoration>();

			animation2.renderer.sharedMaterial = new Material(Shader.Find("Transparent/Cutout/Diffuse"));
			animation2.Father = dec;
			animation2.adaptate();

			AutoAnimator anim = go.GetComponent<AutoAnimator>();
			anim.registerEvent(ev);
		}
	}
	
}