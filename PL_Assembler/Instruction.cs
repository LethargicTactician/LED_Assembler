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

    public string originalInt {  get; set; }
    public string Cond { get; set; } = "1110";
    public string Rn { set; get; }
    public string Rd { get; set; }
    public string B {  set; get; }
    public string BCond { set; get; }
    public string BinaryMeBBG {  get; set; }



    public Instruction(string[] instructions){
      originalInt = String.Join(" ", instructions);

    }

    //make instruction methods??

    public void GeOperationCodes(string firstCondition)
    {
      switch(firstCondition)
      {
        case "AND":
          Cond = "0000"; break;
        case "EOR": 
          Cond = "0001"; break;
        case "SUB":
          Cond = "0010";
          break;
        //case "ADD": Cond = 
      }

    }
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

    public abstract override string ToString();
    public abstract void ParseInstructionsForPookie(string[] instructions);
    public abstract void ProduceInstruction();
    //public abstract void SetUpImmediateOp(string thing);
    public byte[] ConvertBinaryInsToByteArray()
    {
      List<byte> byteUrMom = new List<byte>();

      for(int i = 0; i <= 24; i+= 8)
      {
        byteUrMom.Add(Convert.ToByte(BinaryMeBBG.Substring(i, 8), 2));

      }
      byteUrMom.Reverse();

      /* https://stackoverflow.com/questions/20750062/what-is-the-meaning-of-tostringx2 */

      string hex = string.Join(" ", byteUrMom.Select(b => b.ToString("X2")));
            Console.WriteLine(hex);

            return byteUrMom.ToArray();

    }

    //protected string GetDataProcessingExecutionBinary(string action)
    //{

    //  return "null";
    //}

  }
}