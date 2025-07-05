//using OOP技法練習.OOP技法1_依賴;
//using OOP技法練習.OOP技法2_雙向關聯1對1;
//using OOP技法練習.OOP技法2_雙向關聯多對多;
//using OOP技法練習.OOP技法3_關聯類別1對1;
//using OOP技法練習.OOP技法3_關聯類別1對多;
using OOP技法練習.OOP技法3_關聯類別多對多;
using OOP技法練習.OOP技法4_抽象類別;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {



        //.OOP技法1_依賴
        //Hero hero = new Hero();
        //LevelSheet levelSheet = new LevelSheet();
        //hero.GainExp(0, levelSheet);
        //hero.GainExp(100, levelSheet);
        //hero.GainExp(900, levelSheet);
        //hero.GainExp(-200, levelSheet);
        //Console.WriteLine("Hello, World!");

        //---------------------------------------
        //OOP技法2_雙向關聯1對1
        //Hero hero = new Hero();       
        //Pet cat = new Pet("Cat");
        //hero.setPet(cat);
        //Console.WriteLine($"hero 目前血量 {hero.getHp()}");

        //Trace.Assert(hero.getPet() != null); //因為hero.getPet() 可能是null
        //Console.WriteLine($"hero 寵物名稱 {hero.getPet().GetName()}");
        //for (int i = 0; i < 5; i++)//寵物吃五次水果
        //{
        //    cat.eat(new Fruit());
        //}

        //hero.removePet();//英雄現在棄養寵物
        //for (int i = 0; i < 5; i++)//寵物吃五次水果
        //{
        //    cat.eat(new Fruit());
        //}

        //---------------------------------------
        //OOP技法2_雙向關聯多對多
        //Hero A = new Hero();
        //Hero B = new Hero();
        //Hero C = new Hero();
        //Guild guild = new Guild("水球軟體學院", new List<Hero> { A });
        //Console.WriteLine($"初始成員數量,{guild.GetMembers().Count()}");

        //guild.join(B);
        //Console.WriteLine($"B 加入, 成員數量: {guild.GetMembers().Count()}");
        //guild.join(C);
        //Console.WriteLine($"C 加入, 成員數量:{guild.GetMembers().Count()}");

        //try
        //{
        //    guild.join(A);
        //}
        //catch (Exception)
        //{
        //    Console.WriteLine($"A 成員不能重複加入");
        //}

        //guild.leave(A);
        //Console.WriteLine($"A 離去, 成員數量: {guild.GetMembers().Count()}");
        //guild.leave(B);
        //Console.WriteLine($"B 離去, 成員數量:{guild.GetMembers().Count()}");

        //try
        //{
        //    guild.leave(C);
        //}
        //catch (Exception)
        //{
        //    Console.WriteLine($"唯一工會成員 (C)不能申請離開");
        //}

        //try
        //{
        //    guild.leave(A);
        //}
        //catch (Exception)
        //{
        //    Console.WriteLine($"只有工會成員能申請離開，A 不是成員，無法離開");
        //}

        //---------------------------------------
        //OOP技法3_1對1
        //Student student = new Student("Patrick");
        //Lecture lecture = new Lecture("軟體設計精通之旅");

        //// 測試報名
        //lecture.SignUp(student); // 應該成功
        //lecture.SignUp(student); // 應該失敗（重複報名）

        //// 給分數
        //LectureAttendance? attendance = lecture.getLectureAttendance();
        //Debug.Assert(attendance != null, "attendance 不可為 null");
        //attendance.receiveGrade(99); // 應該成功       

        //// 測試註銷
        //lecture.SignOff(student); // 應該成功
        //lecture.SignOff(student); // 應該失敗（未報名）

        //---------------------------------------
        ///OOP技法3_1對多
        Student student1 = new Student("Patrick");
        Student student2 = new Student("Penny");
        Lecture lecture1 = new Lecture("軟體設計精通之旅");
        Lecture lecture2 = new Lecture("GenAI 工程師產能爆發之路");

        // 測試報名
        lecture1.SignUp(student1);
        lecture2.SignUp(student1); 
        lecture2.SignUp(student2);

        //// 給分數
        //foreach (var attendance in lecture1.getLectureAttendances())
        //{
        //    attendance.receiveGrade(87);    
        //}
        //foreach (var attendance in lecture2.getLectureAttendances())
        //{
        //    attendance.receiveGrade(100);
        //}

        //---------------------------------------
        Game game = new Game(new Human(1), new AI(2));
        game.start();
    }
    // See https://aka.ms/new-console-template for more information

}