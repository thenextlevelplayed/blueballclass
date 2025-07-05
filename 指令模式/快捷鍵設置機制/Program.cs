// See https://aka.ms/new-console-template for more information

using 快捷鍵設置機制.Command_Pattern;

var keyboard = new Keyboard();
var tank = new Tank();
var telecom = new Telecom();
var controller = new Controller(keyboard, tank, telecom);

while (true)
{
    Console.Write("(1) 快捷鍵設置 (2) Undo (3) Redo (字母) 按下按鍵: ");
    var input = Console.ReadLine();
    if (input == "1")
    {
        Console.Write("設置巨集指令 (y/n)：");
        var setMacro = Console.ReadLine();
        if (setMacro == "n")
        {
            Console.Write("Key: ");
            var key = Console.ReadLine();
            Console.Write($"要將哪一道指令設置到快捷鍵 {key} 上: ");
            Console.WriteLine(
                $"\n(0) MoveTankForward\n(1) MoveTankBackward\n(2) ConnectTelecom\n(3) DisconnectTelecom\n(4) ResetMainControlKeyboard");
            var value = Console.ReadLine();
            var newCommand = CreateCommand(int.Parse(String.IsNullOrEmpty(value) ? "" : value), tank, telecom,
                keyboard);
            controller.SetCommand(key, newCommand);
        }

        if (setMacro == "y")
        {
            Console.Write("Key: ");
            var key = Console.ReadLine();
            Console.WriteLine(
                $"\n要將哪些指令設置成快捷鍵 a 的巨集（輸入多個數字，以空白隔開）: \n(0) MoveTankForward\n(1) MoveTankBackward\n(2) ConnectTelecom\n(3) DisconnectTelecom\n(4) ResetMainControlKeyboard");
            var value = Console.ReadLine();
            var commandList = new List<ICommand>();
            var executeList = value
                .Split(' ')
                .ToList();
            foreach (var item in executeList)
            {
                var newCommand = CreateCommand(int.Parse(String.IsNullOrEmpty(item) ? "" : item), tank, telecom,
                    keyboard);
                commandList.Add(newCommand);
            }

            controller.SetCommand(key, new MacroCommand(commandList));
        }
    }
    else if (input == "2")
    {
        controller.Undo();
    }
    else if (input == "3")
    {
        controller.Redo();
    }
    else if (input.Length == 1 && char.IsLetter(input[0]))
    {
        controller.PressButton(input);
    }

    foreach (var kvp in keyboard.Keys)
    {
        if (kvp.Value!=null)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }
    }
}

ICommand CreateCommand(int index, Tank tank, Telecom telecom, Keyboard keyboard)
{
    switch (index)
    {
        case 0: return new MoveForwardTank(tank);
        case 1: return new MoveBackwardTank(tank);
        case 2: return new ConnectTelecom(telecom);
        case 3: return new DisconnectTelecom(telecom);
        case 4: return new ResetKeyboardCommand(keyboard); // 使用新的 Reset 命令
        default: return null; // 無效的索引
    }
}