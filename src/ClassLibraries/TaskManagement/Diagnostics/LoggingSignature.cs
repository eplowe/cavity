namespace Cavity.Diagnostics
{
    using System;
    using System.Diagnostics;
    using log4net;

    public struct LoggingSignature
    {
        public LoggingSignature(StackFrame frame)
            : this()
        {
            if (null == frame)
            {
                return;
            }

            Signature = "[{0}.{1} :: {2}]".FormatWith(
                frame.GetMethod().DeclaringType.Namespace,
                frame.GetMethod().DeclaringType.Name,
                frame.GetMethod().Name);

            Log = LogManager.GetLogger(frame.GetMethod().DeclaringType);
        }

        private ILog Log { get; set; }

        private string Signature { get; set; }

        public static bool operator ==(LoggingSignature obj,
                                       LoggingSignature comparand)
        {
            return obj.Equals(comparand);
        }

        public static bool operator !=(LoggingSignature obj,
                                       LoggingSignature comparand)
        {
            return !obj.Equals(comparand);
        }

        public static void Error(Exception exception)
        {
            if (null == exception)
            {
                Debug();
                return;
            }

            var result = new LoggingSignature(new StackFrame(1));
            if (!result.Log.IsErrorEnabled)
            {
                return;
            }

            result.Log.Error(result.Signature, exception);
            if (null == exception.InnerException)
            {
                return;
            }

            Error(exception.InnerException);
        }

        public static void Fatal(Exception exception)
        {
            if (null == exception)
            {
                Debug();
                return;
            }

            var result = new LoggingSignature(new StackFrame(1));
            if (!result.Log.IsFatalEnabled)
            {
                return;
            }

            result.Log.Fatal(result.Signature, exception);
            if (null == exception.InnerException)
            {
                return;
            }

            Fatal(exception.InnerException);
        }

        public static void Info(string message)
        {
            var result = new LoggingSignature(new StackFrame(1));
            if (result.Log.IsInfoEnabled)
            {
                result.Log.InfoFormat("{0} {1}", result.Signature, message);
            }
        }

        public static LoggingSignature Debug()
        {
            var result = new LoggingSignature(new StackFrame(1));
            if (result.Log.IsDebugEnabled)
            {
                result.Log.Debug(result.Signature);
            }

            return result;
        }

        public static LoggingSignature Debug(string message)
        {
            var result = new LoggingSignature(new StackFrame(1));
            if (result.Log.IsDebugEnabled)
            {
                result.Log.DebugFormat("{0} {1}", result.Signature, message);
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            return false;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return Signature;
        }
    }
}