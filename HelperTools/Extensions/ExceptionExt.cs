using System;
using System.ServiceModel;
using System.Text;

namespace HelperTools.Extensions
{
	public static class ExceptionExt
	{
		public static string GetFullExceptionMessage(this Exception ex) {
			return ex.GetFullExceptionMessage(false, 0);
		}

		private static string GetFullExceptionMessage(this Exception ex, bool isInnerException, int innerExceptionNumber) {
			if (ex == null)
				return null;

			var sb = new StringBuilder();

			if (isInnerException) {
				sb.AppendLine();
				sb.AppendLine("InnerException " + innerExceptionNumber);
				sb.AppendLine("------------------------------");
			}

			if (!string.IsNullOrEmpty(ex.Message))
				sb.AppendLine(ex.Message);

			if (ex.InnerException != null) {
				innerExceptionNumber++;

				sb.AppendLine(ex.InnerException.GetFullExceptionMessage(true, innerExceptionNumber));
			}

			FaultException<ExceptionDetail> fe = ex as FaultException<ExceptionDetail>;
			if (fe?.Detail != null && !string.IsNullOrEmpty(fe.Detail.InnerException?.Message)) {
				innerExceptionNumber++;

				sb.AppendLine();
				sb.AppendLine("InnerException " + innerExceptionNumber);
				sb.AppendLine("------------------------------");
				sb.AppendLine(((FaultException<ExceptionDetail>) ex).Detail.InnerException.Message);
			}

			return sb.ToString();
		}

		public static string GetFullStackTrace(this Exception ex) {
			return ex.GetFullStackTrace(false, 0);
		}

		private static string GetFullStackTrace(this Exception ex, bool isInnerException, int innerExceptionNumber) {
			if (ex == null)
				return null;

			var sb = new StringBuilder();

			if (isInnerException) {
				sb.AppendLine();
				sb.AppendLine("InnerException " + innerExceptionNumber);
				sb.AppendLine("------------------------------");
			}

			if (!string.IsNullOrEmpty(ex.StackTrace))
				sb.AppendLine(ex.StackTrace);

			if (ex.InnerException != null) {
				innerExceptionNumber++;

				sb.AppendLine(ex.InnerException.GetFullStackTrace(true, innerExceptionNumber));
			}

			FaultException<ExceptionDetail> fe = ex as FaultException<ExceptionDetail>;
			if (fe != null && fe.Detail != null && fe.Detail.InnerException != null && !string.IsNullOrEmpty(fe.Detail.InnerException.StackTrace)) {
				innerExceptionNumber++;

				sb.AppendLine();
				sb.AppendLine("InnerException " + innerExceptionNumber);
				sb.AppendLine("------------------------------");
				sb.AppendLine((ex as FaultException<ExceptionDetail>).Detail.InnerException.StackTrace);
			}

			return sb.ToString();
		}
	}
}
