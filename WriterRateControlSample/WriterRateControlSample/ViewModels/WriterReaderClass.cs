using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Threading;
using Avalonia.Threading;
using MFORMATSLib;
using Microsoft.CodeAnalysis.Operations;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

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

    private string folderName;
    public WriterReaderClass(int id, string folderName1)
    {
        ID = id.ToString();
        folderName = folderName1;
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
            
            /*var myPreview = new MFPreviewClass();

            myPreview.PreviewEnable("", 0, 0);*/

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

                        encodingConfig += $" video::b='{10}M' video::g='5' start_timecode='{currentTimecodeZeroed}' ";

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
                        
                        //myPreview.ReceiverFramePut(pFrame, -1, "");
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
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    private bool isReaderInitialized = false;
    private void ReaderThread()
    {
        try
        {
            var myReader = new MFReaderClass();
            var myPreview = new MFPreviewClass();

            myPreview.PreviewEnable("", 0, 0);

            while (doLoopReader)
            {
                try
                {

                    if (!isRecorderInited)
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    if (!isReaderInitialized)
                    {
                        myReader.ReaderOpen(currentWriterDestination,
                            "decoder.nvidia=true decoder.quicksync=true network.open_async=true");

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
                            $"{DateTime.Now} \n Dur: {duration} \n TDur: {timeDuration.TotalSeconds} \n diff:{timeDuration.TotalSeconds - duration}";
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