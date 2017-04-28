using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Application.Employee.Dto
{
    public class UpdateEmployeeInput : AddEmployeeInput
    {

        /// <summary>
        /// 员工Id
        /// </summary>
        public long Id { get; set; }

    }
}
