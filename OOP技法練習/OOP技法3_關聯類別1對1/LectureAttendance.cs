using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP技法練習.OOP技法3_關聯類別1對1
{
    public class LectureAttendance
    {

        private Lecture lecture;
        private Student student;
        private int? grade;
        public LectureAttendance(Student student, Lecture lecture)//關係類別，就像一個橋樑，將兩者的類別都傳入
        {
            this.lecture = lecture;
            this.student = student;
        }

        public void receiveGrade(int grade)
        {

            try
            {
                this.grade = ValidationUtils.shouldBeWithinRange("Grade", grade, 0, 100);
                //Console.WriteLine($"在 {lecture.getName()} 課程中，學生 {student.getName()} 取得 {this.grade} 分。");

            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"範圍錯誤: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"參數錯誤: {ex.Message}");
            }
            Console.WriteLine($"在{lecture.getName()}課程中，學生{student.getName()}取得{grade}分。");
        }

        public int? getGrade()
        {
            return grade;
        }

        public Student getStudent()
        {
            return student;
        }
    }
}
