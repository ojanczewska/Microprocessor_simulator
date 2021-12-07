using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace zad2_175962
{

    public partial class Form1 : Form
    {

        private int currentAddressingType;
        private int currentInstructionType;
        private int currentDestinationType;
        private int currentSourceType;
        private int currentCommandsExecutingType;
        private int currentExecutingCommand = 0;
        private int totalCommandsNumber = 0;
        private readonly int maxNumberOfCommands = 50;

        private readonly Color fontColor = Color.Black;  
        private readonly Color backExecutedCommandsColor = Color.Gray;
        private readonly Color backCommandBoxColor = Color.White;

        private Int16[] registers = { 0, 0, 0, 0 };
        private List<TextBox> textBoxes = new List<TextBox>();
        private List<NumericUpDown> numericBoxes = new List<NumericUpDown>();
        private AddressingTypeContainer addresses = new AddressingTypeContainer();
        private InstructionTypeContainer instructions = new InstructionTypeContainer();
        private SourceAndDestinationTypeContainer sourcesAndDestinations = new SourceAndDestinationTypeContainer();
        private AHValuesContainer ahValuesInBinary = new AHValuesContainer();
        private List<Add> registryCommander = new List<Add>();
        private Stack<int> interruptsStack = new Stack<int>();

        public Form1()
        {
            InitializeComponent();
            InitializeTextBoxList();
            InitializeNumericBoxList();
            ZerujRejestry();
            FormBorderStyle = FormBorderStyle.FixedSingle; 
            MaximizeBox = false;
            interruptsComboBox.SelectedIndex = 0;
            aHValueComboBox.SelectedIndex = 0;
        }
        private void InitializeTextBoxList() 
        {
            textBoxes.Add(textBox2);
            textBoxes.Add(textBox3);
            textBoxes.Add(textBox4);
            textBoxes.Add(textBox5);
            textBoxes.Add(textBox6);
            textBoxes.Add(textBox7);
            textBoxes.Add(textBox8);
            textBoxes.Add(textBox9);
        }
        private enum Command
        {
            TOTAL,          
            STEP_BY_STEP    
        }

        private enum InstructionTypes
        {
            ADD,   
            SUB,    
            MOV     
        }

        private void InitializeNumericBoxList()
        {
            numericBoxes.Add(numericUpDown1);
            numericBoxes.Add(numericUpDown2);
            numericBoxes.Add(numericUpDown3);
            numericBoxes.Add(numericUpDown4);
           
        }

        private string CheckIfOctet(int number) 
        {
            string uintString = Convert.ToString(number, toBase: 2);
            string convertedString = uintString;

            if (uintString.Length < 8)
            {
                for (int i = 0; i < (8 - uintString.Length); i++)
                {
                    convertedString = "0" + convertedString;
                }
            }

            return convertedString;
        }

        private string ConvertToBites(Int16 number) 
        {
            string finalRegisterValue = "";
            finalRegisterValue = (number >= 0) ? "0" : "1";
            string intString = Convert.ToString(Math.Abs(number), toBase: 2);
            string convertedString = intString;

            if (intString.Length < 15)
            {
                for (int i = 0; i < (15 - intString.Length); i++)
                {
                    convertedString = "0" + convertedString;
                }
            }

            return finalRegisterValue + convertedString;
        }
        private void DivideInt16NumberAndWriteToRegister(string stringBytes, TextBox hTextBox, TextBox lTextBox) 
        {
            hTextBox.Text = stringBytes.Substring(0, stringBytes.Length / 2);
            lTextBox.Text = stringBytes.Substring(stringBytes.Length / 2, stringBytes.Length / 2);
        }

        private void ZerujRejestry()
        {
            for (int i = 0; i < registers.Length; i++)
            {
                registers[i] = 0;
            }

            foreach (TextBox textBox in textBoxes)
            {
                textBox.Text = CheckIfOctet(0b_00000000);
            }
        }
        private void ChooseAdressingTypeViaRadioButton(object sender, EventArgs e) 
        {
            RadioButton radioButton = (RadioButton)sender;
            if(radioButton.Tag.ToString() == "0" || radioButton.Tag.ToString() == "1")
            {
                currentAddressingType = int.Parse(radioButton.Tag.ToString());
                popButton.Enabled = false;
                pushButton.Enabled = false;
                Com_groupBox.Enabled = true;

                if (currentAddressingType == 1)
                {
                    panel2.Enabled = false;
                    numeric.Enabled = true;
                }
                else
                {
                    panel2.Enabled = true;
                    numeric.Enabled = false;
                }
            }
            if(radioButton.Tag.ToString() == "2")
            {
                popButton.Enabled = true;
                pushButton.Enabled = true;
                Com_groupBox.Enabled = false;
                panel2.Enabled = true;
                numeric.Enabled = false;

            }
            
            
        }
        private void ChooseInstructionTypeViaRadioButton(object sender, EventArgs e) 
        {
            RadioButton radioButton = (RadioButton)sender;
            currentInstructionType = int.Parse(radioButton.Tag.ToString());
            label14.Text = Enum.GetName(typeof(InstructionTypes), currentInstructionType);
            aHValueComboBox.Text = "NONE";
            interruptsComboBox.Text = "NONE";
        }

        private void ChooseSourceRegisterViaRadioButton(object sender, EventArgs e) 
        {
            RadioButton radioButton = (RadioButton)sender;
            currentSourceType = int.Parse(radioButton.Tag.ToString());
            label16.Text = Enum.GetName(typeof(Registers), currentSourceType);
            aHValueComboBox.Text = "NONE";
            interruptsComboBox.Text = "NONE";

        }

        private void ChooseDestinationRegisterViaRadioButton (object sender, EventArgs e) 
        {
            RadioButton radioButton = (RadioButton)sender;
            currentDestinationType = int.Parse(radioButton.Tag.ToString());
            label15.Text = Enum.GetName(typeof(Registers), currentDestinationType);
            aHValueComboBox.Text = "NONE";
            interruptsComboBox.Text = "NONE";

        }

        private void numeric_Click(object sender, EventArgs e) 
        {
            label16.Text = Convert.ToString(numeric.Value);
            aHValueComboBox.Text = "NONE";
            interruptsComboBox.Text = "NONE";
        }

        private void ChooseCommandsExecutingTypeViaRadioButton(object sender, EventArgs e) 
        {
            RadioButton radioButton = (RadioButton)sender;
            currentCommandsExecutingType = int.Parse(radioButton.Tag.ToString());
            if (currentCommandsExecutingType == (int)Command.TOTAL)
            {
                for (int i = 0; i < registryCommander.Count; i++)
                {
                    ChangeCommandFontColor(i, fontColor);
                  
                }
            }
            else
            {
                //tutaj
                ChangeCommandFontColor(-1, Color.Red);
            }

        }
        private void RegZero_button_Click(object sender, EventArgs e) 
        {
            ZerujRejestry();

            for (int j = 0; j < numericBoxes.Count; j++)
            {
                numericBoxes[j].Value = registers[j];
            }
        }

        private void SaveToFile(object sender, EventArgs e) 
        {
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
            saveFileDialog1.FileName = "commands";
            saveFileDialog1.ShowDialog();

            FileWriter fileWriter = new FileWriter(saveFileDialog1.FileName, richTextBox.Text);
            fileWriter.WriteToFile();
        }

        private void ReadFromFile(object sender, EventArgs e)  
        {
            radioButton14.Enabled = true;
            radioButton15.Enabled = true;
            button4.Enabled = true;
            button2.Enabled = true;

            string fileContent;
            ClearTextBoxResetCommandNumberAndDisableButton();
            openFileDialog1.CheckFileExists = false;
            openFileDialog1.Filter = "Text files (*.txt)|*.txt";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileContent = System.IO.File.ReadAllText(openFileDialog1.FileName);
                richTextBox.Text = fileContent;
                string regexPattern = @"\d+. (ADD|SUB|MOV)|(PUSH|POP)|INT"; 
                Regex regex = new Regex(regexPattern);
                int secondToLast = richTextBox.Lines.Count() - 2;
                bool firstLineIsValid = regex.IsMatch(richTextBox.Lines[0]) && regex.IsMatch(richTextBox.Lines[secondToLast]);

                if (firstLineIsValid)
                {
                    ChangeCommandFontColor(-1, Color.Red);
                    IterateThroughAllLines(regex);
                }
                else
                {
                    Error errorForm = new Error();
                    errorForm.ShowDialog();
                    string errorMessage = "Wybierz poprawny plik!";
                    richTextBox.Text = errorMessage;
                }
             }
        }

        private void IterateThroughAllLines(Regex regex) 

        {
            int lastLine = richTextBox.Lines.Count() - 1;
            for (int i = 0; i < lastLine; i++)
            {
                string currentCommand = richTextBox.Lines[i];
                currentCommand = Regex.Replace(currentCommand, @"\(.*?\)", "");    
                string[] seperateCommandComponents = currentCommand.Split(' ');

                bool isAnyLineBad = !regex.IsMatch(richTextBox.Lines[i]);
                if (isAnyLineBad) { break; }

                if (seperateCommandComponents.Count() == 2) 
                {
                    registryCommander.Add(new Add(true, ChangeTypesToInt(seperateCommandComponents[1].Replace(";", ""))));
                    totalCommandsNumber++;
                }

                else if (seperateCommandComponents.Count() == 3) 
                {
                    int pushOrPopType = ChangeTypesToInt(seperateCommandComponents[1].Replace(";", ""));
                    int registerType = ChangeTypesToInt(seperateCommandComponents[2].Replace(";", ""));

                    registryCommander.Add(new Add(pushOrPopType, registerType));
                    totalCommandsNumber++;
                }
                else if (seperateCommandComponents.Count() == 4)
                {
                    int instructionType = ChangeTypesToInt(seperateCommandComponents[1]);
                    int addressingType = 0;
                    int sourceType = 0;
                    int value = 0;
                    int destinationType = 0;
                    string checkAddressingType = seperateCommandComponents[seperateCommandComponents.Length - 1].Replace(";", String.Empty);
                    Regex regex2 = new Regex(@"X");
                    Match match = regex2.Match(checkAddressingType);
                    Regex regex3 = new Regex(@"h");
                    Match match2 = regex3.Match(checkAddressingType);

                    if (match2.Success)
                    {
                        destinationType = 0;
                        string value2 = seperateCommandComponents[3];
                        value2 = value2.Replace(";", "");
                        registryCommander.Add(new Add(destinationType, value2));
                        totalCommandsNumber++;
                    }
                    else if (match.Success)
                    {
                        sourceType = ChangeTypesToInt(checkAddressingType);
                        destinationType = ChangeTypesToInt(seperateCommandComponents[seperateCommandComponents.Length - 2]);
                        addressingType = ChangeTypesToInt("REG");
                        registryCommander.Add(new Add(addressingType, instructionType, sourceType, destinationType, value));
                        totalCommandsNumber++;
                    }
                    else
                    {
                        value = int.Parse(seperateCommandComponents[seperateCommandComponents.Length - 1].Replace(";", String.Empty));
                        destinationType = ChangeTypesToInt(seperateCommandComponents[seperateCommandComponents.Length - 2]);
                        sourceType = destinationType;
                        addressingType = ChangeTypesToInt("IMM");
                        registryCommander.Add(new Add(addressingType, instructionType, sourceType, destinationType, value));
                        totalCommandsNumber++;
                    }
                   
                }

               
            }
        }

        private int ChangeTypesToInt(string type) 
        {
            var changeDict = new Dictionary<string, int>()
            {
                {Enum.GetName(typeof(AddressingTypes), AddressingTypes.REG), (int)AddressingTypes.REG },
                {Enum.GetName(typeof(AddressingTypes), AddressingTypes.IMM), (int)AddressingTypes.IMM },
                {Enum.GetName(typeof(InstructionTypes), InstructionTypes.ADD), (int)InstructionTypes.ADD },
                {Enum.GetName(typeof(InstructionTypes), InstructionTypes.SUB), (int)InstructionTypes.SUB },
                {Enum.GetName(typeof(InstructionTypes), InstructionTypes.MOV), (int)InstructionTypes.MOV },
                {Enum.GetName(typeof(Registers), Registers.AX), (int)Registers.AX },
                {Enum.GetName(typeof(Registers), Registers.BX), (int)Registers.BX },
                {Enum.GetName(typeof(Registers), Registers.CX), (int)Registers.CX },
                {Enum.GetName(typeof(Registers), Registers.DX), (int)Registers.DX },
                {Enum.GetName(typeof(Interrupts), Interrupts.NONE), (int)Interrupts.NONE },
                {Enum.GetName(typeof(Interrupts), Interrupts.INT10), (int)Interrupts.INT10 },
                {Enum.GetName(typeof(Interrupts), Interrupts.INT13), (int)Interrupts.INT13 },
                {Enum.GetName(typeof(Interrupts), Interrupts.INT14), (int)Interrupts.INT14 },
                {Enum.GetName(typeof(Interrupts), Interrupts.INT16), (int)Interrupts.INT16 },
                {Enum.GetName(typeof(Interrupts), Interrupts.INT17), (int)Interrupts.INT17 },
                {Enum.GetName(typeof(Interrupts), Interrupts.INT19), (int)Interrupts.INT19 },
                {Enum.GetName(typeof(Interrupts), Interrupts.INT1A), (int)Interrupts.INT1A },
                {Enum.GetName(typeof(Interrupts), Interrupts.INT20), (int)Interrupts.INT20 },
                {Enum.GetName(typeof(PushAndPopStack), PushAndPopStack.PUSH), (int)PushAndPopStack.PUSH },
                {Enum.GetName(typeof(PushAndPopStack), PushAndPopStack.POP), (int)PushAndPopStack.POP },
            };
            return changeDict[type];
        }

        private void ExecuteAction(object sender, EventArgs e)
        {
            if (currentCommandsExecutingType == (int)Command.TOTAL)
            {
                for (int i = 0; i < registryCommander.Count; i++)
                {
                    ExecuteCurrentCommand(i);
                }

                currentExecutingCommand = 0;
                System.Threading.Thread.Sleep(300);  
                ChangeAllTextBackgroundColor(backCommandBoxColor);
            }

            else if (currentExecutingCommand < totalCommandsNumber && currentCommandsExecutingType == (int)Command.STEP_BY_STEP)
            {
                ExecuteCurrentCommand(currentExecutingCommand);
                ChangeCommandFontColor(currentExecutingCommand - 1, Color.Black);
                currentExecutingCommand++;

                if (currentExecutingCommand >= totalCommandsNumber)
                {
                    currentExecutingCommand = 0;
                    System.Threading.Thread.Sleep(300);     
                    ChangeRichTextBoxBackColor();
                    //tutaj
                    ChangeCommandFontColor(-1, Color.Red);
                }
                else
                {
                    ChangeCommandFontColor(currentExecutingCommand - 1, Color.Red);
                }
            }
        }
        private void ExecuteCurrentCommand(int i)
        {
            if (registryCommander[i].GetPushOrPopType() == 0)
            {
                interruptsStack.Push(registers[registryCommander[i].GetRegisterType()]);
            }
            else if (registryCommander[i].GetPushOrPopType() == 1)
            {
                try
                {
                    registers[registryCommander[i].GetRegisterType()] = (short)interruptsStack.Pop();
                }
                catch (InvalidOperationException) { }

                string stringBytes = ConvertToBites(registers[registryCommander[i].GetRegisterType()]);
                DivideInt16NumberAndWriteToRegister(stringBytes, textBoxes[2 * registryCommander[i].GetRegisterType()],
                    textBoxes[2 * registryCommander[i].GetRegisterType() + 1]);

                IterateThroughAllNumericBoxes();
            }
            else if (registryCommander[i].GetAHValue() != "NONE")
            {
                registers[registryCommander[i].GetDestinationType()] = ahValuesInBinary.AHValues[registryCommander[i].GetAHValue()];
                string stringBytes = ConvertToBites(registers[registryCommander[i].GetDestinationType()]);
                DivideInt16NumberAndWriteToRegister(stringBytes, textBoxes[2 * registryCommander[i].GetDestinationType()],
                    textBoxes[2 * registryCommander[i].GetDestinationType() + 1]);

                IterateThroughAllNumericBoxes();
            }
            else if (registryCommander[i].GetIsInterrupt())
            {
                ChooseProperExecutingInterrupt(i);
            }
            else if (registryCommander[i].GetAddressingType() != -1)
            {
                Int16 registerValue = (registryCommander[i].GetAddressingType() == (int)AddressingTypes.REG) ?
                    registers[registryCommander[i].GetSourceType()] : (Int16)registryCommander[i].GetValue();

                switch (registryCommander[i].GetInstructionType())
                {
                    case 0:
                        registers[registryCommander[i].GetDestinationType()] += registerValue;
                        break;
                    case 1:
                        registers[registryCommander[i].GetDestinationType()] -= registerValue;
                        break;
                    case 2:
                        registers[registryCommander[i].GetDestinationType()] = registerValue;
                        break;
                }

                string stringBytes = ConvertToBites(registers[registryCommander[i].GetDestinationType()]);
                DivideInt16NumberAndWriteToRegister(stringBytes, textBoxes[2 * registryCommander[i].GetDestinationType()],
                    textBoxes[2 * registryCommander[i].GetDestinationType() + 1]);

                IterateThroughAllNumericBoxes();
            }
        }

     

        private void WriteCommandIntoTextbox(string command, string valueInText = " ")
        {
            if (valueInText.Equals(String.Empty))
            {
                command = command.Substring(0, command.Length - 2) + ";";
            }

            richTextBox.AppendText(command);
            richTextBox.AppendText(Environment.NewLine);
        }
        private void LoadActionsIntoMemory(object sender, EventArgs e)
        {
            radioButton14.Enabled = true;
            radioButton15.Enabled = true;
            button4.Enabled = true;
            button2.Enabled = true;
            

            if (totalCommandsNumber <= maxNumberOfCommands)
            {
                if (interruptsComboBox.SelectedIndex != 0)
                {
                    string command = $"{totalCommandsNumber}. {interruptsComboBox.Text};";
                    registryCommander.Add(new Add(true, interruptsComboBox.SelectedIndex));
                    WriteCommandIntoTextbox(command);
                }
                else if (aHValueComboBox.SelectedIndex != 0)
                {
                    string command = $"{totalCommandsNumber}. MOV AH {aHValueComboBox.Text};";
                    registryCommander.Add(new Add(0, aHValueComboBox.Text));
                    WriteCommandIntoTextbox(command);
                }
                else
                {
                    string command = "";
                    int currentSourceType = 0;
                    string sourceTypeInText = "";
                    string destinationTypeInText = "";
                    int value = 0;
                    string valueInText = "0";

                    if (currentAddressingType == (int)AddressingTypes.REG)
                    {
                        currentSourceType = this.currentSourceType;
                        sourceTypeInText = sourcesAndDestinations.sourcesAndDestinationsData[this.currentSourceType];
                        destinationTypeInText = $"{sourcesAndDestinations.sourcesAndDestinationsData[currentDestinationType]} ";
                        valueInText = value.ToString().Replace("0", String.Empty);
                    }
                    else
                    {
                        value = (Int16)numeric.Value;
                        currentSourceType = currentAddressingType;
                        destinationTypeInText = $"{sourcesAndDestinations.sourcesAndDestinationsData[currentDestinationType]}";
                        valueInText = value.ToString();
                    }

                    command = $"{totalCommandsNumber}. " +
                        $"{instructions.instructionsData[currentInstructionType]} {destinationTypeInText}{sourceTypeInText} {valueInText};";
                    WriteCommandIntoTextbox(command, valueInText);
                    registryCommander.Add(new Add(currentAddressingType, currentInstructionType,
                        currentSourceType, currentDestinationType, value));
                }
                totalCommandsNumber++;
            }
            interruptsComboBox.SelectedIndex = 0;
            aHValueComboBox.SelectedIndex = 0;
            ChangeCommandFontColor(-1, Color.Red);
        }

       

        private void ChooseProperExecutingInterrupt(int i)
        {
            switch (registryCommander[i].GetInterruptIndex())
            {
                case 1:
                    CursorPositionReader cursorPositionReader = new CursorPositionReader(registers[0], Cursor.Position.X, Cursor.Position.Y);
                    break;
                case 2:
                    DriverStatusChecker driverStatusChecker = new DriverStatusChecker(registers[0]);
                    break;
                case 3:
                    SerialPortServices serialPortServices = new SerialPortServices(registers[0]);
                    break;
                case 4:
                    KeyboardServices keyboardServices = new KeyboardServices(registers[0]);
                    registers[0] = (short)keyboardServices.keyValue;
                    string stringBytes = ConvertToBites(registers[0]);
                    DivideInt16NumberAndWriteToRegister(stringBytes, textBoxes[0], textBoxes[1]);
                    numericBoxes[0].Value = registers[0];
                    break;
                case 5:
                    PrinterServices printerServices = new PrinterServices(registers[0]);
                    break;
                case 6:
                    SystemRebooter systemRebooter = new SystemRebooter();
                    break;
                case 7:
                    TimeReader timeReader = new TimeReader(registers[0]);
                    break;
                case 8:
                    SystemSwitcher systemSwitcher = new SystemSwitcher();
                    break;
            }
        }
        
        private void IterateThroughAllNumericBoxes()
        {
            for (int j = 0; j < numericBoxes.Count; j++)
            {
                numericBoxes[j].Value = registers[j];
            }
        }

        private void ChangeCommandsBackgroundColor(int i, Color color)
        {
            richTextBox.SelectionStart = richTextBox.GetFirstCharIndexFromLine(i);
            richTextBox.SelectionLength = richTextBox.Lines[i].Length;
            richTextBox.SelectionBackColor = color;
        }
        private void ChangeCommandFontColor(int i, Color color)
        {
            richTextBox.SelectionStart = richTextBox.GetFirstCharIndexFromLine(i + 1);
            richTextBox.SelectionLength = richTextBox.Lines[i + 1].Length;
            richTextBox.SelectionColor = color;
        }

        
        private void ChangeAllTextBackgroundColor(Color color)
        {
            richTextBox.SelectionStart = richTextBox.GetFirstCharIndexFromLine(0);
            richTextBox.SelectionLength = richTextBox.TextLength;
            richTextBox.SelectionBackColor = color;
        }
        private void ChangeRichTextBoxBackColor()
        {
            for (int i = 0; i < registryCommander.Count; i++)
            {
                ChangeCommandsBackgroundColor(i, backCommandBoxColor);
            }
        }

        private void ClearTextBoxResetCommandNumberAndDisableButton()
        {
            richTextBox.Clear();
            totalCommandsNumber = 0;
            registryCommander.Clear();
            currentExecutingCommand = 0;
        }

        private void SetReg_button_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < numericBoxes.Count; i++)
            {
                registers[i] = (Int16)numericBoxes[i].Value;
                string stringBytes = ConvertToBites(registers[i]);
                DivideInt16NumberAndWriteToRegister(stringBytes, textBoxes[2 * i], textBoxes[2 * i + 1]);
            }

        }
        private void ClearCommands(object sender, EventArgs e)
        {
            ClearTextBoxResetCommandNumberAndDisableButton();
            radioButton14.Enabled = false;
            radioButton15.Enabled = false;
            button4.Enabled = false;
            button2.Enabled = false;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (interruptsComboBox.Text == "NONE")
            {
                label14.Text = Enum.GetName(typeof(InstructionTypes), currentInstructionType);
                label15.Text = Enum.GetName(typeof(Registers), currentSourceType);
                label16.Text = Enum.GetName(typeof(Registers), currentDestinationType);

            }
            else
            {
                label14.Text = interruptsComboBox.Text;
                label15.Text = "";
                label16.Text = "";

            }


        }

        private void PushOrPop(object sender, EventArgs e)
        {
            radioButton14.Enabled = true;
            radioButton15.Enabled = true;
            button4.Enabled = true;
            button2.Enabled = true;

            Button button = (Button)sender;
            int currentPushOrPullType = int.Parse(button.Tag.ToString());
            int sourceOrDestinationType = (currentPushOrPullType == 0) ? this.currentSourceType : this.currentDestinationType;  
            string destinationOrSourceTypeInText = sourcesAndDestinations.sourcesAndDestinationsData[sourceOrDestinationType];
            string command = $"{totalCommandsNumber}. {button.Text} {destinationOrSourceTypeInText};";

            richTextBox.AppendText(command);
            richTextBox.AppendText(Environment.NewLine);
            registryCommander.Add(new Add(currentPushOrPullType, sourceOrDestinationType));
            totalCommandsNumber++;
        }

        private void aHValueComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (aHValueComboBox.Text == "NONE")
            {
                label14.Text = Enum.GetName(typeof(InstructionTypes), currentInstructionType);
                label15.Text = Enum.GetName(typeof(Registers), currentSourceType);
                label16.Text = Enum.GetName(typeof(Registers), currentDestinationType);

            }
            else
            {
                label14.Text = "MOV";
                label15.Text = "AH";
                label16.Text = aHValueComboBox.Text;

            }
        }

        private void groupBox5_MouseHover(object sender, EventArgs e)
        {
            pushButton.Enabled = true;
            popButton.Enabled = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Info info = new Info();
            info.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

       
    }
}
