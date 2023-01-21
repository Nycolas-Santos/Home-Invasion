using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace GameCreator.Editor.Common.Versions
{
    internal static class VersionsManager
    {
        // CONSTANTS: -----------------------------------------------------------------------------
        
        private const string URI = "https://docs.gamecreator.io/assets/public/releases.json";

        private const string KEY_HASH = "gc:versions-latest-hash";
        private const string KEY_LATEST = "gc:versions-latest-data";

        private const string KEY_ASSET = "gc:versions-{0}-data";
        
        // MEMBERS: -------------------------------------------------------------------------------
        
        private static UnityWebRequest RequestLatest;

        // PROPERTIES: ----------------------------------------------------------------------------

        private static bool IsInitialized { get; set; } = false;
        
        public static LatestData Latest { get; private set; }
        public static Dictionary<string, AssetEntry> LatestEntries { get; private set; }
        public static Dictionary<string, string> CurrentEntries { get; private set; }

        // EVENTS: --------------------------------------------------------------------------------

        public static event Action EventChange;

        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public static void Initialize()
        {
            if (IsInitialized) return;
            IsInitialized = true;
            
            Latest = new LatestData();
            LatestEntries = new Dictionary<string, AssetEntry>();

            if (EditorPrefs.HasKey(KEY_LATEST))
            {
                EditorJsonUtility.FromJsonOverwrite(EditorPrefs.GetString(KEY_LATEST), Latest);
                foreach (LatestEntry entry in Latest.List)
                {
                    if (string.IsNullOrEmpty(entry.Id)) continue;

                    string entryKey = string.Format(KEY_ASSET, entry.Id);
                    if (!EditorPrefs.HasKey(entryKey)) continue;

                    AssetEntry jsonEntry = new AssetEntry(State.Ready);
                    EditorJsonUtility.FromJsonOverwrite(EditorPrefs.GetString(entryKey), jsonEntry);
                    
                    LatestEntries.Add(entry.Id, jsonEntry);
                }
                
                Latest.State = State.Ready;
            }
            
            FetchLatest();
        }
        
        // FETCH METHODS: -------------------------------------------------------------------------
        
        private static void FetchLatest()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable) return;
            
            Latest.State = State.Loading;
            
            RequestLatest = UnityWebRequest.Get(URI);
            RequestLatest.SetRequestHeader("ContentType", "application/json");

            UnityWebRequestAsyncOperation operation = RequestLatest.SendWebRequest();
            operation.completed += OnLatestReceive;
        }
        
        private static void FetchAsset(string id, string uri)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable) return;

            UnityWebRequest request = UnityWebRequest.Get(uri);
            request.SetRequestHeader("ContentType", "application/json");
            UnityWebRequestAsyncOperation operation = request.SendWebRequest();
            
            operation.completed += _ =>
            {
                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(request.error);
                    LatestEntries[id] = null;
                    
                    EventChange?.Invoke();
                    return;
                }

                string json = request.downloadHandler.text;
                AssetEntry data = new AssetEntry();

                EditorJsonUtility.FromJsonOverwrite(json, data);
                string entryKey = string.Format(KEY_ASSET, id);
                
                EditorPrefs.SetString(entryKey, EditorJsonUtility.ToJson(data));
                LatestEntries[id] = data;
                
                EventChange?.Invoke();
            };
        }
        
        // PRIVATE METHODS: -----------------------------------------------------------------------
        
        private static void OnLatestReceive(AsyncOperation asyncOperation)
        {
            if (RequestLatest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning(RequestLatest.error);

                Latest.State = State.Error;
                LatestEntries.Clear();
                
                EventChange?.Invoke();
                return;
            }

            string json = RequestLatest.downloadHandler.text;
            LatestData data = new LatestData(State.Ready);
            EditorJsonUtility.FromJsonOverwrite(json, data);

            string dataJson = EditorJsonUtility.ToJson(data, false);
            EditorPrefs.SetString(KEY_LATEST, dataJson);

            int currentHash = EditorPrefs.GetInt(KEY_HASH, 0);
            if (currentHash == json.GetHashCode()) return;
            
            EditorPrefs.SetInt(KEY_HASH, currentHash);

            Latest.State = State.Ready;
            LatestEntries.Clear();
            
            foreach (LatestEntry entry in data.List)
            {
                LatestEntries.Add(entry.Id, new AssetEntry(State.Loading));
            }
            
            EventChange?.Invoke();
            
            foreach (LatestEntry entry in data.List)
            {
                FetchAsset(entry.Id, entry.Path);
            }
        }
    }
}