using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PL_Assembler
{
  /* Notes
  * LDR loads a value from memory into a register. = can be null
  * STR stores a value from a register into memory. = can be null
  * R indicates that the instruction affects the condition flags.
  * <cond> is an optional condition code.
  * <Rd> is the destination register.
  * <Rn> is the base register containing the memory address.
  * <offset> is an optional immediate value or register offset.
   * */

  public class SingleDataTransfer : Instruction
  {
    public SingleDataTransfer(string[] instructions) : base(instructions)
    {
      ParseInstructions(instructions);
      ProduceInstruction();
    }
    #region stuff
    public string _type { get; set; } = "0";
    public string L { get; set; } = "0";
    public string I { get; set; } = "0";
    public string P { get; set; } = "0";
    public string U { get; set; } = "0";
    public string B { get; set; } = "0";
    public string W { get; set; } = "0";
    public string Offset { get; set; } = "0";
    #endregion stuff

    public override void ParseInstructions(string[] instructions)
    {
      //assuming ldr is represented as 01 and str is 00?
      if (instructions[0].Equals("LDR")){
        L = "1";
      }

      SetUpRd(instructions[1]);
      SetUpRn(instructions[2]);
      // Offset = "000000000000";
      Offset = instructions[3] != null ? "000000000000" : Convert.ToString(Convert.ToInt32(instructions[3].TrimStart('*').Replace("0x", ""), 16));
    }

    public override void ProduceInstruction()
    {
      BinaryOutput = $"{Cond}01{I}{P}{U}{B}{W}{L}{Rn}{Rd}{Offset}";

      if (L == "1")
      {
        if (Offset.Contains("-")) 
        {
          P = "1";
          U = "0";
          W = "1";
        }
        else
        {
          //pre-increment load
          P = "1";
          U = "1";
          W = "1";
        }
      }
      //str
      else 
      {
        //if offset starts with other andom character to represent str then

        //P = "1";
        //U = "0";
        //W = "1";

        // else
        //P = "1";
        //U = "1";
        //W = "1";
      } 

    } 

    public override string ToString()
    {
      return $"{BinaryOutput} ({BinaryOutput.Length}) <-SDP Res";
    }
  }
}
