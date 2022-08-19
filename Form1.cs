using MFORMATSLib;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MedialooksMoveByTimecode
{
    public partial class Form1 : Form
    {
        private const int NUM_R = 2;
        private List<MFPreviewClass> MFPreviews = new List<MFPreviewClass>();
        private List<MFReaderClass> MFReaders = new List<MFReaderClass>();
        private List<Panel> Panels = new List<Panel>();
        private ConcurrentDictionary<int, FrameState> FrameInfo = new ConcurrentDictionary<int, FrameState>();

        private List<Thread> Threads = new List<Thread>();	        //Working thread
        private bool m_bWork;
        private bool p_pause = false;
        private CancellationTokenSource cancelSource;
        private static System.Timers.Timer aTimer;
        private string? RequestGotoTimecode = null;

        public Form1()
        {
            InitializeComponent();

            //string strCmdText;
            //strCmdText = "use \\desktop-dve1sr3 /user:fr /persistent:yes fr";
            //Process.Start("net.exe", strCmdText);

            MFormatsSDKLic.IntializeProtection();

            Panels.Add(panelPreview1);
            Panels.Add(panelPreview2);

            for(var ii = 0; ii < NUM_R; ii++)
            {
                FrameInfo.TryAdd(ii, new FrameState());
                MFPreviews.Add(new MFPreviewClass());
                MFReaders.Add(new MFReaderClass());
                MFPreviews[ii].PreviewEnable("", 0, 1);
                MFPreviews[ii].PreviewWindowSet("", Panels[ii].Handle.ToInt64());
                MFPreviews[ii].PropsSet("preview.drop_frames", "true");
                MFPreviews[ii].PropsSet("preview.type", "dx11");
                MFPreviews[ii].PropsSet("preview.downscale", "2");
            }

            //MFReaders[0].ReaderOpen(@"D:\tmp\BBB_50fps_12mbit_60s_snowflake.mp4", "loop=true decoder.nvidia=true");
            //MFReaders[1].ReaderOpen(@"D:\tmp\BBB_50fps_12mbit_60s_snowflake.mp4", "loop=true decoder.nvidia=true");

            MFReaders[0].ReaderOpen(@"\\desktop-dve1sr3\FairReplayD\session-11\Input1\recording--2022-07-20--07-13-34-5\stream.mp4", "loop=true decoder.nvidia=true");
            MFReaders[1].ReaderOpen(@"\\desktop-dve1sr3\FairReplayD\session-11\Input2\recording--2022-07-20--07-13-35-053\stream.mp4", "loop=true decoder.nvidia=true");

            cancelSource = new CancellationTokenSource();
            for(var ii = 0; ii < NUM_R; ii++)
            {
                var threadNum = ii;
                Threads.Add(new Thread(() => m_threadWorker_DoWork(cancelSource.Token, threadNum)));
                Threads[ii].Name = "m_threadWorker_DoWorkPreviewReader"+ii;
                Threads[ii].Start();
            }


            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(100);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += ATimer_Elapsed;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;


            m_bWork = true;



            Task.Run(async () => {
                await Task.Delay(2000);
                //Helpers.SetControlProp(tbPosition, x => x.Value = 500);
                //for(var ii = 0; ii < 25; ii++)
                //{
                //    RequestTimeCode = "10:00:00:" + ii + ".0";
                //    await Task.Delay(500);

                //    RequestTimeCode = "10:00:00:" + ii + ".1";
                //    await Task.Delay(500);
                //}



                //for (var ii = 0; ii < 50; ii++)
                //{
                //    RequestTimeCode = "10:00:00:" + ii;
                //    await Task.Delay(500);
                //}
            });
        }

        private void ATimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            var str = "";
            for(var ii = 0; ii < NUM_R; ii++)
            {
                str += $"[{ii}] " + FrameInfo[ii].Info + "\r\n";
            }
            str += "s: " + hScrollBar1.Value + "\r\n";
            str += "request tc: " + RequestTimeCode;
            Helpers.SetControlProp(tbInfo, x => x.Text = str);
        }

        async void m_threadWorker_DoWork(CancellationToken token, int mfnum)
        {
            SpinWait sw = new SpinWait();
            Stopwatch stopwatch = new Stopwatch();
            var ts100ms = TimeSpan.FromMilliseconds(100);
            while (!token.IsCancellationRequested)
            {
                if (m_bWork)
                {
                    stopwatch.Start();
                    //if (p_pause)
                    //{
                    //    // await canPlay();
                    //    sw.SpinOnce();
                    //    // Thread.Sleep(1);
                    //    continue;
                    //}
                    MFFrame pFrame;
                    var opts = "";
                    if (p_pause)
                    {
                        opts += "pause=true ";
                    }

                    if (mfnum == 1)
                    {
                        //if (stopwatch.ElapsedMilliseconds < 16 || stopwatch.ElapsedMilliseconds > 21)
                        //    Debug.WriteLine("sw:" + stopwatch.ElapsedMilliseconds);
                        //stopwatch.Restart();
                    }

                    var _requestTimeCode = RequestTimeCode + "";
                    //if (FrameInfo[mfnum].RequestTimecode != _requestTimeCode && DateTime.UtcNow - FrameInfo[mfnum].RequestTimecodeSatisfiedAt > ts100ms)
                    if (FrameInfo[mfnum].RequestTimecode != _requestTimeCode)
                    {
                        MFReaders[mfnum].PropsSet("tc_pos", _requestTimeCode);
                        MFReaders[mfnum].SourceFrameGetByNumber(-1, -1, out pFrame, opts);    // Get next frame from file

                        FrameInfo[mfnum].RequestTimecodeSatisfiedAt = DateTime.UtcNow;
                        FrameInfo[mfnum].RequestTimecode = _requestTimeCode;
                        // Debug.WriteLine("Request timecode " + _requestTimeCode + " for video " + mfnum);
                    }
                    else if (FrameInfo[mfnum].addFrameNum.HasValue)
                    {
                        int newFrame = FrameInfo[mfnum].FrameCurrent + FrameInfo[mfnum].addFrameNum.Value;
                        
                        opts = "";

                        if (FrameInfo[mfnum].addFrameNum.Value <= -1)
                        {
                            opts = " reverse=true ";
                        }
                        
                        MFReaders[mfnum].SourceFrameGetByNumber(newFrame, 0,
                            out pFrame, opts);

                        FrameInfo[mfnum].addFrameNum = null;
                    }
                    else
                    {
                        // MFReaders[mfnum].SourceFrameGetByNumber(-1, -1, out pFrame, opts);    // Get next frame from file
                        MFReaders[mfnum].SourceFrameGetByTime(-1, -1, out pFrame, opts);    // Get next frame from file
                    }

                    pFrame.MFTimeGet(out var frameTime);

                    FrameInfo[mfnum].FrameCurrent = frameTime.tcFrame.nExtraCounter;

                    var frameNum = frameTime.tcFrame.nFrames; // *2;

                    if (frameTime.tcFrame.eTCFlags.HasFlag(eMTimecodeFlags.eMTCF_Progressive_Even_Frame))
                    {
                        frameNum *= 2;
                    }

                    if (frameTime.tcFrame.eTCFlags.HasFlag(eMTimecodeFlags.eMTCF_Progressive_Odd_Frame))
                    {
                        frameNum++;
                    }

                    //FrameInfo[mfnum].Duration = MFReaders[mfnum]

                    if (FrameInfo[mfnum].FrameCountS != frameTime.tcFrame.nSeconds)
                    {
                        if(FrameInfo[mfnum].FrameCount > 50 && mfnum == 1)
                            Debug.WriteLine("fps: " + FrameInfo[mfnum].FrameCount);
                        FrameInfo[mfnum].FrameCountS = frameTime.tcFrame.nSeconds;
                        FrameInfo[mfnum].FrameCount = 1;
                    }
                    else
                    {
                        FrameInfo[mfnum].FrameCount++;
                    }
                    
                    if (mfnum == 1)
                    {
                        UpdateSlider(frameTime.tcFrame, frameNum);
                    }
                    else
                    {
                        FrameInfo[mfnum].Info =
                            $"{frameTime.tcFrame.nHours}:{frameTime.tcFrame.nMinutes}:{frameTime.tcFrame.nSeconds}:{frameNum}";
                    }
                    

                    // MFReaders[mfnum].ReaderDurationGet(out var readerDuration);

                    // FrameInfo[mfnum].Info += " dur: " + readerDuration;

                    ((IMFReceiver)MFPreviews[mfnum]).ReceiverFramePut(pFrame, -1, "");  //Send frame to the preview

                    Marshal.ReleaseComObject(pFrame);   // You should call these methods to release MFrame object 
                }
                else
                {
                    // For prevent CPU overload if something wrong happened 
                    Thread.Sleep(1);
                }

            }
        }

        private void UpdateSlider(M_TIMECODE frameTimeTcFrame, int frameNum)
        {
            FrameInfo[1].Info =
                $"{frameTimeTcFrame.nHours}:{frameTimeTcFrame.nMinutes}:{frameTimeTcFrame.nSeconds}:{frameNum}";

            int secondsSum = 0;

            secondsSum += (frameTimeTcFrame.nHours - 9) * 3600;
            
            secondsSum += frameTimeTcFrame.nMinutes  * 60;
            secondsSum += frameTimeTcFrame.nSeconds;
            
            //secondsSum += (int)Math.Floor(frameNum / 50d);

            var sliderValue = (int)Math.Floor(secondsSum / 3.6d);

            if (sliderValue <= 0 || sliderValue > 1000)
            {
                
            }
            
            Helpers.SetControlProp(hScrollBar1, x => x.Value = sliderValue);
            
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            p_pause = !p_pause;
        }

        private DateTime LastTimeRequestTimecode = DateTime.UtcNow;
        private string RequestTimeCode = "";
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            //if(DateTime.UtcNow - LastTimeRequestTimecode < TimeSpan.FromMilliseconds(100))
            //{
            //    return;
            //}
            LastTimeRequestTimecode = DateTime.UtcNow;
            p_pause = true;
            var pos = hScrollBar1.Value / 991d;
            var maxTime = 3600d;
            var seconds = Math.Floor(maxTime * pos) + (9*3600);
            //Debug.WriteLine(seconds);
            var frame = ((maxTime * pos) % 1) * 50;
            var frameFloor = Math.Floor(frame);

            var timecode = string.Format("{0:00}:{1:00}:{2:00}:", Math.Floor(seconds / 3600d), Math.Floor((seconds / 60d) % 60), seconds % 60);

            timecode += Math.Floor(frameFloor / 2);
            timecode += (frameFloor % 2 == 0) ? ".0" : ".1";

            RequestTimeCode = timecode;

            //for (var ii = 0; ii < NUM_R; ii++)
            //{
            //    FrameInfo[ii].RequestTimecode = timecode;

            //}
        }

        private class FrameState
        {
            public string Info = "";
            public double Duration = 0;
            public int FrameCurrent = 0;
            public int FrameCount = 0;
            public int FrameCountS = 0;
            public string? RequestTimecode = null;
            public int? addFrameNum = null;
            public DateTime RequestTimecodeSatisfiedAt = DateTime.UtcNow;
        }

        private void btnGoToTC_Click(object sender, EventArgs e)
        {
            RequestTimeCode = tbRequestTC.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Task.Run(async () => {
                //for(var ii = 0; ii < 25; ii++)
                //{
                //    RequestTimeCode = "10:00:00:" + ii + ".0";
                //    await Task.Delay(500);

                //    RequestTimeCode = "10:00:00:" + ii + ".1";
                //    await Task.Delay(500);
                //}



                //for (var ii = 0; ii < 50; ii++)
                //{
                //    RequestTimeCode = "10:00:00:" + ii;
                //    await Task.Delay(500);
                //}
            });
        }

        private void tbPosition_Scroll(object sender, EventArgs e)
        {
            // Debug.WriteLine(tbPosition.Value);

            //if(DateTime.UtcNow - LastTimeRequestTimecode < TimeSpan.FromMilliseconds(100))
            //{
            //    return;
            //}
            LastTimeRequestTimecode = DateTime.UtcNow;
            p_pause = true;
            var pos = tbPosition.Value / 10000d;
            var maxTime = 3600d;
            var seconds = Math.Floor(maxTime * pos) + (9 * 3600);
            //Debug.WriteLine(seconds);
            var frame = ((maxTime * pos) % 1) * 50;
            var frameFloor = Math.Floor(frame);

            var timecode = string.Format("{0:00}:{1:00}:{2:00}:", Math.Floor(seconds / 3600d), Math.Floor((seconds / 60) % 60), seconds % 60);

            timecode += Math.Floor(frameFloor / 2);
            timecode += (frameFloor % 2 == 0) ? ".0" : ".1";

            RequestTimeCode = timecode;

            //for (var ii = 0; ii < NUM_R; ii++)
            //{
            //    FrameInfo[ii].RequestTimecode = timecode;

            //}
        }

        private void button_NextFrame(object sender, EventArgs e)
        {
            for(var ii = 0; ii < NUM_R; ii++)
            {
                FrameInfo[ii].addFrameNum = 1;
            }
            
            p_pause = true;
        }
        
        private void button_PreviousFrame(object sender, EventArgs e)
        {
            for(var ii = 0; ii < NUM_R; ii++)
            {
                FrameInfo[ii].addFrameNum = -1;
            }
            
            p_pause = true;
        }
    }
}
