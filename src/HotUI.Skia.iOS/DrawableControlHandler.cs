using System;
using CoreGraphics;
using HotUI.iOS;
using HotUI.iOS.Handlers;
using UIKit;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global

namespace HotUI.Skia.iOS
{
    public class DrawableControlHandler : AbstractControlHandler<DrawableControl, iOSDrawableControl>
    {
        public static readonly PropertyMapper<DrawableControl> Mapper = new PropertyMapper<DrawableControl>()
        {
            [nameof(EnvironmentKeys.Animations.Animation)] = MapAnimationProperty,
        };

        protected override iOSDrawableControl CreateView()
        {
            return new iOSDrawableControl();
        }

        protected override void DisposeView(iOSDrawableControl nativeView)
        {
            
        }

        public override void SetView(View view)
        {
            base.SetView(view);

            //SetMapper(VirtualView.ControlDelegate.Mapper);
            SetMapper(DrawableControlHandler.Mapper);
            TypedNativeView.ControlDelegate = VirtualView.ControlDelegate;
            //VirtualView.ControlDelegate.Mapper?.UpdateProperties(this, VirtualView);
            Mapper?.UpdateProperties(this, VirtualView);
        }

        public override void Remove(View view)
        {
            TypedNativeView.ControlDelegate = null;
            SetMapper(null);
            
            base.Remove(view);
        }

        public override SizeF Measure(SizeF availableSize)
        {
            return VirtualView?.ControlDelegate?.Measure(availableSize) ?? availableSize;
        }

        public static void MapAnimationProperty(IViewHandler handler, View virtualView)
        {
            var nativeView = (UIView)handler.NativeView;
            var animation = virtualView.GetAnimation();
            if (animation != null)
            {
                //nativeView.

                System.Diagnostics.Debug.WriteLine($"Starting animation [{animation}] on [{virtualView.GetType().Name}/{nativeView.GetType().Name}]");

                var duration = (animation.Duration ?? 1000.0) / 1000.0;
                var delay = (animation.Delay ?? 0.0) / 1000.0;
                var options = animation.Options.ToAnimationOptions();

                UIView.Animate(
                    duration,
                    delay,
                    options,
                    () =>
                    {
                        System.Diagnostics.Debug.WriteLine($"Animation [{animation}] has been started");

                        //nativeView.

                        //var transform = CGAffineTransform.MakeIdentity();

                        //if (animation.TranslateTo != null)
                        //{
                        //    var translateTransform = CGAffineTransform.MakeTranslation(animation.TranslateTo.Value.X, animation.TranslateTo.Value.Y);
                        //    transform = CGAffineTransform.Multiply(transform, translateTransform);
                        //}
                        //if (animation.RotateTo != null)
                        //{
                        //    var angle = Convert.ToSingle(animation.RotateTo.Value * Math.PI / 180);
                        //    var rotateTransform = CGAffineTransform.MakeRotation(angle);
                        //    transform = CGAffineTransform.Multiply(transform, rotateTransform);
                        //}
                        //if (animation.ScaleTo != null)
                        //{
                        //    var scaleTransform = CGAffineTransform.MakeScale(animation.ScaleTo.Value.X, animation.ScaleTo.Value.Y);
                        //    transform = CGAffineTransform.Multiply(transform, scaleTransform);
                        //}
                        //// TODO: implement other animations

                        //nativeView.Transform = transform;
                    },
                    () =>
                    {
                        System.Diagnostics.Debug.WriteLine($"Animation [{animation}] has been completed");
                        // Do nothing
                    });
            }
        }
    }
}