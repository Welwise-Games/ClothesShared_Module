using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MainHub.Modules.WelwiseClothesSharedModule.Editor.Shared.Scripts
{
    public class ConfigsCopier : AssetPostprocessor
    {
        private static string _packageNamePrefix = "com.welwise.clothessharedmodule";
        private static string _assetsPath = "Assets/Plugins/WelwiseClothesSharedModule";

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            var packagePath = FindPackagePath(_packageNamePrefix);
            
            if (string.IsNullOrEmpty(packagePath))
            {
                Debug.LogWarning($"Package with prefix {_packageNamePrefix} not found.");
                return;
            }

            foreach (var asset in importedAssets)
            {
                if (asset.StartsWith(packagePath))
                {
                    CopyFilesFromPackage(packagePath);
                    break;
                }
            }
        }

        private static string FindPackagePath(string prefix)
        {
            var packagesDir = Path.Combine(Application.dataPath, "../Packages");
            if (!Directory.Exists(packagesDir))
                return null;

            var dirs = Directory.GetDirectories(packagesDir);
            var packageDir = dirs.FirstOrDefault(d => Path.GetFileName(d).StartsWith(prefix));

            if (packageDir == null) return null;
            var relativePath = "Packages/" + Path.GetFileName(packageDir);
            return relativePath;
        }

        private static void CopyFilesFromPackage(string packagePath)
        {
            if (!Directory.Exists(_assetsPath))
            {
                Directory.CreateDirectory(_assetsPath);
            }

            var fullPackagePath = Path.Combine(Application.dataPath, "../", packagePath);

            var oldFiles = Directory.GetFiles(_assetsPath, "*.*", SearchOption.AllDirectories);
            foreach (var oldFile in oldFiles)
            {
                if (!oldFile.EndsWith(".meta"))
                {
                    var metaFile = oldFile + ".meta";
                    if (File.Exists(metaFile))
                        AssetDatabase.DeleteAsset(metaFile);
                    AssetDatabase.DeleteAsset(oldFile);
                }
            }

            var files = Directory.GetFiles(fullPackagePath, "*.txt", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var relativePath = file.Substring(fullPackagePath.Length + 1).Replace("\\", "/");
                var destPath = Path.Combine(_assetsPath, relativePath).Replace("\\", "/");

                var destDir = Path.GetDirectoryName(destPath);
                if (!Directory.Exists(destDir))
                    Directory.CreateDirectory(destDir);

                var success = AssetDatabase.CopyAsset(packagePath + "/" + relativePath, destPath);
                if (success)
                    Debug.Log($"Copied {relativePath} to {destPath}");
                else
                    Debug.LogWarning($"Failed to copy {relativePath}");
            }

            AssetDatabase.Refresh();
        }
    }
}