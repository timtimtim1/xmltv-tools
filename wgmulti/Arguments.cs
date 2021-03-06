﻿using System;
using System.Configuration;
using System.IO;

namespace wgmulti
{
  class Arguments
  {
    public static String[] args = Environment.GetCommandLineArgs();
    public static String cmdArgs = "None";
    public static String configDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
    public static Double timeOffset = 0;
    public static bool help = false;
    public static bool convertTimesToLocal = true;
    public static bool deleteWorkFolder = false;
    public static int maxAsyncProcesses = 10;
    public static bool groupChannelsBySiteIni = true;
    public static int maxChannelsInGroup = 10;
    public static int processTimeout = 240;
    public static bool showConsole = false;
    public static bool randomStartOrder = true;
    public static bool generateReport = true;
    public static bool combineLogFiles = true;
    public static bool debug = false;

    public static bool IsLinux()
    {
      int p = (int)Environment.OSVersion.Platform;
      return (p == 4 || p == 6 || p == 128) ? true : false;
    }

    static Arguments()
    {
      var val = ConfigurationManager.AppSettings["configDir"] ?? @"ServerCare\WebGrab";

      if (val == @"ServerCare\WebGrab")
      {
        if (IsLinux())
          configDir = Directory.GetCurrentDirectory();
        else
          configDir = Path.Combine(configDir, val);
      }
      else
        configDir = val;      

      ///Overwrite config dir by cmd
      if (args.Length == 2)
      {
        configDir = args[1];
        cmdArgs = args[1];
      }

      val = ConfigurationManager.AppSettings["Debug"] ?? "false";
      debug = Convert.ToBoolean(val);

      val = ConfigurationManager.AppSettings["DeleteWorkFolder"] ?? "false";
      deleteWorkFolder = Convert.ToBoolean(val);

      val = ConfigurationManager.AppSettings["GroupChannelsBySiteIni"] ?? "true";
      groupChannelsBySiteIni = Convert.ToBoolean(val);

      val = ConfigurationManager.AppSettings["RandomStartOrder"] ?? "true";
      randomStartOrder = Convert.ToBoolean(val);

      val = ConfigurationManager.AppSettings["GenerateResultsReport"] ?? "true";
      generateReport = Convert.ToBoolean(val);

      val = ConfigurationManager.AppSettings["CombineLogFiles"] ?? "true";
      combineLogFiles = Convert.ToBoolean(val);

      val = ConfigurationManager.AppSettings["ConvertTimesToLocal"] ?? "true";
      convertTimesToLocal = Convert.ToBoolean(val);

      val = ConfigurationManager.AppSettings["MaxAsyncProcesses"] ?? "10";
      maxAsyncProcesses = val != "0" ? Convert.ToInt32(val) : 10;

      val = ConfigurationManager.AppSettings["MaxChannelsInGroup"] ?? "10";
      maxChannelsInGroup = val != "0" ? Convert.ToInt32(val) : 10;

      val = ConfigurationManager.AppSettings["ProcessTimeout"] ?? "240";
      processTimeout = Convert.ToInt32(val);

      //if (!IsLinux())
      //{ 
        val = ConfigurationManager.AppSettings["ShowWebGrabConsole"] ?? "true";
        showConsole = Convert.ToBoolean(val);
      //}
    }
  }
}
