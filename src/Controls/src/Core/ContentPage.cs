using System.Runtime.CompilerServices;

namespace Microsoft.Maui.Controls
{
	[ContentProperty("Content")]
	public partial class ContentPage : TemplatedPage
	{
		public static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(View), typeof(ContentPage), null, propertyChanged: TemplateUtilities.OnContentChanged);

		public View Content
		{
			get { return (View)GetValue(ContentProperty); }
			set {
				SetValue(ContentProperty, value);
				TypeHashCode = null;
			}
		}

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if(propertyName == nameof(Content))
				TypeHashCode = null;
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			View content = Content;
			ControlTemplate controlTemplate = ControlTemplate;
			if (content != null && controlTemplate != null)
			{
				SetInheritedBindingContext(content, BindingContext);
			}
		}

		internal override void OnControlTemplateChanged(ControlTemplate oldValue, ControlTemplate newValue)
		{
			if (oldValue == null)
				return;

			base.OnControlTemplateChanged(oldValue, newValue);
			View content = Content;
			ControlTemplate controlTemplate = ControlTemplate;
			if (content != null && controlTemplate != null)
			{
				SetInheritedBindingContext(content, BindingContext);
			}
		}
	}
}