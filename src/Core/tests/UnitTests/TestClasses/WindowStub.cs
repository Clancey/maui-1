namespace Microsoft.Maui.UnitTests.TestClasses
{
	class WindowStub : IWindow
	{
		public IMauiContext MauiContext { get; set; }
		public IPage Page { get; set; }
	}
}