using System.IO;
using System;
using System.Xml.Linq;
using XmlToSqlScript.Tables;
using System.Collections.Generic;
using CsvToSqlScript;

var path = Path.Combine(Environment.CurrentDirectory);
var filename = Path.Combine(path, "XML", "empData.xml");


var xmlString = File.ReadAllText(filename);
var xElement = XElement.Parse(xmlString);
var empData = xElement.Elements("empDataList");

List<EmpData> models = new List<EmpData>();

foreach (var emp in empData)
{
    var model = new EmpData();
    foreach (XElement xElem in emp.Elements())
    {
        switch (xElem?.Name?.LocalName)
        {
            case nameof(EmpData.payRollNo):
                model.payRollNo = Convert.ToString(xElem?.Value);
                break;
            case nameof(EmpData.firstName):
                model.firstName = Convert.ToString(xElem?.Value);
                break;
            case nameof(EmpData.lastName):
                model.lastName = Convert.ToString(xElem?.Value);
                break;
            case nameof(EmpData.middleName):
                model.middleName = Convert.ToString(xElem?.Value);
                break;
            case nameof(EmpData.costCode):
                model.costCode = Convert.ToString(xElem?.Value);
                break;
            case nameof(EmpData.officeTelephoneExt):
                model.officeTelephoneExt = Convert.ToString(xElem?.Value);
                break;
            case nameof(EmpData.mailStop):
                model.mailStop = Convert.ToString(xElem?.Value);
                break;
            case nameof(EmpData.supervisor):
                model.supervisor = Convert.ToString(xElem?.Value);
                break;
            case nameof(EmpData.managingDirector):
                model.managingDirector = Convert.ToString(xElem?.Value);
                break;
            case nameof(EmpData.federalServiceYears):
                int federalServiceYearsValue;
                bool isfederalServiceYears = int.TryParse(xElem?.Value.ToString(), out federalServiceYearsValue);
                if (isfederalServiceYears)
                    model.federalServiceYears = federalServiceYearsValue;
                break;
            case nameof(EmpData.federalServiceMonths):
                int federalServiceMonthsValue;
                bool isfederalServiceMonths = int.TryParse(xElem?.Value.ToString(), out federalServiceMonthsValue);
                if (isfederalServiceMonths)
                    model.federalServiceMonths = federalServiceMonthsValue;
                break;
            case nameof(EmpData.GPOServiceYears):
                int GPOServiceYearsValue;
                bool isGPOServiceYears = int.TryParse(xElem?.Value.ToString(), out GPOServiceYearsValue);
                if (isGPOServiceYears)
                    model.GPOServiceYears = GPOServiceYearsValue;
                break;
            case nameof(EmpData.GPOServiceMonths):
                int GPOServiceMonthsValue;
                bool isGPOServiceMonths = int.TryParse(xElem?.Value.ToString(), out GPOServiceMonthsValue);
                if (isGPOServiceMonths)
                    model.GPOServiceMonths = GPOServiceMonthsValue;
                break;
        }
    }
    models.Add(model);
}


var modelName = "UserDetails";
var sqlFileName = Path.Combine(path, $"{modelName}.sql"); 
if (models != null)
{
    using (var outputFile = new StreamWriter(sqlFileName))
    {
        outputFile.WriteLine($"SET IDENTITY_INSERT [dbo].[{modelName}] ON");
        outputFile.WriteLine($"Print ' Adding Records for Parking {modelName}- 1 '");
        outputFile.WriteLine($"BEGIN TRANSACTION");
        foreach (var model in models)
        {
            string script = ScriptCreator.GetScript(model, modelName);
            outputFile.Write(script);
        }
        outputFile.WriteLine($"COMMIT TRANSACTION");
        outputFile.WriteLine($"GO");
        outputFile.WriteLine($"Print'End Adding Records for dbo {modelName}>> -----'");
        outputFile.WriteLine($"SET IDENTITY_INSERT [dbo].[{modelName}] OFF");
    }
}    
   
Console.WriteLine(models); 