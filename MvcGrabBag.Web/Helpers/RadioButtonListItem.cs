namespace MvcGrabBag.Web.Helpers
{
    public class RadioButtonListItem<T>
    {
        public bool Selected { get; set; }
        public string Text { get; set; }
        public T Value { get; set; }
        public string Tooltip { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}