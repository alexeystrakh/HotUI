using HotUI.Skia;

namespace HotUI.Samples.Skia
{
    public class SkiaSample7 : View
    {
        readonly State<float> _strokeSize = 2;
        readonly State<string> _strokeColor = "#000000";
        readonly State<string> _canvasColor = "#FF0000";

        [Body]
        View body() => new VStack()
        {
            new VStack()
            {
                new HStack()
                {
                    new Text("Stroke Width:"),
                    new Slider(_strokeSize, 1, 10, 1)
                },
                new HStack()
                {
                    new Text("Stroke Color!:"),
                    new TextField(_strokeColor),
                },
                //new ScrollView{
                    new HStack
                    {
                        new Text("Stroke Color: "),
                        new Button("Black", () =>
                        {
                            _strokeColor.Value = Color.Black.ToHexString();
                        }).Color(Color.Black),
                        new Button("Blue", () =>
                        {
                            _strokeColor.Value = Color.Blue.ToHexString();
                        }).Color(Color.Blue),
                        new Button("Red", () =>
                        {
                            _strokeColor.Value = Color.Red.ToHexString();
                        }).Color(Color.Red),
                    },
                    new HStack
                    {
                        new Text("Canvas Color: "),
                        new Button("White", () =>
                        {
                            _canvasColor.Value = Color.White.ToHexString();
                        }).Color(Color.White),
                        new Button("Magenta", () =>
                        {
                            _canvasColor.Value = Color.Magenta.ToHexString();
                        }).Color(Color.Magenta),
                        new Button("Azure", () =>
                        {
                            _canvasColor.Value = Color.Azure.ToHexString();
                        }).Color(Color.Azure),
                    },
                //},
                new BindableFingerPaint(
                    strokeSize: _strokeSize,
                    strokeColor: _strokeColor,
                    canvasColor: _canvasColor)
                    .ToView()
                    .Frame(height:400)
                    .Padding(100)
                    .Animate(new Animation
                    {
                        Duration = 2000,
                        Delay = 1000,
                        Options = AnimationOptions.CurveEaseOut | AnimationOptions.Repeat,
                        TranslateTo = new PointF(100, 50),
                        //RotateTo = 180,
                        //ScaleTo = new PointF(0.5f, 0.5f),
                    })
            },
        }
        .Background(Color.Purple);
    }
}