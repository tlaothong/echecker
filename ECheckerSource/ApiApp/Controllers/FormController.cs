using ApiApp.Models;
using ApiApp.Repositories;
using ApiApp.Repositories.Imprementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class FormController : ApiController
    {
        private IFormRepository repoRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repoRepository"></param>
        public FormController(IFormRepository repoRepository)
        {
            this.repoRepository = repoRepository;
        }

        /// <summary>
        /// List all values.
        /// </summary>
        /// <returns></returns>
        // GET api/values
        ///GET /forms/{form-id}
        public IEnumerable<Topic> Get(int id)
        {
            return repoRepository.GetForm(id);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Post()
        {
            //repoRepository.CreateForm(GetForm());
        }

        IEnumerable<Topic> GetForm()
        {
            var now = DateTime.Now;
            return new List<Topic>
            {
                //รถยนต์  VehicleTypeId = 11 ,formId = 11
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสอบระดับน้ำมันเบรกและน้ำมันคลัตช์", DamagePercent = 5, IsCritical = true ,  How2Url="th1101.html",SuggestDetail ="น้ำมันยุบผิดปกติให้ตรวจหารอยรั่วเติมน้ำมันเบรก ให้นำรถเข้าตรวจเช็ค อู่ซ่อม/ศูนย์บริการ", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสอบระดับน้ำมันคลัทช์", DamagePercent = 5, IsCritical = true ,  How2Url="th1102.html", SuggestDetail ="น้ำมันยุบผิดปกติให้ตรวจหารอยรั่วเติมน้ำมันเบรก ให้นำรถเข้าตรวจเช็ค อู่ซ่อม/ศูนย์บริการ", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสอบเบรก", DamagePercent = 10, IsCritical = true ,  How2Url="th1103.html", SuggestDetail ="ถ้าพบว่าทำงานผิดปกติ ให้นำรถเข้าตรวจเช็ค อู่ซ่อม/ศูนย์บริการ", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสอบเบรกมือ", DamagePercent = 5, IsCritical = false ,  How2Url="th1104.html", SuggestDetail ="หย่อนให้ปรับตั้งใหม่/ล็อคไม่อยู่ ให้นำรถเข้าตรวจเช็ค อู่ซ่อม/ศูนย์บริการ", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสอบระบบไฟชาร์จ", DamagePercent = 3, IsCritical = false , How2Url="th1105.html", SuggestDetail ="ไฟไม่ชาร์จให้นำรถเข้าตรวจเช็ค อู่ซ่อม/ศูนย์บริการ", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสภาพการทำงานของไฟส่องสว่างและไฟสัญญาณ", DamagePercent = 10, IsCritical = true , How2Url="th1106.html", SuggestDetail ="สัญญาณไฟติดไม่ครบให้นำรถเข้าตรวจเช็ค อู่ซ่อม/ศูนย์บริการ", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสอบระดับน้ำกลั่นในแบตเตอรี่ ขั้วแบตเตอรี่", DamagePercent = 2, IsCritical = false , How2Url="th1107.html", SuggestDetail ="เติมน้ำกลั่น", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจระดับน้ำในหม้อน้ำ หม้อพักน้ำพร้อมฝาปิดหม้อน้ำ", DamagePercent = 5, IsCritical = false , How2Url="th1108.html", SuggestDetail ="มีคราบน้ำไต้ฝาปิดหม้อน้ำให้เปลี่ยนฝาไหม่", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสอบท่อยางหม้อน้ำและรอยรั่ว", DamagePercent = 2, IsCritical = false , How2Url="th1109.html", SuggestDetail ="แข็งมีรอยร้าวรั่วซึมให้นำรถเข้าตรวจเช็ค อู่ซ่อม/ศูนย์บริการ เปลี่ยนท่อยางใหม่", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจระดับน้ำล้างกระจก", DamagePercent = 1, IsCritical = false , How2Url="th1110.html", SuggestDetail ="เติมน้ำสะอาดพร้อมน้ำยาล้างกระจก", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสอบอุปกรณ์ปัดน้ำฝน", DamagePercent = 2, IsCritical = false , How2Url="th1111.html", SuggestDetail ="ปาดไม่เรียบ/ใบปาดกระโดด/ให้เปลี่ยนยางปาดใหม่", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสภาพยาง", DamagePercent = 10, IsCritical = true , How2Url="th1112.html", SuggestDetail ="มีรอยแตกลายงา/ฉีกให้เปลี่ยนใหม่", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสอบไส้กรองอากาศ", DamagePercent = 2, IsCritical = false , How2Url="th1113.html", SuggestDetail ="เป่าทำความสะอาด/เปลี่ยนใหม่", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสภาพการรั่วซึมของน้ำมันในห้องเครื่องยนต์", DamagePercent = 3, IsCritical = false , How2Url="th1114.html", SuggestDetail ="ทำความสะอาดเช็ครอยรั่ว ให้นำรถเข้าตรวจเช็ค อู่ซ่อม/ศูนย์บริการ", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจระดับน้ำมันเครื่องและความสกปรกของน้ำมันเครื่อง", DamagePercent = 10, IsCritical = true , How2Url="th1115.html", SuggestDetail ="ตรวจดูรอยรั่ว/เติมเพิ่มหรือเปลี่ยนถ่าย", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสภาพสายพาน ความตึงของสายพานพร้อมปรับตั้ง", DamagePercent = 5, IsCritical = false , How2Url="th1116.html", SuggestDetail ="มีรอยแตกฉีกเปลี่ยนใหม่ หย่อนปรับตั้งใหม่", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสภาพการทำงานของเครื่องยนต์", DamagePercent = 3, IsCritical = false , How2Url="th1117.html", SuggestDetail ="เครื่องสั่น/เสียงดังผิดปกติ ให้นำรถเข้า อู่ซ่อม/ศูนย์บริการ", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจเข็มขัดนิรภัย", DamagePercent = 5, IsCritical = false , How2Url="th1118.html", SuggestDetail ="เข็มขัดไม่ล็อค/ล็อคไม่อยู่ เปลี่ยนใหม่", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสอบระบบส่งกำลัง ระบบบังคับเลี้ยว และระบบรองรับน้ำหนัก", DamagePercent = 10, IsCritical = true , How2Url="th1119.html", SuggestDetail ="ลูกหมากหลวม/โช้คแตก/เสียงดังผิดปกติ ให้นำรถเข้าตรวจเช็ค อู่ซ่อม/ศูนย์บริการ", CreateDate = now,FormId =11},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 11 , Detail = "ตรวจสอบระดับก๊าซไอเสีย", DamagePercent = 5, IsCritical = false , How2Url="th1120.html", SuggestDetail ="ควันดำ/ขาว ให้นำรถเข้าตรวจเช็ค อู่ซ่อม/ศูนย์บริการ", CreateDate = now,FormId =11},
                //รถมอเตอร์ไซค์  VehicleTypeId = 13 ,formId = 13
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสอบคันเร่ง", DamagePercent = 2, IsCritical = false ,  How2Url="th1301.html",SuggestDetail ="ฝืดให้ทำการหล่อลื่นสาย/หย่อนให้ปรับตั้งสายใหม่", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสอบเบรก", DamagePercent = 10, IsCritical = true ,  How2Url="th1302.html",SuggestDetail ="ผ้าเบรกหมดให้เปลี่ยนใหม่/เบรกหย่อนปรับตั้งใหม่", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสอบคลัทช์", DamagePercent = 5, IsCritical = true ,  How2Url="th1303.html",SuggestDetail ="คลัทซ์ลื่นรถวิ่งไม่ออก ให้นำรถเข้าตรวจเช็ค อู่ซ่อม/ศูนย์บริการ เปลี่ยนคลัทซ์ใหม่", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสภาพการทำงานของไฟส่องสว่างและไฟสัญญาณ", DamagePercent = 5, IsCritical = true ,  How2Url="th1304.html",SuggestDetail ="ตรวจเช็คใส่หลอด/เปลี่ยนหลอด", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสอบระบบหล่อเย็นเครื่องยนต์", DamagePercent = 3, IsCritical = false ,  How2Url="th1305.html",SuggestDetail ="ตรวจดูระดับน้ำขาดให้เติม/ตรวจหารอยรั่ว", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสอบระดับน้ำกลั่นในแบตเตอรี่ ขั้วแบตเตอรี่", DamagePercent = 3, IsCritical = false ,  How2Url="th1306.html",SuggestDetail ="น้ำกลั่นแห้งให้เติม/ขั้วสกปรกให้ล้างทำความสะอาด", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสภาพยาง", DamagePercent = 10, IsCritical = true ,  How2Url="th1307.html",SuggestDetail ="ดอกหมด/แตกลายงา/ให้เปลี่ยนใหม่", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสอบไส้กรองอากาศ", DamagePercent = 2, IsCritical = false ,  How2Url="th1308.html",SuggestDetail ="ทำความสะอาดหรือเปลี่ยนใหม่", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสอบการรั่วซึมของน้ำมันจากถังน้ำมัน ท่อทางน้ำมัน", DamagePercent = 10, IsCritical = true ,  How2Url="th1309.html",SuggestDetail ="ตรวจสอบรอยรั่วกลิ่นทำการแก้ใข/ ให้นำรถเข้าตรวจเช็ค อู่ซ่อม/ศูนย์บริการ", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจระดับน้ำมันเครื่องและความสกปรกของน้ำมันเครื่อง", DamagePercent = 10, IsCritical = true ,  How2Url="th1310.html",SuggestDetail ="ตรวจดูรอยรั่ว/เติมเพิ่มหรือเปลี่ยนถ่าย", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสภาพการรั่วซึมของน้ำมันเครื่องยนต์", DamagePercent = 2, IsCritical = false ,  How2Url="th1311.html",SuggestDetail ="ทำความสะอาดดูรอยรั่วซึมแก้ไขจุดที่รั่ว", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสอบระบบรองรับน้ำหนัก", DamagePercent = 5, IsCritical = false ,  How2Url="th1312.html",SuggestDetail ="โช้ครั่วแตกให้เปลี่ยนใหม่", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสภาพการทำงานของเครื่องยนต์", DamagePercent = 5, IsCritical = true ,  How2Url="th1313.html",SuggestDetail ="เสียงดังผิดปกติมีระเบิดออกปลายท่อควันดำ/ ให้นำรถเข้าตรวจเช็ค อู่ซ่อม/ศูนย์บริการ", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสอบโซ่ และเฟืองโซ่", DamagePercent = 5, IsCritical = false ,  How2Url="th1314.html",SuggestDetail ="โซ่หย่อนให้ปรับตั้ง/ฟันเฟืองสึกให้เปลี่ยนใหม่", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสอบระบบบังคับเลี้ยว", DamagePercent = 5, IsCritical = true ,  How2Url="th1315.html",SuggestDetail ="หลวมคลอนติดขัด/ให้นำรถเข้าตรวจเช็ค อู่ซ่อม/ศูนย์บริการ", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสอบที่พักเท้า-ขาตั้ง", DamagePercent = 3, IsCritical = false ,  How2Url="th1316.html",SuggestDetail ="หลวมยางขาดให้เปลี่ยนใหม่", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสอบแผ่นบังโซ่", DamagePercent = 2, IsCritical = false ,  How2Url="th1317.html",SuggestDetail ="เบียดสีกับโซ่ให้ปรับตั้งใหม่", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสอบกงล้อ", DamagePercent = 5, IsCritical = true ,  How2Url="th1318.html",SuggestDetail ="ลวดขาด/กงล้อเบี้ยว ให้เปลี่ยนใหม่", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสอบกระจกมองหลัง", DamagePercent = 3, IsCritical = false ,  How2Url="th1319.html",SuggestDetail ="หลวมไม่แน่น/ให้ขันปรับตั้งหรือเปลี่ยนใหม่", CreateDate = now,FormId =13},
                new Topic { id  = Guid.NewGuid().ToString(), VehicleTypeId = 13 , Detail = "ตรวจสอบระดับก๊าซไอเสีย", DamagePercent = 5, IsCritical = false ,  How2Url="th1320.html",SuggestDetail ="ควันดำ/ขาว ให้นำรถเข้าตรวจเช็ค อู่ซ่อม/ศูนย์บริการ", CreateDate = now,FormId =13},
            };
        }
    }
}
