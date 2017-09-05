using Clearsoft.BoxOffice.Common.Logging;
using System;
using System.Net.Http;
using log4net;
using System.Web.Http.Tracing;

namespace Clearsoft.BoxOffice.Web.Common
{
    public class SimpleTraceWriter : ITraceWriter
    {
        private readonly ILog _log;

        public SimpleTraceWriter(ILogManager logManager)
        {
            _log = logManager.GetLog(typeof(SimpleTraceWriter));
        }

        void ITraceWriter.Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            var rec = new TraceRecord(request, category, level);
            traceAction(rec);
            WriteTrace(rec);
        }

        public void WriteTrace(TraceRecord rec)
        {
            const string traceFormat = "RequestId={0}; {1}Kind={2};{3}Status={4};{5}Operation={6};{7}Operator={8};{9}Categpry={10} {11}Request={12} {13}Message={14}";
            var args = new object[]
            {
                rec.RequestId,
                Environment.NewLine,
                rec.Kind,
                Environment.NewLine,
                rec.Status,
                Environment.NewLine,
                rec.Operation,
                Environment.NewLine,
                rec.Operator,
                Environment.NewLine,
                rec.Category,
                Environment.NewLine,
                rec.Request,
                Environment.NewLine,
                rec.Message
            };

            switch (rec.Level)
            {
                case TraceLevel.Debug:
                    _log.DebugFormat(traceFormat, args);
                    break;
                case TraceLevel.Info:
                    _log.InfoFormat(traceFormat, args);
                    break;
                case TraceLevel.Warn:
                    _log.WarnFormat(traceFormat, args);
                    break;
                case TraceLevel.Error:
                    _log.ErrorFormat(traceFormat, args);
                    break;
                case TraceLevel.Fatal:
                    _log.FatalFormat(traceFormat, args);
                    break;
            }
        }
    }
}
