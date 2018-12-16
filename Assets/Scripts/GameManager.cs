using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class GameManager : MonoBehaviour {

	//private string jsonTeste = "{\n    \"version\": \"1.0\",\n    \"question\": \"Quantos lados tem um triângulo?\",\n\t\"options\": [\"três\", \"um\", \"dois\", \"quatro\"],\n\t\"type\": \"quiz\"\n}";
	private string jsonTeste = "{\n    \"version\": \"1.0\",\n\t\"task\": [\n\t\t{\n\t\t\t\"question\": \"Quantos lados tem um triângulo?\",\n\t\t\t\"options\": [\"três\", \"um\", \"dois\", \"quatro\"],\n\t\t\t\"type\": \"quiz\"\n\t\t},\n\t\t{\n\t\t\t\"question\": \"Quantos lados tem um pentagono?\",\n\t\t\t\"options\": [\"cinco\", \"três\", \"dois\", \"quatro\"],\n\t\t\t\"type\": \"quiz\"\n\t\t},\n\t\t{\n\t\t\t\"question\": \"Quantos lados tem um hexagono?\",\n\t\t\t\"options\": [\"seis\", \"três\", \"cinco\", \"quatro\"],\n\t\t\t\"type\": \"quiz\"\n\t\t}\n\t]\n}";
	private List<Question> questions;
	private int currentQuestion;

	public bool allowInput;
	public Button optionPrefab;
	public Text questionText;
	public Text responseText;
	public Transform panel;

	void Awake(){
		questions = new List<Question> ();
		var N = JSON.Parse (jsonTeste);

		for (int j = 0; j < N ["task"].Count; j++) {
			Question q = new Question (N ["task"][j]["question"], N ["task"][j]["options"][0], new List<string> ());
			for (int i = 1; i < N ["task"][j]["options"].Count; i++){
				q.addOption (N ["task"][j]["options"] [i]);
			}
			questions.Add(q);
		}

		currentQuestion = 0;
	}

	void Start () {
		NextQuestion ();
	}

	public void NextQuestion(){
		allowInput = true;
		responseText.text = "";
		for (int j = 0; j < panel.childCount; j++) {
			Destroy (panel.GetChild (j).gameObject);
		}

		questionText.text = questions[currentQuestion].Ask;
		questions[currentQuestion].shuffle ();
		for (int i = 0; i < questions[currentQuestion].Options.Count; i++) {
			Button q = Instantiate (optionPrefab, panel) as Button;
			q.GetComponent<Option>().data = questions[currentQuestion].Options [i];
			q.GetComponentInChildren<Text> ().text = questions[currentQuestion].Options [i];
			q.onClick.AddListener (q.GetComponent<Option>().sendAnswer);
		}
	}

	public void VerifyAnswer (string data){
		if (questions[currentQuestion].Answer == data) {
			if (currentQuestion < questions.Count-1)
				currentQuestion++;
			else
				currentQuestion = 0;
			StartCoroutine(responseAnswer (true));
		} else {
			StartCoroutine(responseAnswer (false));
		}
	}

	IEnumerator responseAnswer(bool b){
		if (b) {
			responseText.text = "ACERTOU!!!";
			responseText.color = Color.green;
			allowInput = false;
			yield return new WaitForSeconds (2);
			NextQuestion ();
		} else {
			responseText.text = "ERROU!!!";
			responseText.color = Color.red;
			allowInput = false;
			yield return new WaitForSeconds (2);
			allowInput = true;
			responseText.text = "";
		}
	}
}
