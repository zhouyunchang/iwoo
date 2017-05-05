using Erp.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Migrations.SeedData
{
    public class DefaultProcessCreator
    {

        private readonly ErpDbContext _context;

        public DefaultProcessCreator(ErpDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateProcess1();
            CreateProcess2();
            CreateProcess3();
        }

        private void CreateProcess1()
        {
            var category = _context.ProcessCategory.FirstOrDefault(e => e.Name == "焊接班");
            if (category == null)
            {
                category = new Models.ProcessCategory
                {
                    Name = "焊接班",
                    OrderNum = 0
                };
                _context.ProcessCategory.Add(category);
                _context.SaveChanges();

                _context.Processes.Add(new Models.Process
                {
                    Name = "内纵缝",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "外纵缝",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "割引弧板",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "对装",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "下内环",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "上内环",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "盖面",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "底座连接修磨接头及吸尘",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "阀座打底",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "焊阀座",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "焊吊耳和打钢印",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.SaveChanges();
            }
        }

        private void CreateProcess2()
        {
            var category = _context.ProcessCategory.FirstOrDefault(e => e.Name == "试水班");
            if (category == null)
            {
                category = new Models.ProcessCategory
                {
                    Name = "试水班",
                    OrderNum = 0
                };
                _context.ProcessCategory.Add(category);
                _context.SaveChanges();

                _context.Processes.Add(new Models.Process
                {
                    Name = "热处理",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "称重及记录",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "打压",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "吸水",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "烘干",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "气密",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.SaveChanges();
            }
        }

        private void CreateProcess3()
        {
            var category = _context.ProcessCategory.FirstOrDefault(e => e.Name == "成品班");
            if (category == null)
            {
                category = new Models.ProcessCategory
                {
                    Name = "成品班",
                    OrderNum = 0
                };
                _context.ProcessCategory.Add(category);
                _context.SaveChanges();

                _context.Processes.Add(new Models.Process
                {
                    Name = "打字",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "抛丸",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "修磨",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "吸尘",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "喷漆",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "上下瓶",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "磨端面",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "上托打包",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "缠珍珠棉",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.Processes.Add(new Models.Process
                {
                    Name = "装车",
                    Category = category,
                    OrderNum = 0,
                    GuidePrice = 1
                });
                _context.SaveChanges();
            }
        }

    }
}
