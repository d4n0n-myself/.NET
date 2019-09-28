using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace September26.Helpers
{
    public static class HtmlHelpers
    {
        public static HtmlString MySuperEditorFor<T,TResult>(this IHtmlHelper<T> helper,
            Expression<Func<T,TResult>> expression)
        {
            return MySuperEditorFor((IHtmlHelper) helper, expression);
        }

        public static HtmlString MySuperEditorFor(this IHtmlHelper helper,
            LambdaExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if (expression.Body.NodeType != ExpressionType.MemberAccess)
                throw new ArgumentException("Can't process anything but properties and fields", nameof(expression));

            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new InvalidOperationException("Expression must be a member expression");
            
            var sb = new StringBuilder();
            var memberName = memberExpression.Member.Name;
            
            sb.Append($"<label for=\"{memberName}\">{memberName}</label>");
            sb.Append("<br>");

            sb.Append(TryGetInputByDataType(memberExpression, out var input)
                ? input
                : $"<input id=\"{memberName}\" type=\"{GetInputType(expression.Body.Type)}\"> </input>");

            sb.Append("<br>");

            return new HtmlString(sb.ToString());
        }

        private static bool TryGetInputByDataType(MemberExpression expression, out string input)
        {
            var customAttributes = expression.Member.GetCustomAttributes(typeof(DataTypeAttribute), true);
            if (customAttributes.Length <= 0)
            {
                input = string.Empty;
                return false;
            }

            var attribute = (DataTypeAttribute) customAttributes.First();
            var memberName = expression.Member.Name;
            
            if (attribute.DataType == DataType.MultilineText)
            {
                input = $@"<textarea id=""{memberName}""></textarea>";
                return true;
            }

            input = $"<input id=\"{memberName}\" type=\"{GetInputTypeByDataType(attribute.DataType)}\"> </input>";
            return true;
        }

        private static string GetInputTypeByDataType(DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Time:
                    return "time";
                case DataType.Date:
                    return "date";
                case DataType.DateTime:
                    return "datetime-local";
                case DataType.Password:
                    return "password";
                case DataType.EmailAddress:
                    return "email";
                case DataType.Url:
                    return "url";
                case DataType.PhoneNumber:
                    return "tel";
                case DataType.Upload:
                    return "file";
                case DataType.ImageUrl:
                    return "url";
                default:
                    return "text";
            }
        }

        private static string GetInputType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return "number";
                case TypeCode.DateTime:
                    return "datetime-local";
                default:
                    return "text";
            }
        }
    }
}