using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP技法練習.OOP技法3_關聯類別1對多
{
    public class Student
    {
        private readonly string name;
        private List<LectureAttendance>? lectureAttendances; //代表學生修了這門課


        public Student(string name)
        {
            this.name = name;
            this.lectureAttendances = new List<LectureAttendance>();
        }

        public string getName()
        {
            return name;
        }

        public ReadOnlyCollection<LectureAttendance>? getLectureAttendance()
        {
            return lectureAttendances != null
            ? new List<LectureAttendance>(lectureAttendances).AsReadOnly()
            : null;
        }

        public void addLectureAttendance(LectureAttendance? lectureAttendance)
        {
            if (lectureAttendances != null && lectureAttendance != null)
            {
                lectureAttendances.Add(lectureAttendance);
            }
        }

        public void removeLectureAttendance(LectureAttendance? lectureAttendance)
        {
            if (lectureAttendances != null && lectureAttendance != null)
            {
                lectureAttendances.Remove(lectureAttendance);
            }
        }
    }
}
