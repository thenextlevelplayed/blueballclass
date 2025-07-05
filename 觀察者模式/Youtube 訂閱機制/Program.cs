// See https://aka.ms/new-console-template for more information

using Youtube_訂閱機制.Observer;

Console.WriteLine("Hello, World!");

// var waterBall = new ChannelSubscriber("水球");
// var fireBall = new ChannelSubscriber("火球");
// var pewDiePie = new Channel("PewDiePie ");
// var waterBallSoftwareBootCamp = new Channel("水球軟體學院 ");
// waterBall.Register(new GiveLikeAction());
// fireBall.Register(new UnsubscribeAction());
// waterBall.Subscribe(pewDiePie);
// waterBall.Subscribe(waterBallSoftwareBootCamp);
// fireBall.Subscribe(pewDiePie);
// fireBall.Subscribe(waterBallSoftwareBootCamp);


var waterBall = new WaterBallSubscriber("水球");
var fireBall = new FireBallSubscriber("火球");
var pewDiePie = new Channel("PewDiePie ");
var waterBallSoftwareBootCamp = new Channel("水球軟體學院 ");
waterBall.Subscribe(pewDiePie);
waterBall.Subscribe(waterBallSoftwareBootCamp);
fireBall.Subscribe(pewDiePie);
fireBall.Subscribe(waterBallSoftwareBootCamp);
waterBallSoftwareBootCamp.Register(waterBall);
waterBallSoftwareBootCamp.Register(fireBall);
pewDiePie.Register(waterBall);
pewDiePie.Register(fireBall);
var c1M1S2 = new Video("C1M1S2", "這個世界正是物件導向的呢！", 240, waterBallSoftwareBootCamp);
waterBallSoftwareBootCamp.Upload(c1M1S2);
var helloGuys = new Video("Hello guys", "Clickbait", 30, pewDiePie);
pewDiePie.Upload(helloGuys);
var minecraft = new Video("Minecraft", "Let’s play Minecraft", 1800, pewDiePie);
pewDiePie.Upload(minecraft);