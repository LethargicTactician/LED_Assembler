namespace PL_Assembler
{
  public class MOV : Instruction
  {
    private string type { get; set; } = "0000";
    private string immm4 { get; set; } 
    private string immm12 { get; set; }   
    
    public MOV(string[] instructions) : base(instructions)
    {
      
      ParseInstructions(instructions);
      ProduceInstruction();
    }

    public override void ParseInstructions(string[] instructions)
    {
      if (instructions[0].Equals("MOVT"))
      {
        type = "0100";

      }
      SetUpRd(instructions[1]);
      SetUpImmediateOp(instructions[2]);
    }

    public override void ProduceInstruction()
    {
      BinaryOutput = $"{Cond}0011{type}{immm4}{Rd}{immm12}";
    }


    public void SetUpImmediateOp(string thing)
    {
      int value = ConvertStringToInt(thing);
      string binaryWhatever = Convert.ToString(value, 2).PadLeft(16, '0');

      immm4 = binaryWhatever.Substring(0, 4);
      immm12 = binaryWhatever.Substring(4);

    }

    public override string ToString()
    {
      return $": {BinaryOutput} ({BinaryOutput.Length})";
    }

  }
}
