using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium.Appium.Android;
using System.IO;
using System.Reflection;

namespace XamarinFormsStarterKit.UserInterfaceVisualizer
{
	public static class AppiumRunner
	{

		private static AppiumDriver<IOSElement> iosDriver;
		private static AppiumDriver<AndroidElement> droiddriver;


		public static TimeSpan INIT_TIMEOUT_SEC = TimeSpan.FromSeconds(180);
		public static TimeSpan IMPLICIT_TIMEOUT_SEC = TimeSpan.FromSeconds(5);
		private static string AppPath = "";


		public static void Execute()
		{
			Console.WriteLine("Starting Program...");
			Console.WriteLine(DateTime.Now);

			var appName = "XamlPlayground.iOS.app";
			var appLocation = @"../../../../../../";
			var appExecutingPath = @"XamlPlayground/XamlPlayground.iOS/bin/iPhoneSimulator/Release/";

			var projectLocation = Path.GetDirectoryName(typeof(AppiumRunner).Assembly.Location);
			var projectRoot = Path.GetFullPath(Path.Combine(Assembly.GetExecutingAssembly().Location, appLocation));

			AppPath = Path.Combine(projectRoot, appExecutingPath);
			AppPath = Path.Combine(AppPath, appName);

			// 
			Console.WriteLine("Copying app file");

			var destinationFile = Path.Combine(projectLocation, appName);

			var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                   
            
         //   fix this please 
		//	File.Copy(AppPath,destinationFile , true);

			Console.WriteLine("Copied app file");

			CreateIOSSimulatorAndScreenshots();
			// CreateAndroidSimulatorAndScreenshots();

			Console.WriteLine(DateTime.Now);

			Console.WriteLine("Completed All");

			Console.Read();

		}



		private static void CreateIOSSimulatorAndScreenshots()
		{
			foreach (var device in DeviceListOnlyIOSPhoneEdgeCases)
			{
				Console.WriteLine("Opening... " + device);

				Console.WriteLine(DateTime.Now);

				DesiredCapabilities capabilities = new DesiredCapabilities();
				capabilities.SetCapability("platformVersion", "11.3");
				capabilities.SetCapability("deviceName", device);
				capabilities.SetCapability("platformName", "iOS");
				//capabilities.SetCapability("newCommandTimeout", "120");
				//capabilities.SetCapability("noReset", "true");
				//capabilities.SetCapability("printPageSourceOnFindFailure", "true");
				//capabilities.SetCapability("enablePerformanceLogging", "true");
				capabilities.SetCapability("automationName", "XCUITest");
				capabilities.SetCapability("app", AppPath);

				Uri serverUri = new Uri("http://localhost:4723/wd/hub");
				iosDriver = new IOSDriver<IOSElement>(serverUri, capabilities, INIT_TIMEOUT_SEC);
				iosDriver.Manage().Timeouts().ImplicitWait = INIT_TIMEOUT_SEC;
				Thread.Sleep(5000);

				Console.WriteLine("taking screenshot... " + device);

				var fileName = String.Format("{0}{1}{2}{3}", "Screenshots/", "ios/", device, ".png");
				var screenShot = iosDriver.GetScreenshot();
				screenShot.SaveAsFile(fileName);

				Console.WriteLine("Shutting down... " + device);

				iosDriver.Quit();
				Console.WriteLine(DateTime.Now);


			}
		}

		private static void CreateAndroidSimulatorAndScreenshots()
		{

			DesiredCapabilities capabilities = new DesiredCapabilities();
			capabilities.SetCapability("platformVersion", "6.0");
			capabilities.SetCapability("deviceName", "Android_Accelerated_x86");
			capabilities.SetCapability("avd", "Android_Accelerated_x86");
			capabilities.SetCapability("platformName", "Android");
			capabilities.SetCapability("automationName", "UiAutomator2");
			capabilities.SetCapability("app", "/Users/arunbalakrishnan/Desktop/com.companyname.XamarinFormsStarterKit-x86.apk");

			Uri serverUri = new Uri("http://0.0.0.0:4723/wd/hub");
			droiddriver = new AndroidDriver<AndroidElement>(serverUri, capabilities, INIT_TIMEOUT_SEC);
			droiddriver.Manage().Timeouts().ImplicitWait = INIT_TIMEOUT_SEC;

			Console.WriteLine("taking screenshot... " + "Nexus_Edited_6_API_27");

			var fileName = String.Format("{0}{1}{2}{3}", "Screenshots/", "Pixel 2 API 26" + " ", DateTime.Now.ToString("dd HH mm ss"), ".png");
			var screenShot = droiddriver.GetScreenshot();
			screenShot.SaveAsFile(fileName);


			droiddriver.Quit();
			Console.WriteLine(DateTime.Now);

		}

		public static string iPad5 = "iPad (5th generation)";
		public static string iPadair = "iPad Air";
		public static string iPadair2 = "iPad Air 2";
		public static string iPadpro10 = "iPad Pro (10.5-inch)";
		public static string iPadpro12 = "iPad Pro (12.9-inch)";
		public static string iPadpro9 = "iPad Pro (9.7-inch)";
		public static string iPhone5s = "iPhone 5s";
		public static string iPhone6 = "iPhone 6";
		public static string iPhone6s = "iPhone 6s";
		public static string iPhone6sp = "iPhone 6s Plus";
		public static string iPhone7 = "iPhone 7";
		public static string iPhone7P = "iPhone 7 Plus";
		public static string iPhone8 = "iPhone 8";
		public static string iPhone8p = "iPhone 8 Plus";
		public static string iPhoneSE = "iPhone SE";
		public static string iPhoneX = "iPhone X";


		public static List<string> DeviceListAllIOS = new List<string>
		{

		iPhone5s ,
		iPhone6 ,
		iPhone6s ,
		iPhone6sp ,
		iPhone7 ,
		iPhone7P ,
		iPhone8 ,
		iPhone8p ,
		iPhoneSE ,
		iPhoneX ,
		iPad5 ,
		iPadair ,
		iPadair2 ,
		iPadpro10 ,
		iPadpro12 ,
		iPadpro9 ,
		};


		public static List<string> DeviceListOnlyIOSPhone = new List<string>
		{

		iPhone5s ,
		iPhone6 ,
		iPhone6s ,
		iPhone6sp ,
		iPhone7 ,
		iPhone7P ,
		iPhone8 ,
		iPhone8p ,
		iPhoneSE ,
		iPhoneX ,

		};

		public static List<string> DeviceListOnlyIOSPhoneEdgeCases = new List<string>
		{
		iPhoneX,
		iPhone8 ,
		iPhone8p ,
		iPhoneSE ,
		};


		//android
		//480_800_4
		//480_854_4.3
		//540_960_4.5
		//720_1280_4.6
		//768_1280_4.7
		//800_480_4.8
		//800_1280_5
		//854_480_5.1
		//960_540_5.2
		//1080_1920_5.3
		//1280_720_5.4
		//1440_2560_5.5
		//1920_1080_5.7
		//2560_1440_5.8
		//2960_1440_6
		//2960_1440_6.2


	}
}
