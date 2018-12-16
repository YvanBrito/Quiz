using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question {

	private string ask;
	private string answer;
	private List<string> options;

	public Question (string ask, string answer, List<string> options) {
		this.ask = ask;
		this.answer = answer;
		this.options = options;
		this.options.Add (this.answer);
	}

	public string Ask {
		get{ return this.ask; }
		set{ this.ask = value; }
	}

	public string Answer {
		get{ return this.answer; }
		set{ this.answer = value; }
	}

	public List<string> Options {
		get{ return this.options; }
		set{ this.options = value; }
	}

	public void addOption(string option){
		this.options.Add (option);
		shuffle ();
	}

	public void shuffle(){
		for (int n = this.options.Count-1; n > 1; n--) {
			int k = Random.Range(0,n);
			string value = this.options [k];
			this.options [k] = this.options [n];
			this.options [n] = value;
		}
	}
}
