/*  This file is part of Chummer5a.
 *
 *  Chummer5a is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  Chummer5a is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with Chummer5a.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  You can obtain the full source code for Chummer5a at
 *  https://github.com/chummer5a/chummer5a
 */

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Chummer
{
    internal static class PerformanceDebugUtils
    {
        // Local diagnostics only: no character data or telemetry upload.
        public static bool Enabled { get; } = Environment.GetEnvironmentVariable("CHUMMER_PERF") == "1";
        private static readonly object s_LogLock = new object();
        private static readonly string s_LogName = "performance-" + DateTime.UtcNow.ToString("yyyyMMdd-HHmmss-fffffff", CultureInfo.InvariantCulture) + ".tsv";

        public static IDisposable Measure(string stage)
        {
            return Enabled ? new TimingScope(stage) : null;
        }

        public static void Record(string stage, TimeSpan elapsed)
        {
            if (!Enabled)
                return;
            try
            {
                lock (s_LogLock)
                {
                    string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                    Directory.CreateDirectory(directory);
                    File.AppendAllText(Path.Combine(directory, s_LogName),
                        DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture) + "\t"
                        + elapsed.TotalMilliseconds.ToString("F3", CultureInfo.InvariantCulture) + "\t"
                        + stage.Replace('\t', ' ').Replace('\r', ' ').Replace('\n', ' ') + Environment.NewLine);
                }
            }
            catch (IOException)
            {
                // Diagnostics must not prevent normal operation (e.g. full disk).
            }
            catch (UnauthorizedAccessException)
            {
                // The application directory may be read-only.
            }
        }

        private sealed class TimingScope : IDisposable
        {
            private readonly string _stage;
            private readonly Stopwatch _watch = Stopwatch.StartNew();
            private int _disposed;

            public TimingScope(string stage)
            {
                _stage = stage;
            }

            public void Dispose()
            {
                if (Interlocked.Exchange(ref _disposed, 1) == 0)
                {
                    _watch.Stop();
                    Record(_stage, _watch.Elapsed);
                }
            }
        }

        public static void TaskEnd(this Stopwatch sw, string task)
        {
#if !DEBUG
            if (!Enabled)
                return;
#endif
            sw.Stop();
            Record(task, sw.Elapsed);
#if DEBUG
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} finished in {1} ms", task, sw.ElapsedMilliseconds));
#endif
            sw.Restart();
        }
    }
}
