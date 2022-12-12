using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Threading;
using Avalonia.Threading;
using MFORMATSLib;
using Microsoft.CodeAnalysis.Operations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using WriterRateControlSample.Views;

namespace WriterRateControlSample.ViewModels;

public class WriterReaderClass : ReactiveObject
{
    private bool doLoop = true;
    private bool doLoopReader = true;
    private bool isRecorderInited = false;

    private DateTime FirstFrameTimeAt;
    private DateTime LastFrameTimeAt;

    private string currentWriterDestination;
    
    [Reactive] public string OutputString { get; set; }
    [Reactive] public string ID { get; set; }
    public MfMediaPlayer MfMediaPlayerInst { get; }

    private string folderName;
    public WriterReaderClass(int id, string folderName1)
    {
        ID = id.ToString();
        folderName = folderName1;

        MfMediaPlayerInst = new MfMediaPlayer();
    }
    
    private void RecordingThread()
    {
        try
        {


            MFReader myReader;
            MFWriter myWriter;

            myReader = new MFReaderClass();
            myReader.ReaderOpen("rtsp://localhost/live",
                "decoder.nvidia=true network.open_async=true");
            
            /*var myPreview = new MFSink();
            myPreview.SinkInit("asdf", "asdf", "");*/

            var myPreview = new MFPreviewClass();

            myPreview.PreviewEnable("", 0, 0);

            Thread.Sleep(5000);

            myWriter = new MFWriterClass();

            while (doLoop)
            {
                try
                {


                    if (!isRecorderInited)
                    {
                        // init recorder

                        myWriter.WriterGet(out var targetPath, out var encodingConfig);
                        //  format='mp4' video::codec='n264' video::b='6M' video::g='5' audio::codec='aac'

                        var ct = DateTime.Now;
                        var currentTimecodeZeroed = $"{ct.Hour:00}:{ct.Minute:00}:{ct.Second:00}:00";

                        encodingConfig += $" video::b='{10}M' video::g='5' start_timecode='{currentTimecodeZeroed}' object_name='Writer{ID}' ";

                        currentWriterDestination = @$"{folderName}\stream-{ID}.mov";

                        myWriter.WriterSet(currentWriterDestination, 1, encodingConfig);

                        isRecorderInited = true;
                    }
                    else
                    {
                        myReader.SourceFrameGet(-1, out var pFrame, "");

                        if (FirstFrameTimeAt == DateTime.MinValue)
                        {
                            FirstFrameTimeAt = DateTime.Now;
                        }

                        myWriter.ReceiverFramePut(pFrame, -1, "");
                        
                        myPreview.ReceiverFramePut(pFrame, -1, "");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }


            // stop recording to file
            currentWriterDestination = "";
            myWriter.WriterClose(0);

            LastFrameTimeAt = DateTime.Now;

            Thread.Sleep(10000);

            doLoopReader = false;

            myReader.ReaderClose();
            myPreview.MFClose();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    private bool isReaderInitialized = false;
    private bool IsPreviewPanelSet = false;
    private void ReaderThread()
    {
        try
        {
            var myReader = new MFReaderClass();
            var myPreview = new MFPreviewClass();

            myPreview.PropsSet("preview.downscale", "2");
            myPreview.PreviewEnable("", 0, 0);


            Thread.Sleep(3000);

            while (doLoopReader)
            {
                try
                {

                    if (!isRecorderInited)
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    if (MfMediaPlayerInst.Hwnd != IntPtr.Zero 
                        && !IsPreviewPanelSet)
                    {
                        myPreview.PreviewWindowSet("", MfMediaPlayerInst.Hwnd.ToInt32());
                        IsPreviewPanelSet = true;
                    }

                    if (!isReaderInitialized)
                    {
                        myReader.ReaderOpen(currentWriterDestination,
                            "decoder.nvidia=true " +
                            "decoder.quicksync=true " +
                            "network.open_async=true" +
                            "duration.recalc_on_open=true");

                        /*string fileInfo = string.Empty;

                        void GetFileInfo(string propertyNode)
                        {
                            int nCount = 0;
                            try
                            {
                                // get a number of properties
                                myReader.PropsGetCount(propertyNode, out nCount);
                            }
                            catch (Exception) { }
                            for (int i = 0; i < nCount; i++)
                            {
                                string sName;
                                string sValue;
                                int bNode = 0;
                                myReader.PropsGetByIndex(propertyNode, i, out sName, out sValue, out bNode);
                                // bNode flag indicates whether there are internal properties
                                // to collect a full node name we should separated it with "::", e.g. "info::video.0"
                                string sRelName = propertyNode.Length > 0 ? propertyNode + "::" + sName : sName;
                                if (bNode != 0)
                                {
                                    GetFileInfo(sRelName); // call the method recursively in case of sub-nodes
                                }
                                else
                                {
                                    fileInfo += sRelName + " = " + sValue + Environment.NewLine;
                                }
                            }
                        }

                        GetFileInfo("");*/

                        isReaderInitialized = true;
                    }

                    myReader.SourceFrameGet(-1, out var pFrame, "");

                    myReader.ReaderDurationGet(out var duration);

                    TimeSpan timeDuration;
                    
                    if (LastFrameTimeAt == DateTime.MinValue)
                        timeDuration = DateTime.Now - FirstFrameTimeAt;
                    else
                        timeDuration = LastFrameTimeAt - FirstFrameTimeAt;

                    Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        OutputString =
                            $"{DateTime.Now} \n" +
                            $" Dur: {duration.ToString("0.##")} \n" +
                            $" TDur: {timeDuration.TotalSeconds.ToString("0.##")} \n " +
                            $"diff:{(timeDuration.TotalSeconds - duration).ToString("0.##")} \n";
                    });

                    myPreview.ReceiverFramePut(pFrame, -1, "");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            myReader.ReaderClose();
            myPreview.MFClose();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public void Start()
    {
        doLoop = true;
        
        Observable.Start(RecordingThread);
        Observable.Start(ReaderThread);
    }
    
    public void Stop()
    {
        doLoop = false;
    }
}