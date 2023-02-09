using System;
using System.Diagnostics;
using System.IO.Pipes;
using UnityEngine;
using System.Threading.Tasks;
using Debug = UnityEngine.Debug;

public class AsyncProcessExample : MonoBehaviour
{
	// The path to the Python script
	private string pythonScriptPath = "testScript.py";

	private async void Start()
	{
		// Create a named pipe for communication
		using (var pipe = new NamedPipeClientStream(".", "mypipe", PipeDirection.InOut))
		{
			Debug.Log("Starting");
			// Connect to the pipe
			await pipe.ConnectAsync();

			// Start the Python script as a subprocess
			var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "python",
					Arguments = pythonScriptPath,
					UseShellExecute = false,
					RedirectStandardInput = false,
					RedirectStandardOutput = false,
					RedirectStandardError = false,
					CreateNoWindow = true
				}
			};

			process.Start();

			// Write the data to the pipe asynchronously
			float[] array = { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f, 6.0f };
			foreach (var value in array)
			{
				await pipe.WriteAsync(BitConverter.GetBytes(value), 0, sizeof(float));
			}

			// Read the result from the pipe asynchronously
			byte[] buffer = new byte[sizeof(int)];
			await pipe.ReadAsync(buffer, 0, sizeof(int));

			// Convert the result to an integer
			int result = BitConverter.ToInt32(buffer, 0);

			// Print the result
			Debug.Log("Result: " + result);

			// Stop the process
			process.Kill();
		}
	}
}