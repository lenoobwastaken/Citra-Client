using System;
using System.Linq;
using CitraClient.Modules.Base;
using UnityEngine;
using UnityEngine.Profiling;

namespace CitraClient.Utils.UI
{
	public class Memory : ModuleBase
	{
		public const int SIZE_MB = 1048576;

		public static int yOffset = 0;

		public static float m_FontSizeBase = 15.175f;

		public static float m_SlicedTimer;

		public static float DeltaTime;

		public static float m_LastAllocSet = -9999f;

		public static float m_LastCollectTime;

		public static float m_LastCollectNum;

		public static float m_CollectDeltaTime;

		public static float m_LastCollectDeltaTime;

		public static long m_UsedMemSize;

		public static long m_MaxUsedMemSize;

		public static long m_CollectMemSize;

		public static long m_AllocRate;

		public static long m_LastAllocMemSize;

		public static string m_StrFPS = "0.000";

		public static string m_StrSlicedFPS = "0.000";

		public static bool m_IsShow = true;

		public static string _strMembaseVal;

		public static bool m_IsShowSubMemory = true;

		public static bool m_IsShowGC = true;

		public static bool m_IsShowSystem = false;

		public static bool m_IsShowEditor = true;

		public int xOffset = 0;

		public static string source { get; private set; }

		public static string Str10 { get; private set; }

		private static void MemoryGUI()
		{
			DisplayFPS();
			ProfileStats();
			if (m_IsShow && Application.isPlaying)
			{
				DrawStats();
			}
		}

		private static void OnUpdateTime()
		{
			DeltaTime += (Time.deltaTime - DeltaTime) * 0.1f;
		}

		private static void DisplayFPS()
		{
			int width = Screen.width;
			int height = Screen.height;
			GUIStyle gUIStyle = new GUIStyle();
			Rect position = new Rect(0f, 0f, width, (float)(height * 2) / 100f);
			gUIStyle.alignment = TextAnchor.UpperLeft;
			gUIStyle.fontSize = height * 2 / 100;
			gUIStyle.normal.textColor = new Color(0f, 0f, 0.5f, 1f);
			float num = DeltaTime * 1000f;
			float num2 = 1f / DeltaTime;
			string text = $"{num:0.0} ms ({num2:0.} fps)";
			UnityEngine.GUI.Label(position, text, gUIStyle);
		}

		public static void ProfileStats()
		{
			int num = GC.CollectionCount(0);
			if (Math.Abs(m_LastCollectNum - (float)num) > (float)num)
			{
				m_LastCollectNum = num;
				m_CollectDeltaTime = Time.realtimeSinceStartup - m_LastCollectTime;
				m_LastCollectTime = Time.realtimeSinceStartup;
				m_LastCollectDeltaTime = Time.deltaTime;
				m_CollectMemSize = m_UsedMemSize;
			}
			m_UsedMemSize = GC.GetTotalMemory(forceFullCollection: false);
			if (m_UsedMemSize > m_MaxUsedMemSize)
			{
				m_MaxUsedMemSize = m_UsedMemSize;
			}
			if (Time.realtimeSinceStartup - m_LastAllocSet > 0.3f)
			{
				long num2 = m_UsedMemSize - m_LastAllocMemSize;
				m_LastAllocMemSize = m_UsedMemSize;
				m_LastAllocSet = Time.realtimeSinceStartup;
				if (num2 >= 0)
				{
					m_AllocRate = num2;
				}
			}
			m_StrFPS = (1f / Time.deltaTime).ToString("0.000");
			m_SlicedTimer -= Time.deltaTime;
			float slicedTimer = m_SlicedTimer;
			float num3 = slicedTimer;
			if (num3 < 0f)
			{
				m_SlicedTimer = 0.5f;
				m_StrSlicedFPS = m_StrFPS;
			}
		}

		public static void DrawStats()
		{
			string text = m_StrFPS + " FPS (" + m_StrSlicedFPS + ")\n";
			string text2 = "Used\nMax Used\nUsed Heap(Unity)\nMono Used\nMono Heap\n";
			string text3 = $"{(float)((double)m_UsedMemSize / 1048576.0):0.00} MB\n{(float)((double)m_MaxUsedMemSize / 1048576.0):0.00} MB\n{(float)((double)Profiler.usedHeapSize / 1048576.0):0.00} MB\n{(float)((double)Profiler.GetMonoUsedSize() / 1048576.0):0.00} MB\n{(float)((double)Profiler.GetMonoHeapSize() / 1048576.0):0.00} MB\n";
			string text4 = "";
			string text5 = "";
			if (m_IsShowSubMemory)
			{
				text4 += "Total Alloc\nUnused-Reserved\nReserved\n";
				text5 = $"{(float)((double)Profiler.GetTotalAllocatedMemoryLong() / 1048576.0):0.00} MB\n{(float)((double)Profiler.GetTotalUnusedReservedMemoryLong() / 1048576.0):0.00} MB\n{(float)((double)Profiler.GetTotalReservedMemoryLong() / 1048576.0):0.00} MB\n";
			}
			string text6 = "";
			string text7 = "";
			if (m_IsShowGC)
			{
				text6 += "Allocation Rate\nLast Collection Delta\nCollection Freq\n";
				text7 = $"{(float)((double)m_AllocRate / 1048576.0):0.00} MB\n{m_LastCollectDeltaTime:0.000} s ({(float)(1.0 / (double)m_LastCollectDeltaTime):0.0} fps)\n{m_CollectDeltaTime:0.00} s\n";
			}
			string text8 = "";
			string text9 = "";
			if (m_IsShowSystem)
			{
				text8 += "Graphic\nSystem\n";
				text9 = $"{SystemInfo.graphicsMemorySize} MB\n{SystemInfo.systemMemorySize} MB\n";
			}
			source = text2 + text4 + text6 + text8;
			Str10 = "\n" + text3 + text5 + text7 + text9;
			int num = source.Count((char c) => c.Equals('\n'));
		}

		public override void Update()
		{
			OnUpdateTime();
		}

		public override void GUI()
		{
			MemoryGUI();
		}
	}
}
