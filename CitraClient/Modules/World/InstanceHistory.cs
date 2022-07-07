using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CitraClient.GUI.QM.Console;
using CitraClient.Modules.Base;
using CitraClient.Utils;
using CitraClient.Utils.Managers;
using VRC.Core;

namespace CitraClient.Modules.World
{
	public class InstanceHistory : ModuleBase
	{
		private static readonly string path = FileManager.InstanceHistoryFile;

		public static List<(string, string)> Instances = new List<(string, string)>();

		private ApiWorldInstance _currentInstance;

		private Action<ApiWorldInstance> _onWorldInstance = delegate
		{
		};

		private Type _roomManagerType;

		private void Initialize()
		{
			_onWorldInstance = (Action<ApiWorldInstance>)Delegate.Combine(_onWorldInstance, new Action<ApiWorldInstance>(OnWorldInstance));
			if (File.Exists(path))
			{
				Instances = (from f in File.ReadAllLines(path)
					select f.Split('#') into f
					select (f[0], f[1])).ToList();
				while (Instances.Count > 20)
				{
					Instances.RemoveAt(0);
				}
			}
			else
			{
				File.Create(path);
			}
		}

		private IEnumerator GetWorldInstance()
		{
			_currentInstance = null;
			while (_currentInstance == null)
			{
				_currentInstance = (ApiWorldInstance)(_roomManagerType?.GetProperty("field_Internal_Static_ApiWorldInstance_0", BindingFlags.Static | BindingFlags.Public)?.GetValue(null));
				yield return null;
			}
			OnWorldInstance(_currentInstance);
		}

		private static void OnWorldInstance(ApiWorldInstance world)
		{
			(string, string) tuple = (world.world.name + ": " + world.name, world.id);
			if (Instances.Count > 0)
			{
				(string, string) tuple2 = Instances[Instances.Count - 1];
				(string, string) tuple3 = tuple;
				if (tuple2.Item1 == tuple3.Item1 && tuple2.Item2 == tuple3.Item2)
				{
					return;
				}
			}
			Instances.Add(tuple);
			while (Instances.Count > 20)
			{
				Instances.RemoveAt(0);
			}
			File.WriteAllLines(path, Instances.Select(((string, string) f) => f.Item1 + "#" + f.Item2));
		}

		public override void Start()
		{
			_roomManagerType = AppDomain.CurrentDomain.GetAssemblies().First((Assembly f) => f.GetName().Name == "Assembly-CSharp").GetExportedTypes()
				.FirstOrDefault((Type f) => (from f1 in f.GetProperties()
					select f1.Name).Any((string f1) => f1 == "field_Private_Static_Dictionary_2_Int32_PortalInternal_0"));
			if (_roomManagerType == null)
			{
				ConsoleUtils.OnLogError("Failed to find the RoomManager type");
			}
			Initialize();
		}

		public static void ClearInstanceHistory()
		{
			ApiWorldInstance worldInstance = WorldUtils.GetWorldInstance();
			(string, string) item = (worldInstance.world.name + ": " + worldInstance.name, worldInstance.id);
			int? num = Instances?.Count();
			if (File.Exists(FileManager.InstanceHistoryFile))
			{
				File.Create(FileManager.InstanceHistoryFile);
			}
			Instances = new List<(string, string)> { item };
			CenterConsole.Log(CenterConsole.LogsType.INFO, $"Removed [{num}] from the Instance History.");
		}

		public override void OnSceneLoad(int buildIndex, string sceneName)
		{
			GetWorldInstance().Start();
		}
	}
}
