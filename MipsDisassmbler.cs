﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIPSAssembler_Winform
{
    class MIPSDisassembler
    {
        List<int> Labels;
        public int CurrentAddress { get; private set; }
        Register[] registers;
        private static readonly char[] delimitter = { ' ', '\t', '\n', ';', ',' };

        public MIPSDisassembler()
        {
            this.registers = Register.GetRegisters();
            //CurrentAddress = 0;
        }

        public string BinToMips(string Bincode)
        {
            var instructions = Bincode.Split(delimitter, StringSplitOptions.RemoveEmptyEntries); // 分割指令
            // 第一次扫描
            Labels = new List<int>();
            CurrentAddress = 0;
            string result = "";
            for (int i = 0; i < instructions.Length; i += 2)
            {
                string bincode = instructions[i] + instructions[i + 1];
                FirstScan(bincode);
                CurrentAddress++;
            }
            Labels.Sort();
            //第二次扫描
            CurrentAddress = 0;
            for(int i = 0; i < instructions.Length; i +=2)
            {
                string bincode = instructions[i] + instructions[i + 1];
                if (Labels.Contains(CurrentAddress))
                    result += $"Label{Labels.IndexOf(CurrentAddress)}:\n";
                var ins_res = GetInstruction(bincode);
                result += ins_res + '\n';
                CurrentAddress++;
            }
            return result;
        }

        public string CoeToMips(string Bincode)
        {
            var splitted = Bincode.Split(new char[] { '\n', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            splitted = splitted.Where(s => !s.StartsWith(";") && !s.StartsWith("//")).ToArray();
            var instructions = new ArraySegment<string>(splitted, 2, splitted.Length - 2).ToArray();
            instructions[instructions.Length - 1] = instructions[instructions.Length - 1].Substring(0, instructions[instructions.Length - 1].Length - 1);
            // 第一次扫描
            Labels = new List<int>();
            CurrentAddress = 0;
            string result = "";
            foreach(var bincode in instructions)
            {
                FirstScan(bincode);
                CurrentAddress++;
            }
            Labels.Sort();
            //第二次扫描
            CurrentAddress = 0;
            foreach (var bincode in instructions)
            {
                if (Labels.Contains(CurrentAddress))
                    result += $"Label{Labels.IndexOf(CurrentAddress)}:\n";
                var ins_res = GetInstruction(bincode);
                result += ins_res + '\n';
                CurrentAddress++;
            }
            return result;
        }

        private void FirstScan(string input) // 扫出来所有label
        {
            int[] branch = { 0x1, 0x4, 0x5, 0x6, 0x7 };
            int[] jump_op = { 0x2, 0x3 };
            int[] jump_func = { 0x9, 0x8 };

            int op = GetUInt(input, 26, 6);
            if (branch.Contains(op)) // branch
            {
                Int16 offset = (Int16)GetInt(input, 0, 16);
                Labels.Add(offset + CurrentAddress + 1);
            }
            if (jump_op.Contains(op)) // jump
            {
                Labels.Add(GetUInt(input, 0, 26));
            }
            return;
        }

        private string GetInstruction(string input)
        {
            int op = GetUInt(input, 26, 6);
            int func = GetUInt(input, 0, 6);

            switch (op)
            {
                case 0:
                    switch (func)
                        case 0x21:
                        case 0x22:
                        case 0x23:
                        case 0x2A:
                        case 0x2B:
                        case 0x24:
                        case 0x25:
                        case 0x26:
                        case 0x27:
                        case 0x00:
                        case 0x02:
                        case 0x03:
                        case 0x18:
                        case 0x19:
                        case 0x1A:
                        case 0x1B:
                        case 0x08:
                        case 0x09:
                        case 0x10:
                        case 0x11:
                        case 0x12:
                        case 0x13:
                        case 0x0D:
                        case 0x0C:
                        default:
                case 0x23:
                case 0x20:
                case 0x24:
                case 0x21:
                case 0x25:
                case 0x2B:
                case 0x28:
                case 0x29:
                case 0x08:
                case 0x09:
                case 0x0C:
                case 0x0D:
                case 0x0E:
                case 0x0F:
                case 0x0A:
                case 0x0B:
                case 0x04:
                case 0x05:
                case 0x06:
                case 0x07:
                case 0x01:
                        case (0):
                        default:
                case 0x02:
                case 0x03:
                case 0x10:
                default:
                    throw new Exception($"无法解析的OPcode: {op}");
            }


        #region R-ALU
        private string disassemble_add(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;

            return $"add {rd}, {rs}, {rt}";
        }

        private string disassemble_addu(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;

            return $"addu {rd}, {rs}, {rt}";
        }

        private string disassemble_sub(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;

            return $"sub {rd}, {rs}, {rt}";
        }

        private string disassemble_subu(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;

            return $"subu {rd}, {rs}, {rt}";
        }

        private string disassemble_slt(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;

            return $"slt {rd}, {rs}, {rt}";
        }

        private string disassemble_sltu(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;

            return $"sltu {rd}, {rs}, {rt}";
        }

        private string disassemble_and(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;

            return $"and {rd}, {rs}, {rt}";
        }

        private string disassemble_or(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;

            return $"or {rd}, {rs}, {rt}";
        }

        private string disassemble_xor(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;

            return $"xor {rd}, {rs}, {rt}";
        }

        private string disassemble_nor(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;

            return $"nor {rd}, {rs}, {rt}";
        }

        private string disassemble_sll(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            int shamt = GetUInt(input, 6, 5);

            return $"sub {rd}, {rt}, {shamt}";
        }

        private string disassemble_srl(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            int shamt = GetUInt(input, 6, 5);

            return $"sub {rd}, {rt}, {shamt}";
        }

        private string disassemble_sra(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            int shamt = GetUInt(input, 6, 5);

            return $"sub {rd}, {rt}, {shamt}";
        }

        private string disassemble_mult(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;

            return $"sub {rs}, {rt}";
        }

        private string disassemble_multu(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;

            return $"sub {rs}, {rt}";
        }

        private string disassemble_div(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;

            return $"sub {rs}, {rt}";
        }

        private string disassemble_divu(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;

            return $"sub {rs}, {rt}";
        }

        private string disassemble_jr(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            return $"jr {rs}";
        }

        private string disassemble_jalr(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            return $"jalr {rs}";
        }
        #endregion

        #region I-ALU
        private string disassemble_addi(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"addi {rt}, {rs}, {immediate}";
        }

        private string disassemble_addiu(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetUInt(input, 0, 16);
            return $"addiu {rt}, {rs}, {immediate}";
        }

        private string disassemble_andi(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetUInt(input, 0, 16);
            return $"andi {rt}, {rs}, {immediate}";
        }

        private string disassemble_ori(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetUInt(input, 0, 16);
            return $"ori {rt}, {rs}, {immediate}";
        }

        private string disassemble_xori(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetUInt(input, 0, 16);
            return $"xori {rt}, {rs}, {immediate}";
        }

        private string disassemble_lui(string input)
        {
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"lui {rt}, {immediate}";
        }

        private string disassemble_slti(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"slti {rt}, {rs}, {immediate}";
        }

        private string disassemble_sltiu(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetUInt(input, 0, 16);
            return $"sltiu {rt}, {rs}, {immediate}";
        }
        #endregion

        #region Branch
        private string disassemble_beq(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"beq {rs}, {rt}, Label{Labels.IndexOf(immediate + CurrentAddress + 1)}";
        }

        private string disassemble_bne(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"bne {rs}, {rt}, Label{Labels.IndexOf(immediate + CurrentAddress + 1)}";
        }

        private string disassemble_blez(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"belz {rs}, Label{Labels.IndexOf(immediate + CurrentAddress + 1)}";
        }

        private string disassemble_bgtz(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"bgtz {rs}, Label{Labels.IndexOf(immediate + CurrentAddress + 1)}";
        }

        private string disassemble_bltz(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"bltz {rs}, Label{Labels.IndexOf(immediate + CurrentAddress + 1)}";
        }

        private string disassemble_bgez(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"bgez {rs}, Label{Labels.IndexOf(immediate + CurrentAddress + 1)}";
        }
        #endregion

        #region Jump
        private string disassemble_j(string input) // immediate可能不对
        {
            Int32 immediate = (Int32)GetUInt(input, 0, 26);
            return $"j Label{Labels.IndexOf(immediate)}";
        }

        private string disassemble_jal(string input) // immediate可能不对
        {
            Int32 immediate = (Int32)GetUInt(input, 0, 26);
            return $"jal Label{Labels.IndexOf(immediate)}";
        }
        #endregion

        #region Transfer
        private string disassemble_mfhi(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            return $"mfhi {rd}";
        }

        private string disassemble_mflo(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            return $"mflo {rd}";
        }

        private string disassemble_mthi(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            return $"mthi {rs}";
        }

        private string disassemble_mtlo(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            return $"mtlo {rs}";
        }
        #endregion

        #region Privilege
        private string disassemble_eret(string input)
        {
            return "eret";
        }

        private string disassemble_mfc0(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            int sel = GetUInt(input, 0, 3);
            if (sel != 0)
                return $"mfc0 {rt}, {rd}, {sel}";
            else
                return $"mfc0 {rt}, {rd}";
        }

        private string disassemble_mtc0(string input)
        {
            string rd = registers[GetUInt(input, 11, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            int sel = GetUInt(input, 0, 3);
            if (sel != 0)
                return $"mtc0 {rt}, {rd}, {sel}";
            else
                return $"mtc0 {rt}, {rd}";
        }

        #endregion

        #region memory
        private string disassemble_lw(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"lw {rt}, {immediate}({rs})";
        }

        private string disassemble_lb(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"lb {rt}, {immediate}({rs})";
        }

        private string disassemble_lbu(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"lbu {rt}, {immediate}({rs})";
        }

        private string disassemble_lh(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"lh {rt}, {immediate}({rs})";
        }

        private string disassemble_lhu(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"lhu {rt}, {immediate}({rs})";
        }

        private string disassemble_sw(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"sw {rt}, {immediate}({rs})";
        }

        private string disassemble_sb(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"sb {rt}, {immediate}({rs})";
        }

        private string disassemble_sh(string input)
        {
            string rs = registers[GetUInt(input, 21, 5)].Name;
            string rt = registers[GetUInt(input, 16, 5)].Name;
            Int16 immediate = (Int16)GetInt(input, 0, 16);
            return $"sh {rt}, {immediate}({rs})";
        }
        #endregion

        #region Trap
        private string disassemble_break(string input)
        {
            return "break";
        }

        private string disassemble_syscall(string input)
        {
            return "syscall";
        }
        #endregion

        private int GetUInt(string input, int lower_bound, int length)
        {
            var result = (Convert.ToInt64(input, 16) >> lower_bound) % (1 << length);
            return (int)result;
            //return Convert.ToInt32(Convert.ToString(Convert.ToInt32(input, 16), 2).Substring(lower_bound, length));
        }

        private int GetInt(string input, int lower_bound, int length)
        {
            var result = (Convert.ToInt64(input, 16) >> lower_bound) % (1 << length);
            if (result >> (length - 1) == 1)
            {
                result = -((1 << length) - result);
            }
            return (int)result;
            //return Convert.ToInt32(Convert.ToString(Convert.ToInt32(input, 16), 2).Substring(lower_bound, length));
        }
    }
}