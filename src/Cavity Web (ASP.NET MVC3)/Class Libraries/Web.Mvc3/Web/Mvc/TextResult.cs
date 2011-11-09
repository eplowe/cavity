namespace Cavity.Web.Mvc
{
    using System;
    using System.Text;
    using System.Web.Mvc;

    public class TextResult : ContentResult
    {
        public TextResult()
            : this(null)
        {
        }

        public TextResult(object model)
        {
            ContentEncoding = Encoding.UTF8;
            ContentType = "text/plain";
            if (null != model)
            {
                Content = model.ToString();
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (null == context)
            {
                throw new ArgumentNullException("context");
            }

            base.ExecuteResult(context);
        }
    }
}