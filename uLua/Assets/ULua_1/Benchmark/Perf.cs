﻿using UnityEngine;
using System.Collections;
using LuaInterface;
public class Perf : MonoBehaviour
{
    LuaScriptMgr l = null;
    void Start()
    {
        //Screen.SetResolution(800, 480, true);

        TextAsset asset = Resources.Load<TextAsset>("perf");
        Profiler.BeginSample("init");
        l = new LuaScriptMgr();
        l.Start();
        l.DoString(asset.ToString());
        Profiler.EndSample();
        l.GetLuaFunction("main").Call();

        Application.logMessageReceived += this.log;
	}

	string logText = "";
	void log(string cond, string trace, LogType lt)
	{
		logText += cond;
		logText += "\n";
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(10, 10, 120, 50), "Test1"))
		{
            Profiler.BeginSample("test1");
            l.GetLuaFunction("test1").Call();
            Profiler.EndSample();
		}

		if (GUI.Button(new Rect(10, 100, 120, 50), "Test2"))
		{
            Profiler.BeginSample("test2");
            l.GetLuaFunction("test2").Call();
            Profiler.EndSample();
        }

		if (GUI.Button(new Rect(10, 200, 120, 50), "Test3"))
		{
            Profiler.BeginSample("test3");
            l.GetLuaFunction("test3").Call();
            Profiler.EndSample();
        }

		if (GUI.Button(new Rect(10, 300, 120, 50), "Test4"))
		{
            Profiler.BeginSample("test4");
            l.GetLuaFunction("test4").Call();
            Profiler.EndSample();
        }

		if (GUI.Button(new Rect(200, 10, 120, 50), "Test5"))
		{
            Profiler.BeginSample("test5");
            l.GetLuaFunction("test5").Call();
            Profiler.EndSample();
        }

        if (GUI.Button(new Rect(200, 100, 120, 50), "Test6 jit"))
        {
            Profiler.BeginSample("test6");
            l.GetLuaFunction("test6").Call();
            Profiler.EndSample();
        }

		if (GUI.Button(new Rect(200, 200, 120, 50), "Test6 non-jit"))
		{
			Profiler.BeginSample("test7");
            l.GetLuaFunction("test7").Call();
            Profiler.EndSample();
        }
        
		GUI.Label(new Rect(400, 200, 300, 150), logText);
	}
}
