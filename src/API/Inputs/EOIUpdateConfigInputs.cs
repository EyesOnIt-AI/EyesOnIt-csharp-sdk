namespace EyesOnItSDK.API.Inputs
{
    public class EOIUpdateConfigInputs
    {
        public object Body { get; set; }

        public EOIUpdateConfigInputs(object body)
        {
            Body = body;
        }
    }
}
