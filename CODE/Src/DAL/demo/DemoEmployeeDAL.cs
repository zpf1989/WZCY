using OA.GeneralClass;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.DAL
{
    public class DemoEmployeeDAL : IDemoEmployeeDAL
    {
        static List<DemoEmployee> DemoEmployees = new List<DemoEmployee>()
            {
                new DemoEmployee{Id="001",Code="zs001",Name="张三001",Gender="1",BirthDay=DateTime.Now,Age=24,DeptId="01",DeptName="人事部",Salary=4500.23M},
                new DemoEmployee{Id="002",Code="ls002",Name="李四002",Gender="0",BirthDay=DateTime.Now,Age=34,DeptId="02",DeptName="研发部",Salary=8500M},
                new DemoEmployee{Id="003",Code="zs003",Name="张三003",Gender="1",BirthDay=DateTime.Now,Age=24,DeptId="01",DeptName="人事部",Salary=4500.23M},
                new DemoEmployee{Id="004",Code="ls004",Name="李四004",Gender="0",BirthDay=DateTime.Now,Age=34,DeptId="02",DeptName="研发部",Salary=8500M},
                new DemoEmployee{Id="005",Code="zs005",Name="张三005",Gender="1",BirthDay=DateTime.Now,Age=24,DeptId="01",DeptName="人事部",Salary=4500.23M},
                new DemoEmployee{Id="006",Code="ls006",Name="李四006",Gender="0",BirthDay=DateTime.Now,Age=34,DeptId="02",DeptName="研发部",Salary=8500M},
                new DemoEmployee{Id="007",Code="zs007",Name="张三007",Gender="1",BirthDay=DateTime.Now,Age=24,DeptId="01",DeptName="人事部",Salary=4500.23M},
                new DemoEmployee{Id="008",Code="ls008",Name="李四008",Gender="0",BirthDay=DateTime.Now,Age=34,DeptId="02",DeptName="研发部",Salary=8500M},
                new DemoEmployee{Id="009",Code="zs009",Name="张三009",Gender="1",BirthDay=DateTime.Now,Age=24,DeptId="01",DeptName="人事部",Salary=4500.23M},
                new DemoEmployee{Id="010",Code="ls010",Name="李四010",Gender="0",BirthDay=DateTime.Now,Age=34,DeptId="02",DeptName="研发部",Salary=8500M},
                new DemoEmployee{Id="011",Code="zs011",Name="张三011",Gender="1",BirthDay=DateTime.Now,Age=24,DeptId="01",DeptName="人事部",Salary=4500.23M},
                new DemoEmployee{Id="012",Code="ls012",Name="李四012",Gender="0",BirthDay=DateTime.Now,Age=34,DeptId="02",DeptName="研发部",Salary=8500M},
            };
        public List<DemoEmployee> GetDemoEmployeesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            //1、调用数据库存储过程，获取DataSet
            //2、DataSet—>List<DemoEmployee>
            //3、返回List<DemoEmployee>
            //测试数据
            if (pageEntity == null)
            {
                return null;
            }
            pageEntity.TotalRecords = DemoEmployees.Count;//更新总记录数
            return DemoEmployees.Skip(pageEntity.QueryRange.Start - 1).Take(pageEntity.PageSize).ToList();
        }


        public bool Add(params DemoEmployee[] emps)
        {
            //1、生成ID
            emps.ToList().ForEach(e => e.Id = Guid.NewGuid().ToString());
            //2、组织sql插入到数据库
            //测试数据
            DemoEmployees.AddRange(emps);
            //3、返回成功或失败的标志
            return true;
            return false;
        }

        public bool Delete(params string[] empIds)
        {
            //1、组织sql
            //2、执行sql
            //3、返回成功或失败的标志

            //测试
            foreach (string id in empIds)
            {
                DemoEmployees.Remove(DemoEmployees.Where(e => e.Id == id).FirstOrDefault());
            }
            return true;

            return false;
        }

        public bool Update(params DemoEmployee[] emps)
        {
            //1、组织sql
            //2、执行sql
            //3、返回成功或失败的标志
            
            //测试

            return false;
        }
    }
}
