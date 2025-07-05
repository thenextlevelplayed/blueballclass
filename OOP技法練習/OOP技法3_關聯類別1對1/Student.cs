using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP技法練習.OOP技法3_關聯類別1對1
{
    public class Student
    {
        private readonly string name;
        private LectureAttendance? lectureAttendance; //代表學生修了這門課


        public Student(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return name;
        }

        public LectureAttendance? getLectureAttendance()
        {
            return lectureAttendance;           
        }

        public void setLectureAttendance(LectureAttendance? lectureAttendance)
        {
            this.lectureAttendance = lectureAttendance;           
        }
    }
}
