using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using Microsoft.Office.Interop.Excel;

namespace caffeKambodzaApplication
{
    public class ExcelFile
    {
        private string __filePath;
        private string _slash = @"\";
        private Microsoft.Office.Interop.Excel.Application _xlApp;
        private int _numofSheets;


        #region privatemethods

        /// <summary>
        /// content fits in cell, size cell equals text size 
        /// </summary>
        /// <param name="row">row of cell which context fits</param>
        /// <param name="col">column of cell which context fits</param>
        /// <param name="ws">sheet of cell which context fits</param>
        private void autoFit(int row, int col, Worksheet ws)
        {
            string column = getExcelColumnName(col);
            string rowstr = row.ToString();
            Range range = ws.get_Range(column + rowstr, column + rowstr);
            range.Columns.AutoFit();
        }

        /// <summary>
        /// check if there is a slash in the path and check whether the appropriate extension(.xls)
        /// </summary>
        /// <param name="filepath">file path excel file</param>
        private void mutualExceptions(string filepath)
        {
            if (!FilePath.Contains(_slash))
            {
                throw new Exception("Your file path must have at least one backslash");
            }

            if (FilePath.ElementAt(FilePath.Length - 1) != 's' || FilePath.ElementAt(FilePath.Length - 2) != 'l' || FilePath.ElementAt(FilePath.Length - 3) != 'x' || FilePath.ElementAt(FilePath.Length - 4) != '.')
            {
                if (FilePath.ElementAt(FilePath.Length - 1) != 'x' || FilePath.ElementAt(FilePath.Length - 2) != 's' || FilePath.ElementAt(FilePath.Length - 3) != 'l' || FilePath.ElementAt(FilePath.Length - 4) != 'x' || FilePath.ElementAt(FilePath.Length - 5) != '.')
                {
                    throw new Exception("Your file path " + FilePath + " must have XLS EXTENSION (.xls) or XLSX EXTENSION (.xlsx)");
                }

            }
        }

        /// <summary>
        /// calculate 26^n
        /// </summary>
        /// <param name="n">n is arbitrary finite number</param>
        /// <returns>returns 26^n</returns>
        private int pow(int n)
        {
            int res = 1;
            for (int i = 0; i < n; i++)
            {
                res = res * 26;
            }
            return res;
        }

        /// <summary>
        /// converts column name into number of column(integer)
        /// </summary>
        /// <param name="letters">column name</param>
        /// <returns>number of column</returns>
        private int excelNumbers(string letters)
        {
            char[] array = letters.ToCharArray();
            Array.Reverse(array);

            int number = 0;
            char figure;
            try
            {
                for (int i = 0; i < array.Length; i++)
                {
                    figure = array[i];

                    switch (figure)
                    {
                        case 'a':
                        case 'A':
                            {
                                if (i == 0) number = number + 1;
                                else number = number + 1 * pow(i);
                                break;
                            }
                        case 'b':
                        case 'B':
                            {
                                if (i == 0) number = number + 2;
                                else number = number + 2 * pow(i);
                                break;
                            }
                        case 'c':
                        case 'C':
                            {
                                if (i == 0) number = number + 3;
                                else number = number + 3 * pow(i);
                                break;
                            }
                        case 'd':
                        case 'D':
                            {
                                if (i == 0) number = number + 4;
                                else number = number + 4 * pow(i);
                                break;
                            }
                        case 'e':
                        case 'E':
                            {
                                if (i == 0) number = number + 5;
                                else number = number + 5 * pow(i);
                                break;
                            }
                        case 'f':
                        case 'F':
                            {
                                if (i == 0) number = number + 6;
                                else number = number + 6 * pow(i);
                                break;
                            }
                        case 'g':
                        case 'G':
                            {
                                if (i == 0) number = number + 7;
                                else number = number + 7 * pow(i);
                                break;
                            }
                        case 'h':
                        case 'H':
                            {
                                if (i == 0) number = number + 8;
                                else number = number + 8 * pow(i);
                                break;
                            }
                        case 'i':
                        case 'I':
                            {
                                if (i == 0) number = number + 9;
                                else number = number + 9 * pow(i);
                                break;
                            }
                        case 'j':
                        case 'J':
                            {
                                if (i == 0) number = number + 10;
                                else number = number + 10 * pow(i);
                                break;
                            }
                        case 'k':
                        case 'K':
                            {
                                if (i == 0) number = number + 11;
                                else number = number + 11 * pow(i);
                                break;
                            }
                        case 'l':
                        case 'L':
                            {
                                if (i == 0) number = number + 12;
                                else number = number + 12 * pow(i);
                                break;
                            }
                        case 'm':
                        case 'M':
                            {
                                if (i == 0) number = number + 13;
                                else number = number + 13 * pow(i);
                                break;
                            }
                        case 'n':
                        case 'N':
                            {
                                if (i == 0) number = number + 14;
                                else number = number + 14 * pow(i);
                                break;
                            }
                        case 'o':
                        case 'O':
                            {
                                if (i == 0) number = number + 15;
                                else number = number + 15 * pow(i);
                                break;
                            }
                        case 'p':
                        case 'P':
                            {
                                if (i == 0) number = number + 16;
                                else number = number + 16 * pow(i);
                                break;
                            }
                        case 'q':
                        case 'Q':
                            {
                                if (i == 0) number = number + 17;
                                else number = number + 17 * pow(i);
                                break;
                            }
                        case 'r':
                        case 'R':
                            {
                                if (i == 0) number = number + 18;
                                else number = number + 18 * pow(i);
                                break;
                            }
                        case 's':
                        case 'S':
                            {
                                if (i == 0) number = number + 19;
                                else number = number + 19 * pow(i);
                                break;
                            }
                        case 't':
                        case 'T':
                            {
                                if (i == 0) number = number + 20;
                                else number = number + 20 * pow(i);
                                break;
                            }
                        case 'u':
                        case 'U':
                            {
                                if (i == 0) number = number + 21;
                                else number = number + 21 * pow(i);
                                break;
                            }
                        case 'v':
                        case 'V':
                            {
                                if (i == 0) number = number + 22;
                                else number = number + 22 * pow(i);
                                break;
                            }
                        case 'w':
                        case 'W':
                            {
                                if (i == 0) number = number + 23;
                                else number = number + 23 * pow(i);
                                break;
                            }
                        case 'x':
                        case 'X':
                            {
                                if (i == 0) number = number + 24;
                                else number = number + 24 * pow(i);
                                break;
                            }
                        case 'y':
                        case 'Y':
                            {
                                if (i == 0) number = number + 25;
                                else number = number + 25 * pow(i);
                                break;
                            }
                        case 'z':
                        case 'Z':
                            {
                                if (i == 0) number = number + 26;
                                else number = number + 26 * pow(i);
                                break;
                            }
                        default: throw new Exception(i + ". figure is not a letter!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
                System.Environment.Exit(1);
            }


            return number;

        }

        /// <summary>
        /// converts number of column(integer) into column name
        /// </summary>
        /// <param name="columnNumber">number of column</param>
        /// <returns>column name</returns>
        private string getExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;//ASCII for A is 65
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        #endregion

      
        #region constructors

        /// <summary>
        /// class(ExcelFile) constructor
        /// </summary>
        /// <param name="filePath">file path of excel file</param>
        public ExcelFile(string filePath)
        {
            __filePath = filePath;
            _xlApp = new Microsoft.Office.Interop.Excel.Application();
        }

        #endregion


        #region file

        /// <summary>
        /// create file with only one worksheet, based on file path that sets in class constructor
        /// </summary>
        public void createFile()
        {
            try
            {
                FileInfo finfo = new FileInfo(FilePath);

                if (Directory.Exists(FilePath) == false && FilePath[1].Equals(':')==false)
                {
                    throw new Exception("The directory doesn't exist! You must create directory for file path " + FilePath);
                }

                if (finfo.Exists == true)
                {
                    finfo.Delete();
                }

                mutualExceptions(FilePath);


                if (_xlApp == null)
                {
                    throw new Exception("EXCEL could not be started. Check that your office installation and project references are correct.");
                }

                Workbook wb = _xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
              

                Worksheet ws = (Worksheet)wb.Worksheets[1];

                if (ws == null)
                {
                    throw new Exception("Worksheet could not be created. Check that your office installation and project references are correct.");
                }

                wb.SaveAs(FilePath);
                wb.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }
        /// <summary>
        /// PrintTitleRows set header for printing 
        /// </summary>
        /// <param name="firstrow">first row of header at each printing side</param>
        /// <param name="lastrow">last row od header at each printing side</param>
        public void createFile(int firstrow, int lastrow)
        {
            try
            {
                FileInfo finfo = new FileInfo(FilePath);

                if (Directory.Exists(FilePath) == false && FilePath[1].Equals(':') == false)
                {
                    throw new Exception("The directory doesn't exist! You must create directory for file path " + FilePath);
                }

                if (finfo.Exists == true)
                {
                    finfo.Delete();
                }

                mutualExceptions(FilePath);


                if (_xlApp == null)
                {
                    throw new Exception("EXCEL could not be started. Check that your office installation and project references are correct.");
                }

                Workbook wb = _xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);


                Worksheet ws = (Worksheet)wb.Worksheets[1];
                //ws.PageSetup.PrintTitleRows = "$3:$4";
                ws.PageSetup.PrintTitleRows = "$" + firstrow.ToString() + ":$" + lastrow.ToString();

                if (ws == null)
                {
                    throw new Exception("Worksheet could not be created. Check that your office installation and project references are correct.");
                }

                wb.SaveAs(FilePath);
                wb.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
 
        }

        /// <summary>
        /// PrintTitleRows set header for printing 
        /// </summary>
        /// <param name="firstrow">first row of header at each printing side</param>
        /// <param name="lastrow">last row od header at each printing side</param>
        /// <param name="pageOrientation"> portrait(char p) or landscape(char l) page orientation</param>
        public bool createFile(int firstrow, int lastrow,char pageOrientation)
        {
            try
            {
                FileInfo finfo = new FileInfo(FilePath);

                if (Directory.Exists(FilePath) == false && FilePath[1].Equals(':') == false)
                {
                    throw new Exception("The directory doesn't exist! You must create directory for file path " + FilePath);
                }

                if (finfo.Exists == true)
                {
                    DialogResult dialogResult = MessageBox.Show("Fajl koji je bio na putanji " + FilePath + " biće obrisan, ukoliko kliknete na YES dugme. A ukoliko ne želite da ga obrišete kliknite na dugme NO.", "PREPISIVANJE PREKO POSTOJEĆEG FAJLA", MessageBoxButtons.YesNo);
                    Logger.writeNode(Constants.EXCEPTION_EXCEL, "Fajl koji je bio na putanji " + FilePath + " biće obrisan, ukoliko kliknete na YES dugme. A ukoliko ne želite da ga obrišete kliknite na dugme NO.");
                    if (dialogResult == DialogResult.Yes)
                    {
                        MessageBox.Show("Fajl koji je bio na putanji " + FilePath + " biće obrisan.");
                        Logger.writeNode(Constants.EXCEPTION_EXCEL, "Fajl koji je bio na putanji " + FilePath + " biće obrisan.");
                        finfo.Delete();
                    }
                    else if (dialogResult == DialogResult.No)
                    {

                        return false;
                    }
                    
                }

                mutualExceptions(FilePath);


                if (_xlApp == null)
                {
                    throw new Exception("EXCEL could not be started. Check that your office installation and project references are correct.");
                }

                Workbook wb = _xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);


                Worksheet ws = (Worksheet)wb.Worksheets[1];
                //ws.PageSetup.PrintTitleRows = "$3:$4";
                ws.PageSetup.PrintTitleRows = "$" + firstrow.ToString() + ":$" + lastrow.ToString();
                if (pageOrientation == 'p' || pageOrientation == 'P')
                {
                    ws.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                    ws.PageSetup.Zoom = false;
                    ws.PageSetup.FitToPagesWide = 1;
                    ws.PageSetup.LeftMargin = 0.5;
                    ws.PageSetup.RightMargin = 0.7;
                    ws.PageSetup.TopMargin = 0.4;
                    ws.PageSetup.BottomMargin = 0.8;
                    ws.PageSetup.HeaderMargin = 0.4;
                    ws.PageSetup.FooterMargin = 25;
                
                   
                }
                if (pageOrientation == 'l' || pageOrientation == 'L')
                {
                    ws.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                }
               
                string startRange = "F11";
                string endRange = "I11";
                Range currentRange = (Range)ws.get_Range(startRange , endRange );
                currentRange.Orientation = 90;


                Range er = ws.get_Range("B:B", System.Type.Missing);

                er.EntireColumn.ColumnWidth = 100;



                if (ws == null)
                {
                    throw new Exception("Worksheet could not be created. Check that your office installation and project references are correct.");
                }

                wb.SaveAs(FilePath);
                //wb.Save();
                wb.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                return false;
              
            }

        }

        /// <summary>
        /// open file based on file path that sets in class constructor
        /// </summary>
        public void openFile()
        {
            try
            {
                FileInfo finfo = new FileInfo(FilePath);

                mutualExceptions(FilePath);

                if (finfo.Exists == false)
                {
                    throw new FileNotFoundException("The file was not found.", FilePath);

                }


                _xlApp.Visible = true;

                _xlApp.Workbooks.Open(FilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);

            }
        }

        /// <summary>
        /// close file based on file path that sets in class constructor
        /// </summary>
        public void closeFile()
        {
            try
            {
                mutualExceptions(FilePath);

                FileInfo finfo = new FileInfo(FilePath);

                if (finfo.Exists == false)
                {
                    throw new FileNotFoundException("The file was not found.", FilePath);

                }

                _xlApp.Workbooks.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);

            }
        }

        /// <summary>
        /// delete file based on file path that sets in class constructor
        /// </summary>
        public void deleteFile()
        {
            try
            {
                mutualExceptions(FilePath);

                FileInfo finfo = new FileInfo(FilePath);

                if (finfo.Exists == true)
                {
                    finfo.Delete();
                }
                else
                {
                    throw new Exception("File not found! Deletion Operation!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);

            }
        }

        #endregion

        #region write

        public void writeDoubleDownLine(int row, int col) 
        {
             Workbook wb = _xlApp.Workbooks.Open(FilePath);
             Worksheet ws = wb.Sheets[1];

             ws.Cells[row, col].Borders(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlDouble;
             ws.Cells[row, col].Borders(XlBordersIndex.xlEdgeBottom).Weight = XlBorderWeight.xlThick;

             wb.Save();
             wb.Close();
        }




        /// <summary>
        /// write in only one cell (in first worksheet)
        /// </summary>
        /// <param name="row">row of the cell where we want to write</param>
        /// <param name="col">column of the cell where we want to write</param>
        /// <param name="value">content what we want to write</param>
        public void writeCell(int row, int col, string value,bool autofit = true,int  size = 10)
        {
            Workbook wb = _xlApp.Workbooks.Open(FilePath);
            Worksheet ws = wb.Sheets[1];
            ws.Cells[row, col] = value;
            ws.Cells[row, col].Font.Bold = true;
            ws.Cells[row, col].Font.Size = size;
            ws.Cells[row, col].Font.Name = "Arial";

            if (row == 4 && col == 2)//naziv ugost.organizacije
            {

                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 3]].Merge();
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 3]].Borders(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous;
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 3]].Borders(XlBordersIndex.xlEdgeTop).Weight = XlBorderWeight.xlMedium;
            }

            if (row == 6 && col == 2)//naziv poslovne jedinice
            {

                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 3]].Merge();
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 3]].Borders(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous;
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 3]].Borders(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlContinuous;
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 3]].Borders(XlBordersIndex.xlEdgeTop).Weight = XlBorderWeight.xlMedium;
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 3]].Borders(XlBordersIndex.xlEdgeBottom).Weight = XlBorderWeight.xlMedium;
            }


            if (row == 8 && col == 2)//naziv ugost. Objekta
            {

                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 3]].Merge();
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 3]].Borders(XlBordersIndex.xlEdgeTop).LineStyle = XlLineStyle.xlContinuous;
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 3]].Borders(XlBordersIndex.xlEdgeTop).Weight = XlBorderWeight.xlMedium;
            }

            if (row == 5 && col == 8)//DNEVNI OBRACUN
            {

                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 3]].Merge();
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 3]].Font.Bold = false;
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 3]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            }

            if (row == 6 && col == 7)//PROMETA I ZALIHA ROBE U UGOSTITELJSTVU
            {

                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 5]].Merge();
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 5]].Font.Bold = false;
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 5]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            }
            

            if (row == 11 && (col == 6 || col == 7 || col == 8 || col == 9 || col == 10))
            {
               
                ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col]].Merge();
                ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col]].VerticalAlignment = XlVAlign.xlVAlignCenter;
            }
            if(row == 11 && col == 2)//Red. Broj
            {
                
                 ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col]].Merge();
                 ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                 ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col]].VerticalAlignment = XlVAlign.xlVAlignCenter;
            }
            if (row == 11 && col == 3)//Naziv robe
            {
                ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col + 2]].Merge();
                ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col + 2]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col + 2]].VerticalAlignment = XlVAlign.xlVAlignCenter;
            }
            if (row == 11 && col == 11)
            {
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 1]].Merge();
                ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col]].VerticalAlignment = XlVAlign.xlVAlignCenter;
            }
            if (row == 12 && col == 11)//kolicina
            {
                ws.Range[ws.Cells[row, col], ws.Cells[row + 2, col]].Merge();
                ws.Range[ws.Cells[row, col], ws.Cells[row + 2, col]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Range[ws.Cells[row, col], ws.Cells[row + 2, col]].VerticalAlignment = XlVAlign.xlVAlignBottom; 
            }

            if (row == 12 && col == 12)//vrednost (8x4)
            {
                ws.Range[ws.Cells[row, col], ws.Cells[row + 2, col]].Merge();
                ws.Range[ws.Cells[row, col], ws.Cells[row + 2, col]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Range[ws.Cells[row, col], ws.Cells[row + 2, col]].VerticalAlignment = XlVAlign.xlVAlignBottom; 
            }

            if (row == 11 && col == 13)//razlika (7-8)
            {
                ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col]].Merge();
                ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col]].VerticalAlignment = XlVAlign.xlVAlignCenter; 
            }

            if (row == 11 && col == 14)//zalihe
            {
                ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col]].Merge();
                ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Range[ws.Cells[row, col], ws.Cells[row + 3, col]].VerticalAlignment = XlVAlign.xlVAlignCenter;
            }


            if (autofit)
            {
                autoFit(row, col, ws);
            }

            if (row == 10 && col == 10) // date
            {
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 4]].Merge();
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 4]].HorizontalAlignment = XlHAlign.xlHAlignRight;
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 4]].VerticalAlignment = XlVAlign.xlVAlignCenter;
            }


            if (row == 15 && col == 2)//1
            {
                ws.Cells[row, col].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Cells[row, col].VerticalAlignment = XlVAlign.xlVAlignCenter;
                ws.Cells[row, col].Font.Bold = false;
            }

            if (row == 15 && col == 3)//2
            {
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 2]].Merge();
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 2]].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Range[ws.Cells[row, col], ws.Cells[row, col + 2]].VerticalAlignment = XlVAlign.xlVAlignCenter;
                ws.Cells[row, col].Font.Bold = false;
            }

            if (row == 15 && col == 6 )//3
            {
                ws.Cells[row, col].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Cells[row, col].VerticalAlignment = XlVAlign.xlVAlignCenter;
                ws.Cells[row, col].Font.Bold = false;
            }

            if (row == 15 && col == 7)//4
            {
                ws.Cells[row, col].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Cells[row, col].VerticalAlignment = XlVAlign.xlVAlignCenter;
                ws.Cells[row, col].Font.Bold = false;
            }

            if (row == 15 && col == 8)//5
            {
                ws.Cells[row, col].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Cells[row, col].VerticalAlignment = XlVAlign.xlVAlignCenter;
                ws.Cells[row, col].Font.Bold = false;
            }

            if (row == 15 && col == 9)//6
            {
                ws.Cells[row, col].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Cells[row, col].Font.Bold = false;
            }

            if (row == 15 && col == 10)//7
            {
                ws.Cells[row, col].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Cells[row, col].VerticalAlignment = XlVAlign.xlVAlignCenter;
                ws.Cells[row, col].Font.Bold = false;
            }

            if (row == 15 && col == 11)//8
            {
                ws.Cells[row, col].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Cells[row, col].VerticalAlignment = XlVAlign.xlVAlignCenter;
                ws.Cells[row, col].Font.Bold = false;
            }

            if (row == 15 && col == 12)//9
            {
                ws.Cells[row, col].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Cells[row, col].VerticalAlignment = XlVAlign.xlVAlignCenter;
                ws.Cells[row, col].Font.Bold = false;
            }

            if (row == 15 && col == 13)//10
            {
                ws.Cells[row, col].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Cells[row, col].VerticalAlignment = XlVAlign.xlVAlignCenter;
                ws.Cells[row, col].Font.Bold = false;
            }

            if (row == 15 && col == 14)//11
            {
                ws.Cells[row, col].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                ws.Cells[row, col].VerticalAlignment = XlVAlign.xlVAlignCenter;
                ws.Cells[row, col].Font.Bold = false;
            }


            wb.Save();
            wb.Close();
        }

        /// <summary>
        /// write in horizontal array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in horizontal array where we want to write</param>
        /// <param name="colbeg">column number of the first cell in horizontal array where we want to write</param>
        /// <param name="rowend">row of the last cell in horizontal array where we want to write</param>
        /// <param name="colend">column number of the last cell in horizontal array where we want to write</param>
        /// <param name="values">range of values that we want to write in excel horizontal array</param>
        public void writeArrayHorizontal(int rowbeg, int colbeg, int rowend, int colend, string[] values)
        {
            try
            {
                Workbook wb = _xlApp.Workbooks.Open(FilePath);
                Worksheet ws = wb.Sheets[1];

                if (rowbeg != rowend)
                {
                    throw new Exception("Begin and end rows must be same!");
                }

                if (colend <= colbeg)
                {
                    throw new Exception("End column must be greater than begin column!");
                }

                if ((colend - colbeg + 1) != values.Length)
                {
                    throw new Exception("Values aren't good!");
                }


                for (int i = 0; i < values.Length; i++)
                {
                    ws.Cells[rowbeg, (colbeg + i)] = values[i];
                    autoFit(rowbeg, (colbeg + i), ws);
                }

                wb.Save();
                wb.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }

        }

        /// <summary>
        /// write in horizontal array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in horizontal array where we want to write</param>
        /// <param name="colbegstr">column name of the first cell in horizontal array where we want to write</param>
        /// <param name="rowend">row of the last cell in horizontal array where we want to write</param>
        /// <param name="colendstr">column name of the last cell in horizontal array where we want to write</param>
        /// <param name="values">range of values that we want to write in excel horizontal array</param>
        public void writeArrayHorizontal(int rowbeg, string colbegstr, int rowend, string colendstr, string[] values)
        {

            int colbeg, colend;
            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);

            this.writeArrayHorizontal(rowbeg, colbeg, rowend, colend, values);
        }

        public void WriteDataTable(System.Data.DataTable Tbl, int dailyReport_RowUnder, int dailyReport_ColumnRight)
        {

            Workbook wb = _xlApp.Workbooks.Open(FilePath);
            Worksheet ws = wb.Sheets[1];

            Range erB = ws.get_Range("B:B", System.Type.Missing);
            erB.EntireColumn.ColumnWidth = 9;
            Range erF = ws.get_Range("F:F", System.Type.Missing);
            erF.EntireColumn.ColumnWidth = 7.29;
            Range erG = ws.get_Range("G:G", System.Type.Missing);
            erG.EntireColumn.ColumnWidth = 7.5;
            Range erH = ws.get_Range("H:H", System.Type.Missing);
            erH.EntireColumn.ColumnWidth = 7;
            Range erI = ws.get_Range("I:I", System.Type.Missing);
            erI.EntireColumn.ColumnWidth = 7.57;
            Range erJ = ws.get_Range("J:J", System.Type.Missing);
            erJ.EntireColumn.ColumnWidth = 8;
            Range erK = ws.get_Range("K:K", System.Type.Missing);
            erK.EntireColumn.ColumnWidth = 7.14;
            Range erL = ws.get_Range("L:L", System.Type.Missing);
            erL.EntireColumn.ColumnWidth = 8;
            Range erM = ws.get_Range("M:M", System.Type.Missing);
            erM.EntireColumn.ColumnWidth = 8.86;
            Range erN = ws.get_Range("N:N", System.Type.Missing);
            erN.EntireColumn.ColumnWidth = 8.86;

            //ws.Cells.Font.Name = "Arial";
           

               // rows
                    for (int i = dailyReport_RowUnder; i < Tbl.Rows.Count + dailyReport_RowUnder; i++)
                    {
                      
                        for (int j = dailyReport_ColumnRight; j < Tbl.Columns.Count + dailyReport_ColumnRight; j++)
                        {
                           
                            ws.Cells[(i + 2), (j + 1)] = Tbl.Rows[i - dailyReport_RowUnder][j - dailyReport_ColumnRight];
                            
                        }
                    }

                   

                    


                   
                    for (int i = 1; i <= Tbl.Rows.Count; i++)
                    {
                        Microsoft.Office.Interop.Excel.Range rInv = ws.Range[ws.Cells[dailyReport_RowUnder + 1 + i, 3], ws.Cells[dailyReport_RowUnder + 1 + i, 5]];
                        rInv.Merge(true);
                    }

                    Microsoft.Office.Interop.Excel.Range c1num = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1 + dailyReport_RowUnder , 1 + dailyReport_ColumnRight];
                    Microsoft.Office.Interop.Excel.Range c2num = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1 + dailyReport_RowUnder + Tbl.Rows.Count, Tbl.Columns.Count + dailyReport_ColumnRight];
                    Microsoft.Office.Interop.Excel.Range rangeNumber = ws.get_Range(c1num, c2num);
                    rangeNumber.Font.Name = "Arial";
                    rangeNumber.Font.Size = 12;
                    rangeNumber.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    rangeNumber.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    rangeNumber.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    rangeNumber.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    rangeNumber.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    rangeNumber.Borders.get_Item(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical).LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;


                    rangeNumber.Font.Bold = true;

                    c1num = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1 + dailyReport_RowUnder, 1 + dailyReport_ColumnRight];
                    c2num = (Microsoft.Office.Interop.Excel.Range)ws.Cells[1 + dailyReport_RowUnder, Tbl.Columns.Count + dailyReport_ColumnRight];
                    rangeNumber = ws.get_Range(c1num, c2num);

                    rangeNumber.Font.Bold = false;

                  


            wb.Save();
            wb.Close();
        }

        /// <summary>
        ///  write in vertical array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in vertical array where we want to write</param>
        /// <param name="colbeg">column number of the first cell in vertical array where we want to write</param>
        /// <param name="rowend">row of the last cell in vertical array where we want to write</param>
        /// <param name="colend">column number of the last cell in vertical array where we want to write</param>
        /// <param name="values">range of values that we want to write in excel vertical array</param>
        public void writeArrayVertical(int rowbeg, int colbeg, int rowend, int colend, string[] values)
        {
            try
            {
                Workbook wb = _xlApp.Workbooks.Open(FilePath);
                Worksheet ws = wb.Sheets[1];

                Range erB = ws.get_Range("B:B", System.Type.Missing);
                erB.EntireColumn.ColumnWidth = 9;
                Range erF = ws.get_Range("F:F", System.Type.Missing);
                erF.EntireColumn.ColumnWidth = 7.29;
                Range erG = ws.get_Range("G:G", System.Type.Missing);
                erG.EntireColumn.ColumnWidth = 7.5;
                Range erH = ws.get_Range("H:H", System.Type.Missing);
                erH.EntireColumn.ColumnWidth = 7;
                Range erI = ws.get_Range("I:I", System.Type.Missing);
                erI.EntireColumn.ColumnWidth = 7.57;
                Range erJ = ws.get_Range("J:J", System.Type.Missing);
                erJ.EntireColumn.ColumnWidth = 8;
                Range erK = ws.get_Range("K:K", System.Type.Missing);
                erK.EntireColumn.ColumnWidth = 7.14;
                Range erL = ws.get_Range("L:L", System.Type.Missing);
                erL.EntireColumn.ColumnWidth = 8;
                Range erM = ws.get_Range("M:M", System.Type.Missing);
                erM.EntireColumn.ColumnWidth = 8.86;
                Range erN = ws.get_Range("N:N", System.Type.Missing);
                erN.EntireColumn.ColumnWidth = 8.86;

                ws.Cells.Font.Name = "Arial";



                if (colbeg != colend)
                {
                    throw new Exception("Begin and end columns must be same!");
                }

                if (rowend < rowbeg)
                {
                    throw new Exception("End row must be greater than begin row!");
                }

                if ((rowend - rowbeg + 1) != values.Length)
                {
                    throw new Exception("Values aren't good!");
                }

            

                int maxlength = values[0].Length;

                for (int i = 0; i < values.Length; i++)
                {
                    ws.Cells[(rowbeg + i), colbeg] = values[i];
                    if (colbeg == 3)
                    {
                        ws.Range[ws.Cells[(rowbeg + i), colbeg], ws.Cells[(rowbeg + i), colbeg + 2]].Merge();
                    }
                    ws.Cells[(rowbeg + i), colbeg].Font.Bold = true;
                    ws.Cells[(rowbeg + i), colbeg].Font.Size = 12;

                    if (colbeg == 6 || colbeg == 2)
                    {
                        ws.Cells[(rowbeg + i), colbeg].HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    }

                    if (values[i].Length >= maxlength)
                    {
                        maxlength = values[i].Length;
                        //autoFit((rowbeg + i), colbeg, ws);
                    }
                }

                wb.Save();
                wb.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }

        }

        /// <summary>
        ///  write in vertical array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in vertical array where we want to write</param>
        /// <param name="colbegstr">column name of the first cell in vertical array where we want to write</param>
        /// <param name="rowend">row of the last cell in vertical array where we want to write</param>
        /// <param name="colendstr">column name of the last cell in vertical array where we want to write</param>
        /// <param name="values">range of values that we want to write in excel vertical array</param>
        public void writeArrayVertical(int rowbeg, string colbegstr, int rowend, string colendstr, string[] values)
        {

            int colbeg, colend;
            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);

            this.writeArrayVertical(rowbeg, colbeg, rowend, colend, values);
        }

        /// <summary>
        /// write in horizontal array of cells (in first worksheet)
        /// </summary>
        /// <param name="colbegstr">column name of the first cell in horizontal array where we want to write</param>
        /// <param name="rowbeg">row of the first cell in horizontal array where we want to write</param>
        /// <param name="colendstr">column name of the last cell in horizontal array where we want to write</param>
        /// <param name="rowend">row of the last cell in horizontal array where we want to write</param>
        /// <param name="values">range of values that we want to write in excel horizontal array</param>
        public void writeArrayHor(string colbegstr, int rowbeg, string colendstr, int rowend, string[] values)
        {
            int colbeg, colend;
            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);

            this.writeArrayHorizontal(rowbeg, colbeg, rowend, colend, values);
        }

        /// <summary>
        /// write in vertical array of cells (in first worksheet)
        /// </summary>
        /// <param name="colbegstr">column name of the first cell in vertical array where we want to write</param>
        /// <param name="rowbeg">row of the first cell in vertical array where we want to write</param>
        /// <param name="colendstr">column name of the last cell in vertical array where we want to write</param>
        /// <param name="rowend">row of the last cell in vertical array where we want to write</param>
        /// <param name="values">range of values that we want to write in excel vertical array</param>
        public void writeArrayVer(string colbegstr, int rowbeg, string colendstr, int rowend, string[] values)
        {
            int colbeg, colend;
            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);

            

            this.writeArrayVertical(rowbeg, colbeg, rowend, colend, values);
        }

        public void writeArrayVerWithVerticalAligment(string colbegstr, int rowbeg, string colendstr, int rowend, string[] values)
        {
            int colbeg, colend;
           
            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);

           



            this.writeArrayVertical(rowbeg, colbeg, rowend, colend, values);
        }

        #endregion

        #region read

        /// <summary>
        /// read content of cell (in first worksheet)
        /// </summary>
        /// <param name="row">row of the read cell</param>
        /// <param name="col">column of the read cell</param>
        /// <returns>the content that is read</returns>
        public string readCell(int row, int col)
        {
            Workbook wb = _xlApp.Workbooks.Open(FilePath);
            Worksheet ws = wb.Sheets[1];
            string value;
            Range rng = ws.Cells[row, col] as Range;
            value = rng.Value2;
            wb.Save();
            wb.Close();
            return value;
        }

        /// <summary>
        /// read horizontal array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first read cell in horizontal array</param>
        /// <param name="colbeg">column number of the first read cell in horizontal array</param>
        /// <param name="rowend">row of the last read cell in horizontal array</param>
        /// <param name="colend">column number of the last read cell in horizontal array</param>
        /// <returns>the range of values that are read</returns>
        public string[] readArrayHorizontal(int rowbeg, int colbeg, int rowend, int colend)
        {
            try
            {
                Workbook wb = _xlApp.Workbooks.Open(FilePath);
                Worksheet ws = wb.Sheets[1];
                string[] values = new string[colend - colbeg + 1];

                if (rowbeg != rowend)
                {
                    throw new Exception("Begin and end rows must be same!");
                }

                if (colend <= colbeg)
                {
                    throw new Exception("End column must be greater than begin column!");
                }


                for (int i = 0; i < values.Length; i++)
                {
                    Range rng = ws.Cells[rowbeg, (colbeg + i)] as Range;
                    values[i] = rng.Value2;
                    if (values[i] == null)
                    {
                        throw new Exception("You can't read empty field!");
                    }
                    //values[i] = ws.Cells[rowbeg, (colbeg + i)];
                }

                wb.Save();
                wb.Close();
                return values;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                return new string[1] { "exception readArrayHorizontal" };
            }

        }

        /// <summary>
        /// read horizontal array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first read cell in horizontal array</param>
        /// <param name="colbegstr">column name of the first read cell in horizontal array</param>
        /// <param name="rowend">row of the last read cell in horizontal array</param>
        /// <param name="colendstr">column name of the last read cell in horizontal array</param>
        /// <returns>the range of values that are read</returns>
        public string[] readArrayHorizontal(int rowbeg, string colbegstr, int rowend, string colendstr)
        {

            int colbeg, colend;

            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);
            string[] values = new string[colend - colbeg + 1];

            values = this.readArrayHorizontal(rowbeg, colbeg, rowend, colend);
            return values;
        }

        /// <summary>
        /// read vertical array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first read cell in vertical array</param>
        /// <param name="colbeg">column number of the first read cell in vertical array</param>
        /// <param name="rowend">row of the last read cell in vertical array</param>
        /// <param name="colend">column number of the last read cell in vertical array</param>
        /// <returns>the range of values that are read</returns>
        public string[] readArrayVertical(int rowbeg, int colbeg, int rowend, int colend)
        {
            try
            {
                Workbook wb = _xlApp.Workbooks.Open(FilePath);
                Worksheet ws = wb.Sheets[1];
                string[] values = new string[rowend - rowbeg + 1];

                if (colbeg != colend)
                {
                    throw new Exception("Begin and end columns must be same!");
                }

                if (rowend < rowbeg)
                {
                    throw new Exception("End row must be greater than begin row!");
                }


                for (int i = 0; i < values.Length; i++)
                {
                    Range rng = ws.Cells[(rowbeg + i), colbeg] as Range;
                    values[i] = rng.Value2;
                    if (values[i] == null)
                    {
                        throw new Exception("You can't read empty field!");
                    }
                    //values[i] = ws.Cells[(rowbeg + i), colbeg];
                }

                wb.Save();
                wb.Close();
                return values;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                return new string[1] { "exception readArrayVertical" };
            }

        }

        /// <summary>
        /// read vertical array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first read cell in vertical array</param>
        /// <param name="colbegstr">column name of the first read cell in vertical array</param>
        /// <param name="rowend">row of the last read cell in vertical array</param>
        /// <param name="colendstr">column name of the last read cell in vertical array</param>
        /// <returns>the range of values that are read</returns>
        public string[] readArrayVertical(int rowbeg, string colbegstr, int rowend, string colendstr)
        {

            int colbeg, colend;
            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);
            string[] values = new string[rowend - rowbeg + 1];

            values = this.readArrayVertical(rowbeg, colbeg, rowend, colend);
            return values;
        }



        /// <summary>
        /// read horizontal array of cells (in first worksheet)
        /// </summary>
        /// <param name="colbegstr">column name of the first read cell in horizontal array</param>
        /// <param name="rowbeg">row of the first read cell in horizontal array</param>
        /// <param name="colendstr">column name of the last read cell in horizontal array</param>
        /// <param name="rowend">row of the last read cell in horizontal array</param>
        /// <returns>the range of values that are read</returns>
        public string[] readArrayHor(string colbegstr, int rowbeg, string colendstr, int rowend)
        {
            int colbeg, colend;
            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);
            string[] values = new string[colend - colbeg + 1];

            values = this.readArrayHorizontal(rowbeg, colbeg, rowend, colend);
            return values;
        }

        /// <summary>
        /// read vertical array of cells (in first worksheet)
        /// </summary>
        /// <param name="colbegstr">column name of the first read cell in vertical array</param>
        /// <param name="rowbeg">row of the first read cell in vertical array</param>
        /// <param name="colendstr">column name of the last read cell in vertical array</param>
        /// <param name="rowend">row of the last read cell in vertical array</param>
        /// <returns>the range of values that are read</returns>
        public string[] readArrayVer(string colbegstr, int rowbeg, string colendstr, int rowend)
        {
            int colbeg, colend;
            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);
            string[] values = new string[rowend - rowbeg + 1];

            values = this.readArrayVertical(rowbeg, colbeg, rowend, colend);
            return values;
        }


        #endregion

        #region properties
        /// <summary>
        /// set or get file path of excel file
        /// </summary>
        public string FilePath
        {
            get
            {
                return __filePath;
            }

            set
            {
                __filePath = value;
            }
        }
        #endregion


        #region multiple sheets

        #region file

        /// <summary>
        /// create file with more than one worksheet, based on file path that sets in class constructor
        /// </summary>
        /// <param name="numofSheets">number of worksheets of created excel file</param>
        public void createFile(int numofSheets)
        {
            try
            {

                _numofSheets = numofSheets;
                FileInfo finfo = new FileInfo(FilePath);

                if (Directory.Exists(FilePath) == false)
                {
                    throw new Exception("The directory doesn't exist! You must create directory for file path " + FilePath);
                }

                if (finfo.Exists == true)
                {
                    finfo.Delete();
                }

                mutualExceptions(FilePath);


                if (_xlApp == null)
                {
                    throw new Exception("EXCEL could not be started. Check that your office installation and project references are correct.");
                }

                var wb = _xlApp.Workbooks.Add();
                var collection2 = new Microsoft.Office.Interop.Excel.Worksheet[2];
                var collection = new Microsoft.Office.Interop.Excel.Worksheet[numofSheets];//numofSheets


                // create list1,2
                for (var i = 1; i >= 0; i--)
                {
                    collection2[i] = wb.Worksheets.Add();
                    collection2[i].Name = String.Format("list111111{0}", i + 1);
                }

                // delete Sheet1,2,3
                for (var i = 0; i < 3; i++)
                {
                    wb.Worksheets[3].Delete();
                }


                for (var i = (numofSheets - 1); i >= 0; i--)//numofSheets-1
                {

                    collection[i] = wb.Worksheets.Add();
                    collection[i].Name = String.Format("Sheet{0}", i + 1);
                }

                // delete list1,2
                for (var i = 0; i < 2; i++)
                {
                    wb.Worksheets[(numofSheets + 1)].Delete();//numofSheets+1
                }

                wb.SaveAs(__filePath);
                wb.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);

            }
        }




        /// <summary>
        /// PrintTitleRows set header for printing 
        /// </summary>
        /// <param name="firstrow">first row of header at each printing side</param>
        /// <param name="lastrow">last row od header at each printing side</param>
       /// <param name="numOfSheet">numofsheet where we want to setup print preview</param>
        public void createFile(int firstrow, int lastrow,int numOfSheet)
        {
            try
            {
                FileInfo finfo = new FileInfo(FilePath);

                if (Directory.Exists(FilePath) == false && FilePath[1].Equals(':') == false)
                {
                    throw new Exception("The directory doesn't exist! You must create directory for file path " + FilePath);
                }

                if (finfo.Exists == true)
                {
                    finfo.Delete();
                }

                mutualExceptions(FilePath);


                if (_xlApp == null)
                {
                    throw new Exception("EXCEL could not be started. Check that your office installation and project references are correct.");
                }

                Workbook wb = _xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);


                Worksheet ws = (Worksheet)wb.Worksheets[numOfSheet];
                //ws.PageSetup.PrintTitleRows = "$3:$4";
                ws.PageSetup.PrintTitleRows = "$" + firstrow.ToString() + ":$" + lastrow.ToString();
                

                if (ws == null)
                {
                    throw new Exception("Worksheet could not be created. Check that your office installation and project references are correct.");
                }

                wb.SaveAs(FilePath);
                wb.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }

        }


        /// <summary>
        /// PrintTitleRows set header for printing 
        /// </summary>
        /// <param name="firstrow">first row of header at each printing side</param>
        /// <param name="lastrow">last row od header at each printing side</param>
        /// <param name="numOfSheet">numofsheet where we want to setup print preview</param>
        /// <param name="pageOrientation"> portrait(char p) or landscape(char l) page orientation</param>
        public void createFile(int firstrow, int lastrow, int numOfSheet,char pageOrientation)
        {
            try
            {
                FileInfo finfo = new FileInfo(FilePath);

                if (Directory.Exists(FilePath) == false && FilePath[1].Equals(':') == false)
                {
                    throw new Exception("The directory doesn't exist! You must create directory for file path " + FilePath);
                }

                if (finfo.Exists == true)
                {
                    finfo.Delete();
                }

                mutualExceptions(FilePath);


                if (_xlApp == null)
                {
                    throw new Exception("EXCEL could not be started. Check that your office installation and project references are correct.");
                }

                Workbook wb = _xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);


                Worksheet ws = (Worksheet)wb.Worksheets[numOfSheet];
                //ws.PageSetup.PrintTitleRows = "$3:$4";
                ws.PageSetup.PrintTitleRows = "$" + firstrow.ToString() + ":$" + lastrow.ToString();
                if (pageOrientation == 'p' || pageOrientation == 'P')
                {
                    ws.PageSetup.Orientation = XlPageOrientation.xlPortrait;
                }
                if (pageOrientation == 'l' || pageOrientation == 'L')
                {
                    ws.PageSetup.Orientation = XlPageOrientation.xlLandscape;
                }

                if (ws == null)
                {
                    throw new Exception("Worksheet could not be created. Check that your office installation and project references are correct.");
                }

                wb.SaveAs(FilePath);
                wb.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }

        }




        /// <summary>
        /// create file with more than one worksheet, based on file path that sets in class constructor
        /// </summary>
        /// <param name="numofSheets">number of worksheets of created excel file</param>
        /// <param name="name">name of worksheets different than default "Sheet"</param>
        public void createFile(int numofSheets, string name)
        {
            try
            {

                _numofSheets = numofSheets;
                FileInfo finfo = new FileInfo(FilePath);

                if (finfo.Exists == true)
                {
                    finfo.Delete();
                }

                mutualExceptions(FilePath);


                if (_xlApp == null)
                {
                    throw new Exception("EXCEL could not be started. Check that your office installation and project references are correct.");
                }

                var wb = _xlApp.Workbooks.Add();
                var collection2 = new Microsoft.Office.Interop.Excel.Worksheet[2];
                var collection = new Microsoft.Office.Interop.Excel.Worksheet[numofSheets];//numofSheets


                // create list1,2
                for (var i = 1; i >= 0; i--)
                {
                    collection2[i] = wb.Worksheets.Add();
                    collection2[i].Name = String.Format("list111111{0}", i + 1);
                }

                // delete Sheet1,2,3
                for (var i = 0; i < 3; i++)
                {
                    wb.Worksheets[3].Delete();
                }


                for (var i = (numofSheets - 1); i >= 0; i--)//numofSheets-1
                {

                    collection[i] = wb.Worksheets.Add();
                    collection[i].Name = String.Format(name + "{0}", i + 1);
                }

                // delete list1,2
                for (var i = 0; i < 2; i++)
                {
                    wb.Worksheets[(numofSheets + 1)].Delete();//numofSheets+1
                }

                wb.SaveAs(__filePath);
                wb.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);

            }
        }

        /// <summary>
        /// change the name of the desired sheet
        /// </summary>
        /// <param name="numofSheet">number of sheet which changes its name </param>
        /// <param name="name">new name of renaimed sheet</param>
        public void renameSheet(int numofSheet, string name)
        {
            var excelFile = Path.GetFullPath(__filePath);
            var excel = new Microsoft.Office.Interop.Excel.Application();
            var wb = excel.Workbooks.Open(excelFile);
            var sheet = (Worksheet)wb.Worksheets.Item[numofSheet];
            sheet.Name = name;
            wb.Save();
            excel.Workbooks.Close();
        }

        #endregion

        #region write

        /// <summary>
        /// write in only one cell (in specified worksheet)
        /// </summary>
        /// <param name="row">row of the cell where we want to write</param>
        /// <param name="col">column of the cell where we want to write</param>
        /// <param name="value">content what we want to write</param>
        /// <param name="numofSheet">number of sheet where we want to write</param>
        public void writeCell(int row, int col, string value, int numofSheet)
        {
            try
            {
                if (numofSheet <= 0 || numofSheet > _numofSheets)
                {
                    throw new Exception("Invalid sheet's index!");
                }

                Workbook wb = _xlApp.Workbooks.Open(FilePath);
                Worksheet ws = wb.Sheets[numofSheet];
                ws.Cells[row, col] = value;
                autoFit(row, col, ws);
                wb.Save();
                wb.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// write in horizontal array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in horizontal array where we want to write</param>
        /// <param name="colbeg">column number of the first cell in horizontal array where we want to write</param>
        /// <param name="rowend">row of the last cell in horizontal array where we want to write</param>
        /// <param name="colend">column number of the last cell in horizontal array where we want to write</param>
        /// <param name="values">range of values that we want to write in excel horizontal array</param>
        /// <param name="numofSheet">number of sheet where we want to write</param>
        public void writeArrayHorizontal(int rowbeg, int colbeg, int rowend, int colend, string[] values, int numofSheet)
        {
            try
            {
                if (numofSheet <= 0 || numofSheet > _numofSheets)
                {
                    throw new Exception("Invalid sheet's index!");
                }

                Workbook wb = _xlApp.Workbooks.Open(FilePath);
                Worksheet ws = wb.Sheets[numofSheet];

                if (rowbeg != rowend)
                {
                    throw new Exception("Begin and end rows must be same!");
                }

                if (colend <= colbeg)
                {
                    throw new Exception("End column must be greater than begin column!");
                }

                if ((colend - colbeg + 1) != values.Length)
                {
                    throw new Exception("Values aren't good!");
                }


                for (int i = 0; i < values.Length; i++)
                {
                    ws.Cells[rowbeg, (colbeg + i)] = values[i];
                    autoFit(rowbeg, (colbeg + i), ws);
                }

                wb.Save();
                wb.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }

        }

        /// <summary>
        /// write in horizontal array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in horizontal array where we want to write</param>
        /// <param name="colbegstr">column name of the first cell in horizontal array where we want to write</param>
        /// <param name="rowend">row of the last cell in horizontal array where we want to write</param>
        /// <param name="colendstr">column name of the last cell in horizontal array where we want to write</param>
        /// <param name="values">range of values that we want to write in excel horizontal array</param>
        /// <param name="numofSheet">number of sheet where we want to write</param>
        public void writeArrayHorizontal(int rowbeg, string colbegstr, int rowend, string colendstr, string[] values, int numofSheet)
        {

            int colbeg, colend;
            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);

            this.writeArrayHorizontal(rowbeg, colbeg, rowend, colend, values, numofSheet);
        }

        /// <summary>
        /// write in vertical array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in vertical array where we want to write</param>
        /// <param name="colbeg">column number of the first cell in vertical array where we want to write</param>
        /// <param name="rowend">row of the last cell in vertical array where we want to write</param>
        /// <param name="colend">column number of the last cell in vertical array where we want to write</param>
        /// <param name="values">range of values that we want to write in excel vertical array</param>
        /// <param name="numofSheet">number of sheet where we want to write</param>
        public void writeArrayVertical(int rowbeg, int colbeg, int rowend, int colend, string[] values, int numofSheet)
        {
            try
            {
                if (numofSheet <= 0 || numofSheet > _numofSheets)
                {
                    throw new Exception("Invalid sheet's index!");
                }

                Workbook wb = _xlApp.Workbooks.Open(FilePath);
                Worksheet ws = wb.Sheets[numofSheet];

                if (colbeg != colend)
                {
                    throw new Exception("Begin and end columns must be same!");
                }

                if (rowend < rowbeg)
                {
                    throw new Exception("End row must be greater than begin row!");
                }

                if ((rowend - rowbeg + 1) != values.Length)
                {
                    throw new Exception("Values aren't good!");
                }

                int maxlength = values[0].Length;

                for (int i = 0; i < values.Length; i++)
                {
                    ws.Cells[(rowbeg + i), colbeg] = values[i];
                    if (values[i].Length > maxlength)
                    {
                        maxlength = values[i].Length;
                        autoFit((rowbeg + i), colbeg, ws);
                    }
                }

                wb.Save();
                wb.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }

        }

        /// <summary>
        ///  write in vertical array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in vertical array where we want to write</param>
        /// <param name="colbegstr">column name of the first cell in vertical array where we want to write</param>
        /// <param name="rowend">row of the last cell in vertical array where we want to write</param>
        /// <param name="colendstr">column name of the last cell in vertical array where we want to write</param>
        /// <param name="values">range of values that we want to write in excel vertical array</param>
        /// <param name="numofSheet">number of sheet where we want to write</param>
        public void writeArrayVertical(int rowbeg, string colbegstr, int rowend, string colendstr, string[] values, int numofSheet)
        {

            int colbeg, colend;
            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);

            this.writeArrayVertical(rowbeg, colbeg, rowend, colend, values, numofSheet);
        }


        /// <summary>
        /// write in horizontal array of cells (in specified worksheet)
        /// </summary>
        /// <param name="colbegstr">column name of the first cell in horizontal array where we want to write</param>
        /// <param name="rowbeg">row of the first cell in horizontal array where we want to write</param>
        /// <param name="colendstr">column name of the last cell in horizontal array where we want to write</param>
        /// <param name="rowend">row of the last cell in horizontal array where we want to write</param>
        /// <param name="values">range of values that we want to write in excel horizontal array</param>
        /// <param name="numofSheet">number of sheet where we want to write</param>
        public void writeArrayHor(string colbegstr, int rowbeg, string colendstr, int rowend, string[] values, int numofSheet)
        {
            int colbeg, colend;
            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);

            this.writeArrayHorizontal(rowbeg, colbeg, rowend, colend, values, numofSheet);
        }

        /// <summary>
        /// write in vertical array of cells (in specified worksheet)
        /// </summary>
        /// <param name="colbegstr">column name of the first cell in vertical array where we want to write</param>
        /// <param name="rowbeg">row of the first cell in vertical array where we want to write</param>
        /// <param name="colendstr">column name of the last cell in vertical array where we want to write</param>
        /// <param name="rowend">row of the last cell in vertical array where we want to write</param>
        /// <param name="values">range of values that we want to write in excel vertical array</param>
        /// <param name="numofSheet">number of sheet where we want to write</param>
        public void writeArrayVer(string colbegstr, int rowbeg, string colendstr, int rowend, string[] values, int numofSheet)
        {
            int colbeg, colend;
            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);

            this.writeArrayVertical(rowbeg, colbeg, rowend, colend, values, numofSheet);
        }

        #endregion

        #region read

        /// <summary>
        /// read content of cell (in specified worksheet)
        /// </summary>
        /// <param name="row">row of the read cell</param>
        /// <param name="col">column of the read cell</param>
        /// <param name="numofSheet">number of sheet where we want to read</param>
        /// <returns>the content that is read</returns>
        public string readCell(int row, int col, int numofSheet)
        {
            try
            {
                if (numofSheet <= 0 || numofSheet > _numofSheets)
                {
                    throw new Exception("Invalid sheet's index!");
                }

                Workbook wb = _xlApp.Workbooks.Open(FilePath);
                Worksheet ws = wb.Sheets[numofSheet];
                string value;
                Range rng = ws.Cells[row, col] as Range;
                value = rng.Value2;
                wb.Save();
                wb.Close();
                return value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                return "exception readCell!";
            }
        }

        /// <summary>
        /// read horizontal array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first read cell in horizontal array</param>
        /// <param name="colbeg">column number of the first read cell in horizontal array</param>
        /// <param name="rowend">row of the last read cell in horizontal array</param>
        /// <param name="colend">column number of the last read cell in horizontal array</param>
        /// <param name="numofSheet">number of sheet where we want to read</param>
        /// <returns>the range of values that are read</returns>
        public string[] readArrayHorizontal(int rowbeg, int colbeg, int rowend, int colend, int numofSheet)
        {
            try
            {
                if (numofSheet <= 0 || numofSheet > _numofSheets)
                {
                    throw new Exception("Invalid sheet's index!");
                }

                Workbook wb = _xlApp.Workbooks.Open(FilePath);
                Worksheet ws = wb.Sheets[numofSheet];
                string[] values = new string[colend - colbeg + 1];

                if (rowbeg != rowend)
                {
                    throw new Exception("Begin and end rows must be same!");
                }

                if (colend <= colbeg)
                {
                    throw new Exception("End column must be greater than begin column!");
                }


                for (int i = 0; i < values.Length; i++)
                {
                    Range rng = ws.Cells[rowbeg, (colbeg + i)] as Range;
                    values[i] = rng.Value2;
                    if (values[i] == null)
                    {
                        throw new Exception("You can't read empty field!");
                    }
                    //values[i] = ws.Cells[rowbeg, (colbeg + i)];
                }

                wb.Save();
                wb.Close();
                return values;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                return new string[1] { "exception readArrayHorizontal" };
            }

        }

        /// <summary>
        ///  read horizontal array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first read cell in horizontal array</param>
        /// <param name="colbegstr">column name of the first read cell in horizontal array</param>
        /// <param name="rowend">row of the last read cell in horizontal array</param>
        /// <param name="colendstr">column name of the last read cell in horizontal array</param>
        /// <param name="numofSheet">number of sheet where we want to read</param>
        /// <returns>the range of values that are read</returns>
        public string[] readArrayHorizontal(int rowbeg, string colbegstr, int rowend, string colendstr, int numofSheet)
        {

            int colbeg, colend;

            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);
            string[] values = new string[colend - colbeg + 1];

            values = this.readArrayHorizontal(rowbeg, colbeg, rowend, colend, numofSheet);
            return values;
        }

        /// <summary>
        /// read vertical array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first read cell in vertical array</param>
        /// <param name="colbeg">column number of the first read cell in vertical array</param>
        /// <param name="rowend">row of the last read cell in vertical array</param>
        /// <param name="colend">column number of the last read cell in vertical array</param>
        /// <param name="numofSheet">number of sheet where we want to read</param>
        /// <returns>the range of values that are read</returns>
        public string[] readArrayVertical(int rowbeg, int colbeg, int rowend, int colend, int numofSheet)
        {
            try
            {

                if (numofSheet <= 0 || numofSheet > _numofSheets)
                {
                    throw new Exception("Invalid sheet's index!");
                }

                Workbook wb = _xlApp.Workbooks.Open(FilePath);
                Worksheet ws = wb.Sheets[numofSheet];
                string[] values = new string[rowend - rowbeg + 1];

                if (colbeg != colend)
                {
                    throw new Exception("Begin and end columns must be same!");
                }

                if (rowend < rowbeg)
                {
                    throw new Exception("End row must be greater than begin row!");
                }


                for (int i = 0; i < values.Length; i++)
                {
                    Range rng = ws.Cells[(rowbeg + i), colbeg] as Range;
                    values[i] = rng.Value2;
                    if (values[i] == null)
                    {
                        throw new Exception("You can't read empty field!");
                    }
                    //values[i] = ws.Cells[(rowbeg + i), colbeg];
                }

                wb.Save();
                wb.Close();
                return values;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                return new string[1] { "exception readArrayVertical" };
            }

        }

        /// <summary>
        /// read vertical array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first read cell in vertical array</param>
        /// <param name="colbegstr">column name of the first read cell in vertical array</param>
        /// <param name="rowend">row of the last read cell in vertical array</param>
        /// <param name="colendstr">column name of the last read cell in vertical array</param>
        /// <param name="numofSheet">number of sheet where we want to read</param>
        /// <returns>the range of values that are read</returns>
        public string[] readArrayVertical(int rowbeg, string colbegstr, int rowend, string colendstr, int numofSheet)
        {

            int colbeg, colend;
            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);
            string[] values = new string[rowend - rowbeg + 1];

            values = this.readArrayVertical(rowbeg, colbeg, rowend, colend, numofSheet);
            return values;
        }


        /// <summary>
        ///  read horizontal array of cells (in specified worksheet)
        /// </summary>
        /// <param name="colbegstr">column name of the first read cell in horizontal array</param>
        /// <param name="rowbeg">row of the first read cell in horizontal array</param>
        /// <param name="colendstr">column name of the last read cell in horizontal array</param>
        /// <param name="rowend">row of the last read cell in horizontal array</param>
        /// <param name="numofSheet">number of sheet where we want to read</param>
        /// <returns>the range of values that are read</returns>
        public string[] readArrayHor(string colbegstr, int rowbeg, string colendstr, int rowend, int numofSheet)
        {
            int colbeg, colend;
            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);
            string[] values = new string[colend - colbeg + 1];

            values = this.readArrayHorizontal(rowbeg, colbeg, rowend, colend, numofSheet);
            return values;
        }

        /// <summary>
        ///  read vertical array of cells (in specified worksheet)
        /// </summary>
        /// <param name="colbegstr">column name of the first read cell in vertical array</param>
        /// <param name="rowbeg">row of the first read cell in vertical array</param>
        /// <param name="colendstr">column name of the last read cell in vertical array</param>
        /// <param name="rowend">row of the last read cell in vertical array</param>
        /// <param name="numofSheet">number of sheet where we want to read</param>
        /// <returns>>the range of values that are read</returns>
        public string[] readArrayVer(string colbegstr, int rowbeg, string colendstr, int rowend, int numofSheet)
        {
            int colbeg, colend;
            colbeg = excelNumbers(colbegstr);
            colend = excelNumbers(colendstr);
            string[] values = new string[rowend - rowbeg + 1];

            values = this.readArrayVertical(rowbeg, colbeg, rowend, colend, numofSheet);
            return values;
        }

        #endregion


        #endregion

        #region setting colors

        #region backgrounds

        /// <summary>
        /// set background in only one cell (in specified worksheet)
        /// </summary>
        /// <param name="row">row of the cell where we want to set background</param>
        /// <param name="col">column of the cell where we want to set background</param>
        /// <param name="color">color of background</param>
        /// <param name="numofSheet">number of sheet where we want to set background</param>
        public void setBackgroundCell(int row, int col, Color color, int numofSheet)
        {
            var excelFile = Path.GetFullPath(__filePath);
            var excel = new Microsoft.Office.Interop.Excel.Application();
            var wb = excel.Workbooks.Open(excelFile);
            var ws = (Worksheet)wb.Worksheets.Item[numofSheet];
            string column = getExcelColumnName(col);
            string rowstr = row.ToString();
            Range range = ws.get_Range(column + rowstr, column + rowstr);
            range.Cells.Interior.Color = color;
            wb.Save();
            excel.Workbooks.Close();
        }

        /// <summary>
        /// set background in only one cell (in first worksheet)
        /// </summary>
        /// <param name="row">row of the cell where we want to set background</param>
        /// <param name="col">column of the cell where we want to set background</param>
        /// <param name="color">color of background</param>
        public void setBackgroundCell(int row, int col, Color color)
        {
            var excelFile = Path.GetFullPath(__filePath);
            var excel = new Microsoft.Office.Interop.Excel.Application();
            var wb = excel.Workbooks.Open(excelFile);
            var ws = (Worksheet)wb.Worksheets.Item[1];
            string column = getExcelColumnName(col);
            string rowstr = row.ToString();
            Range range = ws.get_Range(column + rowstr, column + rowstr);
            range.Cells.Interior.Color = color;
            wb.Save();
            excel.Workbooks.Close();
        }

        /// <summary>
        ///  set background in only one cell (in specified worksheet)
        /// </summary>
        /// <param name="cell">full cell name where we want to set background</param>
        /// <param name="color">color of background</param>
        /// <param name="numofSheet">number of sheet where we want to set background</param>
        public void setBackgroundCell(string cell, Color color, int numofSheet)
        {
            var excelFile = Path.GetFullPath(__filePath);
            var excel = new Microsoft.Office.Interop.Excel.Application();
            var wb = excel.Workbooks.Open(excelFile);
            var ws = (Worksheet)wb.Worksheets.Item[numofSheet];
            Range range = ws.get_Range(cell, cell);
            range.Cells.Interior.Color = color;
            wb.Save();
            excel.Workbooks.Close();
        }

        /// <summary>
        /// set background in only one cell (in first worksheet)
        /// </summary>
        /// <param name="cell">full cell name where we want to set background</param>
        /// <param name="color">color of background</param>
        public void setBackgroundCell(string cell, Color color)
        {
            var excelFile = Path.GetFullPath(__filePath);
            var excel = new Microsoft.Office.Interop.Excel.Application();
            var wb = excel.Workbooks.Open(excelFile);
            var ws = (Worksheet)wb.Worksheets.Item[1];
            Range range = ws.get_Range(cell, cell);
            range.Cells.Interior.Color = color;
            wb.Save();
            excel.Workbooks.Close();
        }




        /// <summary>
        /// set background in horizontal array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in horizontal array where we want to set background</param>
        /// <param name="colbeg">column number of the first cell in horizontal array where we want to set background</param>
        /// <param name="rowend">row of the last cell in horizontal array where we want to set background</param>
        /// <param name="colend">column number of the last cell in horizontal array where we want to set background</param>
        /// <param name="color">color of background</param>
        /// <param name="numofSheet">number of sheet where we want to set background</param>
        public void setBackgroundArrayHorizontal(int rowbeg, int colbeg, int rowend, int colend, Color color, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];

                if (rowbeg != rowend)
                {
                    throw new Exception("Begin and end rows must be same!");
                }

                if (colend <= colbeg)
                {
                    throw new Exception("End column must be greater than begin column!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Cells.Interior.Color = color;

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// set background in horizontal array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in horizontal array where we want to set background</param>
        /// <param name="colbeg">column number of the first cell in horizontal array where we want to set background</param>
        /// <param name="rowend">row of the last cell in horizontal array where we want to set background</param>
        /// <param name="colend">column number of the last cell in horizontal array where we want to set background</param>
        /// <param name="color">color of background</param>
        public void setBackgroundArrayHorizontal(int rowbeg, int colbeg, int rowend, int colend, Color color)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];

                if (rowbeg != rowend)
                {
                    throw new Exception("Begin and end rows must be same!");
                }

                if (colend <= colbeg)
                {
                    throw new Exception("End column must be greater than begin column!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Cells.Interior.Color = color;

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// set background in vertical array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in vertical array where we want to set background</param>
        /// <param name="colbeg">column number of the first cell in vertical array where we want to set background</param>
        /// <param name="rowend">row of the last cell in vertical array where we want to set background</param>
        /// <param name="colend">column number of the last cell in vertical array where we want to set background</param>
        /// <param name="color">color of background</param>
        /// <param name="numofSheet">number of sheet where we want to set background</param>
        public void setBackgroundArrayVertical(int rowbeg, int colbeg, int rowend, int colend, Color color, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];

                if (colbeg != colend)
                {
                    throw new Exception("Begin and end columns must be same!");
                }

                if (rowend < rowbeg)
                {
                    throw new Exception("End row must be greater than begin row!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Cells.Interior.Color = color;


                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// set background in vertical array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in vertical array where we want to set background</param>
        /// <param name="colbeg">column number of the first cell in vertical array where we want to set background</param>
        /// <param name="rowend">row of the last cell in vertical array where we want to set background</param>
        /// <param name="colend">column number of the last cell in vertical array where we want to set background</param>
        /// <param name="color">color of background</param>
        public void setBackgroundArrayVertical(int rowbeg, int colbeg, int rowend, int colend, Color color)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];

                if (colbeg != colend)
                {
                    throw new Exception("Begin and end columns must be same!");
                }

                if (rowend < rowbeg)
                {
                    throw new Exception("End row must be greater than begin row!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Cells.Interior.Color = color;


                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }


        /// <summary>
        /// set background in rectangle area (in specified worksheet)
        /// </summary>
        /// <param name="cellbeg">full cell name in left upper corner of rectangle area</param>
        /// <param name="cellend">full cell name in right lower corner of rectangle area</param>
        /// <param name="color">color of background</param>
        /// <param name="numofSheet">number of sheet where we want to set background</param>
        public void setBackgroundArea(string cellbeg, string cellend, Color color, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];


                Range range = ws.get_Range(cellbeg, cellend);
                range.Cells.Interior.Color = color;

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        ///  set background in rectangle area (in first worksheet)
        /// </summary>
        /// <param name="cellbeg">full cell name in left upper corner of rectangle area</param>
        /// <param name="cellend">full cell name in right lower corner of rectangle area</param>
        /// <param name="color">color of background</param>
        public void setBackgroundArea(string cellbeg, string cellend, Color color)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];


                Range range = ws.get_Range(cellbeg, cellend);
                range.Cells.Interior.Color = color;

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }


        #endregion

        #region foreground

        /// <summary>
        /// set foreground in only one cell (in specified worksheet)
        /// </summary>
        /// <param name="row">row of the cell where we want to set foreground</param>
        /// <param name="col">column of the cell where we want to set foreground</param>
        /// <param name="color">color of foreground</param>
        /// <param name="numofSheet">number of sheet where we want to set foreground</param>
        public void setForegroundCell(int row, int col, Color color, int numofSheet)
        {
            var excelFile = Path.GetFullPath(__filePath);
            var excel = new Microsoft.Office.Interop.Excel.Application();
            var wb = excel.Workbooks.Open(excelFile);
            var ws = (Worksheet)wb.Worksheets.Item[numofSheet];
            string column = getExcelColumnName(col);
            string rowstr = row.ToString();
            Range range = ws.get_Range(column + rowstr, column + rowstr);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(color);

            wb.Save();
            excel.Workbooks.Close();
        }

        /// <summary>
        /// set foreground in only one cell (in first worksheet)
        /// </summary>
        /// <param name="row">row of the cell where we want to set foreground</param>
        /// <param name="col">column of the cell where we want to set foreground</param>
        /// <param name="color">color of foreground</param>
        public void setForegroundCell(int row, int col, Color color)
        {
            var excelFile = Path.GetFullPath(__filePath);
            var excel = new Microsoft.Office.Interop.Excel.Application();
            var wb = excel.Workbooks.Open(excelFile);
            var ws = (Worksheet)wb.Worksheets.Item[1];
            string column = getExcelColumnName(col);
            string rowstr = row.ToString();
            Range range = ws.get_Range(column + rowstr, column + rowstr);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(color);

            wb.Save();
            excel.Workbooks.Close();
        }

        /// <summary>
        ///  set foreground in only one cell (in specified worksheet)
        /// </summary>
        /// <param name="cell">full cell name where we want to set foreground</param>
        /// <param name="color">color of foreground</param>
        /// <param name="numofSheet">number of sheet where we want to set foreground</param>
        public void setForegroundCell(string cell, Color color, int numofSheet)
        {
            var excelFile = Path.GetFullPath(__filePath);
            var excel = new Microsoft.Office.Interop.Excel.Application();
            var wb = excel.Workbooks.Open(excelFile);
            var ws = (Worksheet)wb.Worksheets.Item[numofSheet];
            Range range = ws.get_Range(cell, cell);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(color);

            wb.Save();
            excel.Workbooks.Close();
        }

        /// <summary>
        /// set foreground in only one cell (in first worksheet)
        /// </summary>
        /// <param name="cell">full cell name where we want to set foreground</param>
        /// <param name="color">color of foreground</param>
        public void setForegroundCell(string cell, Color color)
        {
            var excelFile = Path.GetFullPath(__filePath);
            var excel = new Microsoft.Office.Interop.Excel.Application();
            var wb = excel.Workbooks.Open(excelFile);
            var ws = (Worksheet)wb.Worksheets.Item[1];
            Range range = ws.get_Range(cell, cell);
            range.Font.Color = System.Drawing.ColorTranslator.ToOle(color);

            wb.Save();
            excel.Workbooks.Close();
        }


        /// <summary>
        /// set foreground in horizontal array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in horizontal array where we want to set foreground</param>
        /// <param name="colbeg">column number of the first cell in horizontal array where we want to set foreground</param>
        /// <param name="rowend">row of the last cell in horizontal array where we want to set foreground</param>
        /// <param name="colend">column number of the last cell in horizontal array where we want to set foreground</param>
        /// <param name="color">color of foreground</param>
        /// <param name="numofSheet">number of sheet where we want to set foreground</param>
        public void setForegroundArrayHorizontal(int rowbeg, int colbeg, int rowend, int colend, Color color, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];

                if (rowbeg != rowend)
                {
                    throw new Exception("Begin and end rows must be same!");
                }

                if (colend <= colbeg)
                {
                    throw new Exception("End column must be greater than begin column!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Font.Color = System.Drawing.ColorTranslator.ToOle(color);

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// set foreground in horizontal array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in horizontal array where we want to set foreground</param>
        /// <param name="colbeg">column number of the first cell in horizontal array where we want to set foreground</param>
        /// <param name="rowend">row of the last cell in horizontal array where we want to set foreground</param>
        /// <param name="colend">column number of the last cell in horizontal array where we want to set foreground</param>
        /// <param name="color">color of foreground</param>
        public void setForegroundArrayHorizontal(int rowbeg, int colbeg, int rowend, int colend, Color color)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];

                if (rowbeg != rowend)
                {
                    throw new Exception("Begin and end rows must be same!");
                }

                if (colend <= colbeg)
                {
                    throw new Exception("End column must be greater than begin column!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Font.Color = System.Drawing.ColorTranslator.ToOle(color);

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// set foreground in vertical array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in vertical array where we want to set foreground</param>
        /// <param name="colbeg">column number of the first cell in vertical array where we want to set foreground</param>
        /// <param name="rowend">row of the last cell in vertical array where we want to set foreground</param>
        /// <param name="colend">column number of the last cell in vertical array where we want to set foreground</param>
        /// <param name="color">color of foreground</param>
        /// <param name="numofSheet">number of sheet where we want to set foreground</param>
        public void setForegroundArrayVertical(int rowbeg, int colbeg, int rowend, int colend, Color color, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];

                if (colbeg != colend)
                {
                    throw new Exception("Begin and end columns must be same!");
                }

                if (rowend < rowbeg)
                {
                    throw new Exception("End row must be greater than begin row!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Font.Color = System.Drawing.ColorTranslator.ToOle(color);


                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// set foreground in vertical array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in vertical array where we want to set foreground</param>
        /// <param name="colbeg">column number of the first cell in vertical array where we want to set foreground</param>
        /// <param name="rowend">row of the last cell in vertical array where we want to set foreground</param>
        /// <param name="colend">column number of the last cell in vertical array where we want to set foreground</param>
        /// <param name="color">color of foreground</param>
        public void setForegroundArrayVertical(int rowbeg, int colbeg, int rowend, int colend, Color color)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];

                if (colbeg != colend)
                {
                    throw new Exception("Begin and end columns must be same!");
                }

                if (rowend < rowbeg)
                {
                    throw new Exception("End row must be greater than begin row!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Font.Color = System.Drawing.ColorTranslator.ToOle(color);


                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }



        /// <summary>
        /// set foreground in rectangle area (in specified worksheet)
        /// </summary>
        /// <param name="cellbeg">full cell name in left upper corner of rectangle area</param>
        /// <param name="cellend">full cell name in right lower corner of rectangle area</param>
        /// <param name="color">color of foreground</param>
        /// <param name="numofSheet">number of sheet where we want to set foreground</param>
        public void setForegroundArea(string cellbeg, string cellend, Color color, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];


                Range range = ws.get_Range(cellbeg, cellend);
                range.Font.Color = System.Drawing.ColorTranslator.ToOle(color);

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        ///  set foreground in rectangle area (in first worksheet)
        /// </summary>
        /// <param name="cellbeg">full cell name in left upper corner of rectangle area</param>
        /// <param name="cellend">full cell name in right lower corner of rectangle area</param>
        /// <param name="color">color of foreground</param>
        public void setForegroundArea(string cellbeg, string cellend, Color color)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];


                Range range = ws.get_Range(cellbeg, cellend);
                range.Font.Color = System.Drawing.ColorTranslator.ToOle(color);

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }


        #endregion

        #endregion

        #region borders

        #region singleBorders

        /// <summary>
        /// set single border at only one cell (in specified worksheet)
        /// </summary>
        /// <param name="row">row of the cell where we want to set single border</param>
        /// <param name="col">column number of the cell where we want to set single border</param>
        /// <param name="tickness">tickness of single border</param>
        /// <param name="numofSheet">number of sheet where we want to set single border</param>
        public void setBorderCell(int row, int col, int tickness, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];
                string column = getExcelColumnName(col);
                string rowstr = row.ToString();
                Range range = ws.get_Range(column + rowstr, column + rowstr);
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                if (tickness < 1 || tickness > 4)
                {
                    throw new Exception("Invalid tickness! Tickness must be beetwen 1 and 4!");
                }
                switch (tickness)
                {
                    case 1: { range.Borders.Weight = XlBorderWeight.xlHairline; break; }
                    case 2: { range.Borders.Weight = XlBorderWeight.xlThin; break; }
                    case 3: { range.Borders.Weight = XlBorderWeight.xlMedium; break; }
                    case 4: { range.Borders.Weight = XlBorderWeight.xlThick; break; }
                }
                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// set single border in only one cell (in first worksheet)
        /// </summary>
        /// <param name="row">row of the cell where we want to set single border</param>
        /// <param name="col">column number of the cell where we want to set single border</param>
        /// <param name="tickness">tickness of single border</param>
        public void setBorderCell(int row, int col, int tickness)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];
                string column = getExcelColumnName(col);
                string rowstr = row.ToString();
                Range range = ws.get_Range(column + rowstr, column + rowstr);
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                if (tickness < 1 || tickness > 4)
                {
                    throw new Exception("Invalid tickness! Tickness must be beetwen 1 and 4!");
                }
                switch (tickness)
                {
                    case 1: { range.Borders.Weight = XlBorderWeight.xlHairline; break; }
                    case 2: { range.Borders.Weight = XlBorderWeight.xlThin; break; }
                    case 3: { range.Borders.Weight = XlBorderWeight.xlMedium; break; }
                    case 4: { range.Borders.Weight = XlBorderWeight.xlThick; break; }
                }
                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        ///  set single border in only one cell (in specified worksheet)
        /// </summary>
        /// <param name="cell">full cell name where we want to set single border</param>
        /// <param name="tickness">tickness of single border</param>
        /// <param name="numofSheet">number of sheet where we want to set single border</param>
        public void setBorderCell(string cell, int tickness, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];
                Range range = ws.get_Range(cell, cell);
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                if (tickness < 1 || tickness > 4)
                {
                    throw new Exception("Invalid tickness! Tickness must be beetwen 1 and 4!");
                }
                switch (tickness)
                {
                    case 1: { range.Borders.Weight = XlBorderWeight.xlHairline; break; }
                    case 2: { range.Borders.Weight = XlBorderWeight.xlThin; break; }
                    case 3: { range.Borders.Weight = XlBorderWeight.xlMedium; break; }
                    case 4: { range.Borders.Weight = XlBorderWeight.xlThick; break; }
                }
                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// set single border in only one cell (in first worksheet)
        /// </summary>
        /// <param name="cell">full cell name where we want to set single border</param>
        /// <param name="tickness">tickness of single border</param>
        public void setBorderCell(string cell, int tickness)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];
                Range range = ws.get_Range(cell, cell);
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                if (tickness < 1 || tickness > 4)
                {
                    throw new Exception("Invalid tickness! Tickness must be beetwen 1 and 4!");
                }
                switch (tickness)
                {
                    case 1: { range.Borders.Weight = XlBorderWeight.xlHairline; break; }
                    case 2: { range.Borders.Weight = XlBorderWeight.xlThin; break; }
                    case 3: { range.Borders.Weight = XlBorderWeight.xlMedium; break; }
                    case 4: { range.Borders.Weight = XlBorderWeight.xlThick; break; }
                }
                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        
        }




        /// <summary>
        /// set single border in horizontal array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in horizontal array where we want to set single border</param>
        /// <param name="colbeg">column number of the first cell in horizontal array where we want to set single border</param>
        /// <param name="rowend">row of the last cell in horizontal array where we want to set single border</param>
        /// <param name="colend">column number of the last cell in horizontal array where we want to set single border</param>
        /// <param name="tickness">tickness of single border</param>
        /// <param name="numofSheet">number of sheet where we want to set single border</param>
        public void setBorderArrayHorizontal(int rowbeg, int colbeg, int rowend, int colend, int tickness, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];

                if (rowbeg != rowend)
                {
                    throw new Exception("Begin and end rows must be same!");
                }

                if (colend <= colbeg)
                {
                    throw new Exception("End column must be greater than begin column!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                if (tickness < 1 || tickness > 4)
                {
                    throw new Exception("Invalid tickness! Tickness must be beetwen 1 and 4!");
                }
                switch (tickness)
                {
                    case 1: { range.Borders.Weight = XlBorderWeight.xlHairline; break; }
                    case 2: { range.Borders.Weight = XlBorderWeight.xlThin; break; }
                    case 3: { range.Borders.Weight = XlBorderWeight.xlMedium; break; }
                    case 4: { range.Borders.Weight = XlBorderWeight.xlThick; break; }
                }

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// set single border in horizontal array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in horizontal array where we want to set single border</param>
        /// <param name="colbeg">column number of the first cell in horizontal array where we want to set single border</param>
        /// <param name="rowend">row of the last cell in horizontal array where we want to set single border</param>
        /// <param name="colend">column number of the last cell in horizontal array where we want to set single border</param>
        /// <param name="tickness">tickness of single border</param>
        public void setBorderArrayHorizontal(int rowbeg, int colbeg, int rowend, int colend, int tickness)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];

                if (rowbeg != rowend)
                {
                    throw new Exception("Begin and end rows must be same!");
                }

                if (colend <= colbeg)
                {
                    throw new Exception("End column must be greater than begin column!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                if (tickness < 1 || tickness > 4)
                {
                    throw new Exception("Invalid tickness! Tickness must be beetwen 1 and 4!");
                }
                switch (tickness)
                {
                    case 1: { range.Borders.Weight = XlBorderWeight.xlHairline; break; }
                    case 2: { range.Borders.Weight = XlBorderWeight.xlThin; break; }
                    case 3: { range.Borders.Weight = XlBorderWeight.xlMedium; break; }
                    case 4: { range.Borders.Weight = XlBorderWeight.xlThick; break; }
                }

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// set single border in vertical array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in vertical array where we want to set single border</param>
        /// <param name="colbeg">column number of the first cell in vertical array where we want to set single border</param>
        /// <param name="rowend">row of the last cell in vertical array where we want to set single border</param>
        /// <param name="colend">column number of the last cell in vertical array where we want to set single border</param>
        /// <param name="tickness">tickness of single border</param>
        /// <param name="numofSheet">number of sheet where we want to set single border</param>
        public void setBorderArrayVertical(int rowbeg, int colbeg, int rowend, int colend, int tickness, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];

                if (colbeg != colend)
                {
                    throw new Exception("Begin and end columns must be same!");
                }

                if (rowend < rowbeg)
                {
                    throw new Exception("End row must be greater than begin row!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                if (tickness < 1 || tickness > 4)
                {
                    throw new Exception("Invalid tickness! Tickness must be beetwen 1 and 4!");
                }
                switch (tickness)
                {
                    case 1: { range.Borders.Weight = XlBorderWeight.xlHairline; break; }
                    case 2: { range.Borders.Weight = XlBorderWeight.xlThin; break; }
                    case 3: { range.Borders.Weight = XlBorderWeight.xlMedium; break; }
                    case 4: { range.Borders.Weight = XlBorderWeight.xlThick; break; }
                }


                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// set single border in vertical array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in vertical array where we want to set single border</param>
        /// <param name="colbeg">column number of the first cell in vertical array where we want to set single border</param>
        /// <param name="rowend">row of the last cell in vertical array where we want to set single border</param>
        /// <param name="colend">column number of the last cell in vertical array where we want to set single border</param>
        /// <param name="tickness">tickness of single border</param>
        public void setBorderArrayVertical(int rowbeg, int colbeg, int rowend, int colend, int tickness)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];

                if (colbeg != colend)
                {
                    throw new Exception("Begin and end columns must be same!");
                }

                if (rowend < rowbeg)
                {
                    throw new Exception("End row must be greater than begin row!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                if (tickness < 1 || tickness > 4)
                {
                    throw new Exception("Invalid tickness! Tickness must be beetwen 1 and 4!");
                }
                switch (tickness)
                {
                    case 1: { range.Borders.Weight = XlBorderWeight.xlHairline; break; }
                    case 2: { range.Borders.Weight = XlBorderWeight.xlThin; break; }
                    case 3: { range.Borders.Weight = XlBorderWeight.xlMedium; break; }
                    case 4: { range.Borders.Weight = XlBorderWeight.xlThick; break; }
                }


                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }


        /// <summary>
        /// set single border in rectangle area (in specified worksheet)
        /// </summary>
        /// <param name="cellbeg">full cell name in left upper corner of rectangle area</param>
        /// <param name="cellend">full cell name in right lower corner of rectangle area</param>
        /// <param name="tickness">tickness of single border</param>
        /// <param name="numofSheet">number of sheet where we want to set single border</param>
        public void setBorderArea(string cellbeg, string cellend, int tickness, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];


                Range range = ws.get_Range(cellbeg, cellend);
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                if (tickness < 1 || tickness > 4)
                {
                    throw new Exception("Invalid tickness! Tickness must be beetwen 1 and 4!");
                }
                switch (tickness)
                {
                    case 1: { range.Borders.Weight = XlBorderWeight.xlHairline; break; }
                    case 2: { range.Borders.Weight = XlBorderWeight.xlThin; break; }
                    case 3: { range.Borders.Weight = XlBorderWeight.xlMedium; break; }
                    case 4: { range.Borders.Weight = XlBorderWeight.xlThick; break; }
                }

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        ///  set single border in rectangle area (in first worksheet)
        /// </summary>
        /// <param name="cellbeg">full cell name in left upper corner of rectangle area</param>
        /// <param name="cellend">full cell name in right lower corner of rectangle area</param>
        /// <param name="tickness">tickness of single border</param>
        public void setBorderArea(string cellbeg, string cellend, int tickness)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];


                Range range = ws.get_Range(cellbeg, cellend);
                range.Borders.LineStyle = XlLineStyle.xlContinuous;
                if (tickness < 1 || tickness > 4)
                {
                    throw new Exception("Invalid tickness! Tickness must be beetwen 1 and 4!");
                }
                switch (tickness)
                {
                    case 1: { range.Borders.Weight = XlBorderWeight.xlHairline; break; }
                    case 2: { range.Borders.Weight = XlBorderWeight.xlThin; break; }
                    case 3: { range.Borders.Weight = XlBorderWeight.xlMedium; break; }
                    case 4: { range.Borders.Weight = XlBorderWeight.xlThick; break; }
                }

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }


        #endregion

        #region doubleBorders


        /// <summary>
        /// set double border at only one cell (in specified worksheet)
        /// </summary>
        /// <param name="row">row of the cell where we want to set double border</param>
        /// <param name="col">column number of the cell where we want to set double border</param>
        /// <param name="numofSheet">number of sheet where we want to set double border</param>
        public void setdoubleBorderCell(int row, int col, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];
                string column = getExcelColumnName(col);
                string rowstr = row.ToString();
                Range range = ws.get_Range(column + rowstr, column + rowstr);
                range.Borders.LineStyle = XlLineStyle.xlDouble;

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// set double border in only one cell (in first worksheet)
        /// </summary>
        /// <param name="row">row of the cell where we want to set double border</param>
        /// <param name="col">column number of the cell where we want to set double border</param>
        public void setdoubleBorderCell(int row, int col)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];
                string column = getExcelColumnName(col);
                string rowstr = row.ToString();
                Range range = ws.get_Range(column + rowstr, column + rowstr);
                range.Borders.LineStyle = XlLineStyle.xlDouble;

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        ///  set double border in only one cell (in specified worksheet)
        /// </summary>
        /// <param name="cell">full cell name where we want to set double border</param>
        /// <param name="numofSheet">number of sheet where we want to set double border</param>
        public void setdoubleBorderCell(string cell, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];
                Range range = ws.get_Range(cell, cell);
                range.Borders.LineStyle = XlLineStyle.xlDouble;

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
            }
        }

        /// <summary>
        /// set double border in only one cell (in first worksheet)
        /// </summary>
        /// <param name="cell">full cell name where we want to set double border</param>
        public void setdoubleBorderCell(string cell)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];
                Range range = ws.get_Range(cell, cell);
                range.Borders.LineStyle = XlLineStyle.xlDouble;

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }




        /// <summary>
        /// set double border in horizontal array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in horizontal array where we want to set double border</param>
        /// <param name="colbeg">column number of the first cell in horizontal array where we want to set double border</param>
        /// <param name="rowend">row of the last cell in horizontal array where we want to set double border</param>
        /// <param name="colend">column number of the last cell in horizontal array where we want to set double border</param>
        /// <param name="numofSheet">number of sheet where we want to set double border</param>
        public void setdoubleBorderArrayHorizontal(int rowbeg, int colbeg, int rowend, int colend, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];

                if (rowbeg != rowend)
                {
                    throw new Exception("Begin and end rows must be same!");
                }

                if (colend <= colbeg)
                {
                    throw new Exception("End column must be greater than begin column!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Borders.LineStyle = XlLineStyle.xlDouble;

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// set double border in horizontal array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in horizontal array where we want to set double border</param>
        /// <param name="colbeg">column number of the first cell in horizontal array where we want to set double border</param>
        /// <param name="rowend">row of the last cell in horizontal array where we want to set double border</param>
        /// <param name="colend">column number of the last cell in horizontal array where we want to set double border</param>
        public void setdoubleBorderArrayHorizontal(int rowbeg, int colbeg, int rowend, int colend)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];

                if (rowbeg != rowend)
                {
                    throw new Exception("Begin and end rows must be same!");
                }

                if (colend <= colbeg)
                {
                    throw new Exception("End column must be greater than begin column!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Borders.LineStyle = XlLineStyle.xlDouble;

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// set double border in vertical array of cells (in specified worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in vertical array where we want to set double border</param>
        /// <param name="colbeg">column number of the first cell in vertical array where we want to set double border</param>
        /// <param name="rowend">row of the last cell in vertical array where we want to set double border</param>
        /// <param name="colend">column number of the last cell in vertical array where we want to set double border</param>
        /// <param name="numofSheet">number of sheet where we want to set double border</param>
        public void setdoubleBorderArrayVertical(int rowbeg, int colbeg, int rowend, int colend, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];

                if (colbeg != colend)
                {
                    throw new Exception("Begin and end columns must be same!");
                }

                if (rowend < rowbeg)
                {
                    throw new Exception("End row must be greater than begin row!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Borders.LineStyle = XlLineStyle.xlDouble;

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        /// set double border in vertical array of cells (in first worksheet)
        /// </summary>
        /// <param name="rowbeg">row of the first cell in vertical array where we want to set double border</param>
        /// <param name="colbeg">column number of the first cell in vertical array where we want to set double border</param>
        /// <param name="rowend">row of the last cell in vertical array where we want to set double border</param>
        /// <param name="colend">column number of the last cell in vertical array where we want to set double border</param>
        public void setdoubleBorderArrayVertical(int rowbeg, int colbeg, int rowend, int colend)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];

                if (colbeg != colend)
                {
                    throw new Exception("Begin and end columns must be same!");
                }

                if (rowend < rowbeg)
                {
                    throw new Exception("End row must be greater than begin row!");
                }

                string columnbeg = getExcelColumnName(colbeg);
                string rowstrbeg = rowbeg.ToString();
                string columnend = getExcelColumnName(colend);
                string rowstrend = rowend.ToString();

                Range range = ws.get_Range(columnbeg + rowstrbeg, columnend + rowstrend);
                range.Borders.LineStyle = XlLineStyle.xlDouble;

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }


        /// <summary>
        /// set double border in rectangle area (in specified worksheet)
        /// </summary>
        /// <param name="cellbeg">full cell name in left upper corner of rectangle area</param>
        /// <param name="cellend">full cell name in right lower corner of rectangle area</param>
        /// <param name="numofSheet">number of sheet where we want to set double border</param>
        public void setdoubleBorderArea(string cellbeg, string cellend, int numofSheet)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[numofSheet];


                Range range = ws.get_Range(cellbeg, cellend);
                range.Borders.LineStyle = XlLineStyle.xlDouble;

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }

        /// <summary>
        ///  set double border in rectangle area (in first worksheet)
        /// </summary>
        /// <param name="cellbeg">full cell name in left upper corner of rectangle area</param>
        /// <param name="cellend">full cell name in right lower corner of rectangle area</param>
        public void setdoubleBorderArea(string cellbeg, string cellend)
        {
            try
            {
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var wb = excel.Workbooks.Open(excelFile);
                var ws = (Worksheet)wb.Worksheets.Item[1];


                Range range = ws.get_Range(cellbeg, cellend);
                range.Borders.LineStyle = XlLineStyle.xlDouble;

                wb.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
            }
        }



        #endregion


        #endregion

        #region findreplace

        /// <summary>
        /// find positions where are found search keyword in workbook
        /// </summary>
        /// <param name="criteria">search keyword</param>
        /// <param name="search">only whole match between search keyword and excel cell is accepted</param>
        /// <returns>all positions where there are matched between search keyword and excel cell</returns>
        public List<string> FindText(string criteria, bool search = false)
        {
            List<string> findings = new List<string>();

            var excelFile = Path.GetFullPath(__filePath);
            var excel = new Microsoft.Office.Interop.Excel.Application();
            var workbook = excel.Workbooks.Open(excelFile);
            int numofSheetss = workbook.Worksheets.Count;
            excel.Workbooks.Close();

            for (int i = 0; i < numofSheetss; i++)
            {
                findings.AddRange(this.FindText((i + 1), criteria, search));
            }

            return findings;
        }

        /// <summary>
        /// find positions where are found search keyword in specified worksheet
        /// </summary>
        /// <param name="numofSheet">number of specified worksheet</param>
        /// <param name="criteria">search keyword</param>
        /// <param name="search">only whole match between search keyword and excel cell is accepted</param>
        /// <returns>all positions where there are matched between search keyword and excel cell</returns>
        public List<string> FindText(int numofSheet, string criteria, bool search = false)
        {

            try
            {
                List<string> findings = new List<string>();
                string currFindings = "";
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var workbook = excel.Workbooks.Open(excelFile);
                var sheet = (Worksheet)workbook.Worksheets.Item[numofSheet];
                sheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[numofSheet];
                bool find = false;
                int counter = 0;
                do
                {
                    Range oRng = null;
                    if (counter == 0)
                    {
                        oRng = GetSpecifiedRange(criteria, sheet, search);
                    }
                    else
                    {
                        oRng = GetSpecifiedRangeNext(criteria, sheet, counter, search);
                    }
                    if (oRng != null)
                    {
                        currFindings = sheet.Name + " : " + getExcelColumnName((int)oRng.Column) + oRng.Row;
                        if (findings.Contains(currFindings) == false)
                        {
                            find = true;
                            findings.Add(currFindings);
                            //MessageBox.Show("Text found, position is Columnow:" + getExcelColumnName((int)oRng.Column) + " and row:" + oRng.Row );
                        }
                        else
                        {
                            find = false;
                        }

                        counter++;
                    }
                    else
                    {
                        MessageBox.Show("Text is not found");
                        Logger.writeNode(Constants.EXCEPTION_EXCEL, "Text is not found");
                        find = false;
                    }
                } while (find == true);
                excel.Workbooks.Close();
                findings.Reverse();
                return findings;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                return new List<string>();
            }
        }

        private Range GetSpecifiedRange(string matchStr, Worksheet ws, bool search = false)
        {
            object missing = System.Reflection.Missing.Value;
            Range currentFind = null;
            Range firstFind = null;
            if (search == false)
            {
                currentFind = ws.UsedRange.Find(matchStr, missing,
                               Microsoft.Office.Interop.Excel.XlFindLookIn.xlValues,
                               Microsoft.Office.Interop.Excel.XlLookAt.xlPart,
                               Microsoft.Office.Interop.Excel.XlSearchOrder.xlByRows,
                               Microsoft.Office.Interop.Excel.XlSearchDirection.xlNext, false, missing, missing);
            }
            else
            {
                currentFind = ws.UsedRange.Find(matchStr, missing,
                               Microsoft.Office.Interop.Excel.XlFindLookIn.xlValues,
                               Microsoft.Office.Interop.Excel.XlLookAt.xlWhole,
                               Microsoft.Office.Interop.Excel.XlSearchOrder.xlByRows,
                               Microsoft.Office.Interop.Excel.XlSearchDirection.xlNext, false, missing, missing);
            }

            return currentFind;
        }

        private Range GetSpecifiedRangeNext(string matchStr, Worksheet ws, int counter, bool search = false)
        {
            object missing = System.Reflection.Missing.Value;
            Range currentFind = null;

            if (search == false)
            {
                currentFind = ws.UsedRange.Find(matchStr, missing,
                               Microsoft.Office.Interop.Excel.XlFindLookIn.xlValues,
                               Microsoft.Office.Interop.Excel.XlLookAt.xlPart,
                               Microsoft.Office.Interop.Excel.XlSearchOrder.xlByRows,
                               Microsoft.Office.Interop.Excel.XlSearchDirection.xlNext, false, missing, missing);
            }
            else
            {
                currentFind = ws.UsedRange.Find(matchStr, missing,
                               Microsoft.Office.Interop.Excel.XlFindLookIn.xlValues,
                               Microsoft.Office.Interop.Excel.XlLookAt.xlWhole,
                               Microsoft.Office.Interop.Excel.XlSearchOrder.xlByRows,
                               Microsoft.Office.Interop.Excel.XlSearchDirection.xlNext, false, missing, missing);
            }
            while (counter > 0)
            {
                currentFind = ws.UsedRange.FindNext(currentFind);
                counter--;
            }

            return currentFind;
        }

        /// <summary>
        /// replace positions where are found search keyword in workbook
        /// </summary>
        /// <param name="criteria">search keyword</param>
        /// <param name="replacement">content of replacement</param>
        /// <param name="search">only whole match between search keyword and excel cell is accepted</param>
        public void FindAndReplaceText(string criteria, string replacement, bool search = false)
        {
            List<string> findings = new List<string>();

            var excelFile = Path.GetFullPath(__filePath);
            var excel = new Microsoft.Office.Interop.Excel.Application();
            var workbook = excel.Workbooks.Open(excelFile);
            int numofSheetss = workbook.Worksheets.Count;
            excel.Workbooks.Close();

            for (int i = 0; i < numofSheetss; i++)
            {
                this.FindAndReplaceText((i + 1), criteria, replacement, search);
            }
        }

        /// <summary>
        /// replace positions where are found search keyword in specified worksheet
        /// </summary>
        /// <param name="numofSheet">number of specified worksheet</param>
        /// <param name="criteria">search keyword</param>
        /// <param name="replacement">content of replacement</param>
        /// <param name="search">only whole match between search keyword and excel cell is accepted</param>
        public void FindAndReplaceText(int numofSheet, string criteria, string replacement, bool search = false)
        {
            try
            {
                string currFindings = "";
                var excelFile = Path.GetFullPath(__filePath);
                var excel = new Microsoft.Office.Interop.Excel.Application();
                var workbook = excel.Workbooks.Open(excelFile);
                var sheet = (Worksheet)workbook.Worksheets.Item[numofSheet];
                sheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[numofSheet];
                bool replace = false;

                do
                {
                    Range oRng = null;

                    oRng = GetSpecifiedRangeAndReplace(criteria, replacement, sheet, search);

                    if (oRng != null)
                    {
                        replace = true;
                    }
                    else
                    {
                        replace = false;
                    }
                } while (replace == true);
                workbook.Save();
                excel.Workbooks.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Logger.writeNode(Constants.EXCEPTION, ex.Message);
                System.Environment.Exit(1);
               
            }

        }

        private Range GetSpecifiedRangeAndReplace(string matchStr, string replacement, Worksheet ws, bool search = false)
        {
            object m = Type.Missing;
            object missing = System.Reflection.Missing.Value;
            Range currentFind = null;

            if (search == false)
            {
                currentFind = ws.UsedRange.Find(matchStr, missing,
                               Microsoft.Office.Interop.Excel.XlFindLookIn.xlValues,
                               Microsoft.Office.Interop.Excel.XlLookAt.xlPart,
                               Microsoft.Office.Interop.Excel.XlSearchOrder.xlByRows,
                               Microsoft.Office.Interop.Excel.XlSearchDirection.xlNext, false, missing, missing);
                if (currentFind != null)
                {
                    bool success = (bool)currentFind.Replace(matchStr,
                                                             replacement,
                                                             XlLookAt.xlPart,
                                                             XlSearchOrder.xlByRows,
                                                             true, m, m, m);
                }
            }
            else
            {
                currentFind = ws.UsedRange.Find(matchStr, missing,
                               Microsoft.Office.Interop.Excel.XlFindLookIn.xlValues,
                               Microsoft.Office.Interop.Excel.XlLookAt.xlWhole,
                               Microsoft.Office.Interop.Excel.XlSearchOrder.xlByRows,
                               Microsoft.Office.Interop.Excel.XlSearchDirection.xlNext, false, missing, missing);

                if (currentFind != null)
                {
                    bool success = (bool)currentFind.Replace(matchStr,
                                                             replacement,
                                                             XlLookAt.xlWhole,
                                                             XlSearchOrder.xlByRows,
                                                             true, m, m, m);
                }
            }

            return currentFind;
        }


        #endregion


        #region pagesetup


        public void setPrintTitleRows()
        {
           
        }


        #endregion


    } // end of class


}
