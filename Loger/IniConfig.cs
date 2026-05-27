namespace Loger
{
    public class IniConfig
    {
        private readonly Dictionary<string, Dictionary<string, string>> _config;

        public IniConfig(string filePath)
        {
            _config = new Dictionary<string, Dictionary<string, string>>();
            LoadConfig(filePath);
        }

        private void LoadConfig(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"INI-файл не найден: {filePath}");

            string currentSection = null;
            foreach (var line in File.ReadAllLines(filePath))
            {
                string trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith(";") || trimmedLine.StartsWith("#"))
                    continue;

                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                {
                    currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2);
                    _config[currentSection] = new Dictionary<string, string>();
                }
                else if (currentSection != null && trimmedLine.Contains("="))
                {
                    var parts = trimmedLine.Split('=', 2);
                    _config[currentSection][parts[0].Trim()] = parts[1].Trim();
                }
            }
        }

        public string GetValue(string section, string key, string defaultValue = "")
        {
            if (_config.TryGetValue(section, out var sectionDict) && sectionDict.TryGetValue(key, out var value))
                return value;
            return defaultValue;
        }
    }
}