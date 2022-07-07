using System;
using System.Runtime.InteropServices;

namespace CitraClient.Utils
{
	public class ConsoleUtils
	{
		public static void LogBase(ConsoleColor col, string header, string text)
		{
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("[");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("Citra");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("] ");
			Console.ForegroundColor = col;
			Console.Write("[" + header + "] ");
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(text + "\n");
		}

		private static void WriteToConsole(ConsoleColor c1, string s1, ConsoleColor c2, string clientName, ConsoleColor c3, string s2, ConsoleColor c4, string s3, ConsoleColor c5, string logType, ConsoleColor c6, string s4, ConsoleColor c7, string logContent)
		{
			Console.ForegroundColor = c1;
			string value = DateTime.Now.ToString("[h:mm:ss.ms] ");
			Console.Write(value);
			Console.Write(s1);
			Console.ForegroundColor = c2;
			Console.Write(clientName);
			Console.ForegroundColor = c3;
			Console.Write(s2);
			Console.ForegroundColor = c4;
			Console.Write(s3);
			Console.ForegroundColor = c5;
			Console.Write(logType);
			Console.ForegroundColor = c6;
			Console.Write(s4);
			Console.ForegroundColor = c7;
			Console.WriteLine(logContent);
			Console.ResetColor();
		}

		public static void OnLogInfo(string logContent)
		{
			WriteToConsole(ConsoleColor.Magenta, "[", ConsoleColor.Cyan, "CitraClient", ConsoleColor.Magenta, "]", ConsoleColor.Magenta, " [", ConsoleColor.Cyan, "INFO", ConsoleColor.Magenta, "] ", ConsoleColor.White, logContent);
		}

		public static void OnLogWarn(string logContent)
		{
			WriteToConsole(ConsoleColor.Magenta, "[", ConsoleColor.Cyan, "CitraClient", ConsoleColor.Magenta, "]", ConsoleColor.Yellow, " [", ConsoleColor.Cyan, "WARNING", ConsoleColor.Yellow, "] ", ConsoleColor.White, logContent);
		}

		public static void OnLogSuccess(string logContent)
		{
			WriteToConsole(ConsoleColor.Magenta, "[", ConsoleColor.Cyan, "CitraClient", ConsoleColor.Magenta, "]", ConsoleColor.Green, " [", ConsoleColor.Cyan, "SUCCESS", ConsoleColor.Green, "] ", ConsoleColor.White, logContent);
		}

		public static void OnLogError(string logContent)
		{
			WriteToConsole(ConsoleColor.Magenta, "[", ConsoleColor.Cyan, "CitraClient", ConsoleColor.Magenta, "]", ConsoleColor.Red, " [", ConsoleColor.Cyan, "ERROR", ConsoleColor.Red, "] ", ConsoleColor.White, logContent);
		}

		public static void ClearConsole([Optional] string clearMessage)
		{
			Console.Clear();
			Console.WriteLine(clearMessage);
		}
	}
}
