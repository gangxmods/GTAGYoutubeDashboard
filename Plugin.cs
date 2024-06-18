using System;
using System.IO;
using System.Reflection;
using BepInEx;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using WebSocketSharp;
using UnityEngine.Video;
using UnityEngine.Networking;

namespace YoutubeDashboard
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.6.13")]
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static Plugin Instance { get; private set; }

        public GameObject YoutubeDashboard;
        public GameObject BASE;
        public GameObject VButton1;
        public GameObject VButton2;
        public GameObject VButton3;
        public GameObject VButton4;
        public GameObject VButton5;
        public GameObject YTLogo;
        public GameObject ForwardBTN;
        public GameObject BackwardBTN;
        public GameObject PauseBTN;
        public GameObject PlayBTN;
        public GameObject MuteBTN;
        public GameObject VPH;
        public GameObject VPH2;
        public GameObject VPH3;
        public GameObject VPH4;
        public GameObject VPH5;
        public GameObject YTPlayerTXT;
        public GameObject PFP;
        public GameObject F;
        public GameObject B;
        public GameObject MainTXT;
        public GameObject Divider;
        public GameObject PauseLogo;
        public GameObject PlayLogo;
        public GameObject MuteLogo;
        public GameObject FastForward;
        public GameObject Rewind;
        public GameObject FastForwardLogo;
        public GameObject RewindLogo;
        
        public bool CD = false;
        public bool CD2 = false;
        public bool mute = false;

        public List<string> YTVideoNames = new List<string>();
        public List<string> YTVideoIds = new List<string>();

        private JToken client;
        public JObject data;

        public int YTVidIndex = 0;

        public const float Debounce = 0.25f;
        public float LastPress;

        public string SubscriberCount;
        public string TotalVideos;
        public string TotalViews;
        public string AvrgViews;
        public string PFPP;
        private string apiKey;
        private string continuationToken;
        private string clickTrackingParams;

        public ConfigEntry<string> username;

        public Material pressedMat;
        public Material unpressedMat;

        private void Start()
        {
            username = Config.Bind<string>("General", "Username", "NONE SET", "The Username That Will Appear Ingame");

            string url = $"https://youtube.com/@{username.Value}";
            
            if (Instance != this)
                Instance = this;
            LoadAssets();
            StartCoroutine(GetInfo($"{url}/about"));
            StartCoroutine(LoadYTProfileCoroutine($"{url}/videos?view=0&flow=grid"));
            GorillaTagger.OnPlayerSpawned(OnGameInitialized);
        }

        private void OnGameInitialized()
        {
            // Board
            YoutubeDashboard = Instantiate(YoutubeDashboard);
            YoutubeDashboard.SetActive(true);
            YoutubeDashboard.gameObject.transform.localPosition = new Vector3(-73.6326f, 10.8116f, - 84.9893f);
            YoutubeDashboard.gameObject.transform.rotation = Quaternion.Euler(0f, 337.7857f, 0f);
            YoutubeDashboard.gameObject.transform.localScale = new Vector3(3f, 3f, 3f);
            StartCoroutine(UpdateHome(true));

            // Buttons ETC
            BASE = GameObject.Find("BASE");
            VButton1 = GameObject.Find("BTN");
            VButton2 = GameObject.Find("BTN2");
            VButton3 = GameObject.Find("BTN3");
            VButton4 = GameObject.Find("BTN4");
            VButton5 = GameObject.Find("BTN5");
            YTLogo = GameObject.Find("YTLogo");
            ForwardBTN = GameObject.Find("Forward");
            BackwardBTN = GameObject.Find("Backward");
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
            Divider = GameObject.Find("divider");
            PauseLogo = GameObject.Find("pauseLogo");
            PlayLogo = GameObject.Find("playLogo");
            MuteLogo = GameObject.Find("MuteLogo");
            FastForwardLogo = GameObject.Find("FastForwardLogo");
            RewindLogo = GameObject.Find("RewindLogo");
            BASE.gameObject.transform.localScale = new Vector3(-BASE.gameObject.transform.localScale.x, BASE.gameObject.transform.localScale.y, BASE.gameObject.transform.localScale.z);
            LoadButtons();
            LoadVideoTitles();

            pressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = Color.red };
            unpressedMat = new Material(Shader.Find("GorillaTag/UberShader")) { color = new Color(164f, 158f, 158f, 255f) };

            // SETTING
            if (username.Value.ToString() == "NONE SET")
            {
                MainTXT.GetComponent<TextMesh>().text = $"Set Username In BepInEx \nConfig File";
            }
            else
            {
                MainTXT.GetComponent<TextMesh>().text = $"CHANNEL: {username.Value}\nSubscriber Count: {SubscriberCount.Replace("subscribers", "")}\nTotal Videos: {TotalVideos:N0}\nTotal Views: {TotalViews:N0}\nAverage Views: {AvrgViews:N0}";
                Material pfpMat = CustomImage(PFPP);
                PFP.GetComponent<Renderer>().material = pfpMat;
            }
        }

        public IEnumerator GetInfo(string link)
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

            // SETTING
            if (username.Value.ToString() == "NONE SET")
            {
                MainTXT.GetComponent<TextMesh>().text = $"Set Username In BepInEx \nConfig File";
            }
            else
            {
                MainTXT.GetComponent<TextMesh>().text = $"CHANNEL: {username.Value}\nSubscriber Count: {SubscriberCount.Replace("subscribers", "")}\nTotal Videos: {TotalVideos:N0}\nTotal Views: {TotalViews:N0}\nAverage Views: {AvrgViews:N0}";
                Material pfpMat = CustomImage(PFPP);
                PFP.GetComponent<Renderer>().material = pfpMat;
            }
        }

        public Material CustomImage(string url)
        {
            Material material = null;
            try
            {
                using WebClient webClient = new WebClient();

                byte[] array = webClient.DownloadData(url);
                if (array != null && array.Length != 0)
                {
                    material = new Material(Shader.Find("GorillaTag/UberShader"))
                    {
                        shaderKeywords = new string[]
                        {
                                "_USE_TEXTURE"
                        }
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
                    _ = clientVersionMatch.Groups[1].Value;

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
                            clickTracking = new { clickTrackingParams },
                            client
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
                    StartCoroutine(UpdateHome(true));   
                    break;
                }

                yield return null;
            }
            Debug.Log("Total videos loaded: " + YTVideoIds.Count);
        }

        private void PlayVideo(string videoId)
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
            foreach (Match m in matches.Cast<Match>())
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
            choice ??= unsortedList.FirstOrDefault();
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

        public Dictionary<string, object> ExtractVideoInfo(JToken videoData)
        {
            var title = videoData["title"]?["runs"]?[0]?["text"] ?? "No Title";
            var viewsText = (string) videoData["viewCountText"]?["simpleText"] ?? "No Views";
            var views = viewsText != "No Views" ? int.Parse(viewsText.Replace(",", "").Split()[0]) : 0;
            var firstThumbnail = videoData["thumbnail"]?["thumbnails"]?[0]?["url"] ?? "No Thumbnail";
            var videoId = videoData["videoId"] ?? "No Video ID";
            return new Dictionary<string, object>
            {
                { "title", title },
                { "views", views },
                { "first_thumbnail", firstThumbnail },
                { "video_id", videoId }
            };
        }

        public void LoadAssets()
        {
            AssetBundle bundle = LoadAssetBundle("YoutubeDashboard.Content.yt");
            YoutubeDashboard = bundle.LoadAsset<GameObject>("YT");
        }

        public void LoadButtons()
        {
            VButton1.AddComponent<VButton1>();
            VButton2.AddComponent<VButton2>();
            VButton3.AddComponent<VButton3>();
            VButton4.AddComponent<VButton4>();
            VButton5.AddComponent<VButton5>();
            ForwardBTN.AddComponent<Forward>();
            BackwardBTN.AddComponent<Backward>();
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
            ForwardBTN.layer = 18;
            BackwardBTN.layer = 18;
            PauseBTN.layer = 18;
            PlayBTN.layer = 18;
            MuteBTN.layer = 18;
            FastForward.layer = 18;
            Rewind.layer = 18;
            YTLogo.layer = 18;
        }

        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }

        public void LoadVideoTitles()
        {
            Debug.Log("Index: " + YTVidIndex);

            static string FitTitle(string title) => title.Length > 34 ? title[..31] + "..." : title;

            VPH.GetComponent<TextMesh>().text = YTVideoNames.Count > YTVidIndex ? FitTitle(YTVideoNames[YTVidIndex]) : "";
            VPH2.GetComponent<TextMesh>().text = YTVideoNames.Count > YTVidIndex + 1 ? FitTitle(YTVideoNames[YTVidIndex + 1]) : "";
            VPH3.GetComponent<TextMesh>().text = YTVideoNames.Count > YTVidIndex + 2 ? FitTitle(YTVideoNames[YTVidIndex + 2]) : "";
            VPH4.GetComponent<TextMesh>().text = YTVideoNames.Count > YTVidIndex + 3 ? FitTitle(YTVideoNames[YTVidIndex + 3]) : "";
            VPH5.GetComponent<TextMesh>().text = YTVideoNames.Count > YTVidIndex + 4 ? FitTitle(YTVideoNames[YTVidIndex + 4]) : "";
        }

        private IEnumerator CDRun()
        {
            CD = true;
            yield return new WaitForSeconds(2f);
            CD = false;
        }

        private IEnumerator VideoInteractableCDRun()
        {
            CD2 = true;
            yield return new WaitForSeconds(.7f);
            CD2 = false;
        }

        public IEnumerator UpdateHome(bool isHome)
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
            ForwardBTN.SetActive(isHome);
            BackwardBTN.SetActive(isHome);
            B.SetActive(isHome);
            F.SetActive(isHome);
            MainTXT.SetActive(isHome);
            Divider.SetActive(isHome);

            PauseBTN.SetActive(!isHome);
            PlayBTN.SetActive(!isHome);
            MuteBTN.SetActive(!isHome);
            FastForward.SetActive(!isHome);
            Rewind.SetActive(!isHome);
            PauseLogo.SetActive(!isHome);
            PlayLogo.SetActive(!isHome);
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

        public void Mute()
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
                StartCoroutine(VideoInteractableCDRun());
            }
        }

        public void FastForwardPress()
        {
            if (!CD2)
            {
                BASE.GetComponent<VideoPlayer>().time += 5f;
                StartCoroutine(VideoInteractableCDRun());
            }
        }

        public void RewindPress()
        {
            if (!CD2)
            {
                BASE.GetComponent<VideoPlayer>().time -= 5f;
                StartCoroutine(VideoInteractableCDRun());
            }
        }

        public void Pause()
        {
            if (!CD2)
            {
                BASE.GetComponent<AudioSource>().Pause();
                BASE.GetComponent<VideoPlayer>().Pause();
                StartCoroutine(VideoInteractableCDRun());
            }
        }

        public void Play()
        {
            if (!CD2)
            {
                BASE.GetComponent<AudioSource>().Play();
                BASE.GetComponent<VideoPlayer>().Play();
                StartCoroutine(VideoInteractableCDRun());
            }
        }

        public void YTHit() => StartCoroutine(UpdateHome(true));

        public void VideoButton1()
        {
            if (!CD && YTVidIndex < YTVideoIds.Count && !YTVideoIds[YTVidIndex].ToString().IsNullOrEmpty())
            {
               PlayVideo(YTVideoIds[YTVidIndex]);
               StartCoroutine(UpdateHome(false));
               StartCoroutine(CDRun());
            }
        }

        public void VideoButton2()
        {
            if (!CD && YTVidIndex + 1 < YTVideoIds.Count && !YTVideoIds[YTVidIndex + 1].ToString().IsNullOrEmpty())
            {
                PlayVideo(YTVideoIds[YTVidIndex + 1]);
                StartCoroutine(UpdateHome(false));
                StartCoroutine(CDRun());
            }
        }

        public void VideoButton3()
        {
            if (!CD && YTVidIndex + 2 < YTVideoIds.Count && !YTVideoIds[YTVidIndex + 2].ToString().IsNullOrEmpty())
            {
                PlayVideo(YTVideoIds[YTVidIndex + 2]);
                StartCoroutine(UpdateHome(false));
                StartCoroutine(CDRun());
            }
        }

        public void VideoButton4()
        {
            if (!CD && YTVidIndex + 3 < YTVideoIds.Count && !YTVideoIds[YTVidIndex + 3].ToString().IsNullOrEmpty())
            {
                PlayVideo(YTVideoIds[YTVidIndex + 3]);
                StartCoroutine(UpdateHome(false));
                StartCoroutine(CDRun());
            }
        }

        public void VideoButton5()
        {
            if (!CD && YTVidIndex + 4 < YTVideoIds.Count && !YTVideoIds[YTVidIndex + 4].ToString().IsNullOrEmpty())
            {
                PlayVideo(YTVideoIds[YTVidIndex + 4]);
                StartCoroutine(UpdateHome(false));
                StartCoroutine(CDRun());
            }
        }

        public void Forward()
        {
            YTVidIndex += 5;
            LoadVideoTitles();
        }

        public void Backward()
        {
            if (YTVidIndex > 0)
            {
                YTVidIndex -= 5;
                LoadVideoTitles();
            }
        }
    }
}
