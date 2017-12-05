// KEYCARD.CS
// GREG BALBIRNIE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keycard : MonoBehaviour {

	public Text code_text;	// The text on the code

	// Use this for initialization
	void Start () 
	{
		// Find the text on the keycard
		code_text = gameObject.GetComponentInChildren<Text>();
	}

	// Set the code to the same as the keypad needs
	void Set_Code(int[] code)
	{
		// Loop through string
		for (int i = 0; i < code.Length; i++)
		{
			// Put the code on the keycard
			code_text.text = (code_text.text + code[i]);
		}	
	}
}
