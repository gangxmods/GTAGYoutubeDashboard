using System;
using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using Utilla;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using BepInEx.Configuration;
using BuildSafe;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using System.Net;
using System.Drawing;
using System.Text;
using WebSocketSharp;
using UnityEngine.Video;
using Steamworks;
using PlayFab.ProfilesModels;

namespace YoutubeDashboard
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static GameObject YoutubeDashboard;
        public static GameObject BASE;
        public static GameObject VButton1;
        public static GameObject VButton2;
        public static GameObject VButton3;
        public static GameObject VButton4;
        public static GameObject VButton5;
        public static GameObject YTLogo;
        public static GameObject Forwardd;
        public static GameObject Backwardd;
        public static GameObject PauseBTN;
        public static GameObject PlayBTN;
        public static GameObject MuteBTN;
        public static GameObject VPH;
        public static GameObject VPH2;
        public static GameObject VPH3;
        public static GameObject VPH4;
        public static GameObject VPH5;
        public static GameObject YTPlayerTXT;
        public static GameObject PFP;
        public static GameObject F;
        public static GameObject B;
        public static GameObject MainTXT;
        public static GameObject divider;
        public static GameObject pauseLogo;
        public static GameObject playLogo;
        public static GameObject MuteLogo;
        public static GameObject FastForward;
        public static GameObject Rewind;
        public static GameObject FastForwardLogo;
        public static GameObject RewindLogo;



        public static bool inModded = false;
        public static bool doonce = false;
        public static bool doOnce = false;
        public static bool CD = false;
        public static bool CD2 = false;
        public static Plugin instance;
        public static List<string> YTVideoNames = new List<string>();
        public static List<string> YTVideoIds = new List<string>();
        public static int YTVidIndex = 0;
        public static string ChannelName;
        public static string YTPFPLink;
        public static string SubscriberCount;
        public static string TotalVideos;
        public static string VidAMT;
        public static string TotalViews;
        public static string TotalVIDS;
        public static string AvrgViews;
        public static string PFPP;
        public static List<JToken> videos = new List<JToken>();
        private static string apiKey;
        private static string continuationToken;
        private static string clickTrackingParams;
        private static JToken client;
        public static JObject data;
        private static string clientVersion;
        public static int count;
        public static ConfigEntry<string> username;
        public static bool mute = false;
        public static bool isHome = true;

        void Start()
        {
            /* A lot of Gorilla Tag systems will not be set up when start is called */
            /* Put code in OnGameInitialized to avoid null references */
            username = Config.Bind<string>(
            "General",
            "Username",
            "NONE SET",
            "The Username That Will Appear Ingame");


            string url = $"https://youtube.com/@{username.Value}";
            
            instance = this;
            LoadAssets();
            instance.StartCoroutine(GetInfo($"{url}/about"));
            instance.StartCoroutine(LoadYTProfileCoroutine($"{url}/videos?view=0&flow=grid"));
            Utilla.Events.GameInitialized += OnGameInitialized;
        }

        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            /* Activate your mod here */
            /* This code will run regardless of if the mod is enabled */
            inModded = true;
        }


        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled */
            inModded = false;
        }

        void OnEnable()
        {
            /* Set up your mod here */
            /* Code here runs at the start and whenever your mod is enabled */
            HarmonyPatches.ApplyHarmonyPatches();
        }

        void OnDisable()
        {
            /* Undo mod setup here */
            /* This provides support for toggling mods with ComputerInterface, please implement it :) */
            /* Code here runs whenever your mod is disabled (including if it disabled on startup) */
            HarmonyPatches.RemoveHarmonyPatches();
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            //board
            YoutubeDashboard = Instantiate(YoutubeDashboard);
            YoutubeDashboard.SetActive(true);
            YoutubeDashboard.gameObject.transform.localPosition = new Vector3(-73.6326f, 10.8116f, - 84.9893f);
            YoutubeDashboard.gameObject.transform.rotation = Quaternion.Euler(0f, 337.7857f, 0f);
            YoutubeDashboard.gameObject.transform.localScale = new Vector3(3f, 3f, 3f);


            //buttons ETC
            BASE = GameObject.Find("BASE");
            VButton1 = GameObject.Find("BTN");
            VButton2 = GameObject.Find("BTN2");
            VButton3 = GameObject.Find("BTN3");
            VButton4 = GameObject.Find("BTN4");
            VButton5 = GameObject.Find("BTN5");
            YTLogo = GameObject.Find("YTLogo");
            Forwardd = GameObject.Find("Forward");
            Backwardd = GameObject.Find("Backward");
            PauseBTN = GameObject.Find("PauseBTN");
            PlayBTN = GameObject.Find("PlayBTN");
            MuteBTN = GameObject.Find("MuteBTN");
            FastForward = GameObject.Find("FastForward");
            Rewind = GameObject.Find("Rewind");
            VPH = GameObject.Find("VPH");
            VPH2 = GameObject.Find("VPH2");
            VPH3 = GameObject.Find("VPH3");
            VPH4 = GameObject.Find("VPH4");
            VPH5 = GameObject.Find("VPH5");
            YTPlayerTXT = GameObject.Find("YTPlayerTXT");
            PFP = GameObject.Find("PFP");
            F = GameObject.Find(">");
            B = GameObject.Find("<");
            MainTXT = GameObject.Find("MainTXT");
            divider = GameObject.Find("divider");
            pauseLogo = GameObject.Find("pauseLogo");
            playLogo = GameObject.Find("playLogo");
            MuteLogo = GameObject.Find("MuteLogo");
            FastForwardLogo = GameObject.Find("FastForwardLogo");
            RewindLogo = GameObject.Find("RewindLogo");
            BASE.gameObject.transform.localScale = new Vector3(-BASE.gameObject.transform.localScale.x, BASE.gameObject.transform.localScale.y, BASE.gameObject.transform.localScale.z);
            LoadButtons();
            LoadVideoTitles();

            //SETTING
            if (username.Value.ToString() == "NONE SET")
            {
                MainTXT.GetComponent<TextMesh>().text = $"Set Username In BepInEx \nConfig File";
            }
            else
            {
                MainTXT.GetComponent<TextMesh>().text = $"CHANNEL: {username.Value.ToString()}\nSubscriber Count: {SubscriberCount.Replace("subscribers", "")}\nTotal Videos: {TotalVideos:N0}\nTotal Views: {TotalViews:N0}\nAverage Views: {AvrgViews:N0}";
                Material pfpMat = CustomImage(PFPP);
                PFP.GetComponent<Renderer>().material = pfpMat;
            }
        }

        public static IEnumerator GetInfo(string link)
        {
            UnityWebRequest www = UnityWebRequest.Get(link);
            yield return www.SendWebRequest();

            string initialHtml = www.downloadHandler.text;

            string subscriberCountPattern = "\"subscriberCountText\":\"([^\"]*)";
            string viewCountPattern = "\"viewCountText\":\"([^\"]*)";
            string videoCountPattern = "\"videoCountText\":\"([^\"]*)\"";

            Match subscriberMatch = Regex.Match(initialHtml, subscriberCountPattern);
            Match viewCountMatch = Regex.Match(initialHtml, viewCountPattern);
            Match videoCountMatch = Regex.Match(initialHtml, videoCountPattern);

            string subscriberCount = subscriberMatch.Success ? subscriberMatch.Groups[1].Value : "Not found";
            string viewCountText = viewCountMatch.Success ? viewCountMatch.Groups[1].Value : "Not found";
            string videoCountText = videoCountMatch.Success ? videoCountMatch.Groups[1].Value : "Not found";

            int totalVideos = 0;
            if (int.TryParse(Regex.Match(videoCountText, @"\d+").Value, out int count))
            {
                totalVideos = count;
            }

            string pfpPattern = "\"avatarViewModel\":{[^}]*\"sources\":\\[\\{\"url\":\"([^\"]+)\"";
            Match pfpMatch = Regex.Match(initialHtml, pfpPattern);
            string pfp = pfpMatch.Success ? pfpMatch.Groups[1].Value : "Not found";
            if (pfp == "Not found")
            {
                Match ifFailMatch = Regex.Match(initialHtml, "width\":48,\"height\":48},{\"url\":\"([^\"]+)\"");
                pfp = ifFailMatch.Success ? ifFailMatch.Groups[1].Value : "Not found";
            }

            string viewsUnparsed = new string(viewCountText.Where(char.IsDigit).ToArray());
            int viewsParsed = int.Parse(viewsUnparsed);
            int avrgviews = totalVideos > 0 ? viewsParsed / totalVideos : 0;

            SubscriberCount = subscriberCount;
            TotalViews = viewsParsed.ToString("N0");
            TotalVideos = totalVideos.ToString();
            PFPP = pfp;
            AvrgViews = avrgviews.ToString("N0");

            Debug.Log($"Subscriber Count: {subscriberCount}");
            Debug.Log($"Total View Count: {viewsParsed:N0}");
            Debug.Log($"Total Videos: {totalVideos}");
            Debug.Log($"PFP: {pfp}");
            Debug.Log($"Average Views: {avrgviews:N0}");

            //SETTING
            if (username.Value.ToString() == "NONE SET")
            {
                MainTXT.GetComponent<TextMesh>().text = $"Set Username In BepInEx \nConfig File";
            }
            else
            {
                MainTXT.GetComponent<TextMesh>().text = $"CHANNEL: {username.Value.ToString()}\nSubscriber Count: {SubscriberCount.Replace("subscribers", "")}\nTotal Videos: {TotalVideos:N0}\nTotal Views: {TotalViews:N0}\nAverage Views: {AvrgViews:N0}";
                Material pfpMat = CustomImage(PFPP);
                PFP.GetComponent<Renderer>().material = pfpMat;
            }
        }

        public static Material CustomImage(string url)
        {
            Material material = null;
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] array = webClient.DownloadData(url);
                    if (array != null && array.Length != 0)
                    {
                        material = new Material(Shader.Find("GorillaTag/UberShader"));
                        material.shaderKeywords = new string[]
                        {
                            "_USE_TEXTURE"
                        };
                        Texture2D texture2D = new Texture2D(2, 2);
                        bool flag2 = texture2D.LoadImage(array);
                        if (flag2)
                        {
                            material.mainTexture = texture2D;
                            texture2D.Apply();
                        }
                        else
                        {
                            Debug.LogError("Failed: " + url);
                        }
                    }
                    else
                    {
                        Debug.LogError("Empty: " + url);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error" + url + "\n" + ex.Message);
            }
            return material;
        }

        public IEnumerator LoadYTProfileCoroutine(string url)
        {
            bool isFirst = true;

            while (true)
            {
                if (isFirst)
                {
                    var request = UnityWebRequest.Get($"{url}?ucbcb=1");
                    request.SetRequestHeader("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36");
                    request.SetRequestHeader("Accept-Language", "en");
                    yield return request.SendWebRequest();

                    var html = request.downloadHandler.text;

                    var clientMatch = Regex.Match(html, @"\{""client"":\{.*?""deviceExperimentId"":""(.*?)""\}", RegexOptions.Singleline);
                    client = JObject.Parse(clientMatch.Groups[0].Value + "}")["client"];

                    var clientVersionMatch = Regex.Match(html, @"""clientVersion"":""(.*?)""");
                    clientVersion = clientVersionMatch.Groups[1].Value;

                    var apiKeyMatch = Regex.Match(html, @"""innertubeApiKey"":""(.*?)""");
                    apiKey = apiKeyMatch.Groups[1].Value;

                    var dataMatch = Regex.Match(html, @"ytInitialData = (.*?);", RegexOptions.Singleline);
                    data = JObject.Parse(dataMatch.Groups[1].Value);

                    var clickTrackingParamsMatch = Regex.Match(html, @"""continuationItemRenderer"":{""trigger"":""CONTINUATION_TRIGGER_ON_ITEM_SHOWN"",""continuationEndpoint"":{""clickTrackingParams"":""(.*?)""");
                    clickTrackingParams = clickTrackingParamsMatch.Groups[1].Value;

                    var continuationTokenMatch = Regex.Match(html, @"""continuationCommand"":{""token"":""(.*?)""");
                    continuationToken = continuationTokenMatch.Groups[1].Value;

                    isFirst = false;
                }
                else
                {
                    var payload = new
                    {
                        context = new
                        {
                            clickTracking = new { clickTrackingParams = clickTrackingParams },
                            client = client
                        },
                        continuation = continuationToken
                    };
                    string jsonPayload = JsonConvert.SerializeObject(payload);
                    UnityWebRequest request = new UnityWebRequest($"https://www.youtube.com/youtubei/v1/browse?key={apiKey}", "POST")
                    {
                        uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonPayload)),
                        downloadHandler = new DownloadHandlerBuffer()
                    };
                    request.SetRequestHeader("Content-Type", "application/json");
                    yield return request.SendWebRequest();
                    var responseData = request.downloadHandler.text;
                    data = JObject.Parse(responseData);
                    var reg = responseData.Replace(" ", "").Replace("\n", "");
                    var token = Regex.Match(reg, @"\""continuationCommand\"":\{\""token\"":\""([^\""]*)\""");
                    continuationToken = token.Groups[1].Value;
                    var click = Regex.Match(reg, @"\""continuationEndpoint\"":\{\""clickTrackingParams\"":\""([^\""]*)\""");
                    clickTrackingParams = click.Groups[1].Value;
                }

                try
                {
                    foreach (var videoData in data["contents"]["twoColumnBrowseResultsRenderer"]["tabs"][1]["tabRenderer"]["content"]["richGridRenderer"]["contents"])
                    {
                        string videoId = videoData["richItemRenderer"]["content"]["videoRenderer"]["videoId"].ToString();
                        string videoTitle = videoData["richItemRenderer"]["content"]["videoRenderer"]["title"]["runs"][0]["text"].ToString();

                        YTVideoIds.Add(videoId);
                        YTVideoNames.Add(videoTitle);

                       // Debug.Log(videoId);
                       // Debug.Log(videoTitle);
                       // Debug.Log(videoData["richItemRenderer"]["content"]["videoRenderer"]["viewCountText"]["simpleText"]);
                       // Debug.Log(videoData["richItemRenderer"]["content"]["videoRenderer"]["thumbnail"]["thumbnails"][0]["url"]);
                    }
                }
                catch
                {
                    try
                    {
                        foreach (var videoData in data["onResponseReceivedActions"])
                        {
                            foreach (var video in videoData["appendContinuationItemsAction"]["continuationItems"])
                            {
                                string videoId = video["richItemRenderer"]["content"]["videoRenderer"]["videoId"].ToString();
                                string videoTitle = video["richItemRenderer"]["content"]["videoRenderer"]["title"]["runs"][0]["text"].ToString();

                                YTVideoIds.Add(videoId);
                                YTVideoNames.Add(videoTitle);

                               // Debug.Log(videoId);
                               // Debug.Log(videoTitle);
                               // Debug.Log(video["richItemRenderer"]["content"]["videoRenderer"]["viewCountText"]["simpleText"]);
                               // Debug.Log(video["richItemRenderer"]["content"]["videoRenderer"]["thumbnail"]["thumbnails"][0]["url"]);
                            }
                        }
                    }
                    catch
                    {
                        try
                        {
                            foreach (var videoData in data["contents"]["twoColumnBrowseResultsRenderer"]["tabs"][1]["tabRenderer"]["content"]["richGridRenderer"]["contents"])
                            {
                                string videoId = videoData["richItemRenderer"]["content"]["videoRenderer"]["videoId"].ToString();
                                string videoTitle = videoData["richItemRenderer"]["content"]["videoRenderer"]["title"]["runs"][0]["text"].ToString();

                                YTVideoIds.Add(videoId);
                                YTVideoNames.Add(videoTitle);

                               // Debug.Log(videoId);
                               // Debug.Log(videoTitle);
                               // Debug.Log(videoData["richItemRenderer"]["content"]["videoRenderer"]["viewCountText"]["simpleText"]);
                                //Debug.Log(videoData["richItemRenderer"]["content"]["videoRenderer"]["thumbnail"]["thumbnails"][0]["url"]);
                            }
                        }
                        catch
                        {
                        }
                    }
                }

                if (continuationToken.IsNullOrEmpty())
                {
                    Debug.Log("No more continuation tokens. Loading Rest Of Profile");
                    LoadButtons();
                    LoadVideoTitles();
                    instance.StartCoroutine(UpdateHome(true));   
                    break;
                }

                yield return null;
            }
            Debug.Log("Total videos loaded: " + YTVideoIds.Count);
        }
    
        private static string GetHtml(HttpClient client, string url)
        {
            var response = client.GetAsync(url).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        private static string PostJson(HttpClient client, string url, HttpContent content)
        {
            var response = client.PostAsync(url, content).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        private static List<JToken> SearchDict(JToken partial, string searchKey)
        {
            List<JToken> results = new List<JToken>();
            Queue<JToken> stack = new Queue<JToken>();
            stack.Enqueue(partial);

            while (stack.Count > 0)
            {
                var currentItem = stack.Dequeue();
                if (currentItem.Type == JTokenType.Object)
                {
                    foreach (var property in currentItem.Children<JProperty>())
                    {
                        if (property.Name == searchKey)
                        {
                            results.Add(property.Value);
                        }
                        else
                        {
                            stack.Enqueue(property.Value);
                        }
                    }
                }
                else if (currentItem.Type == JTokenType.Array)
                {
                    foreach (var item in currentItem.Children())
                    {
                        stack.Enqueue(item);
                    }
                }
            }
            return results;
        }

        static void PlayVideo(string videoId)
        {
            string[] urlList = new string[] { };
            string[] unsortedList = new string[] { };
            string youtubeVideoId = videoId;
            string url = "https://www.youtube.com/youtubei/v1/player";
            string payload = "{" +
                                "\"videoId\": \"" + youtubeVideoId + "\"," +
                                "\"context\": {" +
                                    "\"client\": {" +
                                        "\"clientName\": \"ANDROID_TESTSUITE\"," +
                                        "\"clientVersion\": \"1.9\"," +
                                        "\"androidSdkVersion\": 30," +
                                        "\"hl\": \"en\"," +
                                        "\"gl\": \"US\"," +
                                        "\"utcOffsetMinutes\": 0" +
                                    "}" +
                                "}" +
                             "}";

            string jsonResponse;
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                jsonResponse = client.UploadString(url, payload);
            }
            string pattern = "\"lengthSeconds\":\\s*\"(\\d+)\"";
            Match match = Regex.Match(jsonResponse, pattern);
            int vidLength = int.Parse(match.Groups[1].Value);
            pattern = "\"author\":\\s*\"([^\"]+)\"";
            match = Regex.Match(jsonResponse, pattern);
            string author = match.Groups[1].Value;
            pattern = "\"title\":\\s*\"([^\"]+)\"";
            match = Regex.Match(jsonResponse, pattern);
            string title = match.Groups[1].Value;
            MatchCollection matches = Regex.Matches(jsonResponse, "\"url\":\\s+\"([^\"]+)\"");
            foreach (Match m in matches)
            {
                urlList = urlList.Append(m.Groups[1].Value).ToArray();
            }
            foreach (string u in urlList)
            {
                pattern = "mime=([^&]+)";
                Match match1 = Regex.Match(u, pattern);

                pattern = "itag=([^&]+)";
                Match match2 = Regex.Match(u, pattern);

                if (match1.Success && match2.Success)
                {
                    unsortedList = unsortedList.Append(match1.Groups[1].Value + "-" + match2.Groups[1].Value).ToArray();
                }
            }
            unsortedList = unsortedList.OrderByDescending(x =>
            {
                if (x.Contains("video%2Fmp4"))
                {
                    return 100;
                }
                else if (x.Contains("audio%2Fmp4"))
                {
                    return 50;
                }
                else if (x.Contains("audio%2Fwebm"))
                {
                    return 40;
                }
                else if (x.Contains("video%2Fwebm"))
                {
                    return 30;
                }
                else
                {
                    return 0;
                }
            }).ToArray();
            string choice = unsortedList.FirstOrDefault(x => x.Contains("video%2Fmp4"));
            if (choice == null)
            {
                choice = unsortedList.FirstOrDefault();
            }
            string word = choice.Split('-')[0];
            string num = choice.Split('-')[1];
            pattern = @"\b" + Regex.Escape(word) + @"\b";
            string pattern2 = "itag=" + Regex.Escape(num);
            string videoUrl = "";
            foreach (string u in urlList)
            {
                if (Regex.IsMatch(u, pattern) && Regex.IsMatch(u, pattern2))
                {
                    videoUrl = u;
                    break;
                }
            }
            VideoPlayer videoPlayer = BASE.GetComponent<VideoPlayer>();
            videoPlayer.url = videoUrl;
            videoPlayer.Play();

        }
        public static Dictionary<string, object> ExtractVideoInfo(JToken videoData)
        {
            var title = (string)videoData["title"]?["runs"]?[0]?["text"] ?? "No Title";
            var viewsText = (string)videoData["viewCountText"]?["simpleText"] ?? "No Views";
            var views = viewsText != "No Views" ? int.Parse(viewsText.Replace(",", "").Split()[0]) : 0;
            var firstThumbnail = (string)videoData["thumbnail"]?["thumbnails"]?[0]?["url"] ?? "No Thumbnail";
            var videoId = (string)videoData["videoId"] ?? "No Video ID";
            return new Dictionary<string, object>
            {
                { "title", title },
                { "views", views },
                { "first_thumbnail", firstThumbnail },
                { "video_id", videoId }
            };
        }
        public static void LoadAssets()
        {
            AssetBundle bundle = LoadAssetBundle("YoutubeDashboard.yt");
            YoutubeDashboard = bundle.LoadAsset<GameObject>("YT");
        }

        public static void LoadButtons()
        {
            VButton1.AddComponent<VButton1>();
            VButton2.AddComponent<VButton2>();
            VButton3.AddComponent<VButton3>();
            VButton4.AddComponent<VButton4>();
            VButton5.AddComponent<VButton5>();
            Forwardd.AddComponent<Forward>();
            Backwardd.AddComponent<Backward>();
            PauseBTN.AddComponent<Pause>();
            PlayBTN.AddComponent<Play>();
            MuteBTN.AddComponent<Mute>();
            FastForward.AddComponent<FastForward>();
            Rewind.AddComponent<Rewind>();
            YTLogo.AddComponent<YTlogo>();
            BASE.AddComponent<VideoPlayer>();
            BASE.AddComponent<AudioSource>();
            BASE.gameObject.transform.localScale = new Vector3(-BASE.gameObject.transform.localScale.x, BASE.gameObject.transform.localScale.y, BASE.gameObject.transform.localScale.z);
            VButton1.layer = 18;
            VButton2.layer = 18;
            VButton3.layer = 18;
            VButton4.layer = 18;
            VButton5.layer = 18;
            Forwardd.layer = 18;
            Backwardd.layer = 18;
            PauseBTN.layer = 18;
            PlayBTN.layer = 18;
            MuteBTN.layer = 18;
            FastForward.layer = 18;
            Rewind.layer = 18;
            YTLogo.layer = 18;
        }

        public static AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }
        public static void LoadVideoTitles()
        {
            Debug.Log("Index: " + YTVidIndex);
            string FitTitle(string title)
            {
                return title.Length > 34 ? title.Substring(0, 31) + "..." : title;
            }

            VPH.GetComponent<TextMesh>().text = YTVideoNames.Count > YTVidIndex ? FitTitle(YTVideoNames[YTVidIndex]) : "";
            VPH2.GetComponent<TextMesh>().text = YTVideoNames.Count > YTVidIndex + 1 ? FitTitle(YTVideoNames[YTVidIndex + 1]) : "";
            VPH3.GetComponent<TextMesh>().text = YTVideoNames.Count > YTVidIndex + 2 ? FitTitle(YTVideoNames[YTVidIndex + 2]) : "";
            VPH4.GetComponent<TextMesh>().text = YTVideoNames.Count > YTVidIndex + 3 ? FitTitle(YTVideoNames[YTVidIndex + 3]) : "";
            VPH5.GetComponent<TextMesh>().text = YTVideoNames.Count > YTVidIndex + 4 ? FitTitle(YTVideoNames[YTVidIndex + 4]) : "";
        }


        void L(string msg)
        {
            UnityEngine.Debug.Log(msg);
        }

        void Update()
        {
            if (!PhotonNetwork.InRoom || inModded)
            {
                if (!doonce)
                {
                    instance.StartCoroutine(UpdateHome(true));
                    YoutubeDashboard.gameObject.transform.localPosition = new Vector3(-73.6326f, 10.8116f, -84.9893f);
                    YoutubeDashboard.gameObject.transform.rotation = Quaternion.Euler(0f, 337.7857f, 0f);
                    YoutubeDashboard.gameObject.transform.localScale = new Vector3(3f, 3f, 3f);
                    doonce = true;
                    doOnce = false;
                }

            }
            else if (!inModded && PhotonNetwork.InRoom)
            {
                if (!doOnce)
                {
                    instance.StartCoroutine(UpdateHome(true));
                    YoutubeDashboard.transform.localPosition = new Vector3(0f, 0f, 0f);
                    doonce = false;
                    doOnce = true;
                }
            }
        }

        private static IEnumerator CDRun()
        {
            CD = true;
            yield return new WaitForSeconds(2f);
            CD = false;
        }

        private static IEnumerator VideoInteractableCDRun()
        {
            CD2 = true;
            yield return new WaitForSeconds(.7f);
            CD2 = false;
        }

        public static IEnumerator UpdateHome(bool isHome)
        {
            yield return new WaitForSeconds(0.35f);
            VButton1.SetActive(isHome);
            VButton2.SetActive(isHome);
            VButton3.SetActive(isHome);
            VButton4.SetActive(isHome);
            VButton5.SetActive(isHome);

            VPH.SetActive(isHome);
            VPH2.SetActive(isHome);
            VPH3.SetActive(isHome);
            VPH4.SetActive(isHome);
            VPH5.SetActive(isHome);
            YTPlayerTXT.SetActive(isHome);
            PFP.SetActive(isHome);
            Forwardd.SetActive(isHome);
            Backwardd.SetActive(isHome);
            B.SetActive(isHome);
            F.SetActive(isHome);
            MainTXT.SetActive(isHome);
            divider.SetActive(isHome);

            PauseBTN.SetActive(!isHome);
            PlayBTN.SetActive(!isHome);
            MuteBTN.SetActive(!isHome);
            FastForward.SetActive(!isHome);
            Rewind.SetActive(!isHome);
            pauseLogo.SetActive(!isHome);
            playLogo.SetActive(!isHome);
            MuteLogo.SetActive(!isHome);
            FastForwardLogo.SetActive(!isHome);
            RewindLogo.SetActive(!isHome);

            if (isHome == true)
            {
                BASE.GetComponent<VideoPlayer>().renderMode = VideoRenderMode.RenderTexture;
                BASE.GetComponent<VideoPlayer>().Stop();
            }
            else
            {
                BASE.GetComponent<VideoPlayer>().renderMode = VideoRenderMode.MaterialOverride;
            }
        }

        public static void Mute()
        {
            if (!CD2)
            {
                mute = !mute;
                if (mute)
                {
                    BASE.GetComponent<VideoPlayer>().SetDirectAudioMute(0, true);
                }
                else
                {
                    BASE.GetComponent<VideoPlayer>().SetDirectAudioMute(0, false);
                }
                instance.StartCoroutine(VideoInteractableCDRun());
            }
        }

        public static void FastForwardPress()
        {
            if (!CD2)
            {
                BASE.GetComponent<VideoPlayer>().time += 5f;
                instance.StartCoroutine(VideoInteractableCDRun());
            }
        }

        public static void RewindPress()
        {
            if (!CD2)
            {
                BASE.GetComponent<VideoPlayer>().time -= 5f;
                instance.StartCoroutine(VideoInteractableCDRun());
            }
        }

        public static void Pause()
        {
            if (!CD2)
            {
                BASE.GetComponent<AudioSource>().Pause();
                BASE.GetComponent<VideoPlayer>().Pause();
                instance.StartCoroutine(VideoInteractableCDRun());
            }
        }

        public static void Play()
        {
            if (!CD2)
            {
                BASE.GetComponent<AudioSource>().Play();
                BASE.GetComponent<VideoPlayer>().Play();
                instance.StartCoroutine(VideoInteractableCDRun());
            }
        }

        public static void YTHit()
        {
            instance.StartCoroutine(UpdateHome(true));
        }

        public static void VideoButton1()
        {
            if (!CD && YTVidIndex < YTVideoIds.Count && !YTVideoIds[YTVidIndex].ToString().IsNullOrEmpty())
            {
               PlayVideo(YTVideoIds[YTVidIndex]);
               instance.StartCoroutine(UpdateHome(false));
               instance.StartCoroutine(CDRun());
            }
        }

        public static void VideoButton2()
        {
            if (!CD && YTVidIndex + 1 < YTVideoIds.Count && !YTVideoIds[YTVidIndex + 1].ToString().IsNullOrEmpty())
            {
                PlayVideo(YTVideoIds[YTVidIndex + 1]);
                instance.StartCoroutine(UpdateHome(false));
                instance.StartCoroutine(CDRun());
            }
        }

        public static void VideoButton3()
        {
            if (!CD && YTVidIndex + 2 < YTVideoIds.Count && !YTVideoIds[YTVidIndex + 2].ToString().IsNullOrEmpty())
            {
                PlayVideo(YTVideoIds[YTVidIndex + 2]);
                instance.StartCoroutine(UpdateHome(false));
                instance.StartCoroutine(CDRun());
            }
            
        }

        public static void VideoButton4()
        {
            if (!CD && YTVidIndex + 3 < YTVideoIds.Count && !YTVideoIds[YTVidIndex + 3].ToString().IsNullOrEmpty())
            {
                PlayVideo(YTVideoIds[YTVidIndex + 3]);
                instance.StartCoroutine(UpdateHome(false));
                instance.StartCoroutine(CDRun());
            }
           
        }
        public static void VideoButton5()
        {
            if (!CD && YTVidIndex + 4 < YTVideoIds.Count && !YTVideoIds[YTVidIndex + 4].ToString().IsNullOrEmpty())
            {
                PlayVideo(YTVideoIds[YTVidIndex + 4]);
                instance.StartCoroutine(UpdateHome(false));
                instance.StartCoroutine(CDRun());
            }
        }


        public static void Forward()
        {
            YTVidIndex += 5;
            LoadVideoTitles();
        }

        public static void Backward()
        {
            if (YTVidIndex > 0)
            {
                YTVidIndex -= 5;
                LoadVideoTitles();
            }
        }

    }
}
