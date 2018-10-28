using System;

namespace HelperTools
{
	public static partial class MathExt
	{


		#region CompareValue
		public static bool CompareValue(object leftValue, object rightValue, string operatorValue)
		{
			if (string.IsNullOrEmpty(operatorValue))
				return false;

			if (leftValue == null || rightValue == null)
				return false;

			Type leftType = leftValue.GetType();
			Type rightType = rightValue.GetType();

			if (!leftType.IsValueType || leftType.IsEnum || !leftType.IsPrimitive)
				return false;

			if (!rightType.IsValueType || rightType.IsEnum || !rightType.IsPrimitive)
				return false;

			if ((leftType == typeof(DateTime) || rightType == typeof(DateTime)) && !rightType.Equals(leftValue))
				return false;

			if (leftType == typeof(DateTime) && rightType == typeof(DateTime))
				return CompareValueDateTime((DateTime)leftValue, (DateTime)rightValue, operatorValue);

			return CompareValueDouble((double)leftValue, (double)rightValue, operatorValue);
		}

		public static bool CompareValueDateTime(DateTime? leftValue, DateTime? rightValue, string operatorValue)
		{
			if (string.IsNullOrEmpty(operatorValue))
				return false;

			if (operatorValue == ">=")
				return leftValue >= rightValue;
			if (operatorValue == ">")
				return leftValue > rightValue;
			if (operatorValue == "<=")
				return leftValue <= rightValue;
			if (operatorValue == "<")
				return leftValue < rightValue;

			return false;
		}

		public static bool CompareValueDouble(double leftValue, double rightValue, string operatorValue)
		{
			if (string.IsNullOrEmpty(operatorValue))
				return false;

			if (operatorValue == ">=")
				return leftValue >= rightValue;
			if (operatorValue == ">")
				return leftValue > rightValue;
			if (operatorValue == "<=")
				return leftValue <= rightValue;
			if (operatorValue == "<")
				return leftValue < rightValue;

			return false;
		}

		#endregion

	}
}