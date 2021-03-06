﻿using UnityEngine;
using System.Collections;

public class GameEventInterpreter : ISecuenceInterpreter {

	private bool launched = false;
	private bool finished = false;
	private SecuenceNode node;
	private bool waitTillEventFinished;

	public bool CanHandle(SecuenceNode node)
	{
		return node!= null && node.Content != null && node.Content is SerializableGameEvent;
	}

	public void UseNode(SecuenceNode node){
		this.node = node;
	}

	public bool HasFinishedInterpretation()
	{
		return finished;
	}

	public SecuenceNode NextNode()
	{
		return (this.node.Childs.Length>0)?this.node.Childs[0]:null;
	}

	public void EventHappened(IGameEvent ge)
	{
		Debug.Log ("Something happened: " + ge.Name);
		if(waitTillEventFinished)
			if(ge.Name.ToLower() == "event finished")
			waitTillEventFinished =  GameEvent.CompareEvents(ge, ge.getParameter("event") as IGameEvent);
	}

	private IGameEvent ge;
	public void Tick()
	{
		ge = (node.Content as SerializableGameEvent);
		if(!launched){
			Game.main.enqueueEvent(ge);
			if(ge.getParameter("synchronous")!=null && (bool)ge.getParameter("synchronous") == true)
				waitTillEventFinished = true;
			launched = true;
		}
		if(!waitTillEventFinished)
			finished = true;
	}

	public ISecuenceInterpreter Clone(){
		return new GameEventInterpreter();
	}
}
