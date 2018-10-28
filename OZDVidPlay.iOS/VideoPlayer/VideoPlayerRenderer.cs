using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AVFoundation;
using AVKit;
using CoreGraphics;
using CoreMedia;
using Foundation;
using OZDVidPlay;
using OZDVidPlay.iOS;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(VideoPlayer), typeof(VideoPlayerRenderer))]

namespace OZDVidPlay.iOS
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, UIView>
    {
        AVQueuePlayer player;
     
        AVPlayerViewController playerViewController;

        public override UIViewController ViewController => this.playerViewController;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (this.Control == null)
                {
                    // Create AVPlayerViewController
                    this.playerViewController = new AVPlayerViewController
                    {
                        ShowsPlaybackControls = true,
                        EntersFullScreenWhenPlaybackBegins = true,
                        ExitsFullScreenWhenPlaybackEnds = true
                    };

                    // Set Player property to AVPlayer
                    this.player = new AVQueuePlayer
                    {
                        ActionAtItemEnd = AVPlayerActionAtItemEnd.Advance,
                        AllowsExternalPlayback = true,
                        UsesExternalPlaybackWhileExternalScreenIsActive = true
                    };

                    this.playerViewController.Player = this.player;

                    var view = this.playerViewController.View;
                    var playerLayer = AVPlayerLayer.FromPlayer(this.player);
                    playerLayer.Frame = view.Frame;
                    playerLayer.Bounds = view.Bounds;
                    view.Layer.AddSublayer(playerLayer);

                    // Use the View from the controller as the native control
                    this.SetNativeControl(view);
                }

                e.NewElement.UpdateStatus += this.OnUpdateStatus;
                e.NewElement.PlayRequested += this.OnPlayRequested;
                e.NewElement.PauseRequested += this.OnPauseRequested;
                e.NewElement.StopRequested += this.OnStopRequested;
            }

            if (e.OldElement != null)
            {
                e.OldElement.UpdateStatus -= this.OnUpdateStatus;
                e.OldElement.PlayRequested -= this.OnPlayRequested;
                e.OldElement.PauseRequested -= this.OnPauseRequested;
                e.OldElement.StopRequested -= this.OnStopRequested;
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (this.player != null)
            {
                this.player.ReplaceCurrentItemWithPlayerItem(null);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VideoPlayer.SourceProperty.PropertyName)
            {
                this.SetSource();
            }
            else if (e.PropertyName == VideoPlayer.PositionProperty.PropertyName)
            {
                var controlPosition = ConvertTime(this.player.CurrentTime);
                if (Math.Abs((controlPosition - Element.Position).TotalSeconds) > 1)
                {
                    this.player.Seek(CMTime.FromSeconds(Element.Position.TotalSeconds, 1));
                }
            }
        }

        void SetSource()
        {
            if (this.Element.Source is IEnumerable<FileVideoSource> sources)
            {
                this.player.RemoveAllItems();

                AVPlayerItem lastPlayerItem = null;
                sources.Where(x => !string.IsNullOrWhiteSpace(x.File)).ForEach(x =>
                {
                    var currentPlayerItem = new AVPlayerItem(AVAsset.FromUrl(new NSUrl(x.File)));
                    if (this.player.CanInsert(currentPlayerItem, lastPlayerItem))
                    {
                        this.player.InsertItem(currentPlayerItem, lastPlayerItem);
                        lastPlayerItem = currentPlayerItem;
                    }
                });

                //this.player.ReplaceCurrentItemWithPlayerItem(lastPlayerItem);
                            }
        }

        // Event handler to update status
        void OnUpdateStatus(object sender, EventArgs args)
        {
            VideoStatus videoStatus = VideoStatus.NotReady;

            switch (this.player.Status)
            {
                case AVPlayerStatus.ReadyToPlay:
                    switch (this.player.TimeControlStatus)
                    {
                        case AVPlayerTimeControlStatus.Playing:
                            videoStatus = VideoStatus.Playing;
                            break;

                        case AVPlayerTimeControlStatus.Paused:
                            videoStatus = VideoStatus.Paused;
                            break;
                    }
                    break;
            }
            ((IVideoPlayerController)Element).Status = videoStatus;

            var playerItem = this.player.CurrentItem;
            if (playerItem != null)
            {
                ((IVideoPlayerController)Element).Duration = this.ConvertTime(playerItem.Duration);
                ((IElementController)Element).SetValueFromRenderer(VideoPlayer.PositionProperty, this.ConvertTime(playerItem.CurrentTime));
            }
        }

        TimeSpan ConvertTime(CMTime cmTime)
        {
            return TimeSpan.FromSeconds(Double.IsNaN(cmTime.Seconds) ? 0 : cmTime.Seconds);
        }

        // Event handlers to implement methods
        void OnPlayRequested(object sender, EventArgs args)
        {
            this.player.Play();
        }

        void OnPauseRequested(object sender, EventArgs args)
        {
            this.player.Pause();
        }

        void OnStopRequested(object sender, EventArgs args)
        {
            this.player.Pause();
            this.player.Seek(new CMTime(0, 1));
        }
    }
}