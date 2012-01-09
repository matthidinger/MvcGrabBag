namespace MvcGoodies.Web.Helpers
{

    //public class RouteOptionsUI
    //{
    //    public RadioButtonListViewModel<MileageMode> MileageModes { get; set; }
    //    public RadioButtonListViewModel<RouteOptimizationMode> OptimizationModes { get; set; }
    //    public RadioButtonListViewModel<BorderMode> BorderModes { get; set; }
    //    public RadioButtonListViewModel<bool> TollsDiscouraged { get; set; }
    //    public RadioButtonListViewModel<bool> OverrideRestriction { get; set; }

    //    public RouteOptionsUI()
    //    {
    //        OptimizationModes = RadioButtonListViewModel<RouteOptimizationMode>.FromEnum("OptimizationModes", RouteOptimizationMode.NoOptimization);
    //        BorderModes = RadioButtonListViewModel<BorderMode>.FromEnum("BorderModes", BorderMode.Closed);
    //        MileageModes = RadioButtonListViewModel<MileageMode>.FromEnum("MileageModes", MileageMode.Practical);
    //        TollsDiscouraged = RadioButtonListViewModel<bool>.FromBool("TollsDiscouraged", false, "Toll Discouraged routing will avoid long stretches of toll roads but will not take long, impractical detours to avoid toll bridges and tunnels");
    //        OverrideRestriction = RadioButtonListViewModel<bool>.FromBool("OverrideRestriction", false, "Allow Restricted Roads should be selected when truck weighs less than 80,000 pounds. This will result in truck restricted roads being considered for a route. Truck-prohibited roads will still always be avoided.");
    //    }


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