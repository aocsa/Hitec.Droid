package mono;

import java.io.*;
import java.lang.String;
import java.util.Locale;
import java.util.HashSet;
import java.util.zip.*;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.res.AssetManager;
import android.util.Log;
import mono.android.Runtime;

public class MonoPackageManager {

	static Object lock = new Object ();
	static boolean initialized;

	public static void LoadApplication (Context context, ApplicationInfo runtimePackage, String[] apks)
	{
		synchronized (lock) {
			if (!initialized) {
				System.loadLibrary("monodroid");
				Locale locale       = Locale.getDefault ();
				String language     = locale.getLanguage () + "-" + locale.getCountry ();
				String filesDir     = context.getFilesDir ().getAbsolutePath ();
				String cacheDir     = context.getCacheDir ().getAbsolutePath ();
				String dataDir      = getNativeLibraryPath (context);
				ClassLoader loader  = context.getClassLoader ();

				Runtime.init (
						language,
						apks,
						getNativeLibraryPath (runtimePackage),
						new String[]{
							filesDir,
							cacheDir,
							dataDir,
						},
						loader,
						new java.io.File (
							android.os.Environment.getExternalStorageDirectory (),
							"Android/data/" + context.getPackageName () + "/files/.__override__").getAbsolutePath (),
						MonoPackageManager_Resources.Assemblies,
						context.getPackageName ());
				initialized = true;
			}
		}
	}

	static String getNativeLibraryPath (Context context)
	{
	    return getNativeLibraryPath (context.getApplicationInfo ());
	}

	static String getNativeLibraryPath (ApplicationInfo ainfo)
	{
		if (android.os.Build.VERSION.SDK_INT >= 9)
			return ainfo.nativeLibraryDir;
		return ainfo.dataDir + "/lib";
	}

	public static String[] getAssemblies ()
	{
		return MonoPackageManager_Resources.Assemblies;
	}

	public static String[] getDependencies ()
	{
		return MonoPackageManager_Resources.Dependencies;
	}

	public static String getApiPackageName ()
	{
		return MonoPackageManager_Resources.ApiPackageName;
	}
}

class MonoPackageManager_Resources {
	public static final String[] Assemblies = new String[]{
		"CaminoInca.dll",
		"ByteSmith.WindowsAzure.Messaging.Android.dll",
		"Cirrious.CrossCore.dll",
		"Cirrious.CrossCore.Droid.dll",
		"Cirrious.MvvmCross.Binding.dll",
		"Cirrious.MvvmCross.Binding.Droid.dll",
		"Cirrious.MvvmCross.Community.Plugins.Sqlite.dll",
		"Cirrious.MvvmCross.Community.Plugins.Sqlite.Droid.dll",
		"Cirrious.MvvmCross.dll",
		"Cirrious.MvvmCross.Droid.dll",
		"Cirrious.MvvmCross.Droid.Fragging.dll",
		"Cirrious.MvvmCross.Droid.FullFragging.dll",
		"Cirrious.MvvmCross.Localization.dll",
		"Cirrious.MvvmCross.Plugins.File.dll",
		"Cirrious.MvvmCross.Plugins.File.Droid.dll",
		"Cirrious.MvvmCross.Plugins.PictureChooser.dll",
		"Cirrious.MvvmCross.Plugins.PictureChooser.Droid.dll",
		"GCM.Client.dll",
		"Microsoft.WindowsAzure.Mobile.dll",
		"Microsoft.WindowsAzure.Mobile.Ext.dll",
		"Microsoft.WindowsAzure.Storage.dll",
		"MLearning.Core.dll",
		"Newtonsoft.Json.dll",
		"Square.OkHttp.dll",
		"Square.OkIO.dll",
		"Square.Picasso.dll",
		"System.Net.Http.Extensions.dll",
		"System.Net.Http.Primitives.dll",
		"Xamarin.Android.Support.v4.dll",
		"Xamarin.Android.Support.v7.AppCompat.dll",
		"Xamarin.Android.Support.v7.RecyclerView.dll",
		"System.Diagnostics.Tracing.dll",
		"System.Reflection.Emit.dll",
		"System.Reflection.Emit.ILGeneration.dll",
		"System.Reflection.Emit.Lightweight.dll",
		"System.ServiceModel.Security.dll",
		"System.Threading.Timer.dll",
	};
	public static final String[] Dependencies = new String[]{
	};
	public static final String ApiPackageName = null;
}
