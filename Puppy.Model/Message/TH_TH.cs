using System;
using System.Collections.Generic;

namespace Puppy.Model.Message
{
    public class TH_TH : IMessage
    {
        private readonly Dictionary<int, string> list = 
         new Dictionary<int, string>()
            {
                {210,"เพิ่มข้อมูล {0} สำเร็จ"},
                {220,"ค้นหาข้อมูลสำเร็จ"},
                {410, "เพิ่มข้อมูล {0} ไม่สำเร็จ!"},
                {420, "ค้นหาข้อมูลพบ!"},
                {500, "มีความผิดพลาดเกิดขึ้น!"},
            }; 

        public Dictionary<int, string> MessageList
        {   get { return list; } }

        public string GetMessage(int key)
        {
            return MessageList[key];
        }
              
    }
}