using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Il2CppSystem;
using Il2CppSystem.IO;
using Il2CppSystem.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace CitraClient.Utils
{
	public static class Serialization
	{
		public static byte[] ToByteArray(Il2CppSystem.Object obj)
		{
			if (obj == null)
			{
				return null;
			}
			Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			Il2CppSystem.IO.MemoryStream memoryStream = new Il2CppSystem.IO.MemoryStream();
			binaryFormatter.Serialize(memoryStream, obj);
			return memoryStream.ToArray();
		}

		public static byte[] Vector3ToBytes(Vector3 vector3)
		{
			byte[] array = new byte[12];
			System.Buffer.BlockCopy(System.BitConverter.GetBytes(vector3.x), 0, array, 0, 4);
			System.Buffer.BlockCopy(System.BitConverter.GetBytes(vector3.y), 0, array, 4, 4);
			System.Buffer.BlockCopy(System.BitConverter.GetBytes(vector3.z), 0, array, 8, 4);
			return array;
		}

		public static byte[] IntToBytes(int integer)
		{
			return System.BitConverter.GetBytes(integer);
		}

		public static byte[] FloatToBytes(float floating)
		{
			return System.BitConverter.GetBytes(floating);
		}

		public static byte[] ToByteArray(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
			binaryFormatter.Serialize(memoryStream, obj);
			return memoryStream.ToArray();
		}

		public static T FromByteArray<T>(byte[] data)
		{
			if (data == null)
			{
				return default(T);
			}
			System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			using System.IO.MemoryStream serializationStream = new System.IO.MemoryStream(data);
			return (T)binaryFormatter.Deserialize(serializationStream);
		}

		public static T IL2CPPFromByteArray<T>(byte[] data)
		{
			if (data == null)
			{
				return default(T);
			}
			Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			Il2CppSystem.IO.MemoryStream serializationStream = new Il2CppSystem.IO.MemoryStream(data);
			return (T)(object)binaryFormatter.Deserialize(serializationStream);
		}

		public static T FromIL2CPPToManaged<T>(Il2CppSystem.Object obj)
		{
			return FromByteArray<T>(ToByteArray(obj));
		}

		public static T FromManagedToIL2CPP<T>(object obj)
		{
			return IL2CPPFromByteArray<T>(ToByteArray(obj));
		}
	}
}
