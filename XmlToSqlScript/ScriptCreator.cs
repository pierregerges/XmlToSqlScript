using System.Text;
using XmlToSqlScript.Tables;

namespace CsvToSqlScript
{
    internal class ScriptCreator
    {
        public static string GetScript(EmpData record, string modelName)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"IF NOT EXISTS (SELECT * FROM  [dbo].[{modelName}] Where [PayrollId] = {record.payRollNo})");
            builder.AppendLine($"BEGIN");

            builder.AppendLine($"INSERT INTO [dbo].[{modelName}]");
            builder.AppendLine($"(");
            //builder.AppendLine($"[UserId],");
            builder.AppendLine($"[PayrollId],");
            builder.AppendLine($"[MailStop],");
            builder.AppendLine($"[Shift],");
            builder.AppendLine($"[FederalServiceYears],");
            builder.AppendLine($"[FederalServiceMonths],");
            builder.AppendLine($"[GPOServiceYears],");
            builder.AppendLine($"[GPOServiceMonths],");
            builder.AppendLine($"[SupervisorId],");
            builder.AppendLine($"[BusinessUnitId]");
            builder.AppendLine($")");

            builder.AppendLine($"VALUES");
            builder.AppendLine($"(");
            //builder.AppendLine($"{UserId},");
            builder.AppendLine($"'{record.payRollNo}',");
            builder.AppendLine($"'{record.mailStop}',");
            builder.AppendLine($"Null,");
            builder.AppendLine($"{record.federalServiceYears},");
            builder.AppendLine($"{record.federalServiceMonths},");
            builder.AppendLine($"{record.GPOServiceYears},");
            builder.AppendLine($"{record.GPOServiceMonths},");
            builder.AppendLine($"'{record.supervisor}',");
            builder.AppendLine($"Null");
            builder.AppendLine($")");
            builder.AppendLine($"END");

            return builder.ToString();
        }
    }
}
