using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GachiSnailMusic.Utils
{
    internal class Bundle
    {
        public static AssetBundle LoadBundle(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Resource ID cannot be null or whitespace.", nameof(name));

            using Stream stream = typeof(Plugin).Assembly.GetManifestResourceStream($"{Plugin.ProjectNamespace}.Resources.{name}");

            if (stream != null)
            {
                byte[] bytes;
                using (MemoryStream ms = new())
                {
                    stream.CopyTo(ms);
                    bytes = ms.ToArray();
                }

                AssetBundle bundle = AssetBundle.LoadFromMemory(bytes);
                return bundle ?? throw new InvalidOperationException($"Failed to load AssetBundle from resource '{name}'.");
            }

            throw new ArgumentException($"Resource '{name}' not found in assembly");
        }
    }
}
