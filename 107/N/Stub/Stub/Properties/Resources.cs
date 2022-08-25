using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Stub.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (Resources.resourceMan == null)
			{
				Resources.resourceMan = new ResourceManager("Stub.Properties.Resources", typeof(Resources).Assembly);
			}
			return Resources.resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return Resources.resourceCulture;
		}
		set
		{
			Resources.resourceCulture = value;
		}
	}

	internal static string String1 => Resources.ResourceManager.GetString("String1", Resources.resourceCulture);

	internal Resources()
	{
	}
}
