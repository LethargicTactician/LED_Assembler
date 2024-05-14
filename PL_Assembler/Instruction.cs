using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PL_Assembler
{
  public abstract class Instruction
  {

    //public abstract Instruction Parse(string line);
    #region stuff
    public string originalInt {  get; set; }
    public string Cond { get; set; } = "1110";
    public string Rn { set; get; }
    public string Rd { get; set; }
    public string B {  set; get; }
    public string BCond { set; get; }
    public string BinaryOutput {  get; set; }
    public string label { set; get; }
    public string Title { set; get; } = string.Empty;
    public Dictionary<string, int> labels = new Dictionary<string, int>();

    #endregion stuff



    public abstract override string ToString();
    public abstract void ParseInstructions(string[] instructions);
    public abstract void ProduceInstruction();
    //public abstract void SetUpImmediateOp(string thing);

    public Instruction(string[] instructions){
      originalInt = String.Join(" ", instructions);

    }

    public void SetLabel(string[] instruction)
    {

      foreach (string part in instruction)
      {
                Console.WriteLine($"kms: {part}");
                if (part.Contains(":"))
        {
          int index = part.IndexOf(':');
          label = part.Substring(index + 1).Trim();
        } else
        if (part.Contains("$"))
        {
          int index = part.IndexOf('$');
          label = part.Substring(index + 1).Trim();
        }
      }
    }

    

    //make instruction methods??
    public void GetConditionalExecutionBinary(string condition)
    {
      switch (condition)
      {
        case "EQ":
          Cond = "0000";
            break;
        case "NE": 
          Cond = "0001";
          break;
        case "CS": 
          Cond = "0010";
          break;
        case "CC": 
          Cond = "0011";
          break;
        case "MI": 
          Cond = "0100";
          break;
        case "PL": 
          Cond = "0101";
          break;
        case "VS": 
          Cond = "0110";
          break;
        case "VC": 
          Cond = "0111"; 
          break;
        case "HI": 
          Cond = "1000"; 
          break;
        case "LS": 
          Cond = "1001"; 
          break; 
        case "GE": 
          Cond = "1010"; 
          break;
        case "LT": 
          Cond = "1011"; 
          break;
        case "GT": 
          Cond = "1100"; 
          break;
        case "LE": 
          Cond = "1101"; 
          break;
        case "AL": 
          Cond = "1110"; 
          break;
        default: 
          Cond = "1110"; 
          break; 
          
      }
    }


    public void SetUpRn(string rn)
    {
      rn = rn.Replace("R", "");
      Rn = Convert.ToString(int.Parse(rn), 2).PadLeft(4, '0');
    
    }
    public void SetUpRd(string rd)
    {
      rd = rd.Replace("R", "");
      Rd = Convert.ToString(int.Parse(rd), 2).PadLeft(4, '0');
    
    }

    public int ConvertStringToInt(string str)
    {
      if (str.Contains("x"))
      {
        return Convert.ToInt32(str,16);
      }
      else
      {
        return Convert.ToInt32(str);
      }

    }

    public byte[] ConvertBinaryInsToByteArray()
    {
      List<byte> byteUrMom = new List<byte>();

      for(int i = 0; i <= 24; i+= 8)
      {
        byteUrMom.Add(Convert.ToByte(BinaryOutput.Substring(i, 8), 2));

      }
      byteUrMom.Reverse();

      /* https://stackoverflow.com/questions/20750062/what-is-the-meaning-of-tostringx2 */

      string hex = string.Join(" ", byteUrMom.Select(b => b.ToString("X2")));
            Console.WriteLine(hex);

            return byteUrMom.ToArray();

    }

  }
}