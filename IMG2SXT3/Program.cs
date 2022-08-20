using System;
using System.IO;

namespace IMG2SXT3
{
	internal class Program
	{
		static void Main(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("Usage: IMG2SXT3.exe <input_file>");
				return;
			}

			if (!File.Exists(args[0]))
			{
				throw new FileNotFoundException("Input file was not found or access denied.");
			}

			byte[] data;
			byte[] inFile = File.ReadAllBytes(args[0]);

			using (BinaryReader br = new BinaryReader(new MemoryStream(inFile)))
			{
				int size = (int)br.BaseStream.Length;
				br.BaseStream.Seek(0x70, SeekOrigin.Begin);
				data = br.ReadBytes(size - 0x70);
			}

			string targetPath = null;
			if (Path.GetExtension(args[0]) == ".IDP0")
			{
				targetPath = Path.GetFullPath(args[0]).Replace(".IDP0", ".txs");
			}
			else if (Path.GetExtension(args[0]) == ".img")
			{
				targetPath = Path.GetFullPath(args[0]).Replace(".img", ".txs");
			}

			File.WriteAllBytes(targetPath, data);
		}
	}
}
