using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        private JsonWriter writer;
        public Calculator()
        {
            /*
            StreamWriter logFile = File.CreateText("calculator.log");
            Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            Trace.AutoFlush = true;
            Trace.WriteLine("Starting Calculator Log");
            Trace.WriteLine(String.Format("Started {0}", System.DateTime.Now.ToString()));
            */
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }
        public decimal DoOperation(decimal num1, decimal num2, char op)
        {
            decimal result = 0;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");
            switch (op)
            {
                case 'a':
                    result = num1 + num2;
                    //Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, result));
                    writer.WriteValue("Subtract");
                    break;
                case 's':
                    result = num1 - num2;
                    //Trace.WriteLine(String.Format("{0} - {1} = {2}", num1, num2, result));
                    writer.WriteValue("Multiply");
                    break;
                case 'm':
                    result = num1 * num2;
                    //Trace.WriteLine(String.Format("{0} * {1} = {2}", num1, num2, result));
                    writer.WriteValue("Add");
                    break;
                case 'd':
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        //Trace.WriteLine(String.Format("{0} / {1} = {2}", num1, num2, result));
                        writer.WriteValue("Divide");
                    }
                    else
                    {
                        string message = "num2 parameter must be a non-zero decimal when performing a division.";
                        //Trace.WriteLine(message);
                        throw new DivideByZeroException(message);                        
                    }
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(result);
            writer.WriteEndObject();
            return result;
        }

        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }
    }
}
