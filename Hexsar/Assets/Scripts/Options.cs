using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{

	private bool Music=true;
	private bool SFX=true;
	private float MusicVolume=1f;
	private float SFXVolume=1f;

	public bool GetMusic()
	{
		return Music;
	}
	
	public bool GetSFX()
	{
		return SFX;
	}

	public void ToggleMusic()
	{
		if (Music)
			Music=false;
		else
			Music=true;
	}
	
	public void ToggleSFX()
	{
		if (SFX)
			SFX=false;
		else
			SFX=true;
	}
	
	public void SetMusicVolume(Slider Vol)
	{
		MusicVolume=Vol.value;
		
	}
	
	public void SetSFXVolume(Slider Vol)
	{
		SFXVolume=Vol.value;
		
	}
	
	public float GetMusicVolume()
	{
		return MusicVolume;	
	}
	
	public float GetSFXVolume()
	{
		return SFXVolume;
	}
	
	public void Reset()
	{
		Music=true;
		SFX=true;
	}

	public void Confirm()//might want to allow the score to be passed here so it can be displayed
	{
		//write some info to a text file
		string Info=((Music)?"1":"0")+" "+((SFX)?"1":"0")+" "+Convert.ToString(MusicVolume)+" "+Convert.ToString(SFXVolume)+" ";
		if (!Directory.Exists("Save Info"))
			Directory.CreateDirectory("Save Info");
		FileStream options = File.Create("Save Info/Options.txt");
		options.Close();
		File.WriteAllText("Save Info/Options.txt",Info);
	}
	
	public void Load()
	{
		string Info;
		try
		{
			Info = File.ReadAllText("Save Info/Options.txt");
		}
		catch /*(DirectoryNotFoundException)*/
		{
			Confirm();
			Info = File.ReadAllText("Save Info/Options.txt");
		}
		/*catch (IsolatedStorageException)
		{
			Confirm();
			Info = File.ReadAllText("Save Info/Options.txt");
		}*/
		int SpaceCount=0;
		string CurrentInfo="";
		for (int x=0;x<Info.Length;x++)
		{
			
			if (Info[x].ToString()!=" ")
				CurrentInfo+=Info[x].ToString();
			else
			{
				if (SpaceCount==0)
				{
					if(CurrentInfo=="1")
						Music=true;
					else
						Music=false;
				}
				else if (SpaceCount==1)
				{
					if(CurrentInfo=="1")
						SFX=true;
					else
						SFX=false;
				}
				else if (SpaceCount==2)
				{
					MusicVolume=float.Parse(CurrentInfo);
				}
				else if (SpaceCount==3)
				{
					SFXVolume=float.Parse(CurrentInfo);
				}
				
				
				SpaceCount+=1;
				CurrentInfo="";
			}
		}
	}
	
	public void Exit()
	{
		Application.Quit();
	}
}

