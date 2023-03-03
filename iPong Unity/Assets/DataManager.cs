using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Argyle.UnclesToolkit.Base;
using Argyle.UnclesToolkit.Extensions;
using Argyle.UnclesToolkit.Jobs;
using UnityEngine;
using UnityEngine.Serialization;

public class DataManager : ArgyleComponent
{
	public float _captureDelay = .1f;
    public float _saveDelay = 1f;
    
    string _dataString = String.Empty;
    private string _fileName = $"PongData {DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day} " +
                               $"{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}.tsv";

    
    
    
    private string FullPath(string filename)
    {
	    var path = Path.Combine("Data");
	    if (!Directory.Exists(path))
		    Directory.CreateDirectory(path);
	    return Path.Combine(path, filename);
    }
    
    [SerializeField] private Paddle p1;
    [SerializeField] private Paddle p2;

    private float previousPaddleVelocity = 0;
    private float timeAsIs = 0;
    private float timeLastCapture = 0;
    public PlayData LastData { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
	    captureLoop.Add(Capture);
	    SaveLoop.Add(Save);
	    
	    captureLoop.LoopDuration = _captureDelay;
	    SaveLoop.LoopDuration = _saveDelay;

	    _dataString = new PlayData(p1, p2, 0).ToTsvHeader();
	    timeLastCapture = Time.realtimeSinceStartup;
    }

    private void OnEnable()
    {
	    captureLoop.StartLoop();
	    SaveLoop.StartLoop();
    }

    private void OnDisable()
    {
	    captureLoop.StopLoop();
	    SaveLoop.StopLoop();
    }

    private Looper captureLoop = new Looper("Capture Loop", .1f);
    private Looper SaveLoop = new Looper("Save Loop", 1);


    private void Capture()
    {
	    if (previousPaddleVelocity == p1._rb.velocity.x)
		    timeAsIs += Time.realtimeSinceStartup - timeLastCapture;
	    else
		    timeAsIs = 0;
	    timeLastCapture = Time.realtimeSinceStartup;
	    
	    LastData = new PlayData(p1, p2, timeAsIs);
	    _dataString += LastData.ToTsvLine();
	    previousPaddleVelocity = p1._rb.velocity.x;
    }

    private async void Save()
    {
	    await StoreAsync(_dataString, _fileName);
    }
    public async Task StoreAsync(string thing, string fileName)
    {
	    using (FileStream stream = new FileStream(FullPath(fileName), FileMode.Create, FileAccess.Write, FileShare.None))
	    {
		    using (StreamWriter writer = new StreamWriter(stream))
		    {
			    await writer.WriteAsync(thing);
		    }
	    }
    }

}
