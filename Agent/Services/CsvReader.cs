using System.Globalization;
using Agent.Models;
using Agent.Services.Interfaces;

namespace Agent.Services
{
    public class CsvReader : ICsvReader, IDisposable
    {
        private readonly StreamReader _gpsStreamReader;
        private readonly StreamReader _accelerometerStreamReader;
        private readonly ICommonLogger _logger;

        public CsvReader(ICommonLogger logger)
        {
            _accelerometerStreamReader = new StreamReader(@"Data/accelerometer.csv");
            _gpsStreamReader = new StreamReader(@"Data/gps.csv");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            InitializeStreamReaders();
        }

        private void InitializeStreamReaders()
        {
            SkipHeaderLine(_accelerometerStreamReader);
            SkipHeaderLine(_gpsStreamReader);
        }

        private void SkipHeaderLine(StreamReader reader)
        {
            reader.ReadLine();
        }

        public async Task<AggregatedData?> Read()
        {
            var accelerometerData = await ReadDataLineAsync(_accelerometerStreamReader, ParseAccelerometer);
            var gpsData = await ReadDataLineAsync(_gpsStreamReader, ParseGps);

            if (gpsData is null && accelerometerData is null)
            {
                return null;
            }

            return new AggregatedData
            {
                Accelerometer = accelerometerData,
                Gps = gpsData,
                Timestamp = DateTime.UtcNow,
                UserId = 0
            };
        }

        private async Task<T?> ReadDataLineAsync<T>(StreamReader reader, Func<string, T?> parser) where T : class
        {
            var line = await reader.ReadLineAsync();
            if (line is null)
            {
                _logger.Warning($"{typeof(T).Name} line is null");
                return null;
            }

            return parser(line);
        }

        private Accelerometer? ParseAccelerometer(string line)
        {
            return ParseDataLine(line, parts =>
            {
                if (int.TryParse(parts[0], out int x) &&
                    int.TryParse(parts[1], out int y) &&
                    int.TryParse(parts[2], out int z))
                {
                    return new Accelerometer
                    {
                        X = x,
                        Y = y,
                        Z = z
                    };
                }
                else
                {
                    _logger.Warning("Invalid accelerometer data format");
                    return null;
                }
            });
        }

        private Gps? ParseGps(string line)
        {
            return ParseDataLine(line, parts =>
            {
                if (float.TryParse(parts[0], NumberStyles.Any, CultureInfo.InvariantCulture, out float longitude) &&
                    float.TryParse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture, out float latitude))
                {
                    return new Gps
                    {
                        Longitude = longitude,
                        Latitude = latitude
                    };
                }
                else
                {
                    _logger.Warning("Invalid GPS data format");
                    return null;
                }
            });
        }


        private T? ParseDataLine<T>(string line, Func<string[], T?> createInstance) where T : class
        {
            var parts = line.Split(',');
            if (parts.Length < 2)
            {
                _logger.Warning("Invalid data format");
                return null;
            }

            try
            {
                return createInstance(parts);
            }
            catch (Exception ex)
            {
                _logger.Warning($"Failed to parse data: {ex.Message}");
                return null;
            }
        }

        public void Dispose()
        {
            _accelerometerStreamReader.Dispose();
            _gpsStreamReader.Dispose();
        }
    }
}