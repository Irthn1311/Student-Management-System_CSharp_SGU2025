using System;
using System.IO;
using System.Web.Script.Serialization;
using Student_Management_System_CSharp_SGU2025.Config;

namespace Student_Management_System_CSharp_SGU2025.Scheduling
{
	/// <summary>
	/// Service for loading and managing timetable configuration from JSON file.
	/// </summary>
	public static class TimetableConfigService
	{
		private static readonly string ConfigFileName = "timetable_config.json";
		private static readonly JavaScriptSerializer JsonSerializer = new JavaScriptSerializer();

		/// <summary>
		/// Get the path to the config file (relative to executable directory).
		/// </summary>
		private static string GetConfigPath()
		{
			string exeDir = AppDomain.CurrentDomain.BaseDirectory;
			// Look for Config folder in the same directory as executable
			string configPath = Path.Combine(exeDir, "Config", ConfigFileName);
			
			// If not found, try parent directory (for development)
			if (!File.Exists(configPath))
			{
				string parentConfigPath = Path.Combine(Directory.GetParent(exeDir).FullName, "Config", ConfigFileName);
				if (File.Exists(parentConfigPath))
					return parentConfigPath;
			}
			
			return configPath;
		}

		/// <summary>
		/// Load configuration from JSON file. Creates default config if file doesn't exist.
		/// </summary>
		public static TimetableConfigRoot Load()
		{
			string configPath = GetConfigPath();

			// If config file doesn't exist, create default and return it
			if (!File.Exists(configPath))
			{
				var defaultConfig = CreateDefaultConfig();
				Save(defaultConfig, configPath);
				return defaultConfig;
			}

			try
			{
				string jsonContent = File.ReadAllText(configPath);
				var config = JsonSerializer.Deserialize<TimetableConfigRoot>(jsonContent);
				
				// Ensure defaults nếu null
				if (config.CauHinhTietHoc == null) config.CauHinhTietHoc = new SlotsConfig();
				if (config.CauHinhTrongSo == null) config.CauHinhTrongSo = new WeightConfig();
				if (config.ThamSoThuatToan == null) config.ThamSoThuatToan = new AlgorithmDefaults();

				return config;
			}
			catch (Exception ex)
			{
				// On error, return default config
				Console.WriteLine($"[WARNING] Failed to load config from {configPath}: {ex.Message}. Using defaults.");
				return CreateDefaultConfig();
			}
		}

		/// <summary>
		/// Create default configuration with sensible defaults.
		/// </summary>
		private static TimetableConfigRoot CreateDefaultConfig()
		{
			return new TimetableConfigRoot
			{
				CauHinhTietHoc = new SlotsConfig
				{
					ThuBatDau = 2,
					ThuKetThuc = 7,
					SoTietMoiNgay = 10
				},
				CauHinhTrongSo = new WeightConfig
				{
					TrongSoMonNangLienTiep = 5,
					TrongSoTrenMotNgay = 3,
					TrongSoCanBangNgay = 2,
					TrongSoOnDinh = 1
				},
				ThamSoThuatToan = new AlgorithmDefaults
				{
					SoVongLapToiDa = 5000,
					DoDaiTabu = 9,
					ThoiGianChayToiDaGiay = 90,
					GioiHanKhongCaiThien = 500
				}
			};
		}

		/// <summary>
		/// Save configuration to JSON file.
		/// </summary>
		private static void Save(TimetableConfigRoot config, string path)
		{
			try
			{
				string directory = Path.GetDirectoryName(path);
				if (!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}

				// Format JSON nicely (simple indentation)
				var jsonContent = JsonSerializer.Serialize(config);
				File.WriteAllText(path, jsonContent);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[WARNING] Failed to save config to {path}: {ex.Message}");
			}
		}

		/// <summary>
		/// Apply configuration to a ScheduleRequest. Overrides request's config and algorithm parameters from config defaults.
		/// </summary>
		public static ScheduleRequest ApplyConfigToRequest(ScheduleRequest request, TimetableConfigRoot config)
		{
			if (request == null) throw new ArgumentNullException(nameof(request));
			if (config == null) throw new ArgumentNullException(nameof(config));

			// Apply slots and weight config
			if (config.CauHinhTietHoc != null)
			{
				request.SlotsConfig = config.CauHinhTietHoc;
			}

			if (config.CauHinhTrongSo != null)
			{
				request.WeightConfig = config.CauHinhTrongSo;
			}

			// Apply algorithm defaults (can be overridden later by UI)
			if (config.ThamSoThuatToan != null)
			{
				request.IterMax = config.ThamSoThuatToan.SoVongLapToiDa;
				request.TabuTenure = config.ThamSoThuatToan.DoDaiTabu;
				request.TimeBudgetSec = config.ThamSoThuatToan.ThoiGianChayToiDaGiay;
				request.NoImproveLimit = config.ThamSoThuatToan.GioiHanKhongCaiThien;
			}

			return request;
		}
	}
}

