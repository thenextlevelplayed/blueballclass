using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP技法練習.OOP技法3_關聯類別1對1
{
    public class Lecture
    {
        private readonly string name;
        private LectureAttendance? lectureAttendance; //代表哪個學生參與這門課
        //private Student student; //想知道學生的成績，需要持有學生類別

        public Lecture(string name)
        {
            this.name = name;
        }



        public string getName()
        {
            return name;
        }

        public void SignUp(Student student)
        {
            try
            {
                if (student.getLectureAttendance() != null)
                {
                    throw new ArgumentException("不能重複報名課程");
                }

                if (lectureAttendance != null)//這門課有沒有人報名
                {
                    throw new ArgumentException("課程只能有一個學生報名");
                }

                this.lectureAttendance = new LectureAttendance(student, this);//雙向關聯
                student.setLectureAttendance(lectureAttendance);//雙向關聯

                Console.WriteLine($"{student.getName()} 成功報名 {name}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"報名失敗: {ex.Message}");
            }

            //if (student.getLectureAttendance() != null)
            //{
            //    throw new ArgumentException("不能重複報名課程");
            //}

            //if (lectureAttendance != null) //這門課有沒有人報名
            //{
            //    throw new ArgumentException("課程只能有一個學生報名");
            //}
            //this.lectureAttendance = new LectureAttendance(student, this); //雙向關聯
            //student.setLectureAttendance(lectureAttendance);//雙向關聯
        }

        public void SignOff(Student student)
        {
            try
            {
                if (lectureAttendance == null || lectureAttendance.getStudent() != student)
                {
                    throw new ArgumentException("此學生沒有報名這課程");
                }

                lectureAttendance = null;//雙向關聯
                student.setLectureAttendance(null);//雙向關聯
                Console.WriteLine($"{student.getName()} 成功註銷 {name}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"註銷失敗: {ex.Message}");
            }


            //if (lectureAttendance == null || lectureAttendance.getStudent() != student) //這兩個表達都可以 
            //{
            //    throw new ArgumentException("此學生沒有報名這課程");

            //}
            //lectureAttendance = null;//雙向關聯
            //student.setLectureAttendance(null);//雙向關聯

        }

        public LectureAttendance? getLectureAttendance()
        {
            return lectureAttendance;
        }
    }
}
