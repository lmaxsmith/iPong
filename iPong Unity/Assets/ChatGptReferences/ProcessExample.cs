using System;
using System.Diagnostics;
using System.IO;
using EasyButtons;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ProcessExample : MonoBehaviour
{
	// The path to the Python script
	private string pythonScriptPath = "testScript.py";

	Process process;
	StreamWriter standardInput;
	StreamReader standardOutput;

	private void Start()
	{
		// Create a new process
		process = new Process
		{
			StartInfo = new ProcessStartInfo
			{
				FileName = "testScript",
				Arguments = pythonScriptPath,
				UseShellExecute = false,
				RedirectStandardInput = true,
				RedirectStandardOutput = true,
				CreateNoWindow = true
			}
		};

		// Start the process
		process.Start();

		// Get the standard input and output streams
		standardInput = process.StandardInput;
		standardOutput = process.StandardOutput;


	}

	private void OnEnable()
	{
		//Do nothing
	}

	private void OnDisable()
	{
		// Stop the process
		process.Kill();
	}

	[Button]
	public void TestTheThing()
	{
		// An example array of 6 floats
		float[] array = { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f };

		// Write the array to the standard input stream
		foreach (var value in array)
		{
			standardInput.WriteLine(value);
		}

		// Read the result from the standard output stream
		int result = int.Parse(standardOutput.ReadLine());

		// Print the result
		Debug.Log("Result: " + result);

	}
}