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
        public static HtmlString MySuperEditorForModel<T>(this IHtmlHelper<T> helper)
        {
            var sb = new StringBuilder();
            foreach (var memberInfo in helper.ViewData.Model.GetType().GetProperties())
            {
                var parameterExpression = Expression.Parameter(helper.ViewData.Model.GetType());
                var expr = Expression.Lambda(Expression.MakeMemberAccess(parameterExpression, memberInfo), parameterExpression);
                sb.Append(helper.MySuperEditorFor(expr));
            }

            return new HtmlString(sb.ToString());
        }
        
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

            if (!(expression.Body is MemberExpression memberExpression))
                throw new InvalidOperationException("Expression must be a member expression");
            
            var sb = new StringBuilder();

//            if (!memberExpression.Type.IsPrimitive && memberExpression.Type.IsClass)
//            {
//                foreach (var memberInfo in memberExpression.Type.GetProperties())
//                {
//                    var parameterExpression = Expression.Parameter(memberExpression.Type);
//                    var expr = Expression.Lambda(Expression.MakeMemberAccess(parameterExpression, memberInfo), parameterExpression);
//                    sb.Append(helper.MySuperEditorFor(expr));
//                }
//                return new HtmlString(sb.ToString());
//            }
            
            var memberName = memberExpression.Member.Name;
            
            sb.Append($"<label for=\"{memberName}\">{memberName}</label>");
            sb.Append("<br>");

            var memberValue = helper.ViewData.Model.GetType().GetProperty(memberExpression.Member.Name)?.GetValue(helper.ViewData.Model);
            
            sb.Append(TryGetInputByDataType(memberExpression, memberValue, out var input)
                ? input
                : GetInput(expression.Body.Type, memberName, memberValue));

            sb.Append("<br>");

            return new HtmlString(sb.ToString());
        }

        private static bool TryGetInputByDataType(MemberExpression expression, object memberValue, out string input)
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
                input = $@"<textarea id=""{memberName}"">{memberValue}</textarea>";
                return true;
            }

            input = $"<input id=\"{memberName}\" type=\"{GetInputTypeByDataType(attribute.DataType)}\" value=\"{memberValue}\"></input>";
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

        private static string GetInput(Type type, string memberName, object memberValue)
        {
            if (type.IsEnum)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"<select id={memberName}>");
                foreach (var enumName in type.GetEnumNames())
                {
                    sb.AppendLine($@"<option value=""{enumName}"">{enumName}</option>");
                }
                sb.Append("</select>");
                return sb.ToString();
            }

            string inputType;
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
                    inputType = "number";
                    break;
                case TypeCode.Boolean:
                    inputType = "checkbox";
                    break;
                case TypeCode.DateTime:
                    inputType = "datetime-local";
                    break;
                default:
                    inputType = "text";
                    break;
            }

            return $"<input id=\"{memberName}\" type=\"{inputType}\" value=\"{memberValue}\"></input>";
        }
    }
}