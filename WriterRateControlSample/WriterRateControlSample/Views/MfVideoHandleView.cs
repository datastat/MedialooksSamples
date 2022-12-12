using System;
using System.Runtime.InteropServices;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Platform;
using Avalonia.VisualTree;
using Avalonia.Win32;
using Splat;

namespace WriterRateControlSample.Views;

public class MfVideoHandleView : NativeControlHost
{
    private IPlatformHandle? _platformHandle;
    private MfMediaPlayer? _mediaPlayer;

    public static readonly DirectProperty<MfVideoHandleView, MfMediaPlayer?> MediaPlayerProperty =
        AvaloniaProperty.RegisterDirect<MfVideoHandleView, MfMediaPlayer?>(
            nameof(MediaPlayer),
            o => {
                    return o.MediaPlayer;
            },
            (o, v) => {
                o.MediaPlayer = v;
            },
            defaultBindingMode: BindingMode.TwoWay);

    public MfMediaPlayer? MediaPlayer
    {
        get => _mediaPlayer;
        set
        {
            if (ReferenceEquals(_mediaPlayer, value))
            {
                return;
            }

            Detach();
            _mediaPlayer = value;
            Attach();
        }
    }

    private void Attach()
    {
        
        //if (_mediaPlayer == null || _platformHandle == null || !IsInitialized)
        if (_mediaPlayer == null || _platformHandle == null)
            return;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            _mediaPlayer.Hwnd = _platformHandle.Handle;
        }
    }

    private void Detach()
    {
        if (_mediaPlayer == null)
            return;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            _mediaPlayer.Hwnd = IntPtr.Zero;
        }
    }

    /// <inheritdoc />
    protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
    {
        var toplevel = (TopLevel)this.GetVisualRoot();

        var platformImpl = (WindowImpl)toplevel.PlatformImpl;

        _platformHandle = base.CreateNativeControlCore(platformImpl.Handle);

        //_platformHandle.Handle.ToPointer();

        if (_mediaPlayer == null)
            return _platformHandle;

        Attach();

        return _platformHandle;
    }

    /// <inheritdoc />
    protected override void DestroyNativeControlCore(IPlatformHandle control)
    {
        Detach();

        base.DestroyNativeControlCore(control);

        if (_platformHandle is { })
        {
            _platformHandle = null;
        }
    }
}


// internal class Win32WindowControlHandle : PlatformHandle, INativeControlHostDestroyableControlHandle
// {
//     public Win32WindowControlHandle(IntPtr handle, string descriptor) : base(handle, descriptor)
//     {
//     }
//
//     public void Destroy()
//     {
//         _ = WinApi.DestroyWindow(Handle);
//     }
// }

