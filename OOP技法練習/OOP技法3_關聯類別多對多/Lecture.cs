using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP技法練習.OOP技法3_關聯類別多對多
{
    public class Lecture
    {
        private readonly string name;
        private List<LectureAttendance>? lectureAttendances; //代表有哪些學生參與這門課   

        public Lecture(string name)
        {
            this.name = name;
            this.lectureAttendances = new List<LectureAttendance>();
        }



        public string getName()
        {
            return name;
        }

        public void SignUp(Student student)
        {
            try
            {
                if (lectureAttendances.Select(l => l.getStudent() == student).FirstOrDefault())
                {
                    throw new ArgumentException("不能重複報名課程");
                }

                LectureAttendance lectureAttendance = new LectureAttendance(student, this);//雙向關聯
                lectureAttendances.Add(lectureAttendance);
                student.addLectureAttendance(lectureAttendance);//雙向關聯

                Console.WriteLine($"{student.getName()} 成功報名 {name}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"報名失敗: {ex.Message}");
            }
        }

        public void SignOff(Student student)
        {
            try
            {
                LectureAttendance attendance = lectureAttendances.FindAll(l => l.getStudent() == student).FirstOrDefault();
                if (attendance == null)
                {
                    throw new ArgumentException("此學生沒有報名這課程");
                }
                lectureAttendances.Remove(attendance);
                student.removeLectureAttendance(attendance);//雙向關聯

                Console.WriteLine($"{student.getName()} 成功註銷 {name}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"註銷失敗: {ex.Message}");
            }
        }

        public List<LectureAttendance>? getLectureAttendances()
        {
            return lectureAttendances;
        }
    }
}
