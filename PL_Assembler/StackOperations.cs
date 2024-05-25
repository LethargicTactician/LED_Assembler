using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_Assembler
{
  public class StackOperations : Instruction
  {
    public StackOperations(string[] instructions) : base(instructions)
    {
      ParseInstructions(instructions);
      ProduceInstruction();
    }
    //set default
    private string P { get; set; } = "1";
    private string U { get; set; } = "0";  
    private string W { get; set; } = "1";  
    private string L { get; set; } = "0"; 
    private string Rlist { get; set; }  

    public override void ParseInstructions(string[] instructions)
    {
      Title = "STMEA";
      if (instructions[0].Equals("LDMEA"))
      {
        L = "1";
        Title = "LDMEA";
      }

      SetUpRn(instructions[1]);
      Rlist = GetRegisterList(instructions[2]);
    }

    private string GetRegisterList(string registers)
    {
      registers = registers.Replace("{", "").Replace("}", "");
      var regList = registers.Split('-');
      // Assuming simple case where registers are in continuous range
      return Convert.ToString((1 << (int.Parse(regList[1].Replace("R", "")) + 1)) - 1, 2).PadLeft(16, '0');
    }

    public override void ProduceInstruction()
    {
      BinaryOutput = $"{Cond}100{P}{U}{0}{W}{L}{Rn}1101{Rlist}";
    }

    public override string ToString()
    {
      return $"{BinaryOutput} ({BinaryOutput.Length}) <- Stack Operation Res";
    }
  }
}
