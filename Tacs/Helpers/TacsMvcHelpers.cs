using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Tacs.Helpers
{
    public static class TacsMvcHelpers
    {

        public static MvcHtmlString ListArrayItems(this HtmlHelper html, string[] list)
        {
            TagBuilder tag = new TagBuilder("ul");
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString TacsDisplayTextFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            MemberExpression member = expression.Body as MemberExpression;
            String fieldName = member.Member.Name;

            String requiredMark = "";

            if (member.Member.CustomAttributes.Where(a => a.AttributeType.Equals(typeof(RequiredAttribute))).Any())
            {
                requiredMark = "*";
            }

            var displayName = member.Member.CustomAttributes.Where(a => a.AttributeType.Equals(typeof(LocalizedDisplayNameAttribute))).FirstOrDefault();
            String labelText = "";
            if (displayName != null)
            {
                labelText = i18n.T(displayName.ConstructorArguments.FirstOrDefault().Value.ToString());
            }

            return new MvcHtmlString(labelText + requiredMark);
        }

        public static MvcHtmlString TacsDateSelectFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            TagBuilder tag = TacsMvcHelpers.TacsFormGroupFor(html, expression);

            var label = TacsMvcHelpers.TacsLabelFor(html, expression);
            tag.InnerHtml += label.ToHtmlString();
            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("date");
            div.AddCssClass("input-group");
            //var dateField = System.Web.Mvc.Html.InputExtensions.TextBoxFor(html, expression, new { type="text", @class = "form-control date-filter" });
            //String dateField = "<input data-format=\"dd/MM/yyyy hh:mm\" type=\"text\"></input>";
            TagBuilder datefield = new TagBuilder("input");
            datefield.Attributes.Add("data-format", "dd/MM/yyyy hh:mm");
            datefield.Attributes.Add("type", "text");

            MemberExpression memberExp = (MemberExpression)expression.Body;
            var objectMember = Expression.Convert(memberExp, typeof(object));

            var getterLambda = Expression.Lambda<Func<object>>(objectMember);

            var getter = getterLambda.Compile();

            String fieldValue = (String) getter.DynamicInvoke();
            datefield.InnerHtml = fieldValue;

            TagBuilder span = new TagBuilder("span");
            span.AddCssClass("input-group-addon");

            TagBuilder i = new TagBuilder("i");
            i.AddCssClass("fa");
            i.AddCssClass("fa-calendar");
            i.AddCssClass("fa-lg");

            span.InnerHtml = i.ToString();

            div.InnerHtml += datefield.ToString() + span.ToString();
            TagBuilder small = new TagBuilder("small");
            small.AddCssClass("help-block");
            var validationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(html, expression);
            small.InnerHtml += validationMessage.ToHtmlString();
            tag.InnerHtml += div + small.ToString();
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString TacsEnumDropDownFor<TModel, TEnum>(this HtmlHelper<TModel> html, Expression<Func<TModel, TEnum>> expression, string optionalLabel, object htmlAttributes)
        {
            TagBuilder tag = TacsMvcHelpers.TacsFormGroupFor(html, expression);
            var label = TacsMvcHelpers.TacsLabelFor(html, expression);
            tag.InnerHtml += label.ToHtmlString();
            TagBuilder div = new TagBuilder("div");

            var dropDown = System.Web.Mvc.Html.SelectExtensions.EnumDropDownListFor(html, expression, optionalLabel, htmlAttributes);
            div.InnerHtml += dropDown.ToHtmlString();
            TagBuilder small = new TagBuilder("small");
            small.AddCssClass("help-block");
            var validationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(html, expression);
            small.InnerHtml += validationMessage.ToHtmlString();
            div.InnerHtml += small.ToString();
            tag.InnerHtml += div;
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString TacsLiteralEnumDropDownFor<TModel, TEnum>(this HtmlHelper<TModel> html, Expression<Func<TModel, TEnum>> expression, string optionalLabel, object htmlAttributes)
        {
            TagBuilder tag = TacsMvcHelpers.TacsFormGroupFor(html, expression);
            var label = TacsMvcHelpers.TacsLabelFor(html, expression);
            tag.InnerHtml += label.ToHtmlString();
            TagBuilder div = new TagBuilder("div");

            TagBuilder dropDown = new TagBuilder("select");



            div.InnerHtml += dropDown.ToString();
            TagBuilder small = new TagBuilder("small");
            small.AddCssClass("help-block");
            var validationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(html, expression);
            small.InnerHtml += validationMessage.ToHtmlString();
            div.InnerHtml += small.ToString();
            tag.InnerHtml += div;
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString TacsCheckboxFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            MemberExpression member = expression.Body as MemberExpression;
            String fieldName = member.Member.Name;

            TagBuilder tag = new TagBuilder("div");
            tag.AddCssClass("checkbox");
            tag.AddCssClass("form-group");

            TagBuilder tagLabel = new TagBuilder("label");
            tagLabel.AddCssClass("form-checkbox");
            tagLabel.AddCssClass("form-normal");
            tagLabel.AddCssClass("form-primary");
            tagLabel.AddCssClass("form-text");
            if (!html.ViewData.ModelState.IsValidField(fieldName))
            {
                tag.AddCssClass("has-error");
            }

            var tagCheckbox = System.Web.Mvc.Html.InputExtensions.CheckBox(html, fieldName);
            var tagDisplay = TacsMvcHelpers.TacsDisplayTextFor(html, expression);
            var tagValidation = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(html, expression);
            tagLabel.InnerHtml += tagCheckbox.ToHtmlString();
            tagLabel.InnerHtml += tagDisplay.ToHtmlString();
            tagLabel.InnerHtml += "<br />";
            tagLabel.InnerHtml += tagValidation.ToHtmlString();

            tag.InnerHtml += tagLabel.ToString();
            return new MvcHtmlString(tag.ToString());

        }

        public static TagBuilder TacsFormGroupFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            MemberExpression member = expression.Body as MemberExpression;
            String fieldName = member.Member.Name;
            TagBuilder tag = new TagBuilder("div");
            tag.AddCssClass("form-group");

            if (!html.ViewData.ModelState.IsValidField(fieldName))
            {
                tag.AddCssClass("has-error");
            }
            return tag;
        }

        public static MvcHtmlString TacsLabelFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            MemberExpression member = expression.Body as MemberExpression;
            String requiredMark = "";

            if (member.Member.CustomAttributes.Where(a => a.AttributeType.Equals(typeof(RequiredAttribute))).Any())
            {
                requiredMark = "*";
            }

            var displayName = member.Member.CustomAttributes.Where(a => a.AttributeType.Equals(typeof(LocalizedDisplayNameAttribute))).FirstOrDefault();
            String labelText = "";
            if (displayName != null)
            {
                labelText = i18n.T(displayName.ConstructorArguments.FirstOrDefault().Value.ToString());
            }
            var tagHtmlString = System.Web.Mvc.Html.LabelExtensions.Label(html, labelText + requiredMark, new { @class = "control-label" });
            return tagHtmlString;
        }

        public static MvcHtmlString TacsTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            TagBuilder tag = TacsMvcHelpers.TacsFormGroupFor(html, expression);

            var label = TacsMvcHelpers.TacsLabelFor(html, expression);
            tag.InnerHtml += label.ToHtmlString();
            TagBuilder div = new TagBuilder("div");
            var dropDown = System.Web.Mvc.Html.TextAreaExtensions.TextAreaFor(html, expression, new { @class = "form-control", rows = "10" });
            div.InnerHtml += dropDown.ToHtmlString();
            TagBuilder small = new TagBuilder("small");
            small.AddCssClass("help-block");
            var validationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(html, expression);
            small.InnerHtml += validationMessage.ToHtmlString();
            div.InnerHtml += small.ToString();
            tag.InnerHtml += div;
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString TacsTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            MemberExpression member = expression.Body as MemberExpression;


            TagBuilder tag = TacsMvcHelpers.TacsFormGroupFor(html, expression);

            var label = TacsMvcHelpers.TacsLabelFor(html, expression);
            tag.InnerHtml += label.ToHtmlString();
            TagBuilder div = new TagBuilder("div");
            var textBox = System.Web.Mvc.Html.InputExtensions.TextBoxFor(html, expression, new { @class = "form-control" });
            div.InnerHtml += textBox.ToHtmlString();
            TagBuilder small = new TagBuilder("small");
            small.AddCssClass("help-block");
            var validationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(html, expression);
            small.InnerHtml += validationMessage.ToHtmlString();
            div.InnerHtml += small.ToString();
            tag.InnerHtml += div;
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString TacsPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            MemberExpression member = expression.Body as MemberExpression;


            TagBuilder tag = TacsMvcHelpers.TacsFormGroupFor(html, expression);

            var label = TacsMvcHelpers.TacsLabelFor(html, expression);
            tag.InnerHtml += label.ToHtmlString();
            TagBuilder div = new TagBuilder("div");
            var textBox = System.Web.Mvc.Html.InputExtensions.PasswordFor(html, expression, new { @class = "form-control" });
            div.InnerHtml += textBox.ToHtmlString();
            TagBuilder small = new TagBuilder("small");
            small.AddCssClass("help-block");
            var validationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(html, expression);
            small.InnerHtml += validationMessage.ToHtmlString();
            div.InnerHtml += small.ToString();
            tag.InnerHtml += div;
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString TacsTextBoxDisabledFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            MemberExpression member = expression.Body as MemberExpression;


            TagBuilder tag = TacsMvcHelpers.TacsFormGroupFor(html, expression);

            var label = TacsMvcHelpers.TacsLabelFor(html, expression);
            tag.InnerHtml += label.ToHtmlString();
            TagBuilder div = new TagBuilder("div");
            var dropDown = System.Web.Mvc.Html.InputExtensions.TextBoxFor(html, expression, new { @class = "form-control", disabled = "disabled" });
            div.InnerHtml += dropDown.ToHtmlString();
            TagBuilder small = new TagBuilder("small");
            small.AddCssClass("help-block");
            var validationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(html, expression);
            small.InnerHtml += validationMessage.ToHtmlString();
            div.InnerHtml += small.ToString();
            tag.InnerHtml += div;
            return new MvcHtmlString(tag.ToString());
        }

        public static MvcHtmlString TacsDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, String emptyLabel)
        {
            TagBuilder tag = TacsMvcHelpers.TacsFormGroupFor(html, expression);
            var label = TacsMvcHelpers.TacsLabelFor(html, expression);
            tag.InnerHtml += label.ToHtmlString();
            TagBuilder div = new TagBuilder("div");
            var dropDown = System.Web.Mvc.Html.SelectExtensions.DropDownListFor(html, expression, selectList, emptyLabel, new { @class = "form-control selectpicker", width = "200", style = "width: 200px" });
            div.InnerHtml += dropDown.ToHtmlString();
            TagBuilder small = new TagBuilder("small");
            small.AddCssClass("help-block");
            var validationMessage = System.Web.Mvc.Html.ValidationExtensions.ValidationMessageFor(html, expression);
            small.InnerHtml += validationMessage.ToHtmlString();
            div.InnerHtml += small.ToString();
            tag.InnerHtml += div;
            return new MvcHtmlString(tag.ToString());
        }


        public static void FlashInfo(this Controller controller, string message)
        {
            controller.TempData["info"] = message;
        }
        public static void FlashWarning(this Controller controller, string message)
        {
            controller.TempData["warning"] = message;
        }
        public static void FlashError(this Controller controller, string message)
        {
            controller.TempData["error"] = message;
        }

        public static MvcHtmlString Flash(this HtmlHelper helper)
        {

            var message = "";
            var typeName = "";
            if (helper.ViewContext.TempData["info"] != null)
            {
                message = helper.ViewContext.TempData["info"].ToString();
                typeName = "success";
            }
            else if (helper.ViewContext.TempData["warning"] != null)
            {
                message = helper.ViewContext.TempData["warning"].ToString();
                typeName = "warning";
            }
            else if (helper.ViewContext.TempData["error"] != null)
            {
                message = helper.ViewContext.TempData["error"].ToString();
                typeName = "danger";
            }
            var sb = new StringBuilder();
            if (!String.IsNullOrEmpty(message))
            {
                sb.AppendLine("<script>");
                sb.AppendLine("$(document).ready(function(){");
                sb.AppendLine(" $.niftyNoty({");
                sb.AppendLine("     type: '" + typeName + "',");
                sb.AppendLine("     timer: 3500, ");
                //sb.AppendLine("     icon : 'fa fa-bolt fa-2x',");
                sb.AppendLine("     container : '#content-container',");
                //sb.AppendLine("     title : 'Server Load Limited',");
                sb.AppendLine("     message : '" + HttpUtility.HtmlEncode(message).Replace(Environment.NewLine, "<br />") + "'");
                sb.AppendLine(" });");
                sb.AppendLine("});");
                sb.AppendLine("</script>");
            }
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString TacsSubmitButton(this HtmlHelper helper, string buttonText, object htmlAttributes = null)
        {
            StringBuilder html = new StringBuilder();
            html.AppendFormat("<input type = 'submit' value = '{0}' ", buttonText);
            //{ class = btn btn-default, id = create-button }
            var attributes = helper.AttributeEncode(htmlAttributes);
            if (!string.IsNullOrEmpty(attributes))
            {
                attributes = attributes.Trim('{', '}');
                var attrValuePairs = attributes.Split(',');
                foreach (var attrValuePair in attrValuePairs)
                {
                    var equalIndex = attrValuePair.IndexOf('=');
                    var attrValue = attrValuePair.Split('=');
                    html.AppendFormat("{0}='{1}' ", attrValuePair.Substring(0, equalIndex).Trim(), attrValuePair.Substring(equalIndex + 1).Trim());
                }
            }
            html.Append("/>");
            return new MvcHtmlString(html.ToString());
        }
    }
}
