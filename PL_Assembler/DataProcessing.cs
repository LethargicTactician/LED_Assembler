using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace PL_Assembler
{

  /*Notes
  *OP is the operation to be performed (e.g., ADD, SUB, AND, ORR, etc.).
  *{S} is an optional flag indicating whether to update the condition flags based on the result of the operation.
  *{cond} is an optional condition code specifying when the instruction should be executed.
  *Rd is the destination register where the result will be stored.
  *Rn is the first operand register.
  *Operand2 is the second operand, which can be an immediate value, register value, or shifted register value.
   */

  public class DataProcessing : Instruction
  {
    //would each OpCode be it's own class? (SUB, ADD, etc.)

    public string S { set; get; }
    public string OpCode { set; get; }
    public string Operand2 { get; set; }

    public DataProcessing(string[] instructions) : base(instructions)
    {



    }

    public override void ParseInstructionsForPookie(string[] instructions)
    {
      throw new NotImplementedException();
    }

    public override void ProduceInstruction()
    {
      throw new NotImplementedException();
    }

    public override string ToString()
    {
      throw new NotImplementedException();
    }


  }
}
