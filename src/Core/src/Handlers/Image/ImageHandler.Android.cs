﻿using System.Threading.Tasks;
using Android.Graphics.Drawables;
using Android.Widget;
using AndroidX.AppCompat.Widget;

namespace Microsoft.Maui.Handlers
{
	public partial class ImageHandler : ViewHandler<IImage, ImageView>
	{
		protected override ImageView CreatePlatformView() => new AppCompatImageView(Context);

		protected override void DisconnectHandler(ImageView platformView)
		{
			base.DisconnectHandler(platformView);
			SourceLoader.Reset();
		}

		public override bool NeedsContainer =>
			VirtualView?.Background != null ||
			base.NeedsContainer;

		public static void MapBackground(IImageHandler handler, IImage image)
		{
			handler.UpdateValue(nameof(IViewHandler.ContainerView));

			handler.ToPlatform().UpdateBackground(image);
			handler.ToPlatform().UpdateOpacity(image);
		}

		public static void MapAspect(IImageHandler handler, IImage image) =>
			handler.PlatformView?.UpdateAspect(image);

		public static void MapIsAnimationPlaying(IImageHandler handler, IImage image) =>
			handler.PlatformView?.UpdateIsAnimationPlaying(image);

		public static void MapSource(IImageHandler handler, IImage image) =>
			MapSourceAsync(handler, image).FireAndForget(handler);

		public static Task MapSourceAsync(IImageHandler handler, IImage image)
		{
			handler.PlatformView.Clear();
			return handler.SourceLoader.UpdateImageSourceAsync();
		}

		void OnSetImageSource(Drawable? obj)
		{
			PlatformView.SetImageDrawable(obj);
		}

		public override void PlatformArrange(Graphics.Rect frame)
		{
			if (PlatformView.GetScaleType() == ImageView.ScaleType.CenterCrop)
			{
				// If the image is center cropped (AspectFill), then the size of the image likely exceeds
				// the view size in some dimension. So we need to clip to the view's bounds.

				var (left, top, right, bottom) = PlatformView.Context!.ToPixels(frame);
				var clipRect = new Android.Graphics.Rect(0, 0, right - left, bottom - top);
				PlatformView.ClipBounds = clipRect;
			}
			else
			{
				PlatformView.ClipBounds = null;
			}

			base.PlatformArrange(frame);
		}
	}
}